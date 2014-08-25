using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DMAM.Gracenote.Responses
{
    internal class UserResponse : ResponseElement
    {
        private static readonly XName XName_User = XName.Get("USER");

        public string UserID { get; private set; }

        public override XName ElementName
        {
            get
            {
                return XName_User;
            }
        }

        public override void Parse(XElement element)
        {
            UserID = element.Value;
        }
    }
}
