using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using IdentityAppFirstAttempt.Data;
using IdentityAppFirstAttempt.Models;
using System.Security.Claims;

namespace IdentityAppFirstAttempt.Controllers {
  public class AccountController: Controller {
    private readonly ApplicationDbContext context;
    public AccountController(ApplicationDbContext context){
      this.context = context;
    }

    public IActionResult Signup(){
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signup(SignupUser _user){
      if(!ModelState.IsValid) return View();

      User user = new User {
        UserName = _user.UserName,
        Email = _user.Email,
        Role = _user.Role,
        Password = BCrypt.Net.BCrypt.HashPassword(_user.Password)
      };

      await this.context.Users.AddAsync(user);
      await this.context.SaveChangesAsync();

      return RedirectToAction(nameof(Signin));
    }

    public IActionResult Signin(){
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SigninUser _user){

      if(!ModelState.IsValid) return View();

      var user = this.context.Users.FirstOrDefault<User>(user => user.Email == _user.Email);

      if(user == null){
        ModelState.AddModelError(nameof(_user.Email), "Invalid Email");
        return View();
      }

      if(!BCrypt.Net.BCrypt.Verify(_user.Password, user.Password)){
        ModelState.AddModelError(nameof(_user.Password), "Invaid password");
        return View();
      }

      List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, (user.Role).ToString()),
        new Claim("Id", (user.Id).ToString())
      };

      ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "TestingCookie");
      ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
      await HttpContext.SignInAsync("TestingCookie", claimsPrincipal);


      return Redirect("/");
    }
  }
}