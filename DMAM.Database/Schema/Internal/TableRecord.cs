using System;
using System.Collections.Generic;

using DMAM.Database.Schema;

namespace DMAM.Database.Schema.Internal
{
    internal class TableRecord
    {
        private Dictionary<string, ISchemaFieldEntry> _columns = new Dictionary<string, ISchemaFieldEntry>();

        public TableRecord(Type type, ITableSchema tableSchema)
        {
            Type = type;
            TableSchema = tableSchema;

            ValidateColumns();
        }

        public Type Type { get; private set; }
        public ITableSchema TableSchema { get; private set; }

        public IDictionary<string, ISchemaFieldEntry> Columns
        {
            get
            {
                return _columns;
            }
        }

        private void ValidateColumns()
        {
            foreach (var schemaFieldEntry in TableSchema.Schema)
            {
                if (schemaFieldEntry == null)
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ISchemaFieldEntry value is <null> in type '{0}'.",
                        SchemaUtils.GetDisplayName(Type)));
                }

                if (string.IsNullOrWhiteSpace(schemaFieldEntry.ColumnName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "Column name used by type '{0}' is empty.",
                        SchemaUtils.GetDisplayName(Type)));
                }

                if (!SchemaUtils.IsValidDbLabel(schemaFieldEntry.ColumnName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "Column name '{0}' used by type '{1}' contains invalid characters.",
                        schemaFieldEntry.ColumnName, SchemaUtils.GetDisplayName(Type)));
                }

                var columnName = schemaFieldEntry.ColumnName.ToUpper();
                if (_columns.ContainsKey(columnName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "Column name '{0}' used by type '{1}' cannot be used more than once.",
                        schemaFieldEntry.ColumnName, SchemaUtils.GetDisplayName(Type)));
                }

                _columns.Add(columnName, schemaFieldEntry);
            }
        }
    }
}
