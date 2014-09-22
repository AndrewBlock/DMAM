using System;

using DMAM.Core.Metadata;
using DMAM.Database.Attributes;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Schema
{
    [Table("Albums")]
    [MetadataMap]
    public class Album
    {
        [PrimaryKey]
        public long AlbumId { get; set; }

        [MetadataField(typeof(Resources), "Title")]
        [ExternalField(GracenoteFields.TITLE)]
        public string Title { get; set; }

        [MetadataField(typeof(Resources), "Artist")]
        [ExternalField(GracenoteFields.ARTIST)]
        public string Artist { get; set; }

        [MetadataField(typeof(Resources), "Album")]
        public string AlbumTitle { get; set; }

        [MetadataField(typeof(Resources), "Year")]
        [ExternalField(GracenoteFields.DATE)]
        public string Year { get; set; }

        [MetadataField(typeof(Resources), "TotalTracks")]
        [ExternalField(GracenoteFields.TRACK_COUNT)]
        public string TotalTracks { get; set; }

        [MetadataField(typeof(Resources), "DiscNumber")]
        public string DiscNumber { get; set; }

        [MetadataField(typeof(Resources), "TotalDiscs")]
        public string TotalDiscs { get; set; }

        [MetadataField(typeof(Resources), "Genre")]
        [ExternalField(GracenoteFields.GENRE)]
        public string Genre { get; set; }

        [MetadataField(typeof(Resources), "Composer")]
        public string Composer { get; set; }

        [MetadataField(typeof(Resources), "Comments")]
        public string Comments { get; set; }
    }
}
