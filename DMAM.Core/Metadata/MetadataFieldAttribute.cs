using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMAM.Core.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetadataFieldAttribute : Attribute
    {
        public MetadataFieldAttribute(Type resourcesType, string resourcesKey)
        {
            ResourcesType = resourcesType;
            ResourcesKey = resourcesKey;
        }

        public Type ResourcesType { get; private set; }
        public string ResourcesKey { get; private set; }
    }
}
