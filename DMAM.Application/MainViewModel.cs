using System;
using System.Collections.Generic;

using DMAM.Core.MVVM;
using DMAM.Device;
using DMAM.Editors;
using DMAM.Gracenote;

namespace DMAM.Application
{
    public class MainViewModel : ViewModelBase
    {
        public IEnumerable<VolumeInfo> Volumes
        {
            get
            {
                return VolumeService.GetInstance().Volumes;
            }
        }

        public void Initialize()
        {
            VolumeService.GetInstance().Initialize();
            CDDBService.GetInstance().Initialize();
            AlbumViewService.GetInstance().Initialize();
        }

        public void Shutdown()
        {
            AlbumViewService.GetInstance().Shutdown();
            CDDBService.GetInstance().Shutdown();
            VolumeService.GetInstance().Shutdown();
        }

        public void NotifyDoubleClick(object dataItem)
        {
            var volumeInfo = dataItem as VolumeInfo;
            if ((volumeInfo == null) || (volumeInfo.VolumeType != VolumeType.CDRomDrive))
            {
                return;
            }

            AlbumViewService.GetInstance().ExecuteCommand(volumeInfo.DriveLetter, CDRomCommand.Open);
        }
    }
}
