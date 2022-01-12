﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.RequestObjects
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }
      
        [Required]
        public string Password { get; set; }
    }
}