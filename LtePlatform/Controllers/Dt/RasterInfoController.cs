using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.ViewModels;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("网格测试信息查询控制器")]
    public class RasterInfoController : ApiController
    {
        private readonly RasterInfoService _service;

        public RasterInfoController(RasterInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有网格的测试信息")]
        [ApiResponse("所有网格的测试信息视图，包括网格坐标、所属镇区和包含的测试数据表列表")]
        public IEnumerable<RasterInfoView> Get()
        {
            return _service.QueryAllList();
        }

        [HttpGet]
        [ApiDoc("查询包含指定数据类型的所有网格的测试信息")]
        [ApiParameterDoc("dataType", "指定数据类型（2G、3G、4G）")]
        [ApiResponse("包含指定数据类型的所有网格的测试信息视图，包括网格坐标、所属镇区和包含的测试数据表列表")]
        public IEnumerable<RasterInfoView> Get(string dataType)
        {
            return _service.QueryWithDataType(dataType);
        } 
    }

    [ApiControl("网格测试文件信息查询控制器")]
    public class RasterFileController : ApiController
    {
        private readonly RasterInfoService _service;

        public RasterFileController(RasterInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询包含指定数据类型的所有网格的测试文件信息")]
        [ApiParameterDoc("dataType", "指定数据类型（2G、3G、4G）")]
        [ApiResponse("包含指定数据类型的所有网格的测试文件信息视图，包括测试文件编号和包含的网格编号列表")]
        public IEnumerable<FileRasterInfoView> Get(string dataType)
        {
            return _service.QueryFileNames(dataType);
        }

        [HttpGet]
        [ApiDoc("查询包含指定数据类型和坐标范围的所有网格的测试文件信息")]
        [ApiParameterDoc("dataType", "指定数据类型（2G、3G、4G）")]
        [ApiParameterDoc("west", "坐标西界")]
        [ApiParameterDoc("east", "坐标东界")]
        [ApiParameterDoc("south", "坐标南界")]
        [ApiParameterDoc("north", "坐标北界")]
        [ApiResponse("包含指定数据类型的所有网格的测试文件信息视图，包括测试文件编号和包含的网格编号列表")]
        public IEnumerable<FileRasterInfoView> Get(string dataType, double west, double east, double south,
            double north)
        {
            return _service.QueryFileNames(dataType, west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("查询包含指定数据类型和坐标范围的所有网格的测试文件信息")]
        [ApiParameterDoc("dataType", "指定数据类型（2G、3G、4G）")]
        [ApiParameterDoc("west", "坐标西界")]
        [ApiParameterDoc("east", "坐标东界")]
        [ApiParameterDoc("south", "坐标南界")]
        [ApiParameterDoc("north", "坐标北界")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("包含指定数据类型的所有网格的测试文件信息视图，包括测试文件编号和包含的网格编号列表")]
        public IEnumerable<FileRasterInfoView> Get(string dataType, double west, double east, double south,
            double north, DateTime begin, DateTime end)
        {
            return _service.QueryFileNames(dataType, west, east, south, north, begin, end);
        }
    }
}
