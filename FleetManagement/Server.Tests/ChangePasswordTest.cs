using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass]
    public class ChangePasswordTest
    {
        private static IService service;
        private static readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private static User _user = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = "James007",
            IsAdmin = false,
            Password = BCrypt.Net.BCrypt.HashPassword("geheim"),
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _userRepositoryMock.Setup(x => x.GetUser(_user.Username))
                .Returns(_user);
            _userRepositoryMock.Setup(x => x.SetPassword(It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            service = new Service(_userRepositoryMock.Object, null, null, null, null);

        }

        [TestMethod]
        public void ChangePasswordSuccess()
        {
            Assert.AreEqual("Passwort wurde erfolgreich geändert", service.ChangePassword(_user, "geheim", "neu", "neu"));
        }

        [TestMethod]
        public void ChangePasswordNewPasswordNotEqual()
        {
            Assert.AreEqual("Die neuen Passwörter stimmen nicht überein", service.ChangePassword(_user, "geheim", "neu", "falsch"));
        }

        [TestMethod]
        public void ChangePasswordOldPasswordWrong()
        {
            Assert.AreEqual("Das alte Passwort ist falsch", service.ChangePassword(_user, "falsch", "neu", "neu"));
        }
    }
}
