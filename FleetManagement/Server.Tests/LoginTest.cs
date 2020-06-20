using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server;
using Server.Framework;
using Server.Models;

namespace Client.Tests
{
    [TestClass]
    public class LoginTest
    {
        private User userNull;
        private User userValid;
        private User userInvalid;

        private static IService service;
        private static readonly Mock<UserRepository> _userRepositoryMock = new Mock<UserRepository>();
        private static readonly User _user = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = "James007",
            IsAdmin = false,
            Password = "$2y$10$1coNSX5TTw3cUDZdXw1AhOoRw6ewMTcVAU.1YVx.gEzQLpQNnvXjq",
        };

        [ClassInitialize]
        private static void Setup(TestContext context)
        {
            _userRepositoryMock.Setup(x => x.GetUser(_user.Username))
                .Returns(_user);

            service = new Service(_userRepositoryMock.Object, null, null, null, null);

        }

        [TestMethod]
        private static void TestCorrectLogin()
        {
            var result = service.Login(_user.Username, _user.Password);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
