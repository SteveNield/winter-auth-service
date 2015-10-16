using System;
using Moq;
using NUnit.Framework;
using Winter.Auth.Repository;

namespace Winter.Auth.Service.Test.Unit
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public async void Can_Return_User_With_Username_Provided()
        {
            IUserRepository userRepository = new UserRepository();

            var user = await userRepository.GetAsync("TEST", It.IsAny<string>());

            Assert.AreEqual(user.Username, "TEST");
        }
    }
}
