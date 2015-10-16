using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Winter.Auth.Model;
using Winter.Auth.Repository;
using Winter.Auth.Service.Controllers;
using Winter.Auth.Service.Models;
using Winter.Core.Auth;
using Winter.Core.DependencyInversion;

namespace Winter.Auth.Service.Test.Unit
{
    [TestFixture]
    public class LoginControllerTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IJwtProvider> _jwtProvider;
        private Mock<IDateProvider> _dateProvider;
        private Mock<IDtoValidator<LoginAttempt>> _loginAttemptValidator;
        private ILoginController _loginController;

        [TestFixtureSetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new User("TEST", "admin")));

            _jwtProvider = new Mock<IJwtProvider>();
            _jwtProvider.Setup(m => m.CreateJwt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("TEST");

            _dateProvider = new Mock<IDateProvider>();
            _dateProvider.Setup(m => m.Now()).Returns(new DateTime(2015, 1, 1, 1, 1, 1));

            _loginAttemptValidator = new Mock<IDtoValidator<LoginAttempt>>();
        }

        [Test]
        public async void Can_Return_Token_For_Valid_Credentials()
        {
            _loginAttemptValidator.Setup(m => m.IsValid(It.IsAny<LoginAttempt>())).Returns(true);

            _loginController = new LoginController(_userRepository.Object, _jwtProvider.Object, _dateProvider.Object, _loginAttemptValidator.Object);

            IHttpActionResult response = await _loginController.Login(new LoginAttempt("TEST", "TEST"));

            Assert.IsInstanceOf<OkNegotiatedContentResult<string>>(response);
            Assert.AreEqual("TEST", ((OkNegotiatedContentResult<string>)response).Content);
            _userRepository.Verify(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _jwtProvider.Verify(m => m.CreateJwt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
        }

        [Test]
        public async void Can_Return_BadRequest_For_Missing_Parameters()
        {
            _loginAttemptValidator.Setup(m => m.IsValid(It.IsAny<LoginAttempt>())).Returns(false);

            _loginController = new LoginController(_userRepository.Object, _jwtProvider.Object, _dateProvider.Object, _loginAttemptValidator.Object);

            IHttpActionResult response = await _loginController.Login(new LoginAttempt(null, "TEST"));

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
            _userRepository.Verify(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _jwtProvider.Verify(m => m.CreateJwt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never());
        }

        [Test]
        public async void Can_Return_BadRequest_For_Null_Parameter()
        {
            _loginAttemptValidator.Setup(m => m.IsValid(It.IsAny<LoginAttempt>())).Returns(false);

            _loginController = new LoginController(_userRepository.Object, _jwtProvider.Object, _dateProvider.Object, _loginAttemptValidator.Object);

            IHttpActionResult response = await _loginController.Login(null);

            Assert.IsInstanceOf<BadRequestErrorMessageResult>(response);
            _userRepository.Verify(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _jwtProvider.Verify(m => m.CreateJwt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never());
        }
    }
}
