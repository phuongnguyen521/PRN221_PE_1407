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
using BookWebApplicationWithDAO.Pages.Model;

namespace BookWebApplicationWithDAO.Pages.BookManagement
{
    public class IndexModel : PageModel
    {
        private readonly IBookRepository bookRepository;

        public IndexModel()
        {
            bookRepository = new BookRepository();
        }
        public string BookNameSort { get; set; }
        public string BookQuantitySort { get; set; }
        public string CurrentSort { get; set; }
        public string SearchBookName { get; set; }
        public PaginatedList<Book> Book { get; set; }

        public async Task OnGetAsync(string? sortOrder, string? searchName, int? pageIndex)
        {
            CurrentSort = sortOrder;
            BookNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            BookQuantitySort = sortOrder == "Quantity" ? "Quantity" : "quantity_desc";
            int pageSize = 5;
            var books = bookRepository.GetBooks();
            if (searchName != null)
            {
                books = books.Where(b =>
                b.BookName.ToLower().Trim().Contains(searchName.ToLower().Trim())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.BookName).ToList();
                    break;
                case "Quantity":
                    books = books.OrderBy(b => b.Quantity).ToList();
                    break;
                case "quantity_desc":
                    books = books.OrderByDescending(b => b.Quantity).ToList();
                    break;
                default:
                    books = books.OrderBy(b => b.BookName).ToList();
                    break;
            }
            Book = await PaginatedList<Book>.CreateAsync(books.AsQueryable(), pageIndex ?? 1, pageSize);
        }
    }
}
