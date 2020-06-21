using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Server.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Tests
{
    [TestClass]
    class CostsMonthTest
    {
        private static readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private static IService _service;
        private static Vehicle _vehicleNew = new Vehicle()
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

        [ClassInitialize]
        public static void Setup(TestContext context)
        {

            _vehicleRepositoryMock.Setup(x => x.GetAll())
                .Returns(new List<Vehicle> { _vehicleNew, _sameLicense });

            _service = new Service(null, null, null, _vehicleRepositoryMock.Object, null);

        }

        [TestMethod]
        public void TestGettingAllVehicles()
        {
            var result = _service.GetAllVehicles();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestGettingFirstVehicle()
        {
            var result = _service.GetAllVehicles().Find(x => x.Id == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, _vehicleNew);
        }

        [TestMethod]
        public void TestGettingSecondVehicle()
        {
            var result = _service.GetAllVehicles().Find(x => x.Id == 2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, _sameLicense);
        }
    }
}
