using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Linq;

using DMAM.Core.Metadata.Internal;
using DMAM.Core.Services;

namespace DMAM.Core.Metadata
{
    public class MetadataService : ServiceBase<MetadataService>
    {
        public void RegisterMetadataType(Type type)
        {
            var fieldRecords = new Dictionary<string, FieldRecord>();

            var mapAttribute = GetPrimaryAttribute<MetadataMapAttribute>(type);
            if (mapAttribute == null)
            {
                return;
            }

            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var fieldAttribute = GetPrimaryAttribute<MetadataFieldAttribute>(propertyInfo);
                if (fieldAttribute == null)
                {
                    continue;
                }

                var fieldRecord = new FieldRecord
                {
                    OwnerType = type,
                    FieldName = propertyInfo.Name,
                    PropertyInfo = propertyInfo
                };

                fieldRecords.Add(fieldRecord.FieldName, fieldRecord);

                if ((fieldAttribute.ResourcesType != null)
                    && (!string.IsNullOrWhiteSpace(fieldAttribute.ResourcesKey)))
                {
                    var resourceManager = new ResourceManager(fieldAttribute.ResourcesType.FullName,
                        fieldAttribute.ResourcesType.Assembly);
                    fieldRecord.DisplayName = resourceManager.GetString(fieldAttribute.ResourcesKey);
                }
                else
                {
                    fieldRecord.DisplayName = propertyInfo.Name;
                }

                var externalAttribute = GetPrimaryAttribute<ExternalFieldAttribute>(propertyInfo);
                if ((externalAttribute != null)
                    && (!string.IsNullOrWhiteSpace(externalAttribute.FieldName)))
                {
                    fieldRecord.ExternalFieldName = externalAttribute.FieldName;
                }
            }
        }

        private static T GetPrimaryAttribute<T>(MemberInfo memberInfo) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), false);
            if (attributes.Count() == 0)
            {
                return null;
            }

            return attributes.First() as T;
        }
    }
}
