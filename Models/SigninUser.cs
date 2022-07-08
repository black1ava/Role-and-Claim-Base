using System.ComponentModel.DataAnnotations;

namespace IdentityAppFirstAttempt.Models {
  public class SigninUser {
    [EmailAddress]
    [Required(ErrorMessage = "Please insert you email")]
    public String Email { get; set; } = String.Empty;
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please insert your password")]
    public String Password { get; set; } = String.Empty;
  }
}