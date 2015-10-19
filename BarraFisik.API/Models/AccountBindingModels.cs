using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel;

namespace BarraFisik.API.Models
{
    public class CreateUserBindingModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Senha e Confirmar senha não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordBindingModel
    {
       
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter no mínimo {2} caracteres.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirma nova senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação da nova senha não conferem.")]
        public string ConfirmPassword { get; set; }
    
    }

    public class ClaimBindingModel
    {
        [Required]
        [Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Claim Value")]
        public string Value { get; set; }
    }

    public class EditUserBindingModel
    {
        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}