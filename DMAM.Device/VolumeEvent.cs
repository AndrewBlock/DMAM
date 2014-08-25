namespace DMAM.Device
{
    internal class VolumeEvent
    {
        public char DriveLetter;
        public VolumeEventType EventType;

        public static readonly VolumeEvent None = new VolumeEvent
        {
            DriveLetter = (char) 0x0000,
            EventType = VolumeEventType.None
        };
    }
}
