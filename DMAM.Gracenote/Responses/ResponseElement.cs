using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Responses
{
    internal abstract class ResponseElement
    {
        public abstract XName ElementName { get; }

        public abstract void Parse(XElement element);
    }
}
