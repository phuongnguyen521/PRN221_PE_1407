using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementRepoWithDAO.Models;

namespace BookManagementRepoWithDAO.Repository.Interfaces
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();
        public Book GetBook(string id);
        public void AddBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(string id);
        public Book CheckExistingBook(string id);
    }
}
