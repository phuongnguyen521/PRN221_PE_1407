using BookManagementRepoWithDAO.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementRepoWithDAO.DAO
{
    public class BookDAO
    {
        public List<Book> GetBooks()
        {
            List<Book> books = null;
            using (var _context = new BookPublisherContext())
            {
                books = _context.Books
                .Include(b => b.Publisher).ToList();
            }
            return books;
        }

        public Book GetBook(string id)
        {
            Book book = null;
            using (var _context = new BookPublisherContext())
            {
                book = _context.Books
                .Include(b => b.Publisher).FirstOrDefault(m => m.BookId == id);
            }
            return book;
        }

        public void AddBook(Book book)
        {
            using (var _context = new BookPublisherContext())
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
        }

        public void UpdateBook(Book book)
        {
            using (var _context = new BookPublisherContext())
            {
                _context.Attach(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteBook(string id)
        {
            using (var _context = new BookPublisherContext())
            {
                var book = _context.Books.Find(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }
            }
        }

        public Book CheckExistingBook(string id)
        {
            Book book = null;
            using (var _context = new BookPublisherContext())
            {
                book = _context.Books.SingleOrDefault(temp =>
            temp.BookId.Trim().ToLower().Equals(id.ToLower().Trim()));
            }
            return book;
        }
    }
}
