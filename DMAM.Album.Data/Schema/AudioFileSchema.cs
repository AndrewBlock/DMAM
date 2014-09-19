using System;
using System.Collections.Generic;

using DMAM.Database.Schema;
using DMAM.Gracenote;

namespace DMAM.Album.Data.Schema
{
    public class AudioFileSchema : TableSchemaBase
    {
        public const string AudioFileId = "AudioFileId";
        public const string Filename = "Filename";

        public static readonly ISchemaFieldEntry AudioFileIdField = new PrimaryKeyFieldEntry(AudioFileId);
        public static readonly ISchemaFieldEntry FilenameField = new TextFieldEntry(Filename, Resources.Path, null, 512);

        public override string TableName
        {
            get { return "AudioFiles"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                AudioFileIdField,
                FilenameField
            };
        }
    }
}
