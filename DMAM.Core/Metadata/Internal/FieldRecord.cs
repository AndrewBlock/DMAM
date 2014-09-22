using System;
using System.Collections.Generic;
using System.Reflection;

namespace DMAM.Core.Metadata.Internal
{
    internal class FieldRecord
    {
        public Type OwnerType { get; set; }
        public string FieldName { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public string DisplayName { get; set; }
        public string ExternalFieldName { get; set; }
    }
}
