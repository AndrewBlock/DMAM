using System;

using DMAM.Core.MVVM;

namespace DMAM.Editors
{
    public class AlbumTrack : ViewModelBase
    {
        private string _trackTitle = string.Empty;
        
        public AlbumTrack(int trackNumber, string trackLength)
        {
            TrackNumber = trackNumber;
            TrackLength = trackLength;
        }

        public int TrackNumber { get; private set; }
        public string TrackLength { get; private set; }

        public string TrackTitle
        {
            get
            {
                return _trackTitle;
            }
            set
            {
                if (value == _trackTitle)
                {
                    return;
                }

                _trackTitle = value;
                NotifyPropertyChanged("TrackTitle");
            }
        }
    }
}
