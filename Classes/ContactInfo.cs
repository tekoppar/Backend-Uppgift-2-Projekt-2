using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2.Classes
{
    public enum ContactType
    {
        Null,
        Phone,
        Mobile,
        Email
    };

    public class DPContactInfo
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ContactData { get; set; }
        public int ContactType { get; set; }
    }

    public class ContactInfo
    {
        private int DBID = -1;
        private Customer Customer;
        public string ContactData { get; set; } = "";
        public ContactType ContactType = ContactType.Null;

        public ContactInfo(string contactData, ContactType contactType = ContactType.Null)
        {
            this.ContactData = contactData;
            this.ContactType = contactType;
        }

        public ContactInfo(string contactData, ContactType contactType, Customer customer)
        {
            this.ContactData = contactData;
            this.ContactType = contactType;
            this.Customer = customer;
            DPContactInfo dp = new DPContactInfo();
            dp.ContactType = (int)this.ContactType;
            dp.ContactData = this.ContactData;
            dp.CustomerId = this.Customer.GetId();
            DatabaseSystem.DBS.InsertSingle<DPContactInfo>("contactInfo", new List<string>() { "customerId", "contactData", "contactType" }, dp);
        }

        public ContactInfo(DPContactInfo dp)
        {
            this.ContactData = dp.ContactData;
            this.ContactType = (ContactType)dp.ContactType;
            this.DBID = dp.Id;
        }

        public void SetInfo(string data, ContactType type)
        {
            this.ContactData = data;
            this.ContactType = type;
        }
    }
}
