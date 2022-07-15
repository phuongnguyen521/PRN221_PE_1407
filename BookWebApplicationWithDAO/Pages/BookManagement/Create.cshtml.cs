using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using BookManagementRepo.Repository.Implements;
using BookManagementRepoWithDAO.Repository.Implements;

namespace BookWebApplicationWithDAO.Pages.BookManagement
{
    public class CreateModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        private readonly IPublisherRepository publisherRepository;

        public CreateModel()
        {
            bookRepository = new BookRepository();
            publisherRepository = new PublisherRepository();
        }

        public IActionResult OnGet()
        {
            var publisher = publisherRepository.GetPublishers();
            ViewData["PublisherId"] = publisher.Select(p => new SelectListItem()
            {
                Text = p.PublisherName,
                Value = p.PublisherId
            });
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var publisher = publisherRepository.GetPublishers();
            ViewData["PublisherId"] = publisher.Select(p => new SelectListItem()
            {
                Text = p.PublisherName,
                Value = p.PublisherId,
                Selected = p.PublisherId.ToLower().Trim().Equals(Book.PublisherId.ToLower().Trim())
            });
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var book = bookRepository.CheckExistingBook(Book.BookId);

            if (book != null)
            {
                ViewData["Message"] = "Duplicated Book Id";
                return Page();
            }
            else
            {
                bookRepository.AddBook(Book);
                return RedirectToPage("./Index");
            }
        }
    }
}
