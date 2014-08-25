using System;
using System.Xml.Linq;

namespace DMAM.Gracenote.Queries
{
    internal class Authentication : QueryElement
    {
        protected static readonly XName XName_Auth = XName.Get("AUTH");

        private readonly string _clientID;
        private readonly string _userID;

        public Authentication(string clientID, string userID)
        {
            _clientID = clientID;
            _userID = userID;
        }
        
        public override void OnAddQueryElement(XElement parentElement)
        {
            var authElement = new XElement(XName_Auth);
            parentElement.Add(authElement);

            var clientElement = new XElement(XName_Client);
            clientElement.Value = _clientID;
            authElement.Add(clientElement);

            var userElement = new XElement(XName_User);
            userElement.Value = _userID;
            authElement.Add(userElement);
        }
    }
}
