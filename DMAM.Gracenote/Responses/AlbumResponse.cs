using System;
using System.Collections.Generic;
using System.Xml.Linq;

using DMAM.Core.Metadata;
using DMAM.Gracenote.Utils;

namespace DMAM.Gracenote.Responses
{
    internal class AlbumResponse : ResponseElement
    {
        private static readonly XName XName_Album = XName.Get("ALBUM");
        private static readonly XName XName_Track = XName.Get("TRACK");
        private static readonly XName XName_Url = XName.Get("URL");
        private static readonly XName XName_Type = XName.Get("TYPE");

        private const string Name_CoverArt = "COVERART";
        
        public ItemMetadata AlbumMetadata { get; private set; }
        public IEnumerable<ItemMetadata> TrackMetadata { get; private set; }
        public IEnumerable<Uri> CoverArtUrls { get; private set; }

        public override XName ElementName
        {
            get
            {
                return XName_Album;
            }
        }

        public override void Parse(XElement element)
        {
            AlbumMetadata = ItemMetadata.ProcessSingleItem(element.Elements(), XName_Track, XName_Url);
            TrackMetadata = ItemMetadata.ProcessChildItems(element.Elements(), XName_Track);
            CoverArtUrls = ItemMetadata.ProcessSingleUrls(element.Elements(), XName_Url, XName_Type, Name_CoverArt);
        }
    }
}
