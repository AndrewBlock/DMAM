using System;
using System.Collections.Generic;

using DMAM.Database.Schema;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Schema
{
    public class CoverArtFileSchema : TableSchemaBase
    {
        public const string CoverArtId = "CoverArtId";
        public const string Filename = "Filename";
        public const string Width = "Width";
        public const string Height = "Height";

        public static readonly ISchemaFieldEntry CoverArtIdField = new PrimaryKeyFieldEntry(CoverArtId);
        public static readonly ISchemaFieldEntry FilenameField = new TextFieldEntry(Filename, Resources.Path, null, 512);
        public static readonly ISchemaFieldEntry WidthField = new IntegerFieldEntry(Width, Resources.Width, null, 0, int.MaxValue);
        public static readonly ISchemaFieldEntry HeightField = new IntegerFieldEntry(Height, Resources.Height, null, 0, int.MaxValue);

        public override string TableName
        {
            get { return "CoverArtFiles"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                CoverArtIdField,
                FilenameField,
                WidthField,
                HeightField
            };
        }
    }
}
