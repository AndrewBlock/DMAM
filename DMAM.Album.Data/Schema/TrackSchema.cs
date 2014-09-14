using System;
using System.Collections.Generic;

using DMAM.Core.Schema;

namespace DMAM.Album.Data.Schema
{
    public class TrackSchema : SchemaBase
    {
        protected override IEnumerable<SchemaFieldEntry> LoadSchema()
        {
            return new List<SchemaFieldEntry>()
            {
                new TextFieldEntry(Resources.Title, "Title", "TITLE", 512),
                new TextFieldEntry(Resources.Artist, "Artist", "ARTIST", 512),
                new TextFieldEntry(Resources.AlbumArtist, "AlbumArtist", null, 512),
                new TextFieldEntry(Resources.Album, "Album", null, 512),
                new TextFieldEntry(Resources.Year, "Year", "DATE", 4),
                new TextFieldEntry(Resources.TrackNumber, "TrackNumber", "TRACK_NUM", 4),
                new TextFieldEntry(Resources.TotalTracks, "TotalTracks", "TRACK_COUNT", 4),
                new TextFieldEntry(Resources.DiscNumber, "DiscNumber", null, 4),
                new TextFieldEntry(Resources.TotalDiscs, "TotalDiscs", null, 4),
                new TextFieldEntry(Resources.Genre, "Genre", "GENRE", 64),
                new TextFieldEntry(Resources.Composer, "Composer", null, 512),
                new TextFieldEntry(Resources.Comments, "Comments", null, 1024)
            };
        }
    }
}
