using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.IO
{
    public struct _CDROM_READ_TOC_EX
    {
        public CDROM_READ_TOC_EX_FORMAT Format;
        public byte Reserved1;
        public byte Msf;
        public byte SessionTrack;
        public byte Reserved2;
        public byte Reserved3;
    }
}
