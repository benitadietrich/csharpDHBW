using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        private static IService service;
        private static readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        private static Employee _newEmp = new Employee()
        {
            Id = 1,
            FirstName = "Hans",
            LastName = "Mueller",
            EmployeeNumber = 1,
            BusinessUnitId = new BusinessUnit()
            {
                Name = "Personal"
            },
            Salutation = "Herr"
        };

        private static Employee _existingEmp = new Employee()
        {
            Id = 2,
            FirstName = "Jana",
            LastName = "Maier",
            EmployeeNumber = 2,
            BusinessUnitId = new BusinessUnit()
            {
                Name = "Personal"
            },
            Salutation = "Frau"
        };

        private static Employee _doubleEmp = new Employee()
        {
            Id = 3,
            FirstName = "Heinrich",
            LastName = "Schmid",
            EmployeeNumber = 2,
            BusinessUnitId = new BusinessUnit()
            {
                Name = "Personal"
            },
            Salutation = "Herr"
        };
        private static Employee _editedEmp = new Employee()
        {
            Id = 2,
            FirstName = "Jana",
            LastName = "Mueller",
            EmployeeNumber = 2,
            BusinessUnitId = new BusinessUnit()
            {
                Name = "Personal"
            },
            Salutation = "Frau"
        };


        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _employeeRepositoryMock.Setup(x => x.GetEmployeeByNumber(2)).Returns(_existingEmp);
            _employeeRepositoryMock.Setup(x => x.Save(It.IsAny<Employee>())).Verifiable();
            _employeeRepositoryMock.Setup(x => x.GetEmployee(2)).Returns(_existingEmp);

            service = new Service(null, _employeeRepositoryMock.Object, null, null, null);
        }

        [TestMethod]
        public void TestAddEmp()
        {
            Assert.IsTrue(service.AddEmployee(_newEmp));
        }

        [TestMethod]
        public void TestAddExistingEmp()
        {
            Assert.IsFalse(service.AddEmployee(_doubleEmp));
        }

        [TestMethod]
        public void TestEditEmp()
        {
            _existingEmp.FirstName = "Julia";
            Assert.IsTrue(service.EditEmployee(_editedEmp));
        }
    }
}
