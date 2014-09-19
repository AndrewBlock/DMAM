using System;
using System.Collections.Generic;

using DMAM.Database.Schema;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Schema
{
    public class TrackCoverArtSchema : TableSchemaBase
    {
        public const string CoverArtIndex = "CoverArtIndex";

        public static readonly ISchemaFieldEntry TrackIdField = new ForeignKeyFieldEntry(TrackSchema.TrackId, typeof(TrackSchema));
        public static readonly ISchemaFieldEntry CoverArtIdField = new ForeignKeyFieldEntry(CoverArtFileSchema.CoverArtId, typeof(CoverArtFileSchema));
        public static readonly ISchemaFieldEntry CoverArtIndexField = new IntegerFieldEntry(CoverArtIndex, Resources.CoverArtIndex, null, 0, int.MaxValue);

        public override string TableName
        {
            get { return "TrackCoverArt"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                TrackIdField,
                CoverArtIdField,
                CoverArtIndexField
            };
        }
    }
}
