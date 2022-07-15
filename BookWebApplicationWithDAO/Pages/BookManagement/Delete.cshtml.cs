using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using BookManagementRepo.Repository.Implements;

namespace BookWebApplicationWithDAO.Pages.BookManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        public DeleteModel()
        {
            bookRepository = new BookRepository();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = bookRepository.GetBook(id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = bookRepository.GetBook(id);

            if (Book != null)
            {
                bookRepository.DeleteBook(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
