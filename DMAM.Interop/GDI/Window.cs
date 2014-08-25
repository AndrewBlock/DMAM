using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.GDI
{
    public class Window
    {
        private IntPtr _instance = Marshal.GetHINSTANCE(typeof(Window).Module);
        private IntPtr _handle = IntPtr.Zero;

        private WindowProc _wndProc;
        private string _windowClassName;

        public event WindowProcHandler WindowProc;

        public Window()
        {
        }

        public IntPtr Handle
        {
            get
            {
                return _handle;
            }
        }

        public IntPtr Instance
        {
            get
            {
                return _instance;
            }
        }

        public void CreateWindow(IntPtr parent, string windowClassName, WindowClassStyle classStyle, WindowStyle style, WindowExStyle exStyle)
        {
            if (_handle != IntPtr.Zero)
            {
                return;
            }

            _windowClassName = windowClassName;
            _wndProc = new WindowProc(WindowProcedure);

            var windowClass = new WNDCLASS
            {
                style = classStyle,
                lpfnWndProc = _wndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = Instance,
                lpszClassName = _windowClassName
            };

            var atom = WindowInterop.RegisterClass(ref windowClass);
            if (atom == IntPtr.Zero)
            {
                _wndProc = null;
                _windowClassName = null;
                return;
            }

            _handle = WindowInterop.CreateWindowEx(exStyle, _windowClassName, "", style, 0, 0, 0, 0, parent,
                IntPtr.Zero, _instance, IntPtr.Zero);

            if (_handle == IntPtr.Zero)
            {
                WindowInterop.UnregisterClass(_windowClassName, _instance);
                _windowClassName = "";
                _wndProc = null;
            }
        }

        public bool DestroyWindow()
        {
            if (_handle != IntPtr.Zero)
            {
                if (WindowInterop.DestroyWindow(_handle) == false)
                {
                    return false;
                }
            }

            if (_windowClassName.Length > 0)
            {
                if (!WindowInterop.UnregisterClass(_windowClassName, _instance))
                {
                    return false;
                }
            }

            _windowClassName = "";
            _wndProc = null;

            return true;
        }

        public static string GetUniqueClassName(string rootName)
        {
            var guidBytes = Guid.NewGuid().ToByteArray();

            string className = rootName + "_";
            for (var byteIndex = 0; byteIndex < guidBytes.Length; byteIndex++)
            {
                className += guidBytes[byteIndex].ToString("X");
            }

            return className;
        }

        private IntPtr WindowProcedure(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam)
        {
            if (message == WindowMessages.WM_NCCREATE)
            {
                _handle = hWnd;
            }
            else if (message == WindowMessages.WM_NCDESTROY)
            {
                _handle = IntPtr.Zero;
            }

            return OnWindowMessage(hWnd, message, wParam, lParam);
        }

        protected virtual IntPtr OnWindowMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam)
        {
            if (WindowProc != null)
            {
                var windowProcArgs = new WindowProcArgs(hWnd, message, wParam, lParam);
                WindowProc(this, windowProcArgs);

                if (windowProcArgs.Result != null)
                {
                    return windowProcArgs.Result.Value;
                }
            }
            
            return WindowInterop.DefWindowProc(hWnd, message, wParam, lParam);
        }
    }
}
