namespace DMAM.Device
{
    public class AudioCDTrack
    {
        private int _index;
        private int _address;
        private int _length;
        private string _displayLength;

        public AudioCDTrack(int index, int address, int length)
        {
            _index = index;
            _address = address;
            _length = length;
            _displayLength = AudioCDUtils.GetTrackLengthDisplay(_length);
        }

        public int Index
        {
            get { return _index; }
        }

        public int Address
        {
            get { return _address; }
        }

        public int Length
        {
            get { return _length; }
        }

        public string DisplayLength
        {
            get { return _displayLength; }
        }
    }
}
