using System;
using System.Collections.Generic;
using System.Reflection;
using Lte.Domain.LinqToCsv.Description;
using Lte.Domain.Regular;

namespace Lte.Domain.LinqToCsv.Mapper
{
    public class FieldMapperReading<T> : FieldMapper<T> where T : new()
    {
        public FieldMapperReading(
                    CsvFileDescription fileDescription,
                    string fileName,
                    bool writingFile)
            : base(fileDescription, fileName, writingFile)
        {
        }

        public void ReadNames(IDataRow row)
        {

            var currentNameIndex = 0;
            for (var i = 0; i < row.Count; i++)
            {
                if (row[i].Value == null) { continue; }
                if (!nameToInfo.ContainsKey(row[i].Value))
                {
                    //If we have to ignore this column
                    if (FileDescription.IgnoreUnknownColumns)
                    {
                        continue;
                    }

                    // name not found
                    throw new NameNotInTypeException(typeof(T).ToString(), row[i].Value, FileName);
                }

                //Map the column index in the CSV file with the column index of the business object.
                fieldIndexInfo.AddMappingIndex(i, currentNameIndex);
                currentNameIndex++;
            }

            fieldIndexInfo.UpdateIndexToInfo<T>(row, nameToInfo, FileDescription.EnforceCsvColumnAttribute,
                FileName);
        }


        public List<int> GetCharLengths()
        {
            return !FileDescription.NoSeparatorChar ? null : fieldIndexInfo.GetCharLengthList();
        }

        public T ReadObject(IDataRow row, AggregatedException ae)
        {
            if (row.Count > fieldIndexInfo.IndexToInfo.Length)
            {
                //Are we ignoring unknown columns?
                if (!FileDescription.IgnoreUnknownColumns)
                {
                    // Too many fields
                    throw new TooManyDataFieldsException(typeof(T).ToString(), row[0].LineNbr, FileName);
                }
            }

            var obj = new T();

            //If we will be using the mappings, we just iterate through all the cells in this row
            var maxRowCount = fieldIndexInfo.GetMaxRowCount(row.Count);


            for (var i = 0; i < maxRowCount; i++)
            {
                var tfi = fieldIndexInfo.QueryTypeFieldInfo(FileDescription.IgnoreUnknownColumns, i);
                if (tfi == null) { continue; }

                if (FileDescription.EnforceCsvColumnAttribute &&
                        (!tfi.HasColumnAttribute))
                {
                    // enforcing column attr, but this field/prop has no column attr.
                    // So there are too many fields in this record.
                    throw new TooManyNonCsvColumnDataFieldsException(typeof(T).ToString(), row[i].LineNbr, FileName);
                }

                if ((!FileDescription.FirstLineHasColumnNames) &&
                        (tfi.Index == ColumnAttribute.McDefaultFieldIndex))
                {
                    // First line in the file does not have field names, so we're 
                    // depending on the FieldIndex of each field in the type
                    // to ensure each value is placed in the correct field.
                    // However, now hit a field where there is no FieldIndex.
                    throw new MissingFieldIndexException(typeof(T).ToString(), row[i].LineNbr, FileName);
                }

                if (FileDescription.UseFieldIndexForReadingData && (!FileDescription.FirstLineHasColumnNames) &&
                    (tfi.Index > row.Count))
                {
                    throw new WrongFieldIndexException(typeof(T).ToString(), row[i].LineNbr, FileName);
                }


                var index = FileDescription.UseFieldIndexForReadingData ? tfi.Index - 1 : i;

                // value to put in the object
                var value = row[index].Value;

                if (string.IsNullOrEmpty(value) && tfi.TypeName!="String")
                {
                    if (!tfi.CanBeNull)
                    {
                         throw new MissingRequiredFieldException(
                            typeof (T).ToString(), tfi.Name, row[i].LineNbr, FileName);
                    }
                    
                }
                else
                {
                    if (tfi.OutputFormat == "HH:mm:ss.fff")
                    {
                        if (value != null) value = value.Substring(0, 8) + "." + value.Substring(9);
                    }
                    var objValue = tfi.UpdateObjectValue(value, FileDescription.FileCultureInfo);

                    var info = tfi.MemberInfo as PropertyInfo;
                    if (info != null)
                    {
                        info.SetValue(obj, objValue, null);
                    }
                    else
                    {
                        ((FieldInfo) tfi.MemberInfo).SetValue(obj, objValue);
                    }
                }
            }

            for (var i = row.Count; i < fieldIndexInfo.IndexToInfo.Length; i++)
            {
                var tfi = fieldIndexInfo.IndexToInfo[i];

                if (((!FileDescription.EnforceCsvColumnAttribute) || tfi.HasColumnAttribute) &&
                    (!tfi.CanBeNull))
                {
                    ae.AddException(new MissingRequiredFieldException(typeof(T).ToString(), tfi.Name,
                        row[row.Count - 1].LineNbr, FileName));
                }
            }
            return obj;
        }
    }
}
