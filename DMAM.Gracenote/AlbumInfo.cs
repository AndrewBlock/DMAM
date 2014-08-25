using System;
using System.Collections.Generic;

using DMAM.Core.Metadata;

namespace DMAM.Gracenote
{
    public class AlbumInfo
    {
        public ItemMetadata AlbumMetadata { get; private set; }
        public IEnumerable<ItemMetadata> TrackMetadata { get; private set; }
        public IEnumerable<Uri> CoverArtUrls { get; private set; }

        internal AlbumInfo(ItemMetadata albumMetadata, IEnumerable<ItemMetadata> trackMetadata, IEnumerable<Uri> coverArtUrls)
        {
            AlbumMetadata = albumMetadata;
            TrackMetadata = trackMetadata;
            CoverArtUrls = coverArtUrls;
        }
    }
}
