using System.ComponentModel.DataAnnotations;
using IdentityAppFirstAttempt.Enum;

namespace IdentityAppFirstAttempt.Models {
  public class SignupUser {
    [Required(ErrorMessage = "Please enter your username")]
    [Display(Name = "Username")]
    public String UserName { get; set; } = String.Empty;
    [EmailAddress]
    [Required(ErrorMessage = "Please enter your email address")]
    public String Email { get; set; } = String.Empty;
    [EnumDataType(typeof(Roles), ErrorMessage = "Please choose a role")]
    public Roles Role { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password")]
    [RegularExpression(
      @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[`~!@#$%^&*()\-_+=?/>.<,|\\]).{8,}$",
      ErrorMessage = "Password needs to be 8 characters long, contains at lease a capital letter, a small letter and a symbol"
      )
    ]
    public String Password { get; set; } = String.Empty;
    [Required(ErrorMessage = "Please confirm password")]
    [Compare("Password", ErrorMessage = "Password doesn't match")]
    public String PasswordConfirmation { get; set; } = String.Empty;
  }
}