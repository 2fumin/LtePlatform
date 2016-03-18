﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtePlatform.Models
{
    public class ResetPasswordViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
