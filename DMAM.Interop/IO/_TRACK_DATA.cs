using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.IO
{
    public struct _TRACK_DATA
    {
        public byte Reserved;
        public byte ControlAdr;
        public byte TrackNumber;
        public byte Reserved1;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.U1, SizeConst=4)]
        public byte[] Address;
    }
}
