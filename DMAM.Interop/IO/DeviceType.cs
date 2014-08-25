using System;

namespace DMAM.Interop.IO
{
    public enum DeviceType : int
    {
        DBT_DEVTYP_DEVICEINTERFACE = 0x00000005,
        DBT_DEVTYP_HANDLE = 0x00000006,
        DBT_DEVTYP_OEM = 0x00000000,
        DBT_DEVTYP_PORT = 0x00000003,
        DBT_DEVTYP_VOLUME = 0x00000002
    }
}
