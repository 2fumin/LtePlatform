using System;
using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    // AccountController 操作返回的模型。
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "家乡")]
        public string Hometown { get; set; }
    }
}
