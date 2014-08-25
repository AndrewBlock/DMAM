using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;

using DMAM.Core.Events;
using DMAM.Core.Services;
using DMAM.Interop.IO;

namespace DMAM.Device
{
    public class VolumeService : ServiceBase<VolumeService>
    {
        private Dispatcher _dispatcher;

        private ObservableCollection<VolumeInfo> _volumes = new ObservableCollection<VolumeInfo>();
        private Dictionary<char, VolumeInfo> _volumeLookup = new Dictionary<char, VolumeInfo>();

        private VolumeMonitor _volumeMonitor = new VolumeMonitor();

        public readonly Event<CDRomDriveStatus> CDRomDriveUpdate = new Event<CDRomDriveStatus>();

        public VolumeService()
        {
            _volumeMonitor.VolumeUpdate += _volumeMonitor_VolumeUpdate;
        }

        public IEnumerable<VolumeInfo> Volumes
        {
            get
            {
                return _volumes;
            }
        }

        protected override bool OnInitialize()
        {
            lock (this)
            {
                _dispatcher = Dispatcher.CurrentDispatcher;
            }
            
            var volumeList = VolumeInterop.GetLogicalDrives();
            var driveLetters = VolumeUtils.GetDriveLettersFromUnitsMask(volumeList);
            foreach (var driveLetter in driveLetters)
            {
                var drivePath = VolumeUtils.GetDrivePathFromDriveLetter(driveLetter);
                var driveType = VolumeInterop.GetDriveType(drivePath);

                if (driveType == DriveType.DRIVE_NO_ROOT_DIR)
                {
                    continue;
                }

                var volumeInfo = new VolumeInfo(driveLetter, VolumeUtils.Convert(driveType));

                var labelBuffer = new StringBuilder(512);
                if (VolumeInterop.GetVolumeInformation(drivePath, labelBuffer, labelBuffer.Capacity,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0))
                {
                    volumeInfo.Label = labelBuffer.ToString();
                }

                if (volumeInfo.VolumeType == VolumeType.CDRomDrive)
                {
                    volumeInfo.ContainsCDAudio = AudioCDUtils.DoesDriveContainMusicCD(driveLetter);
                }

                _volumes.Add(volumeInfo);
                _volumeLookup.Add(driveLetter, volumeInfo);
            }

            _volumeMonitor.Initialize();
            return true;
        }

        protected override void OnShutdown()
        {
            _volumeMonitor.Shutdown();
            
            _volumeLookup.Clear();
            _volumes.Clear();

            lock (this)
            {
                _dispatcher = null;
            }
        }

        private void _volumeMonitor_VolumeUpdate(object sender, VolumeEvent eventInfo)
        {
            _dispatcher.BeginInvoke(new VolumeUpdateHandler(_volumeMonitor_VolumeUpdate_InThread),
                new object[] { sender, eventInfo });
        }

        private void _volumeMonitor_VolumeUpdate_InThread(object sender, VolumeEvent eventInfo)
        {
            if (!_volumeLookup.ContainsKey(eventInfo.DriveLetter))
            {
                return;
            }

            var volumeInfo = _volumeLookup[eventInfo.DriveLetter];
            if (volumeInfo.VolumeType != VolumeType.CDRomDrive)
            {
                return;
            }

            var containsCDAudio = AudioCDUtils.DoesDriveContainMusicCD(eventInfo.DriveLetter);
            if (containsCDAudio == volumeInfo.ContainsCDAudio)
            {
                return;
            }

            volumeInfo.ContainsCDAudio = containsCDAudio;
            if (volumeInfo.VolumeType == VolumeType.CDRomDrive)
            {
                CDRomDriveUpdate.Notify(new CDRomDriveStatus
                {
                    DriveLetter = volumeInfo.DriveLetter,
                    ContainsAudioCD = volumeInfo.ContainsCDAudio
                });
            }
        }
    }
}
