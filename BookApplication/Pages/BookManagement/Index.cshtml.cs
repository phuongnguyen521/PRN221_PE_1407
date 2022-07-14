using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_PE_1407.Models;

namespace BookApplication.Pages.BookManagement
{
    public class IndexModel : PageModel
    {
        private readonly BookDbContext _context;

        public IndexModel(BookDbContext context)
        {
            _context = context;
        }
        public string BookNameSort { get; set; }
        public string BookQuantitySort { get; set; }
        public string CurrentSort { get; set; }
        public string SearchingBookName { get; set; }
        public string SelectedBookQuantityFromList { get; set; }
        public PaginatedList<Book> Book { get; set; }
        public async Task OnGetAsync(string? sortOrder, string? selectedBookQuantity, string? searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            BookNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            BookQuantitySort = sortOrder == "Quantity" ? "Quantity" : "quantity_desc";
            IList<Book> book = await _context.Book.Include(b => b.Publisher).ToListAsync();
            if (searchString != null || selectedBookQuantity != null)
                book = GetListBookFromSearch(selectedBookQuantity, searchString, book);
            switch (sortOrder)
            {
                case "name_desc":
                    book = book.OrderByDescending(b => b.BookName).ToList();
                    break;
                case "Quantity":
                    book = book.OrderBy(b => b.Quantity).ToList();
                    break;
                case "quantity_desc":
                    book = book.OrderByDescending(b => b.Quantity).ToList();
                    break;
                default:
                    book = book.OrderBy(b => b.BookName).ToList();
                    break;

            }
            int pageSize = 5;
            var list = GetQuantityList();
            ViewData["searchingBookQuantity"] = list.Select(item => new SelectListItem
            {
                Text = item.ToString(),
                Value = item.ToString(),
                Selected = (SelectedBookQuantityFromList == item.ToString())
            });
            Book = await PaginatedList<Book>.CreateAsync(book.AsQueryable(), pageIndex ?? 1, pageSize);
        }

        private List<String> GetQuantityList()
        {
            List<String> list = new List<string>();
            int counter = 5;
            for(int number = 0; number < 5; number++)
            {
                string result = number == 4 ? $"> {counter}": $"< {counter}";
                counter += (number == 4 ? 0 : 5);
                list.Add(result);
            }
            return list;
        }

        private List<Book> GetListBookFromSearch(string? selectedBookQuantity, string? searchString, IList<Book> book)
        {
            if (selectedBookQuantity != null && searchString != null)
            {
                SearchingBookName = searchString.ToLower().Trim();
                SelectedBookQuantityFromList = selectedBookQuantity;
                int number = ExtractQuantityChoice(selectedBookQuantity);
                if (number == 21)
                    book = book.Where(b => b.BookName.ToLower().Contains(SearchingBookName) 
                    && b.Quantity > (number - 1)).ToList();
                else
                    book = book.Where(b => b.BookName.ToLower().Contains(SearchingBookName)
                    && b.Quantity < number).ToList();
            } else if (selectedBookQuantity != null)
            {
                SelectedBookQuantityFromList = selectedBookQuantity;
                int number = ExtractQuantityChoice(selectedBookQuantity);
                if (number == 21)
                    book = book.Where(b => b.Quantity > (number - 1)).ToList();
                else
                    book = book.Where(b => b.Quantity < number).ToList();
            } else
            {
                SearchingBookName = searchString.ToLower().Trim();
                book = book.Where(b => b.BookName.ToLower().Contains(SearchingBookName)).ToList();
            } 
            return book.ToList();
        }

        private int ExtractQuantityChoice(string selectedBookQuantity)
        {
            string[] array = selectedBookQuantity.Split(" ");
            if (array[0].Equals(">"))
                return 21;
            return Int32.Parse(array[1]);
        }
    }
}
