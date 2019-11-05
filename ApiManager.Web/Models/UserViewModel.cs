using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ApiManager.Web.Models
{
    public class UserViewModel
    {
       
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Apikey { get; set; }
        [Required]
        public string Role { get; set; }
        [Display(Name = "Debtors")]
        public IEnumerable<Debtor> Debtors { get; set; }
        public IEnumerable<Url> Urls { get; set; }
        [Required]
        public string AllowedIP { get; set; }
        
        public bool Enabled { get; set; }
        
        public int State { get; set; }
        
        public string Description { get; set; }
        
        public ServiceApiModel Service { get; set; }
        
        public PasswordHashingApiModel PasswordHashing { get; set; }
        
        public DateTime SysCreated { get; set; }
        
        public int SysCreator { get; set; }
        
        public DateTime SysModified { get; set; }
        
        public int SysModifier { get; set; }
    }
}