using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2.Classes
{
    public class DPCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactInfoId { get; set; }
    }

    public class Customer
    {
        static public List<Customer> AllCustomers = new List<Customer>();

        private int CustomerID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string CustomerName { get; set; }
        public List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();

        public Customer(string firstName, string lastName, int customerID = -1)
        {
            this.FirstName = firstName;
            this.LastName = lastName;

            if (customerID == -1)
            {
                this.NewCustomer();
            }
            Customer.AllCustomers.Add(this);
            this.CustomerName = this.FirstName + ", " + this.LastName;
        }

        public Customer(DPCustomer customer)
        {
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.CustomerID = customer.Id;
            List<DPContactInfo> contactinfo = DatabaseSystem.DBS.SelectWhere<DPContactInfo>("contactInfo", new List<string>() { "Id", "customerID", "contactData", "contactType" }, new List<string>() { "customerID" }, new List<string>() { this.CustomerID.ToString() });
            contactinfo.ForEach(delegate (DPContactInfo info) {
                this.ContactInfos.Add(new ContactInfo(info));
            });
            this.CustomerName = this.FirstName + ", " + this.LastName;
        }

        public int GetId()
        {
            return this.CustomerID;
        }

        public void AddContactInfo(ContactInfo info)
        {
            this.ContactInfos.Add(info);
        }

        public void RemoveContactInfo(ContactInfo info)
        {
            this.ContactInfos.Remove(info);
        }

        public void AddContactInfo(List<ContactInfo> infos)
        {
            this.ContactInfos.AddRange(infos);
        }

        public void UpdateDB()
        {
            DatabaseSystem.DBS.UpdateSingle("customer", new List<string>() { "firstName", "lastName" }, new List<string>() { this.FirstName, this.LastName }, "Id", this.CustomerID.ToString());
        }

        private void NewCustomer()
        {
            int testId = DatabaseSystem.DBS.SelectSingleWhere<int>("customer", new List<string>() { "Id, FirstName, LastName, ContactInfoId" }, new List<string>() { "FirstName", "LastName" }, new List<string>() { this.FirstName, this.LastName });

            if (testId > 0)
            {
                this.CustomerID = testId;
            } else 
            {
                DPCustomer dp = new DPCustomer();
                dp.FirstName = this.FirstName;
                dp.LastName = this.LastName;
                dp.ContactInfoId = 0;
                DatabaseSystem.DBS.InsertSingle<DPCustomer>("customer", new List<string>() { "FirstName", "LastName", "ContactInfoId" }, dp);
                this.CustomerID = DatabaseSystem.DBS.SelectSingle<int>("customer", new List<string>() { "max(Id)" }) + 1;
            }
        }
    }
}
