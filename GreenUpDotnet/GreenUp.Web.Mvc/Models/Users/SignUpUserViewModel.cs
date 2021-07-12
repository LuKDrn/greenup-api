﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GreenUp.Web.Mvc.Models.Users
{
    public class SignUpUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}