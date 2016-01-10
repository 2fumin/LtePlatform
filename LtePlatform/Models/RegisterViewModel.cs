using System.ComponentModel.DataAnnotations;

namespace LtePlatform.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "�û���")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "�����ʼ�")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} �������ٰ��� {2} ���ַ���", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "����")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ȷ������")]
        [Compare("Password", ErrorMessage = "�����ȷ�����벻ƥ�䡣")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "����")]
        public string Hometown { get; set; }
    }
}