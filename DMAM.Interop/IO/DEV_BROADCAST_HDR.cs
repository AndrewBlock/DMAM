using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.IO
{
    public struct DEV_BROADCAST_HDR
    {
        public int dbch_size;
        public DeviceType dbch_devicetype;
        public int dbch_reserved;
    }
}
