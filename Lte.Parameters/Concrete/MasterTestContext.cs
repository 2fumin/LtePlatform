using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    [Database(Name = "masterTest")]
    public class MasterTestContext : DataContext
    {
        private static MappingSource _mappingSource = new AttributeMappingSource();

        public MasterTestContext()
            : base(
                "Data source=WIN-E7U0ZAGEQAQ;initial catalog=masterTest;user id=ouyanghui;password=123456;",
                _mappingSource)
        {

        }

        public Table<AreaTestDate> AreaTestDates => GetTable<AreaTestDate>();

        public Table<CsvFilesInfo> CsvFilesInfos => GetTable<CsvFilesInfo>();
    }
}
