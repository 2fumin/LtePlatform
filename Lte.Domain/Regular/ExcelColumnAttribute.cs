using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ExcelColumnAttribute : Attribute
    {
        private readonly string _columnName;
        private readonly TransformEnum _transformation;

        public ExcelColumnAttribute(string columnName, TransformEnum transformation = TransformEnum.Default)
        {
            _columnName = columnName;
            _transformation = transformation;
        }

        public string ColumnName
        {
            get { return _columnName; }
        }

        public Func<string, object> Transformation
        {
            get
            {
                //这里可以根据需要增加我们的转换规则
                switch(_transformation)
                {
                    default:
                        return null;
                }
            }
        }
    }
}
