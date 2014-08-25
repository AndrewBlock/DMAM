using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DMAM.Interop.IO
{
    public class VolumeInterop
    {
        public static readonly IntPtr INVALID_HANDLE_VALUE =
            (Marshal.SizeOf(typeof(IntPtr)) == 8) ? new IntPtr(-1L) : new IntPtr(-1);
        
        [DllImport("Kernel32.dll", EntryPoint = "GetLogicalDrives")]
        public static extern uint GetLogicalDrives();

        [DllImport("Kernel32.dll", EntryPoint = "GetDriveTypeW", CharSet=CharSet.Unicode)]
        public static extern DriveType GetDriveType(string lpRootPathName);

        [DllImport("Kernel32.dll", EntryPoint = "GetVolumeInformationW", CharSet=CharSet.Unicode)]
        public static extern bool GetVolumeInformation(string lpRootPathName,
            [In, Out] StringBuilder lpVolumeNameBuffer, int nVolumeNameSize,
            IntPtr lpVolumeSerialNumber, IntPtr lpMaximumComponentLength, IntPtr lpFileSystemFlags,
            IntPtr lpFileSystemNameBuffer, int nFileSystemNameSize);

        [DllImport("Kernel32.dll", EntryPoint = "CreateFileW", CharSet=CharSet.Unicode)]
        public static extern IntPtr CreateFile(string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode,
            IntPtr lpSecurityAttributes, CreationDisposition dwCreationDisposition, FileAttribute dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("Kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("Kernel32.dll", EntryPoint = "DeviceIoControl")]
        public static extern bool DeviceIoControl(IntPtr hDevice, IoControlCode dwIoControlCode, IntPtr lpInBuffer,
            int nInBufferSize, out _CDROM_TOC lpOutBuffer, int nOutBufferSize,
            out int lpBytesReturned, IntPtr lpOverlapped);
    }
}
