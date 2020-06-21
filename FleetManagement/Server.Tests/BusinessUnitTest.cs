using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass]
    public class BusinessUnitTest
    {
        private static IService service;
        private static readonly Mock<IBusinessUnitRepository> _unitRepositoryMock = new Mock<IBusinessUnitRepository>();
        private static readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        private static BusinessUnit _unitNew = new BusinessUnit()
        {
            Id = 1,
            Name = "Human Ressources",
            Description = "Personalabteilung",
            Version = 1
        };

        private static BusinessUnit _unit2 = new BusinessUnit()
        {
            Id = 3,
            Name = "Produktion",
            Description = "Produktion von Chips",
            Version = 1,
        };

        private static BusinessUnit _unit2Edited = new BusinessUnit()
        {
            Id = 3,
            Name = "Produktionsabteilung",
            Description = "Produktion von Chips",
            Version = 1,
        };

        private static BusinessUnit _unitExisting = new BusinessUnit()
        {
            Id = 2,
            Name = "produktion",
            Description = "Hier findet unsere Produktion von Chips statt",
            Version = 1
        };

        private static BusinessUnit _referredU = new BusinessUnit()
        {
            Id = 4,
            Name = "Test",
            Description = "Testabteilung",
            Version = 1
        };

        [ClassInitialize]
        public static void Setup(TestContext context )
        {
            _unitRepositoryMock.Setup(x => x.GetBusinessUnitName("Produktion".ToLower())).Returns(_unit2);
            _unitRepositoryMock.Setup(x => x.GetBusinessUnit(3)).Returns(_unit2);
            _unitRepositoryMock.Setup(x => x.Save(It.IsAny<BusinessUnit>())).Verifiable();
            _unitRepositoryMock.Setup(x => x.GetBusinessUnit(4)).Returns(_referredU);
            _unitRepositoryMock.Setup(x => x.GetAll()).Returns(new List<BusinessUnit> { _unit2 });
            
            _employeeRepositoryMock.Setup(x => x.IsEmployeeReferred(_referredU)).Returns(true);

            service = new Service(null, _employeeRepositoryMock.Object, _unitRepositoryMock.Object, null, null);
        }

        [TestMethod]
        public void TestAddBU()
        {
            Assert.IsTrue(service.AddBusinessUnit(_unitNew));
        }

        [TestMethod]
        public void AddExistingBu()
        {
            Assert.IsFalse(service.AddBusinessUnit(_unitExisting));
        }

        [TestMethod]
        public void EditBU()
        {
            Assert.IsTrue(service.EditBusinessUnit(_unit2Edited));
        }

        [TestMethod]
        public void TestCanDeleteUnitWIthData()
        {
            Assert.IsTrue(service.CannotRemoveBusinessUnit(_referredU));
        }


    }
}
