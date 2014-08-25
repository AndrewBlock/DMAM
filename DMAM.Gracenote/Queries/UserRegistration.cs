using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal class UserRegistration : QueryElement
    {
        protected const string Name_Register = "REGISTER";

        private readonly string _clientID;

        public UserRegistration(string clientID)
        {
            _clientID = clientID;
        }
        
        public override void OnAddQueryElement(XElement parentElement)
        {
            var queryElement = new XElement(XName_Query);
            queryElement.SetAttributeValue(XName_Cmd, Name_Register);
            parentElement.Add(queryElement);

            var clientElement = new XElement(XName_Client);
            clientElement.Value = _clientID;
            queryElement.Add(clientElement);
        }
    }
}
