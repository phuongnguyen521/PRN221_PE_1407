using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using BookManagementRepoWithDAO.Repository.Implements;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BookWebApplicationWithDAO.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountUserRepository accountUserRepository;

        public LoginModel()
        {
            accountUserRepository = new AccountUserRepository();
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
                return Page();
            }

            var user = accountUserRepository.GetAccountUser(AccountUser);

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
                ViewData["Message"] = "You are not allowed to access to this function";
                return Page();
            }
            ViewData["Message"] = "Your account does not exist";
            return Page();
        }
    }
}
