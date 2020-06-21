using Server.Models;
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
        bool AddVehicle(Vehicle vehicle);

        [OperationContract]
        bool RemoveVehicle(Vehicle veh);

        [OperationContract]
        List<Vehicle> GetAllVehicles();

        [OperationContract]
        bool AddEmployee(Employee emp);

        [OperationContract]
        bool RemoveEmployee(Employee emp);

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
        bool DeleteUser(User user);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool EditUser(User newUser);

        [OperationContract]
        bool EditBusinessUnit(BusinessUnit businessUnit);

        [OperationContract]
        bool CannotRemoveBusinessUnit(BusinessUnit businessUnit);

        [OperationContract]
        bool CannotRemoveEmployee(Employee emp);

        [OperationContract]
        bool EditEmployee(Employee emp);

        [OperationContract]
        List<VehicleToEmployeeRelation> GetAllRelations();

        [OperationContract]
        bool RemoveRelation(VehicleToEmployeeRelation rel);

        [OperationContract]
        bool AddRelation(VehicleToEmployeeRelation rel);

        [OperationContract]
        List<VehicleToEmployeeRelation> GetRelationFromVehicle(Vehicle vehicle);

        [OperationContract]
        bool EditVehicle(Vehicle veh);

    }
}
