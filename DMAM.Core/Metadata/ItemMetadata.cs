using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DMAM.Core.Metadata
{
    public class ItemMetadata
    {
        private Dictionary<string, string> _values = new Dictionary<string, string>();

        private ItemMetadata()
        {
        }

        public bool ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        public string this[string key]
        {
            get
            {
                if (_values.ContainsKey(key))
                {
                    return _values[key];
                }

                return null;
            }
        }

        public static ItemMetadata ProcessSingleItem(IEnumerable<XElement> elements, params XName[] skipNames)
        {
            var item = new ItemMetadata();

            foreach (var element in elements)
            {
                if (DoesListContainItem(skipNames, element.Name))
                {
                    continue;
                }

                var name = element.Name.LocalName;
                if (!item._values.ContainsKey(name))
                {
                    item._values.Add(name, element.Value);
                }
            }

            return item;
        }

        public static IEnumerable<ItemMetadata> ProcessChildItems(IEnumerable<XElement> elements, params XName[] processNames)
        {
            var items = new List<ItemMetadata>();

            foreach (var element in elements)
            {
                if (!DoesListContainItem(processNames, element.Name))
                {
                    continue;
                }

                items.Add(ItemMetadata.ProcessSingleItem(element.Elements()));
            }

            return items;
        }

        public static IEnumerable<string> ProcessSingleValues(IEnumerable<XElement> elements, XName xName, XName xAttributeName, string attributeValue)
        {
            var items = new List<string>();

            var names = new[] { xName };
            foreach (var element in elements)
            {
                if (!DoesListContainItem(names, element.Name))
                {
                    continue;
                }

                var attribute = element.Attribute(xAttributeName);
                if ((attribute == null) || (attribute.Value != attributeValue))
                {
                    continue;
                }

                items.Add(element.Value);
            }

            return items;
        }

        public static IEnumerable<Uri> ProcessSingleUrls(IEnumerable<XElement> elements, XName xName, XName xAttributeName, string attributeValue)
        {
            var values = ProcessSingleValues(elements, xName, xAttributeName, attributeValue);

            var items = new List<Uri>();
            foreach (var value in values)
            {
                items.Add(new Uri(value, UriKind.Absolute));
            }

            return items;
        }

        private static bool DoesListContainItem(XName[] listItems, XName item)
        {            
            foreach (var listItem in listItems)
            {
                if (item == listItem)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
