using BookManagementRepoWithDAO.DAO;
using BookManagementRepoWithDAO.Models;
using BookManagementRepoWithDAO.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementRepoWithDAO.Repository.Implements
{
    public class PublisherRepository : IPublisherRepository
    {
        PublisherDAO publisherDAO = new PublisherDAO();
        public List<Publisher> GetPublishers() => publisherDAO.GetPublishers();
    }
}
