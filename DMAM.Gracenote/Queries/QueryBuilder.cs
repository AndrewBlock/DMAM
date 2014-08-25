using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;

using DMAM.Gracenote.Utils;

namespace DMAM.Gracenote.Queries
{
    internal static class QueryBuilder
    {
        private static readonly XName XName_Queries = XName.Get("QUERIES");

        public static HttpContent Build(params QueryElement[] elements)
        {
            var document = new XDocument();

            var rootElement = new XElement(XName_Queries);
            document.Add(rootElement);

            foreach (var element in elements)
            {
                element.OnAddQueryElement(rootElement);
            }

            return ContentConverter.ConvertFromXDocument(document);
        }
    }
}
