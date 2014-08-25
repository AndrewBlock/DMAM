using System;
using System.Collections.Generic;
using System.Net.Http;

using DMAM.Core.Services;
using DMAM.Gracenote.Queries;
using DMAM.Gracenote.Responses;
using DMAM.Gracenote.Tasks;

namespace DMAM.Gracenote
{
    public class CDDBService : ServiceBase<CDDBService>
    {
        private readonly TaskContext _context = new TaskContext();
        
        protected override bool OnInitialize()
        {
            return true;
        }

        protected override void OnShutdown()
        {
        }

        public void QueueAlbumLookupByToc(string toc, Action<AlbumLookupInfo> completion, object clientData)
        {
            QueueTask(new AlbumLookupByTocTask(_context, toc, completion, clientData));
        }

        public void QueueCoverArtDownload(Uri coverArtUrl, Action<CoverArtInfo> completion, object clientData)
        {
            QueueTask(new CoverArtDownloadTask(_context, coverArtUrl, completion, clientData));
        }
    }
}
