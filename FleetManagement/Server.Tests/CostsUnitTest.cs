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
    public class CostsUnitTest
    {
        private static readonly Mock<IBusinessUnitRepository> _businessUnitRepositoryMock = new Mock<IBusinessUnitRepository>();
        private static readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        private static readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private static readonly Mock<IVehicleToEmployeeRelation> _relationRepositoryMock = new Mock<IVehicleToEmployeeRelation>();

        private static IService _service;

        private static readonly BusinessUnit _un1 = new BusinessUnit
        {
            Id = 1,
            Name = "Personalabteilung",
        };
        private static readonly BusinessUnit _un2 = new BusinessUnit
        {
            Id = 2,
            Name = "IT",
        };
        private static readonly Employee _emp1 = new Employee
        {
            Id = 1,
            EmployeeNumber = 123,
            FirstName = "Julia",
            LastName = "Schmid"
        };
        private static readonly Employee _emp2 = new Employee
        {
            Id = 2,
            EmployeeNumber = 101,
            FirstName = "Martin",
            LastName = "Schmid"
        };
        private static Vehicle _veh1 = new Vehicle()
        {
            Brand = "Suzuki",
            Id = 1,
            Insurance = 1200,
            LeasingFrom = new DateTime().Date,
            LeasingTo = new DateTime().Date,
            LeasingRate = 100,
            LicensePlate = "fds-z-123",
            Model = "Swift"
        };

        private static Vehicle _veh2 = new Vehicle()
        {
            Brand = "Audi",
            Id = 2,
            Insurance = 1500,
            LeasingRate = 300,
            LeasingFrom = new DateTime().Date,
            LeasingTo = new DateTime().Date,
            LicensePlate = "FDS A 123",
            Model = "A8"
        };

        private static VehicleToEmployeeRelation _rel1 = new VehicleToEmployeeRelation()
        {
            EmployeeId = _emp1,
            VehicleId = _veh1,
            Id = 1
        };
        private static VehicleToEmployeeRelation _rel2 = new VehicleToEmployeeRelation()
        {
            EmployeeId = _emp2,
            VehicleId = _veh2,
            Id = 2
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _vehicleRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Vehicle> { _veh1, _veh2 });
            _businessUnitRepositoryMock.Setup(x => x.GetAll()).Returns(new List<BusinessUnit> {_un1, _un2 });
            _employeeRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee> { _emp1, _emp2 });
            _relationRepositoryMock.Setup(x => x.GetAll()).Returns(new List<VehicleToEmployeeRelation> { _rel1, _rel2 });

            _service = new Service(null, _employeeRepositoryMock.Object, _businessUnitRepositoryMock.Object, _vehicleRepositoryMock.Object, _relationRepositoryMock.Object);

        }

        [TestMethod]
        public void TestAllVehicles()
        {
            Assert.AreEqual(_service.GetAllVehicles().Count(), 2);
        }

        [TestMethod]
        public void TestAllEmployees()
        {
            Assert.AreEqual(_service.GetAllEmployees().Count(), 2);
        }

        [TestMethod]
        public void TestAllBusinessUnits()
        {
            Assert.AreEqual(_service.GetAllBusinessUnits().Count(), 2);
        }

        [TestMethod]
        public void TestAllRelation()
        {
            Assert.AreEqual(_service.GetAllRelations().Count(), 2);
        }

    }
}
