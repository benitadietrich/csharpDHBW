using Server.Framework;
using Server.Models;
using System;
using System.Collections.Generic;

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

        public void AddUser(User user)
        {
            if (user.Password == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword("geheim");
            }
            user.Username = user.Username.ToLower();
            _userRepository.Save(user);
        }
    }
}
