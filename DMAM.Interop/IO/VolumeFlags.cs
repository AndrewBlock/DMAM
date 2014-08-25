using System;

namespace DMAM.Interop.IO
{
    [Flags]
    public enum VolumeFlags : ushort
    {
        DBTF_NONE = 0x0000,
        DBTF_MEDIA = 0x0001,
        DBTF_NET = 0x0002
    }
}
