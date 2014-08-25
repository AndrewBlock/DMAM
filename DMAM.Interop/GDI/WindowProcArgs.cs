using System;

namespace DMAM.Interop.GDI
{
    public class WindowProcArgs
    {
        public WindowProcArgs(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam)
        {
            HWnd = hWnd;
            Message = message;
            WParam = wParam;
            LParam = lParam;
        }

        public IntPtr HWnd { get; private set; }
        public int Message { get; private set; }
        public IntPtr WParam { get; private set; }
        public IntPtr LParam { get; private set; }
        public IntPtr? Result { get; set; }
    }
}
