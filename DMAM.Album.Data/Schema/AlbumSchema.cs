using System;
using System.Collections.Generic;

using DMAM.Core.Schema;

namespace DMAM.Album.Data.Schema
{
    public class AlbumSchema : SchemaBase
    {
        public const string AlbumId = "AlbumId";
        public const string Title = "Title";
        public const string Artist = "Artist";
        public const string Album = "Album";
        public const string Year = "Year";
        public const string TotalTracks = "TotalTracks";
        public const string DiscNumber = "DiscNumber";
        public const string TotalDiscs = "TotalDiscs";
        public const string Genre = "Genre";
        public const string Composer = "Composer";
        public const string Comments = "Comments";

        protected override string TableName
        {
            get { return "Albums"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                new PrimaryKeyFieldEntry(AlbumId),
                new TextFieldEntry(Title, Resources.Title, GracenoteFields.TITLE, 512),
                new TextFieldEntry(Artist, Resources.Artist, GracenoteFields.ARTIST, 512),
                new TextFieldEntry(Album, Resources.Album, null, 512),
                new TextFieldEntry(Year, Resources.Year, GracenoteFields.DATE, 4),
                new TextFieldEntry(TotalTracks, Resources.TotalTracks, GracenoteFields.TRACK_COUNT, 4),
                new TextFieldEntry(DiscNumber, Resources.DiscNumber, null, 4),
                new TextFieldEntry(TotalDiscs, Resources.TotalDiscs, null, 4),
                new TextFieldEntry(Genre, Resources.Genre, GracenoteFields.GENRE, 64),
                new TextFieldEntry(Composer, Resources.Composer, null, 512),
                new TextFieldEntry(Comments, Resources.Comments, null, 1024)
            };
        }
    }
}
