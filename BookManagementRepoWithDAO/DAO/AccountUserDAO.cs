using BookManagementRepoWithDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementRepoWithDAO.DAO
{
    public class AccountUserDAO
    {
        public AccountUser GetAccountUser(AccountUser accountUser)
        {
            AccountUser user = null;
            using (var _context = new BookPublisherContext())
            {
                user = _context.AccountUsers.SingleOrDefault(temp =>
            temp.UserFullName.ToLower().Trim().Equals(accountUser.UserFullName.ToLower().Trim()) &&
            temp.UserPassword.Trim().Equals(accountUser.UserPassword.Trim()));
            }
            return user;
        }
    }
}
