using System;
using System.Collections.Generic;

using DMAM.Database.Schema;

namespace DMAM.Album.Data.Schema
{
    public class TrackCoverArtSchema : TableSchemaBase
    {
        public const string CoverArtIndex = "CoverArtIndex";

        public override string TableName
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
