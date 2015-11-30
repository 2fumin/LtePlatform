﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LtePlatform.Models
{
    public static class HttpFileUploadService
    {
        public static string UploadKpiFile(this HttpPostedFileBase httpPostedFileBase)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads\\Kpi",
                    httpPostedFileBase.FileName);
            httpPostedFileBase.SaveAs(path);
            return path;
        }

        public static string UploadParametersFile(this HttpPostedFileBase httpPostedFileBase)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads\\Parameters",
                    httpPostedFileBase.FileName);
            httpPostedFileBase.SaveAs(path);
            return path;
        }
    }
}