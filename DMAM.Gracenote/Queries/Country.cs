using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal class Country : QueryElement
    {
        protected static readonly XName XName_Country = XName.Get("COUNTRY");

        private readonly string _country;

        public Country(string country)
        {
            _country = country;
        }
        
        public override void OnAddQueryElement(XElement parentElement)
        {
            var countryElement = new XElement(XName_Country);
            countryElement.Value = _country;
            parentElement.Add(countryElement);
        }
    }
}
