using System;
using System.Collections.Generic;

using DMAM.Database.Schema.Internal;

namespace DMAM.Database.Schema
{
    public class DbSchemaValidator
    {
        private Dictionary<Type, TableRecord> _tables = new Dictionary<Type, TableRecord>();

        public void Validate(IEnumerable<ITableSchema> tables)
        {
            ValidateTables(tables);
            ValidateForeignKeyDependencies();
        }

        private void ValidateTables(IEnumerable<ITableSchema> tables)
        {
            _tables.Clear();

            var tableNames = new List<string>();
            foreach (var table in tables)
            {
                if (table == null)
                {
                    throw new InvalidTableSchemaException("ITableSchema value is <null>.");
                }

                var type = table.GetType();
                if (_tables.ContainsKey(type))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema type '{0}' cannot be used more than once.",
                        SchemaUtils.GetDisplayName(type)));
                }

                if (string.IsNullOrWhiteSpace(table.TableName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema name used by type '{0}' is empty.",
                        SchemaUtils.GetDisplayName(type)));
                }

                if (!SchemaUtils.IsValidDbLabel(table.TableName))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema name '{0}' used by type '{1}' contains invalid characters.",
                        table.TableName, SchemaUtils.GetDisplayName(type)));
                }

                var name = table.TableName.ToUpper();
                if (tableNames.Contains(name))
                {
                    throw new InvalidTableSchemaException(string.Format(
                        "ITableSchema name '{0}' used by type '{1}' cannot be used more than once.",
                        table.TableName, SchemaUtils.GetDisplayName(type)));
                }

                _tables.Add(type, new TableRecord(type, table));
                tableNames.Add(name);
            }
        }

        private void ValidateForeignKeyDependencies()
        {
            foreach (var tableRecord in _tables.Values)
            {
                ValidateTableForeignKeyDependencies(tableRecord);
            }
        }

        private void ValidateTableForeignKeyDependencies(TableRecord tableRecord)
        {
            foreach (var columnName in tableRecord.Columns.Keys)
            {
                ValidateForeignKeyDependency(tableRecord.Columns[columnName],
                    columnName, tableRecord.Type);
            }
        }

        private void ValidateForeignKeyDependency(ISchemaFieldEntry column, string columnName, Type ownerType)
        {
            var foreignKeyColumn = column as ForeignKeyFieldEntry;
            if (foreignKeyColumn == null)
            {
                return;
            }

            var foreignTableType = foreignKeyColumn.ForeignSchemaType;
            if (!_tables.ContainsKey(foreignTableType))
            {
                throw new InvalidTableSchemaException(string.Format(
                    "ITableSchema '{0}' referred to by ITableSchema '{1}', column '{2}' does not exist.",
                    SchemaUtils.GetDisplayName(foreignTableType), SchemaUtils.GetDisplayName(ownerType),
                    foreignKeyColumn.ColumnName));
            }

            var foreignTable = _tables[foreignTableType];
            if (!foreignTable.Columns.ContainsKey(columnName))
            {
                throw new InvalidTableSchemaException(string.Format(
                    "Column '{0}' in ITableSchema '{1}' referred to by ITableSchema '{2}' does not exist.",
                    foreignKeyColumn.ColumnName, SchemaUtils.GetDisplayName(foreignTableType),
                    SchemaUtils.GetDisplayName(ownerType)));
            }

            var primaryColumn = foreignTable.Columns[columnName] as PrimaryKeyFieldEntry;
            if (primaryColumn == null)
            {
                throw new InvalidTableSchemaException(string.Format(
                    "Column '{0}' in ITableSchema '{1}' referred to by ITableSchema '{2}' is not a PrimaryKeyField.",
                    foreignKeyColumn.ColumnName, SchemaUtils.GetDisplayName(foreignTableType),
                    SchemaUtils.GetDisplayName(ownerType)));
            }
        }
    }
}
