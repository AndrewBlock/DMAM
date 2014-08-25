using System;

using DMAM.Core.MVVM;
using DMAM.Interop.IO;

namespace DMAM.Device
{
    public class VolumeInfo : ViewModelBase
    {
        private readonly char _driveLetter;
        private readonly VolumeType _volumeType;
        private string _label = string.Empty;
        private bool _containsCDAudio = false;

        public VolumeInfo(char driveLetter, VolumeType volumeType)
        {
            _driveLetter = driveLetter;
            _volumeType = volumeType;
        }

        public char DriveLetter
        {
            get
            {
                return _driveLetter;
            }
        }

        public string DrivePath
        {
            get
            {
                return VolumeUtils.GetDrivePathFromDriveLetter(_driveLetter);
            }
        }

        public VolumeType VolumeType
        {
            get
            {
                return _volumeType;
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                if (value == _label)
                {
                    return;
                }

                _label = value;
                NotifyPropertyChanged("Label");
            }
        }

        public bool ContainsCDAudio
        {
            get
            {
                return _containsCDAudio;
            }
            set
            {
                if (value == _containsCDAudio)
                {
                    return;
                }

                _containsCDAudio = value;
                NotifyPropertyChanged("ContainsCDAudio");
            }
        }
    }
}
