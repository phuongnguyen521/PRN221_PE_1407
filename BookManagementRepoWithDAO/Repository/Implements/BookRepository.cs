using BookManagementRepoWithDAO.DAO;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using System.Collections.Generic;

namespace BookManagementRepo.Repository.Implements
{
    public class BookRepository : IBookRepository
    {
        BookDAO bookDao = new BookDAO();
        public void AddBook(Book book) => bookDao.AddBook(book);

        public Book CheckExistingBook(string id) => bookDao.CheckExistingBook(id);

        public void DeleteBook(string id) => bookDao.DeleteBook(id);

        public Book GetBook(string id) => bookDao.GetBook(id);

        public List<Book> GetBooks() => bookDao.GetBooks();

        public void UpdateBook(Book book) => bookDao.UpdateBook(book);
    }
}
