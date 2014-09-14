using System;
using System.Collections.Generic;

using DMAM.Core.Schema;

namespace DMAM.Album.Data.Schema
{
    public class CoverArtFileSchema : SchemaBase
    {
        public const string CoverArtId = "CoverArtId";
        public const string Filename = "Filename";
        public const string Width = "Width";
        public const string Height = "Height";

        protected override string TableName
        {
            get { return "CoverArtFiles"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                new PrimaryKeyFieldEntry(CoverArtId),
                new TextFieldEntry(Filename, Resources.Path, null, 512),
                new IntegerFieldEntry(Width, Resources.Width, null, 0, int.MaxValue),
                new IntegerFieldEntry(Height, Resources.Height, null, 0, int.MaxValue)
            };
        }
    }
}
