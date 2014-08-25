using System;
using System.Runtime.InteropServices;

using DMAM.Core.Services;
using DMAM.Interop.GDI;
using DMAM.Interop.IO;

namespace DMAM.Device
{
    internal class VolumeMonitor : UIThreadBase
    {
        private Window _window;
        
        public event VolumeUpdateHandler VolumeUpdate;

        public VolumeMonitor()
            : base("VolumeMonitor")
        {
        }

        protected override bool OnInitialize()
        {
            var className = Window.GetUniqueClassName("VolumeMonitor");

            _window = new Window();
            _window.WindowProc +=_window_WindowProc;
            _window.CreateWindow(IntPtr.Zero, className, WindowClassStyle.CS_NONE,
                WindowStyle.WS_OVERLAPPED, WindowExStyle.WS_EX_NONE);

            return true;
        }

        protected override void OnShutdown()
        {
            _window.DestroyWindow();
            _window.WindowProc -=_window_WindowProc;
            _window = null;
        }

        private void _window_WindowProc(object sender, WindowProcArgs args)
        {
            if (args.Message == WindowMessages.WM_DEVICECHANGE)
            {
                OnDeviceChange(args.WParam, args.LParam);
            }
        }

        private void OnDeviceChange(IntPtr wParam, IntPtr lParam)
        {
            var volumeEvent = ProcessDeviceChanged(wParam, lParam);
            if ((volumeEvent != VolumeEvent.None) && (VolumeUpdate != null))
            {
                VolumeUpdate(this, volumeEvent);
            }
        }

        private static VolumeEvent ProcessDeviceChanged(IntPtr wParam, IntPtr lParam)
        {
            var deviceEvent = (DeviceEvent) wParam.ToInt32();

            if ((deviceEvent != DeviceEvent.DBT_DEVICEARRIVAL)
                && (deviceEvent != DeviceEvent.DBT_DEVICEREMOVECOMPLETE))
            {
                return VolumeEvent.None;
            }

            var header = (DEV_BROADCAST_HDR) Marshal.PtrToStructure(lParam,
                typeof(DEV_BROADCAST_HDR));
            if (header.dbch_devicetype != DeviceType.DBT_DEVTYP_VOLUME)
            {
                return VolumeEvent.None;
            }

            var volume = (DEV_BROADCAST_VOLUME) Marshal.PtrToStructure(lParam,
                typeof(DEV_BROADCAST_VOLUME));

            var driveLetter = VolumeUtils.GetDriveLettersFromUnitsMask(volume.dbcv_unitmask)[0];

            return new VolumeEvent
            {
                EventType = (deviceEvent == DeviceEvent.DBT_DEVICEARRIVAL) ?
                    VolumeEventType.Mounted : VolumeEventType.Unmounted,
                DriveLetter = driveLetter
            };
        }
    }
}
