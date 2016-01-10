using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}