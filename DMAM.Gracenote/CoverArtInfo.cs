using System;
using System.Collections.Generic;

using DMAM.Core.Metadata;

namespace DMAM.Gracenote
{
    public class CoverArtInfo
    {
        public Uri CoverArtUrl { get; private set; }
        public Exception Error { get; private set; }
        public byte[] CoverArtImage { get; private set; }
        public object ClientData { get; private set; }

        internal CoverArtInfo(Uri coverArtUrl, Exception error, byte[] coverArtImage, object clientData)
        {
            CoverArtUrl = coverArtUrl;
            Error = error;
            CoverArtImage = coverArtImage;
            ClientData = clientData;
        }
    }
}
