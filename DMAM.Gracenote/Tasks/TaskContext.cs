using System;

using DMAM.Gracenote.Queries;

namespace DMAM.Gracenote.Tasks
{
    internal class TaskContext
    {
        public readonly string Country = "USA";
        public readonly string Language = Languages.ENGLISH;
        public readonly string TocLookupMode = TocLookup.SINGLE_BEST_COVER;

        public readonly string WebApiUrlBase = "https://c{0}.web.cddbp.net/webapi/xml/1.0/";
        public readonly string ClientId = "";
        public readonly string UserId = "";

        public Uri WebApiUrl
        {
            get
            {
                lock (this)
                {
                    var clientIdPrefix = ClientId.Substring(0, ClientId.IndexOf('-'));
                    return new Uri(string.Format(WebApiUrlBase, clientIdPrefix), UriKind.Absolute);
                }
            }
        }
    }
}
