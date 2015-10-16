using System;
using System.Threading.Tasks;
using Winter.Auth.Model;

namespace Winter.Auth.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetAsync(string username, string password)
        {
            return await Task.Run(() => new User(username, "admin"));
        }
    }
}