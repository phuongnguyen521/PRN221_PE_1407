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
    public class AccountUserRepository : IAccountUserRepository
    {
        AccountUserDAO AccountUserDAO = new AccountUserDAO();
        public AccountUser GetAccountUser(AccountUser accountUser) => AccountUserDAO.GetAccountUser(accountUser);
    }
}
