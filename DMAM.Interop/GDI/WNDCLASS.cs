using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.GDI
{
    public struct WNDCLASS
    {
        public WindowClassStyle style;
        public WindowProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public IntPtr lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 512)]
        public string lpszClassName;
    }
}
