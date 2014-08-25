using System;

namespace DMAM.Interop.IO
{
    [Flags]
    public enum FileShare : uint
    {
        FILE_SHARE_READ = 0x00000001,
        FILE_SHARE_WRITE = 0x00000002,
        FILE_SHARE_DELETE = 0x00000004
    }
}
