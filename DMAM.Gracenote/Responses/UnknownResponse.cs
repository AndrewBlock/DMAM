using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DMAM.Gracenote.Responses
{
    internal class UnknownResponse : ResponseElement
    {
        private XName _elementName = null;

        public override XName ElementName
        {
            get
            {
                return _elementName;
            }
        }

        public override void Parse(XElement element)
        {
            _elementName = element.Name;
        }
    }
}
