using System;
using System.Collections.Generic;

using DMAM.Core.Schema;

namespace DMAM.Album.Data.Schema
{
    public class TrackCoverArtSchema : SchemaBase
    {
        public const string CoverArtIndex = "CoverArtIndex";

        protected override string TableName
        {
            get { return "TrackCoverArt"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                new ForeignKeyFieldEntry(TrackSchema.TrackId, typeof(TrackSchema)),
                new ForeignKeyFieldEntry(CoverArtFileSchema.CoverArtId, typeof(CoverArtFileSchema)),
                new IntegerFieldEntry(CoverArtIndex, Resources.CoverArtIndex, null, 0, int.MaxValue)
            };
        }
    }
}
