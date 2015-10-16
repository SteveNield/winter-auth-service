using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winter.Auth.Model;

namespace Winter.Auth.Repository
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string username, string password);
    }
}
