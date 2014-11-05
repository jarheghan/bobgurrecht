using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.Models
{
    public class Users
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public String ConfirmPassword { get; set; }
        public bool Ok { get; set; }
    }
}