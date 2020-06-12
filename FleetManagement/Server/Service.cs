using Server.Framework;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.CodeDom;

namespace Server
{
    public class Service : IService
    {
        private static readonly string _DATABASE = "D:/git/csharpDHBW/FleetManagement/Server/Database/FleetManagement.db";
        private UserRepository _userRepository = new UserRepository(_DATABASE);
        public bool AddBusinessUnit()
        {
            throw new NotImplementedException();
        }

        public bool AddEmployee()
        {
            throw new NotImplementedException();
        }

        public bool AddVehicle()
        {
            throw new NotImplementedException();
        }

        public List<BusinessUnit> GetAllBusinessUnits()
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<Vehicle> GetAllVehicles()
        {
            throw new NotImplementedException();
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

            if (BCrypt.Net.BCrypt.Verify(oldPw, user.Password))
            {
                if (BCrypt.Net.BCrypt.Verify(newPw, newPwWdh))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(newPw);
                    return "Passwort wurde erfolgreich geändert";
                }
                return "Die neuen Passwörter stimmen nicht überein";
            }
            return "Das alte Passwort ist falsch";
        }

        public bool RemoveBusinessUnit()
        {
            throw new NotImplementedException();
        }

        public bool RemoveEmployee()
        {
            throw new NotImplementedException();
        }

        public bool RemoveVehicle()
        {
            throw new NotImplementedException();
        }
    }
}
