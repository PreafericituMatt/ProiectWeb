

using ProiectWebData.Entities;

namespace ProiectWebData.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<ServiceResponse<User>> Register(User user);

        Task<ServiceResponse<string>> Login(User user);

        Task<ServiceResponse<User>> GetUserByEmail(string email);

        Task<bool> UserExists(string username);   
    }
}
