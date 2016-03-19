using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace LtePlatform.Models
{
    [TypeDoc("应用程序注册用户信息视图")]
    public class ApplicationUserViewModel
    {
        [MemberDoc("用户名")]
        public string UserName { get; set; }

        [MemberDoc("电话号码")]
        public string PhoneNumber { get; set; }

        [MemberDoc("家乡")]
        public string Hometown { get; set; }

        [MemberDoc("电子邮箱")]
        public string Email { get; set; }

        [MemberDoc("电子邮箱是否已确认")]
        public bool EmailHasBeenConfirmed { get; set; }
    }
}
