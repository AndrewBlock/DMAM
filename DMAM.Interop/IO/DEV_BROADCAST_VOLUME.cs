namespace DMAM.Interop.IO
{
    public struct DEV_BROADCAST_VOLUME
    {
        public int dbcv_size;
        public DeviceType dbcv_devicetype;
        public int dbcv_reserved;
        public uint dbcv_unitmask;
        public VolumeFlags dbcv_flags;
    }
}
