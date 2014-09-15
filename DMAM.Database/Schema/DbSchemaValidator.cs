using System;
using System.Collections.Generic;

namespace DMAM.Database.Schema
{
    public class DbSchemaValidator
    {
        private Dictionary<Type, ITableSchema> _tablesByType = new Dictionary<Type, ITableSchema>();

        public void Validate(IEnumerable<ITableSchema> tables)
        {
            ProcessTables(tables);
        }

        private void ProcessTables(IEnumerable<ITableSchema> tables)
        {
            _tablesByType.Clear();

            var tableNames = new List<string>();
            foreach (var table in tables)
            {
                if (table == null)
                {
                    throw new InvalidTableSchemaException("ITableSchema value is <null>.");
                }

                var type = table.GetType();
                if (_tablesByType.ContainsKey(type))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema type '{0}' cannot be used more than once.",
                        type.FullName));
                }

                if (!IsValidDbLabel(table.TableName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema name '{0}' used by type '{1}' contains invalid characters.",
                        table.TableName, type.FullName));
                }

                var name = table.TableName.ToUpper();
                if (tableNames.Contains(table.TableName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema name '{0}' used by type '{1}' cannot be used more than once.",
                        table.TableName, type.FullName));
                }

                _tablesByType.Add(type, table);
                tableNames.Add(name);
            }
        }

        private static bool IsValidDbLabel(string tableName)
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

        private static bool IsValidDbLabelCharacter(char character)
        {
            return (((character >= '0') && (character <= '9'))
                || ((character >= '0') && (character <= '9'))
                || ((character >= 'a') && (character <= 'z'))
                || (character == '_'));
        }
    }
}
