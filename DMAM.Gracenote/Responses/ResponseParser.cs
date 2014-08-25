using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

using DMAM.Gracenote.Utils;

namespace DMAM.Gracenote.Responses
{
    internal static class ResponseParser
    {
        private const string Name_Ok = "OK";
        private const string Name_Error = "ERROR";

        private static readonly XName XName_Responses = XName.Get("RESPONSES");
        private static readonly XName XName_Response = XName.Get("RESPONSE");
        private static readonly XName XName_Message = XName.Get("MESSAGE");
        private static readonly XName XName_Status = XName.Get("STATUS");

        public static IEnumerable<ResponseElement> Parse(HttpContent response)
        {
            try
            {
                return ProcessResponse(response);
            }
            catch (ServerReportedErrorException)
            {
                throw;
            }
            catch (InvalidServerResponseException)
            {
                throw;
            }
            catch
            {
                throw new InvalidServerResponseException();
            }
        }

        private static IEnumerable<XElement> GetResponseElements(XDocument document)
        {
            var rootElements = document.Elements();
            if ((rootElements.Count() != 1)
                || (rootElements.First().Name != XName_Responses))
            {
                throw new InvalidServerResponseException();
            }

            return rootElements.First().Elements();
        }

        private static IEnumerable<ResponseElement> ProcessResponse(HttpContent response)
        {
            var message = string.Empty;
            var document = ContentConverter.ConvertToXDocument(response);
            var elements = GetResponseElements(document);

            var responses = new List<ResponseElement>();
            foreach (var element in elements)
            {
                if (element.Name == XName_Message)
                {
                    message = element.Value;
                }
                else if (element.Name == XName_Response)
                {
                    var status = element.Attributes(XName_Status).FirstOrDefault();
                    if (status == null)
                    {
                        throw new InvalidServerResponseException();
                    }

                    if (status.Value == Name_Error)
                    {
                        throw new ServerReportedErrorException(message);
                    }
                    else if (status.Value == Name_Ok)
                    {
                        foreach (var subElement in element.Elements())
                        {
                            responses.Add(ResponseFactory.Create(subElement));
                        }
                    }
                }
            }

            return responses;
        }
    }
}
