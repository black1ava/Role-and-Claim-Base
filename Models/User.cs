using IdentityAppFirstAttempt.Enum;

namespace IdentityAppFirstAttempt.Models {
  public class User {
    public int Id { get; set; }
    public String UserName { get; set; } = String.Empty;
    public String Email { get; set; } = String.Empty;
    public Roles Role { get; set; }
    public String Password { get; set; } = String.Empty;
  }
}