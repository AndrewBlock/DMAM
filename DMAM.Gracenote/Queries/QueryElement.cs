using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal abstract class QueryElement
    {
        protected static readonly XName XName_Client = XName.Get("CLIENT");
        protected static readonly XName XName_User = XName.Get("USER");
        protected static readonly XName XName_Query = XName.Get("QUERY");
        protected static readonly XName XName_Cmd = XName.Get("CMD");

        public abstract void OnAddQueryElement(XElement parentElement);
    }
}
