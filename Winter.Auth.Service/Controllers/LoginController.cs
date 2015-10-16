using System.Threading.Tasks;
using System.Web.Http;
using Winter.Auth.Repository;
using Winter.Auth.Service.Models;
using Winter.Core.Auth;
using Winter.Core.DependencyInversion;

namespace Winter.Auth.Service.Controllers
{
    public class LoginController : ApiController, ILoginController
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IDtoValidator<LoginAttempt> _loginAttemptValidator; 
        private readonly IDateProvider _dateProvider;

        public LoginController(IUserRepository userRepository, IJwtProvider jwtProvider, IDateProvider dateProvider, 
            IDtoValidator<LoginAttempt> loginAttemptValidator)
        {
            _jwtProvider = jwtProvider;
            _dateProvider = dateProvider;
            _loginAttemptValidator = loginAttemptValidator;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginAttempt loginAttempt)
        {
            if (loginAttempt == null || !_loginAttemptValidator.IsValid(loginAttempt))
            {
                return BadRequest("Details missing.");
            }

            var user = await _userRepository.GetAsync(loginAttempt.Username, loginAttempt.Password);

            var token = _jwtProvider.CreateJwt(user.Username, user.Role, _dateProvider.Now());

            return Ok(token);
        }
    }
}