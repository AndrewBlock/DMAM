using System;
using System.Collections.Generic;

using DMAM.Core.DataModels;
using DMAM.Core.MVVM;
using DMAM.Device;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Models
{
    public class AlbumData : ViewModelBase, IDisposable
    {
        protected AlbumData(CoverArtData coverArtInfo, FieldSet metadataFields, IEnumerable<TrackData> tracks)
        {
            CoverArtInfo = coverArtInfo;
            MetadataFields = metadataFields;
            Tracks = tracks;
        }

        public void Dispose()
        {
            CoverArtInfo.Dispose();
            MetadataFields.Dispose();

            foreach (var track in Tracks)
            {
                track.Dispose();
            }
        }

        public CoverArtData CoverArtInfo { get; private set; }
        public FieldSet MetadataFields { get; private set; }
        public IEnumerable<TrackData> Tracks { get; private set; }

        public void LoadAlbumInfo(AlbumInfo albumInfo)
        {
        }

        public static AlbumData FromAudioCDTracks(IEnumerable<AudioCDTrack> tracks)
        {
            return null;
        }
    }
}
