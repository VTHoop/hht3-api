using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> Create(string firstName, string lastName, string email, string password, string phone, string location);
        Task<User> Login(string email, string password);
        Task<User> Find(string email);
        Task<User> FindById(string id);
    }

}
