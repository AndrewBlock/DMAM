using System;

using DMAM.Core.DataModels;
using DMAM.Core.MVVM;

namespace DMAM.Album.Data.Models
{
    public class TrackData : ViewModelBase, IDisposable
    {
        public TrackData(int trackNumber, FieldSet metadataFields, string trackLength)
        {
            TrackNumber = trackNumber;
            MetadataFields = metadataFields;
            TrackLength = trackLength;
        }

        public void Dispose()
        {
            MetadataFields.Dispose();
        }

        public int TrackNumber { get; private set; }
        public FieldSet MetadataFields { get; private set; }
        public string TrackLength { get; private set; }
    }
}
