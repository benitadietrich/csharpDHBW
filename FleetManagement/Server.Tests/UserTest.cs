using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server.Interfaces;
using Server.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Server.Tests
{

    [TestClass]
    public class UserTest
    {
        private static IService service;
        private static readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private static readonly User _editUser = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = "James007",
            IsAdmin = false,
            Password = "$2a$11$.XPMa.x8aqtq21Kgk9TLQO8.6HXzT7Xp42ajiwQWGY.HXOUoTFmRy",
        };

        private static readonly User _newUser = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = "james",
            IsAdmin = false,
        };

        private static readonly User _invalidUser = new User
        {
            Id = 1,
            Firstname = "James",
            Lastname = "Bond",
            Username = null,
            IsAdmin = false,
        };

        private static readonly User _nonUser = new User
        {
            Id = 2,
            Firstname = "James",
            Lastname = "Bond",
            Username = "jamie",
            IsAdmin = false,
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _userRepositoryMock.Setup(x => x.GetUser(_editUser.Username)).Returns(_editUser);
            _userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Verifiable();
            _userRepositoryMock.Setup(x => x.Delete(It.IsAny<User>())).Verifiable();
            _userRepositoryMock.Setup(x => x.UpdateUser(_editUser)).Verifiable();

            service = new Service(_userRepositoryMock.Object, null, null, null, null);
        }

        [TestMethod]
        public void AddUser()
        {
            Assert.IsTrue(service.AddUser(_newUser));
        }

        [TestMethod]
        public void AddInvalidUser()
        {
            Assert.IsFalse(service.AddUser(_invalidUser));
        }

        [TestMethod]
        public void EditUser()
        {
            Assert.IsTrue(service.EditUser(_editUser));
        }

        [TestMethod]
        public void DeleteUser()
        {
            Assert.IsTrue(service.DeleteUser(_editUser));
        }

        [TestMethod]
        public void DeleteNonExUser()
        {
            Assert.IsFalse(service.DeleteUser(_nonUser));
        }
    }
}
