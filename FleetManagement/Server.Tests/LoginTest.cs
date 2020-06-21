using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server;
using Server.Framework;
using Server.Interfaces;
using Server.Models;

namespace Client.Tests
{
    [TestClass]
    public class LoginTest
    {
        private static IService service;
        private static readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private static readonly User _user = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = "James007",
            IsAdmin = false,
            Password = "$2a$11$.XPMa.x8aqtq21Kgk9TLQO8.6HXzT7Xp42ajiwQWGY.HXOUoTFmRy",
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _userRepositoryMock.Setup(x => x.GetUser(_user.Username))
                .Returns(_user);

            service = new Service(_userRepositoryMock.Object, null, null, null, null);

        }

        [TestMethod]
        public void TestCorrectLogin()
        {
            Assert.IsNotNull(service.Login("James007", "geheim"));
        }

        [TestMethod]
        public void TestIncorrectLogin()
        {
            Assert.IsNull(service.Login("James007", "falsch"));
        }

        [TestMethod]
        public void TestUserNull()
        {
            Assert.IsNull(service.Login(null, null));
        }


    }
}
