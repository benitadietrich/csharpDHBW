﻿using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IService1" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool AddVehicle();

        [OperationContract]
        bool RemoveVehicle();

        [OperationContract]
        List<Vehicle> GetAllVehicles();

        [OperationContract]
        bool AddEmployee();

        [OperationContract]
        bool RemoveEmployee();

        [OperationContract]
        List<Employee> GetAllEmployees();

        [OperationContract]
        bool AddBusinessUnit(BusinessUnit businessUnit);

        [OperationContract]
        bool RemoveBusinessUnit(BusinessUnit businessUnit);

        [OperationContract]
        List<BusinessUnit> GetAllBusinessUnits();

        [OperationContract]
        User Login(string username, string password);

        [OperationContract]
        string ChangePassword(User user, string oldPw, string newPw, string newPwWdh);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        void DeleteUser(User user);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        void EditUser(User newUser);

        [OperationContract]
        void EditBusinessUnit(BusinessUnit businessUnit);

        [OperationContract]
        bool CanRemoveBusinessUnit(BusinessUnit businessUnit);

    }
}