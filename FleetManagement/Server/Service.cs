using Autofac;
using Server.Framework;
using Server.Interfaces;
using Server.Models;
using System.Collections.Generic;

namespace Server
{
    public class Service : IService
    {
        public IContainer Container { get; private set; }

        private static readonly string _DATABASE = "Database/FleetManagement.db";
        private IUserRepository _userRepository;
        private IEmployeeRepository _employeeReposiotry;
        private IBusinessUnitRepository _businessUnitRepository;
        private IVehicleRepository _vehicleRepository;
        private IVehicleToEmployeeRelation _relationRepository;

        public Service()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            containerBuilder.RegisterType<BusinessUnitRepository>().As<IBusinessUnitRepository>();
            containerBuilder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            containerBuilder.RegisterType<VehicleRepository>().As<IVehicleRepository>();
            containerBuilder.RegisterType<VehicleToEmployeeRelationRepository>().As<IVehicleToEmployeeRelation>();

            Container = containerBuilder.Build();

            _userRepository = Container.Resolve<IUserRepository>(new NamedParameter("databaseFile", _DATABASE));
            _businessUnitRepository = Container.Resolve<IBusinessUnitRepository>(new NamedParameter("databaseFile", _DATABASE));
            _employeeReposiotry = Container.Resolve<IEmployeeRepository>(new NamedParameter("databaseFile", _DATABASE));
            _vehicleRepository = Container.Resolve<IVehicleRepository>(new NamedParameter("databaseFile", _DATABASE));
            _relationRepository = Container.Resolve<IVehicleToEmployeeRelation>(new NamedParameter("databaseFile", _DATABASE));

        }

        public Service(IUserRepository u, IEmployeeRepository e, IBusinessUnitRepository b, IVehicleRepository v, IVehicleToEmployeeRelation r)
        {
            _userRepository = u;
            _employeeReposiotry = e;
            _businessUnitRepository = b;
            _vehicleRepository = v;
            _relationRepository = r;
        }

        public bool AddBusinessUnit(BusinessUnit businessUnit)
        {
            var b = _businessUnitRepository.GetBusinessUnit(businessUnit.Id);
            var be = _businessUnitRepository.GetBusinessUnitName(businessUnit.Name.ToLower());
            if (b != null || be != null)
            {
                return false;
            }
            else
            {
                businessUnit.Name = businessUnit.Name.ToLower();
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
            vehicle.LicensePlate = vehicle.LicensePlate.ToLower().Replace(" ", "-");
            if (_vehicleRepository.GetByLicense(vehicle.LicensePlate) == null)
            {
                _vehicleRepository.Save(vehicle);
                return true;
            }
            else
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

        public bool CannotRemoveBusinessUnit(BusinessUnit businessUnit)
        {
            var b = _businessUnitRepository.GetBusinessUnit(businessUnit.Id);
            return _employeeReposiotry.IsEmployeeReferred(b);
        }

        public bool CannotRemoveEmployee(Employee emp)
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

        public bool EditUser(User newUser)
        {
            try

            {
                _userRepository.UpdateUser(newUser);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteUser(User user)
        {
            var u = _userRepository.GetUser(user.Username);
            if (u != null)
            {
                _userRepository.Delete(u);
                return true;
            }
            else
                return false;
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

        public bool EditBusinessUnit(BusinessUnit businessUnit)
        {
            try
            {
                _businessUnitRepository.UpdateBusinessUnit(businessUnit);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditEmployee(Employee emp)
        {
            try
            {
                _employeeReposiotry.UpdateEmployee(emp);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public List<VehicleToEmployeeRelation> GetAllRelations()
        {
            return _relationRepository.GetAll();
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

        public bool EditVehicle(Vehicle veh)
        {
            try
            {
                _vehicleRepository.UpdateVehicle(veh);
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
