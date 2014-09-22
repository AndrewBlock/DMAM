using System;

namespace DMAM.Database.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public ForeignKeyAttribute(Type foreignTable, string foreignColumn)
        {
            ForeignTable = foreignTable;
            ForeignColumn = foreignColumn;
        }

        public Type ForeignTable { get; private set; }
        public string ForeignColumn { get; private set; }
    }
}
