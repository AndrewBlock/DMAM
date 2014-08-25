using System;

using DMAM.Core.Events;

namespace DMAM.Device
{
    public class CDRomDriveStatus : EventData
    {
        public char DriveLetter { get; set; }
        public bool ContainsAudioCD { get; set; }
        
        public override EventData Clone()
        {
            return new CDRomDriveStatus
            {
                DriveLetter = DriveLetter,
                ContainsAudioCD = ContainsAudioCD
            };
        }
    }
}
