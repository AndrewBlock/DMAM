using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DMAM.Gracenote.Responses
{
    internal static class ResponseFactory
    {
        static ResponseFactory()
        {
            RegisterResponseTypes();
        }

        private static void RegisterResponseTypes()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                RegisterResponseType(type);
            }
        }

        private static void RegisterResponseType(Type type)
        {
            if (!type.IsSubclassOf(typeof(ResponseElement)))
            {
                return;
            }

            var instance = (ResponseElement) Activator.CreateInstance(type);

            if ((instance.ElementName == null)
                || (_responseTypeLookup.ContainsKey(instance.ElementName)))
            {
                return;
            }

            _responseTypeLookup.Add(instance.ElementName, type);
        }

        private static readonly Dictionary<XName, Type> _responseTypeLookup = new Dictionary<XName, Type>();

        public static ResponseElement Create(XElement element)
        {
            ResponseElement response = null;

            if (_responseTypeLookup.ContainsKey(element.Name))
            {
                response = (ResponseElement) Activator.CreateInstance(_responseTypeLookup[element.Name]);
            }
            else
            {
                response = new UnknownResponse();
            }

            response.Parse(element);
            return response;
        }
    }
}
