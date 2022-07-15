using BookManagementRepoWithDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementRepoWithDAO.DAO
{
    public class PublisherDAO
    {
        public List<Publisher> GetPublishers()
        {
            List<Publisher> publishers = null;
            using (var _context = new BookPublisherContext())
            {
                publishers = _context.Publishers.ToList();
            }
            return publishers;
        }
    }
}
