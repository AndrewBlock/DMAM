using System;
using System.Collections.Generic;

using DMAM.Database.Schema;

namespace DMAM.Album.Data.Schema
{
    public class TrackSchema : TableSchemaBase
    {
        public const string TrackId = "TrackId";
        public const string Title = "Title";
        public const string Artist = "Artist";
        public const string AlbumArtist = "AlbumArtist";
        public const string Album = "Album";
        public const string Year = "Year";
        public const string TrackNumber = "TrackNumber";
        public const string TotalTracks = "TotalTracks";
        public const string DiscNumber = "DiscNumber";
        public const string TotalDiscs = "TotalDiscs";
        public const string Genre = "Genre";
        public const string Composer = "Composer";
        public const string Comments = "Comments";

        public override string TableName
        {
            get { return "Tracks"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                new PrimaryKeyFieldEntry(TrackId),
                new ForeignKeyFieldEntry(AlbumSchema.AlbumId, typeof(AlbumSchema)),
                new ForeignKeyFieldEntry(AudioFileSchema.AudioFileId, typeof(AudioFileSchema)),
                new TextFieldEntry(Title, Resources.Title, GracenoteFields.TITLE, 512),
                new TextFieldEntry(Artist, Resources.Artist, GracenoteFields.ARTIST, 512),
                new TextFieldEntry(AlbumArtist, Resources.AlbumArtist, null, 512),
                new TextFieldEntry(Album, Resources.Album, null, 512),
                new TextFieldEntry(Year, Resources.Year, GracenoteFields.DATE, 4),
                new TextFieldEntry(TrackNumber, Resources.TrackNumber, GracenoteFields.TRACK_NUM, 4),
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
