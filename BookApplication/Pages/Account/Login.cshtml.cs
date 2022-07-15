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
            var user = _context.AccountUser.SingleOrDefault(temp =>
            temp.UserFullName.ToLower().Trim().Equals(AccountUser.UserFullName.ToLower().Trim()) &&
            temp.UserPassword.Trim().Equals(AccountUser.UserPassword.Trim()));
            if (user != null)
            {
                if (user.UserRole == 1)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserFullName),
                        new Claim(ClaimTypes.Role, user.UserRole.ToString())
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var authenProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
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
