using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Winter.Auth.Service.Models;

namespace Winter.Auth.Service.Test.Unit
{

    [TestFixture]
    public class LoginAttemptValidatorTests
    {
        private IDtoValidator<LoginAttempt> _loginAttemptValidator;

        [TestFixtureSetUp]
        public void Setup()
        {
            _loginAttemptValidator = new LoginAttemptValidator();
        }
            
        [Test]
        public void Can_Validate_When_Username_And_Password_Are_Populated()
        {
            var loginAttempt = new LoginAttempt("TEST", "TEST");

            Assert.That(_loginAttemptValidator.IsValid(loginAttempt));
        }

        [Test]
        public void Can_Invalidate_For_Missing_Username()
        {
            var loginAttempt = new LoginAttempt(null, "TEST");

            Assert.That(!_loginAttemptValidator.IsValid(loginAttempt));
        }

        [Test]
        public void Can_Invalidate_For_Zero_Character_Username()
        {
            var loginAttempt = new LoginAttempt(string.Empty, "TEST");

            Assert.That(!_loginAttemptValidator.IsValid(loginAttempt));
        }

        [Test]
        public void Can_Invalidate_For_Missing_Password()
        {
            var loginAttempt = new LoginAttempt("TEST", null);

            Assert.That(!_loginAttemptValidator.IsValid(loginAttempt));
        }

        [Test]
        public void Can_Invalidate_For_Zero_Character_Password()
        {
            var loginAttempt = new LoginAttempt("TEST", string.Empty);

            Assert.That(!_loginAttemptValidator.IsValid(loginAttempt));
        }
    }
}
