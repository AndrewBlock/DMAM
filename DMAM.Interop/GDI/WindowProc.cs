using System;

namespace DMAM.Interop.GDI
{
    public delegate IntPtr WindowProc(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
}
