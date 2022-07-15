using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using BookManagementRepo.Repository.Implements;

namespace BookWebApplicationWithDAO.Pages.BookManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository bookRepository;

        public DetailsModel()
        {
            bookRepository = new BookRepository();
        }

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
    }
}
