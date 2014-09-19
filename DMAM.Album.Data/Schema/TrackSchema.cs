using System;
using System.Collections.Generic;

using DMAM.Database.Schema;
using DMAM.Gracenote;

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

        public static readonly ISchemaFieldEntry TrackIdField = new PrimaryKeyFieldEntry(TrackId);
        public static readonly ISchemaFieldEntry AlbumIdField = new ForeignKeyFieldEntry(AlbumSchema.AlbumId, typeof(AlbumSchema));
        public static readonly ISchemaFieldEntry AudioFileIdField = new ForeignKeyFieldEntry(AudioFileSchema.AudioFileId, typeof(AudioFileSchema));
        public static readonly ISchemaFieldEntry TitleField = new TextFieldEntry(Title, Resources.Title, GracenoteFields.TITLE, 512);
        public static readonly ISchemaFieldEntry ArtistField = new TextFieldEntry(Artist, Resources.Artist, GracenoteFields.ARTIST, 512);
        public static readonly ISchemaFieldEntry AlbumArtistField = new TextFieldEntry(AlbumArtist, Resources.AlbumArtist, null, 512);
        public static readonly ISchemaFieldEntry AlbumField = new TextFieldEntry(Album, Resources.Album, null, 512);
        public static readonly ISchemaFieldEntry YearField = new TextFieldEntry(Year, Resources.Year, GracenoteFields.DATE, 4);
        public static readonly ISchemaFieldEntry TrackNumberField = new TextFieldEntry(TrackNumber, Resources.TrackNumber, GracenoteFields.TRACK_NUM, 4);
        public static readonly ISchemaFieldEntry TotalTracksField = new TextFieldEntry(TotalTracks, Resources.TotalTracks, GracenoteFields.TRACK_COUNT, 4);
        public static readonly ISchemaFieldEntry DiscNumberField = new TextFieldEntry(DiscNumber, Resources.DiscNumber, null, 4);
        public static readonly ISchemaFieldEntry TotalDiscsField = new TextFieldEntry(TotalDiscs, Resources.TotalDiscs, null, 4);
        public static readonly ISchemaFieldEntry GenreField = new TextFieldEntry(Genre, Resources.Genre, GracenoteFields.GENRE, 64);
        public static readonly ISchemaFieldEntry ComposerField = new TextFieldEntry(Composer, Resources.Composer, null, 512);
        public static readonly ISchemaFieldEntry CommentsField = new TextFieldEntry(Comments, Resources.Comments, null, 1024);

        public override string TableName
        {
            get { return "Tracks"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                TrackIdField,
                AlbumIdField,
                AudioFileIdField,
                TitleField,
                ArtistField,
                AlbumArtistField,
                AlbumField,
                YearField,
                TrackNumberField,
                TotalTracksField,
                DiscNumberField,
                TotalDiscsField,
                GenreField,
                ComposerField,
                CommentsField
            };
        }
    }
}
