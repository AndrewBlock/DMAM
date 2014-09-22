using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMAM.Core.Metadata
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MetadataMapAttribute : Attribute
    {
        public MetadataMapAttribute()
        {
        }
    }
}
