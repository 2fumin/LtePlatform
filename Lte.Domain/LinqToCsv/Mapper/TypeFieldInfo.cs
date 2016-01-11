﻿using System;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using Lte.Domain.Regular;
using Lte.Domain.Common;

namespace Lte.Domain.LinqToCsv.Mapper
{
    public class TypeFieldInfo : IComparable<TypeFieldInfo>
    {
        private int _index = ColumnAttribute.McDefaultFieldIndex;

        public int Index => _index;

        public string Name { get; private set; }

        private bool _canBeNull = true;

        public bool CanBeNull => _canBeNull;

        private NumberStyles _inputNumberStyle = NumberStyles.Any;

        public string OutputFormat { get; private set; }

        public bool HasColumnAttribute { get; private set; }

        public int CharLength;

        public void UpdateAttributes()
        {
            _index = ColumnAttribute.McDefaultFieldIndex;
            Name = _memberInfo.Name;
            _inputNumberStyle = NumberStyles.Any;
            OutputFormat = "";
            HasColumnAttribute = false;
            CharLength = 0;

            foreach (var attribute in _memberInfo.GetCustomAttributes(typeof(CsvColumnAttribute), true))
            {
                var cca = (CsvColumnAttribute)attribute;

                if (!string.IsNullOrEmpty(cca.Name))
                {
                    Name = cca.Name;
                }
                _index = cca.FieldIndex;
                HasColumnAttribute = true;
                _canBeNull = cca.CanBeNull;
                OutputFormat = cca.OutputFormat;
                _inputNumberStyle = cca.NumberStyle;
                CharLength = cca.CharLength;
            }
        }

        public void ValidateAttributes<T>(bool allCsvColumnFieldsMustHaveFieldIndex,
            bool allRequiredFieldsMustHaveFieldIndex)
        {
            if (allCsvColumnFieldsMustHaveFieldIndex &&
                HasColumnAttribute &&
                _index == ColumnAttribute.McDefaultFieldIndex)
            {
                throw new ToBeWrittenButMissingFieldIndexException(
                                typeof(T).ToString(),
                                Name);
            }
            
            if (allRequiredFieldsMustHaveFieldIndex && (!_canBeNull) &&
                (_index == ColumnAttribute.McDefaultFieldIndex))
            {
                throw new RequiredButMissingFieldIndexException(typeof(T).ToString(), Name);
            }
        }

        private MemberInfo _memberInfo;

        public MemberInfo MemberInfo
        {
            get { return _memberInfo; }
            set 
            { 
                _memberInfo = value;

                var info = value as PropertyInfo;
                _fieldType = info?.PropertyType ?? ((FieldInfo)value).FieldType;

            }
        }

        private Type _fieldType;

        public string TypeName => _fieldType.Name;

        private TypeConverter _typeConverter;
        private MethodInfo _parseNumberMethod;
        private MethodInfo _parseExactMethod;

        public void UpdateParseParameters(bool useOutputFormatForParsingCsvValue)
        {
            _parseNumberMethod = _fieldType.GetParseNumberMethod();

            if (_parseNumberMethod != null) return;
            if (useOutputFormatForParsingCsvValue)
            {
                _parseExactMethod = _fieldType.GetParseExactMethod();
            }

            _typeConverter = null;
            if (_parseExactMethod == null)
            {
                _typeConverter = TypeDescriptor.GetConverter(_fieldType);
            }
        }

        public int CompareTo(TypeFieldInfo other)
        {
            return _index.CompareTo(other._index);
        }
        
        public override string ToString()
        {
            return $"Index: {_index}, Name: {Name}";
        }

        public string UpdateLastName<T>(string lastName, ref int lastFieldIndex)
        {
            if ((_index == lastFieldIndex) &&
                    (_index != ColumnAttribute.McDefaultFieldIndex))
            {
                throw new DuplicateFieldIndexException(typeof(T).ToString(),
                            Name, lastName, _index);
            }

            lastFieldIndex = _index;
            return Name;
        }

        public object UpdateObjectValue(string value, 
            CultureInfo fileCultureInfo)
        {
            object objValue;

            // Normally, either tfi.typeConverter is not null,
            // or tfi.parseNumberMethod is not null. 
            // 
            if (_typeConverter != null)
            {
                return _typeConverter.ConvertFromString(null, fileCultureInfo, value);
            }
            if (_parseExactMethod != null)
            {
                objValue = _parseExactMethod.Invoke(_fieldType,
                    new object[] { value, 
                        OutputFormat, 
                        fileCultureInfo });
            }
            else if (_parseNumberMethod != null)
            {
                objValue = _parseNumberMethod.Invoke(_fieldType,
                    new object[] { value, 
                        _inputNumberStyle, 
                        fileCultureInfo });
            }
            else
            {
                // No TypeConverter and no Parse method available.
                // Try direct approach.
                objValue = value;
            }

            return objValue;
        }
    }

}
