using System.Threading.Tasks;
using System.Web.Http;
using Winter.Auth.Service.Models;

namespace Winter.Auth.Service.Controllers
{
    public interface ILoginController
    {
        Task<IHttpActionResult> Login(LoginAttempt loginAttempt);
    }
}