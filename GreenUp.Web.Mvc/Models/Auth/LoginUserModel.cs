﻿using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Auth
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
