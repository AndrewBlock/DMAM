using System;
using System.Collections.Generic;

using DMAM.Core.Metadata;

namespace DMAM.Gracenote
{
    public class AlbumLookupInfo
    {
        public string Toc { get; private set; }
        public Exception Error { get; private set; }
        public IEnumerable<AlbumInfo> Albums { get; private set; }
        public object ClientData { get; private set; }

        internal AlbumLookupInfo(string toc, Exception error, IEnumerable<AlbumInfo> albums, object clientData)
        {
            Toc = toc;
            Error = error;
            Albums = albums;
            ClientData = clientData;
        }
    }
}
