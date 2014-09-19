using System;
using System.Collections.Generic;

using DMAM.Database.Schema;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Schema
{
    public class AlbumSchema : TableSchemaBase
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

        public static readonly ISchemaFieldEntry AlbumIdField = new PrimaryKeyFieldEntry(AlbumId);
        public static readonly ISchemaFieldEntry TitleField = new TextFieldEntry(Title, Resources.Title, GracenoteFields.TITLE, 512);
        public static readonly ISchemaFieldEntry ArtistField = new TextFieldEntry(Artist, Resources.Artist, GracenoteFields.ARTIST, 512);
        public static readonly ISchemaFieldEntry AlbumTitleField = new TextFieldEntry(Album, Resources.Album, null, 512);
        public static readonly ISchemaFieldEntry YearField = new TextFieldEntry(Year, Resources.Year, GracenoteFields.DATE, 4);
        public static readonly ISchemaFieldEntry TotalTracksField = new TextFieldEntry(TotalTracks, Resources.TotalTracks, GracenoteFields.TRACK_COUNT, 4);
        public static readonly ISchemaFieldEntry DiscNumberField = new TextFieldEntry(DiscNumber, Resources.DiscNumber, null, 4);
        public static readonly ISchemaFieldEntry TotalDiscsField = new TextFieldEntry(TotalDiscs, Resources.TotalDiscs, null, 4);
        public static readonly ISchemaFieldEntry GenreField = new TextFieldEntry(Genre, Resources.Genre, GracenoteFields.GENRE, 64);
        public static readonly ISchemaFieldEntry ComposerField = new TextFieldEntry(Composer, Resources.Composer, null, 512);
        public static readonly ISchemaFieldEntry CommentsField = new TextFieldEntry(Comments, Resources.Comments, null, 1024);

        public override string TableName
        {
            get { return "Albums"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                AlbumIdField,
                TitleField,
                ArtistField,
                AlbumTitleField,
                YearField,
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
