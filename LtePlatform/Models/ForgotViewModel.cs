using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "�û���")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "�����ʼ�")]
        public string Email { get; set; }
    }
}