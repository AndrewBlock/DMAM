using System;
using System.Collections.Generic;

using DMAM.Core.Schema;

namespace DMAM.Album.Data.Schema
{
    public class AudioFileSchema : SchemaBase
    {
        public const string AudioFileId = "AudioFileId";
        public const string Filename = "Filename";

        protected override string TableName
        {
            get { return "AudioFiles"; }
        }

        protected override IEnumerable<ISchemaFieldEntry> LoadSchema()
        {
            return new List<ISchemaFieldEntry>()
            {
                new PrimaryKeyFieldEntry(AudioFileId),
                new TextFieldEntry(Filename, Resources.Path, null, 512)
            };
        }
    }
}
