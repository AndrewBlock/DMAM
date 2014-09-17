using System;
using System.Collections.Generic;

namespace DMAM.Database.Schema.Internal
{
    internal class SchemaUtils
    {
        public static bool IsValidDbLabel(string tableName)
        {
            foreach (var character in tableName)
            {
                if (!IsValidDbLabelCharacter(character))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidDbLabelCharacter(char character)
        {
            return (((character >= '0') && (character <= '9'))
                || ((character >= 'A') && (character <= 'Z'))
                || ((character >= 'a') && (character <= 'z'))
                || (character == '_'));
        }

        public static string GetDisplayName(Type type)
        {
            return type.FullName;
        }
    }
}
