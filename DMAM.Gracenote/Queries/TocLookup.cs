using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal class TocLookup : QueryElement
    {
        public const string SINGLE_BEST = "SINGLE_BEST";
        public const string SINGLE_BEST_COVER = "SINGLE_BEST_COVER";
        
        protected const string Name_AlbumToc = "ALBUM_TOC";

        protected static readonly XName XName_Mode = XName.Get("MODE");
        protected static readonly XName XName_Toc = XName.Get("TOC");
        protected static readonly XName XName_Offsets = XName.Get("OFFSETS");

        private readonly string _mode;
        private readonly string _toc;

        public TocLookup(string mode, string toc)
        {
            _mode = mode;
            _toc = toc;
        }
        
        public override void OnAddQueryElement(XElement parentElement)
        {
            var queryElement = new XElement(XName_Query);
            queryElement.SetAttributeValue(XName_Cmd, Name_AlbumToc);
            parentElement.Add(queryElement);

            if (!string.IsNullOrWhiteSpace(_mode))
            {
                var modeElement = new XElement(XName_Mode);
                modeElement.Value = _mode;
                queryElement.Add(modeElement);
            }

            var tocElement = new XElement(XName_Toc);
            queryElement.Add(tocElement);

            var offsetsElement = new XElement(XName_Offsets);
            offsetsElement.Value = _toc;
            tocElement.Add(offsetsElement);
        }
    }
}
