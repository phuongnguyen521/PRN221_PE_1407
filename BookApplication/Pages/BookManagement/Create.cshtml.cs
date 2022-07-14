using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_PE_1407.Models;

namespace BookApplication.Pages.BookManagement
{
    public class CreateModel : PageModel
    {
        private readonly BookDbContext _context;

        public CreateModel(BookDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var list = _context.Publisher.ToList();
            ViewData["PublisherId"] = list.Select(item => new SelectListItem
            {
                Text = item.PublisherName,
                Value = item.PublisherId
            });
                //new SelectList(_context.Set<Publisher>(), "PublisherId", "PublisherId");
        }

        [BindProperty]
        public Book Book { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var list = _context.Publisher.ToList();
            ViewData["PublisherId"] = list.Select(item => new SelectListItem
            {
                Text = item.PublisherName,
                Value = item.PublisherId,
                Selected = item.PublisherId.ToLower().Trim().Equals(Book.PublisherId.ToLower().Trim())
            });
            string error = checkDuplicatedBookId(Book.BookId);
            if (error != null)
            {
                ViewData["Message"] = error;
                return Page();
            }
            else
            {
                _context.Book.Add(Book);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
        }

        private String? checkDuplicatedBookId(string bookId)
        {
            var book = _context.Book.SingleOrDefault(b => b.BookId.ToLower().Trim().Equals(bookId.ToLower().Trim()));
            return book == null ? null : "Duplicated Book Id";
        }
    }
}
