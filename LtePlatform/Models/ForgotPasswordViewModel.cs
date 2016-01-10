using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}