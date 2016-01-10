using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "����")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "��ס�������?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}