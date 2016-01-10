using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "�û���")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "�����ʼ�")]
        public string Email { get; set; }
    }
}