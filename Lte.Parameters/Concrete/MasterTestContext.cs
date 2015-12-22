using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Concrete
{
    [Database(Name = "masterTest")]
    public class MasterTestContext : DataContext
    {
        private static readonly MappingSource _mappingSource = new AttributeMappingSource();

        public MasterTestContext()
            : base(
                "Data source=WIN-E7U0ZAGEQAQ;initial catalog=masterTest;user id=ouyanghui;password=123456;",
                _mappingSource)
        {

        }

        public Table<AreaTestDate> AreaTestDates => GetTable<AreaTestDate>();

        public Table<CsvFilesInfo> CsvFilesInfos => GetTable<CsvFilesInfo>();

        public Table<RasterInfo> RasterInfos => GetTable<RasterInfo>();

        [Function(Name = "dbo.sp_get4GFileContents")]
        public ISingleResult<FileRecord4G> Get4GFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), tableName);
            return (ISingleResult<FileRecord4G>) result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get4GFileContentsRasterConsidered")]
        public ISingleResult<FileRecord4G> Get4GFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), tableName, rasterNum);
            return (ISingleResult<FileRecord4G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get3GFileContents")]
        public ISingleResult<FileRecord3G> Get3GFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), tableName);
            return (ISingleResult<FileRecord3G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get3GFileContentsRasterConsidered")]
        public ISingleResult<FileRecord3G> Get3GFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), tableName, rasterNum);
            return (ISingleResult<FileRecord3G>)result?.ReturnValue;
        }
    }
}
