using Server.Framework;
using Server.Models;
using System;
using System.Collections.Generic;

namespace Server
{
    public class Service : IService
    {
        private static readonly string _DATABASE = "Database/FleetManagement.db";
        private UserRepository _userRepository = new UserRepository(_DATABASE);
        private EmployeeRepository _employeeReposiotry = new EmployeeRepository(_DATABASE);
        private BusinessUnitRepository _businessUnitRepository = new BusinessUnitRepository(_DATABASE);
        private VehicleRepository _vehicleRepository = new VehicleRepository(_DATABASE);
        private VehicleToEmployeeRelationRepository _relationRepository = new VehicleToEmployeeRelationRepository(_DATABASE);

        public bool AddBusinessUnit(BusinessUnit businessUnit)
        {
            var b = _businessUnitRepository.GetBusinessUnit(businessUnit.Id);
            if (b != null)
            {
                return false;
            }
            else
            {
                _businessUnitRepository.Save(businessUnit);
                return true;
            }
        }

        public bool AddEmployee(Employee emp)
        {
            var e = _employeeReposiotry.GetEmployeeByNumber(emp.EmployeeNumber);
            if (e != null)
            {
                return false;
            }
            else
            {
                _employeeReposiotry.Save(emp);
                return true;
            }
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                _vehicleRepository.Save(vehicle);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<BusinessUnit> GetAllBusinessUnits()
        {
            return _businessUnitRepository.GetAll();
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeReposiotry.GetAll();
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleRepository.GetAll();
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetUser(username);
            if (user == null)
            {
                return null;
            }
            else if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return new User()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    IsAdmin = user.IsAdmin,
                    Username = user.Username,

                };
            };

            return null;
        }

        public string ChangePassword(User user, string oldPw, string newPw, string newPwWdh)
        {

            var userNew = _userRepository.GetUser(user.Username);
            if (BCrypt.Net.BCrypt.Verify(oldPw, userNew.Password))
            {
                if (newPw == newPwWdh)
                {
                    var newHash = BCrypt.Net.BCrypt.ValidateAndReplacePassword(oldPw, userNew.Password, newPw);
                    user.Password = newHash;
                    if (_userRepository.SetPassword(userNew.Id, newHash))
                        return "Passwort wurde erfolgreich geändert";
                    else
                        return "Passwort konnte nicht geändert werden";
                }
                return "Die neuen Passwörter stimmen nicht überein";
            }
            return "Das alte Passwort ist falsch";
        }

        public bool RemoveBusinessUnit(BusinessUnit businessUnit)
        {
            var b = _businessUnitRepository.GetBusinessUnit(businessUnit.Id);
            _businessUnitRepository.Delete(b);
            return true;

        }

        public bool CanRemoveBusinessUnit(BusinessUnit businessUnit)
        {
            var b = _businessUnitRepository.GetBusinessUnit(businessUnit.Id);
            return _employeeReposiotry.IsEmployeeReferred(b);
        }

        public bool CanRemoveEmployee(Employee emp)
        {
            var e = _employeeReposiotry.GetEmployee(emp.Id);
            return _vehicleRepository.IsVehicleReferredToEmp(e);
        }

        public bool RemoveEmployee(Employee emp)
        {
            var e = _employeeReposiotry.GetEmployee(emp.Id);
            _employeeReposiotry.Delete(e);
            return true;
        }

        public bool RemoveVehicle(Vehicle veh)
        {
            try
            {
                _vehicleRepository.Delete(veh);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public void EditUser(User newUser)
        {
            _userRepository.UpdateUser(newUser);

        }

        public void DeleteUser(User user)
        {
            var u = _userRepository.GetUser(user.Username);
            _userRepository.Delete(u);
        }

        public bool AddUser(User user)
        {
            if (user.Password == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword("geheim");
            }
            if (_userRepository.GetUser(user.Username) != null)
                return false;
            else
            {
                if (user.Username == null) return false;
                user.Username = user.Username.ToLower();
                _userRepository.Save(user);
                return true;
            }

        }

        public void EditBusinessUnit(BusinessUnit businessUnit)
        {
            _businessUnitRepository.UpdateBusinessUnit(businessUnit);
        }

        public void EditEmployee(Employee emp)
        {
            _employeeReposiotry.UpdateEmployee(emp);
        }

        public List<VehicleToEmployeeRelation> GetAllRelations()
        {
            return new List<VehicleToEmployeeRelation>();
        }

        public bool RemoveRelation(VehicleToEmployeeRelation rel)
        {
            try
            {
                _relationRepository.Delete(rel);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool AddRelation(VehicleToEmployeeRelation rel)
        {
            try
            {

                _relationRepository.Save(rel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<VehicleToEmployeeRelation> GetRelationFromVehicle(Vehicle vehicle)
        {
            return _relationRepository.GetRelationFromVehicle(vehicle);
        }

        public void EditVehicle(Vehicle veh)
        {
            _vehicleRepository.UpdateVehicle(veh);
        }

    }
}
