using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using BookManagementRepo.Repository.Implements;
using BookManagementRepoWithDAO.Repository.Implements;

namespace BookWebApplicationWithDAO.Pages.BookManagement
{
    public class EditModel : PageModel
    {
        private readonly IBookRepository bookRepository;
        private readonly IPublisherRepository publisherRepository;

        public EditModel()
        {
            bookRepository = new BookRepository();
            publisherRepository = new PublisherRepository();
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
            var publisher = publisherRepository.GetPublishers();
            ViewData["PublisherId"] = publisher.Select(p => new SelectListItem()
            {
                Text = p.PublisherName,
                Value = p.PublisherId,
                Selected = p.PublisherId.ToLower().Trim().Equals(Book.PublisherId.ToLower().Trim())
            });
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            try
            {
                bookRepository.UpdateBook(Book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (bookRepository.CheckExistingBook(Book.BookId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
