using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt2FuckAll.Classes;

namespace Projekt2.Classes
{
    public class DPCustomerBookings
    {
        public int Id { get; set; }
        public int customerId { get; set; }
    }

    public class CustomerBookings
    {
        private Dictionary<int, Booking> Bookings = new Dictionary<int, Booking>();
        public List<Booking> ListBookings = new List<Booking>();
        public int Id { get; set; }
        private int CustomerID;
        public string CBName { get; set; }

        public CustomerBookings(int customerId, int bookingsId = -1)
        {
            this.CustomerID = customerId;
            this.Id = bookingsId;
            this.CBName = "ID: " + this.Id.ToString() + ", Bookings: " + this.Bookings.Count.ToString();
        }

        public CustomerBookings(DPCustomerBookings dp)
        {
            this.CustomerID = dp.customerId;
            this.Id = dp.Id;
            this.CBName = "ID: " + this.Id.ToString() + ", Bookings: " + this.Bookings.Count.ToString();
        }

        public void AddBooking(Booking booking)
        {
            if (this.Bookings.ContainsKey(booking.Id) == false)
            {
                this.Bookings.Add(booking.Id, booking);
                this.ListBookings.Add(booking);
                this.CBName = "ID: " + this.Id.ToString() + ", Bookings: " + this.Bookings.Count.ToString();
            }
        }

        public void NewBookings()
        {
            DatabaseSystem.DBS.InsertSingle("bookings", new List<string>() { "customerId" }, new List<string>() { this.CustomerID.ToString() });
            this.Id = DatabaseSystem.DBS.SelectSingle<int>("bookings",
                new List<string>() { "max(Id)" }
                );
        }

        public void UpdateDB()
        {   
            foreach(KeyValuePair<int, Booking> booking in this.Bookings)
            {
                booking.Value.UpdateDB();
            }
        }

        public int GetID()
        {
            return this.Id;
        }
    }

    public class BookingSystem
    {
        public Dictionary<int, CustomerBookings> Bookings = new Dictionary<int, CustomerBookings>();
        private Dictionary<int, List<int>> CustomerBookingsLUT = new Dictionary<int, List<int>>();
        public List<Booking> ListBookings = new List<Booking>();

        public BookingSystem()
        {

        }

        public void AddCustomers(List<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                if (this.Bookings.ContainsKey(customer.GetId()) == false)
                    this.Bookings.Add(customer.GetId(), new CustomerBookings(customer.GetId()));
            }
        }

        public void GetBookings()
        {
            List<DPCustomerBookings> dpCustomerBookings = DatabaseSystem.DBS.Select<DPCustomerBookings>("bookings", new List<string>() { "Id", "customerId" }).ToList();

            foreach (DPCustomerBookings dp in dpCustomerBookings)
            {
                this.RegisterBooking(new CustomerBookings(dp), dp.customerId);
            }

            List<DPBooking> dpBookings = DatabaseSystem.DBS.Select<DPBooking>("booking", new List<string>() { "Id", "bookingsId", "customerId", "roomId", "paymentMethod", "bookingDatesId", "bookingValid" }).ToList();

            foreach (DPBooking dp in dpBookings)
            {
                if (this.Bookings.ContainsKey(dp.bookingsId) == true)
                {
                    this.AddCustomerBookingsLUT(dp.bookingsId, dp.customerId);
                    this.Bookings[dp.bookingsId].AddBooking(new Booking(dp));
                    this.ListBookings.Add(new Booking(dp));
                }
                else
                {
                    this.AddCustomerBookingsLUT(dp.bookingsId, dp.customerId);
                    this.Bookings.Add(dp.bookingsId, new CustomerBookings(dp.customerId, dp.bookingsId));
                    this.Bookings[dp.bookingsId].AddBooking(new Booking(dp));
                    this.ListBookings.Add(new Booking(dp));
                }
            }
        }

        private void AddCustomerBookingsLUT(int bookingsId, int customerId)
        {
            if (this.CustomerBookingsLUT.ContainsKey(customerId) == false)
                this.CustomerBookingsLUT.Add(customerId, new List<int>() { bookingsId });
            else if (this.CustomerBookingsLUT[customerId].Contains(bookingsId) == false)
                this.CustomerBookingsLUT[customerId].Add(bookingsId);
        }

        public List<Booking> GetBookinsByRoom(int roomId)
        {
            List<Booking> bookings = new List<Booking>();

            foreach (Booking booking in this.ListBookings)
            {
                if (booking.GetRoomID() == roomId)
                    bookings.Add(booking);
            }

            return bookings;
        }

        public List<CustomerBookings> GetCustomerBookings(int customerId)
        {
            if (this.CustomerBookingsLUT.ContainsKey(customerId) == true)
            {
                List<CustomerBookings> bookings = new List<CustomerBookings>();
                List<int> customerBookings = this.CustomerBookingsLUT[customerId];

                foreach(int id in customerBookings)
                {
                    bookings.Add(this.Bookings[id]);
                }

                return bookings;
            }
            else
                return new List<CustomerBookings>();
        }

        public void RegisterBooking(int customerId, int roomId, PaymentMethod payment, BookingDates dates, int bookingsId = -1)
        {
            if (this.CustomerIsNew(customerId) == true)
            {
                CustomerBookings cb = new CustomerBookings(customerId);
                this.Bookings.Add(cb.GetID(), cb);
                bookingsId = cb.GetID();
            }
            else
            {
                bookingsId = this.Bookings[bookingsId].GetID();
            }

            Booking booking = new Booking(bookingsId, customerId, roomId, payment, dates);
            this.Bookings[customerId].AddBooking(booking);
            this.AddCustomerBookingsLUT(bookingsId, customerId);
        }

        public void RegisterBooking(CustomerBookings cbs, int customerID)
        {
            this.Bookings.Add(cbs.GetID(), cbs);
            this.AddCustomerBookingsLUT(cbs.GetID(), customerID);
        }

        public bool CustomerIsNew(int customerId)
        {
            return !this.Bookings.ContainsKey(customerId);
        }

        public bool DoesBookingExist(int bookingId)
        {
            return this.Bookings.ContainsKey(bookingId);
        }
    }
}
