using BookManagementRepoWithDAO.Models;

namespace BookManagementRepoWithDAO.Repository.Interfaces
{
    public interface IAccountUserRepository
    {
        public AccountUser GetAccountUser(AccountUser accountUser);
    }
}
