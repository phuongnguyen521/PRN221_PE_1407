using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_PE_1407.Models;

namespace BookApplication.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly BookDbContext _context;

        public LoginModel(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountUser AccountUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Page();
            }

            AccountUser user = _context.AccountUser.SingleOrDefault(user =>
            user.UserFullName.Equals(AccountUser.UserFullName) &&
            user.UserPassword.Equals(AccountUser.UserPassword));
            if (user != null)
            {
                if (user.UserRole == 1)
                {
                    var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, AccountUser.UserFullName),
                    new Claim(ClaimTypes.Role, user.UserRole.ToString())
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var authenProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity),
                        authenProperties);
                    return LocalRedirect("/BookManagement/Index");
                }
                ViewData["Message"] = "You are not allowed to access this function";
                return Page();
            }
            ViewData["Message"] = "Your account does not exist";
            return Page();
        }
    }
}
