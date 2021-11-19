using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_AuthTest.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Credential.UserName == "u" && Credential.Password == "1")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "u"),
                    new Claim(ClaimTypes.Email, "u@u.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "true"),
                    new Claim("Manager", "true"),
                    new Claim("Date", "2021-11-01")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe // 이 필드 자체가 bool // browser 닫아도 유지되는 쿠키 설정
                };

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties); // token create, put token in cookie

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
