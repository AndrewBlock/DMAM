using System;

using DMAM.Album.Data.Schema;
using DMAM.Core.Metadata;
using DMAM.Database.Schema;

namespace DMAM.Test.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var dbSchema = new DbSchemaValidator();
            dbSchema.Validate(new ITableSchema[]
            {
                new AlbumSchema(),
                new TrackSchema(),
                new AudioFileSchema(),
                new CoverArtFileSchema(),
                new TrackCoverArtSchema()
            });

            var metadataService = new MetadataService();
            metadataService.RegisterMetadataType(typeof(DMAM.Album.Data.Schema.Album));

            return 0;
        }
    }
}
