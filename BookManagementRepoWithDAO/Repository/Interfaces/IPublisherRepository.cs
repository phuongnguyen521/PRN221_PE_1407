using BookManagementRepoWithDAO.Models;
using System.Collections.Generic;

namespace BookManagementRepoWithDAO.Repository.Interfaces
{
    public interface IPublisherRepository
    {
        public List<Publisher> GetPublishers();
    }
}
