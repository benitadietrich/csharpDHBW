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
    public class VehicleTests
    {
        private static IService service;
        private static readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        private static readonly Mock<IVehicleToEmployeeRelation> _RelationRepositoryMock = new Mock<IVehicleToEmployeeRelation>();
        private static readonly Mock<IVehicleRepository> _vehicleRepositroyMock = new Mock<IVehicleRepository>();

        private  static Vehicle _vehicleNew = new Vehicle()
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
        private static Vehicle _veh1 = new Vehicle()
        {
            Brand = "Opel",
            Id = 4,
            Insurance = 1200,
            LeasingFrom = new DateTime().Date,
            LeasingTo = new DateTime().Date,
            LeasingRate = 100,
            LicensePlate = "s-au-44",
            Model = "Adam"
        };

        private static Vehicle _veh1Edited = new Vehicle()
        {
            Brand = "Opel",
            Id = 4,
            Insurance = 1200,
            LeasingFrom = new DateTime().Date,
            LeasingTo = new DateTime().Date,
            LeasingRate = 100,
            LicensePlate = "s-ab-44",
            Model = "Adam"
        };

        private static Vehicle _sameLicense = new Vehicle()
        {
            Brand = "Audi",
            Id = 2,
            Insurance = 1500,
            LeasingRate = 300,
            LeasingFrom = new DateTime().Date,
            LeasingTo = new DateTime().Date,
            LicensePlate = "FDS A 123",
            Model = "A6"
        };

        private static Employee _emp = new Employee()
        {
            EmployeeNumber = 1,
            Id = 1,
            FirstName = "Hans",
            LastName = "Mueller",
            BusinessUnitId = new BusinessUnit()
            {
                Id = 1
            },
        };

        private static VehicleToEmployeeRelation _relation = new VehicleToEmployeeRelation()
        {
            EmployeeId = _emp,
            Id = 1,
            StartDate = new DateTime().Date,
            VehicleId = _vehicleNew
        };

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _vehicleRepositroyMock.Setup(x => x.Save(It.IsAny<Vehicle>())).Verifiable();
            _vehicleRepositroyMock.Setup(x => x.GetByLicense("fds-a-123")).Returns(_sameLicense);
            _vehicleRepositroyMock.Setup(x => x.GetVehicle(4)).Returns(_veh1);

            service = new Service(null, _employeeRepositoryMock.Object, null, _vehicleRepositroyMock.Object, _RelationRepositoryMock.Object);
        }

        [TestMethod]
        public void TestAddVehicle()
        {
            Assert.IsTrue(service.AddVehicle(_vehicleNew));
        }

        [TestMethod]
        public void TestAddExistingVeh()
        {

            Assert.IsFalse(service.AddVehicle(_sameLicense));
        }

        [TestMethod]
        public void TestAddRelation()
        {
            Assert.IsTrue(service.AddRelation(_relation));
        }

        [TestMethod]
        public void RemoveRelation()
        {
            Assert.IsTrue(service.RemoveRelation(_relation));
        }

        [TestMethod]
        public void RemoveVehicle()
        {
            Assert.IsTrue(service.RemoveVehicle(_vehicleNew));
        }

        [TestMethod]
        public void TestEditVehicle()
        {
            Assert.IsTrue(service.EditVehicle(_veh1Edited));
        }
    }
}
