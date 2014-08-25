using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Threading;

using DMAM.Core.Services;
using DMAM.Gracenote.Queries;
using DMAM.Gracenote.Responses;

namespace DMAM.Gracenote.Tasks
{
    internal class AlbumLookupByTocTask : HttpTask<AlbumLookupInfo>
    {
        private readonly TaskContext _context;
        private readonly string _toc;

        public AlbumLookupByTocTask(TaskContext context, string toc, Action<AlbumLookupInfo> clientNotify, object clientData)
            : base(clientNotify, clientData)
        {
            _context = context;
            _toc = toc;
        }

        public override void Start()
        {
            var tocSearchQuery = QueryBuilder.Build(
                new Authentication(_context.ClientId, _context.UserId),
                new Language(_context.Language),
                new Country(_context.Country),
                new TocLookup(_context.TocLookupMode, _toc)
            );

            Put(_context.WebApiUrl, tocSearchQuery);
        }

        protected override AlbumLookupInfo ProcessHttpResult(HttpResponseMessage response)
        {
            var albums = new List<AlbumInfo>();

            var results = ResponseParser.Parse(response.Content);
            foreach (var result in results)
            {
                var albumResponse = result as AlbumResponse;
                if (albumResponse == null)
                {
                    continue;
                }

                albums.Add(new AlbumInfo(albumResponse.AlbumMetadata, albumResponse.TrackMetadata,
                    albumResponse.CoverArtUrls));
            }

            return new AlbumLookupInfo(_toc, null, albums, ClientData);
        }
    }
}
