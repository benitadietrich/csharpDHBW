using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aufgabe12_ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CustomerService : ICustomerService
    {
        private List<Customer> mCustomers = new List<Customer>()
        {
            new Customer()
            {
                FirstName = "Jens",
                LastName = "Mander",
                isPremiumCustomer = true
            },
            new Customer()
            {
                FirstName = "Hans",
                LastName = "Wurst",
                isPremiumCustomer = false
            },
            new Customer()
            {
                FirstName = "Guybrush",
                LastName = "Threepwod",
                isPremiumCustomer = true
            }
        };

        public bool AddCustomer(Customer customer)
        {
            if (mCustomers.Contains(customer))
            {
                return false;
            }

            mCustomers.Add(customer);
            return true;

        }

        public bool RemoveCustomer(Customer customer)
        {
            if (!mCustomers.Contains(customer))
            {
                return false;
            }

            mCustomers.Remove(customer);
            return true;

        }

        public List<Customer> GetAllCustomers()
        {
            return mCustomers;
        }

        public List<Customer> GetCustomers(string s)
        {
            var result = new List<Customer>();

            return (from customer in mCustomers
                    where customer.LastName.Equals(s)
                    select customer).ToList();

        }

    }
}
