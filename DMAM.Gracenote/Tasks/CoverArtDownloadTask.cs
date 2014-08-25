using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Threading;

using DMAM.Core.Services;
using DMAM.Gracenote.Queries;
using DMAM.Gracenote.Responses;
using DMAM.Gracenote.Utils;

namespace DMAM.Gracenote.Tasks
{
    internal class CoverArtDownloadTask : HttpTask<CoverArtInfo>
    {
        private readonly TaskContext _context;
        private readonly Uri _coverArtUrl;

        public CoverArtDownloadTask(TaskContext context, Uri coverArtUrl, Action<CoverArtInfo> clientNotify, object clientData)
            : base(clientNotify, clientData)
        {
            _context = context;
            _coverArtUrl = coverArtUrl;
        }

        public override void Start()
        {
            Get(_coverArtUrl);
        }

        protected override CoverArtInfo ProcessHttpResult(HttpResponseMessage response)
        {
            var coverArtImage = ContentConverter.ConvertToByteArray(response.Content);
            return new CoverArtInfo(_coverArtUrl, null, coverArtImage, ClientData);
        }
    }
}
