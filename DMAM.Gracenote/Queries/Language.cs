using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal class Language : QueryElement
    {
        protected static readonly XName XName_Language = XName.Get("LANG");

        private readonly string _language;

        public Language(string language)
        {
            _language = language;
        }
        
        public override void OnAddQueryElement(XElement parentElement)
        {
            var languageElement = new XElement(XName_Language);
            languageElement.Value = _language;
            parentElement.Add(languageElement);
        }
    }
}
