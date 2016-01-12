using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public const int McDefaultFieldIndex = int.MaxValue;

        public string Name { get; set; }

        public bool CanBeNull { get; set; }

        public int FieldIndex { get; set; }

        public string DateTimeFormat { get; set; }

        public ColumnAttribute()
        {
            Name = "";

            FieldIndex = McDefaultFieldIndex;

            CanBeNull = true;
        }

        public ColumnAttribute(string name, int fieldIndex, bool canBeNull)
        {
            Name = name;

            FieldIndex = fieldIndex;

            CanBeNull = canBeNull;
        }
    }
}
