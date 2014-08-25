using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace DMAM.Gracenote.Utils
{
    internal class ContentConverter
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        public static HttpContent ConvertFromXDocument(XDocument document)
        {
            return new StringContent(document.ToString(SaveOptions.None), Encoding);
        }

        public static XDocument ConvertToXDocument(HttpContent content)
        {
            var stringContent = content.ReadAsStringAsync();
            stringContent.Wait();
            return XDocument.Parse(stringContent.Result, LoadOptions.None);
        }

        public static byte[] ConvertToByteArray(HttpContent content)
        {
            var byteContent = content.ReadAsByteArrayAsync();
            byteContent.Wait();
            return byteContent.Result;
        }
    }
}
