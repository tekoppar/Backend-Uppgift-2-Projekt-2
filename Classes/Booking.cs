using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt2FuckAll.Classes;

namespace Projekt2.Classes
{
    public enum PaymentMethod
    {
        Null,
        CreditCard,
        Invoice,
        Cash
    }

    public class DPBooking
    {
        public int Id { get; set; }
        public int bookingsId { get; set; }
        public int customerId { get; set; }
        public int roomId { get; set; }
        public int paymentMethod{ get; set; }
        public int bookingDatesId { get; set; }
        public bool bookingValid { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }
        public int BookingsID { get; set; }
        private int CustomerID;
        private int RoomID;
        private int PaymentMethodID;
        private bool BookingValid = true;
        private PaymentMethod PaymentMethod = PaymentMethod.Null;
        private BookingDates BookingDates;
        public string BookingName { get; set; }

        public Booking(int bookingsId, int customerId, int roomId, PaymentMethod paymentMethod, BookingDates bookingDates)
        {
            this.BookingsID = bookingsId;
            this.CustomerID = customerId;
            this.RoomID = roomId;
            this.PaymentMethod = paymentMethod;
            this.BookingDates = bookingDates;

            this.BookingName = RoomSystem.GetRoom(this.RoomID).RoomName + " Dates: " + BookingDates.CheckInDate.ToShortDateString() + " - " + BookingDates.CheckOutDate.ToShortDateString();
        }

        public Booking(DPBooking dp)
        {
            this.Id = dp.Id;
            this.BookingsID = dp.bookingsId;
            this.CustomerID = dp.customerId;
            this.RoomID = dp.roomId;
            this.PaymentMethodID = dp.paymentMethod;
            this.BookingValid = dp.bookingValid;
            string paymentString = DatabaseSystem.DBS.SelectSingleWhere<string>("paymentMethod", new List<string>() { "method" }, new List<string>() { "Id" }, new List<string>() { dp.paymentMethod.ToString() });
            paymentString = paymentString.Replace(" ", "");
            this.PaymentMethod = Enum.Parse<PaymentMethod>(paymentString);
            DPBookingDates dpDates = DatabaseSystem.DBS.SelectWhereObject<DPBookingDates>("bookingDates", new List<string>() { "Id", "checkInDate", "checkOutDate" }, new List<string>() { "Id" }, new List<string>() { dp.bookingDatesId.ToString() });
            this.BookingDates = new BookingDates(dpDates);

            this.BookingName = RoomSystem.GetRoom(this.RoomID).RoomName + " Dates: " + BookingDates.CheckInDate.ToShortDateString() + " - " + BookingDates.CheckOutDate.ToShortDateString();
        }

        public void UpdateDB()
        {
            DPBooking dp = new DPBooking();
            dp.roomId = this.RoomID;
            dp.Id = this.Id;
            dp.paymentMethod = this.PaymentMethodID;
            dp.bookingValid = this.BookingValid;
            dp.bookingDatesId = this.BookingDates.Id;
            dp.customerId = this.CustomerID;

            int bookExists = DatabaseSystem.DBS.SelectSingleWhere<int>("booking", new List<string>() { "Id" }, new List<string>() { "Id" }, new List<string>() { this.Id.ToString() });

            if (bookExists == 0)
            {
                this.BookingDates.UpdateDB();
                DatabaseSystem.DBS.InsertSingle("booking",
                new List<string>() { "bookingsId", "customerId", "roomId", "paymentMethod", "bookingDatesId", "bookingValid" },
                new List<string>() { this.BookingsID.ToString(), this.CustomerID.ToString(), this.RoomID.ToString(), this.PaymentMethodID.ToString(), this.BookingDates.Id.ToString(), (this.BookingValid == true ? 1 : 0).ToString() }
                );
                this.Id = DatabaseSystem.DBS.SelectSingle<int>("booking",
                new List<string>() { "max(Id)" }
                );
            } 
            else
            {
                DatabaseSystem.DBS.UpdateSingle("booking",
                new List<string>() { "bookingsId", "roomId", "paymentMethod", "bookingDatesId", "bookingValid" },
                new List<string>() { this.BookingsID.ToString(), this.RoomID.ToString(), this.PaymentMethodID.ToString(), this.BookingDates.Id.ToString(), (this.BookingValid == true ? 1 : 0).ToString() },
                "Id", this.Id.ToString());
            }
        }

        public int GetID()
        {
            return this.BookingsID;
        }

        public int GetRoomID()
        {
            return this.RoomID;
        }

        public void SetDates(BookingDates dates)
        {
            this.BookingDates = dates;
        }

        public void SetRoom(int room)
        {
            this.RoomID = room;
        }

        public PaymentMethod GetPaymentMethod()
        {
            return this.PaymentMethod;
        }

        public void SetPaymentMethod(PaymentMethod method)
        {
            this.PaymentMethod = method;
            this.PaymentMethodID = ((int)method) + 1;
        }

        public BookingDates GetDates()
        {
            return this.BookingDates;
        }
    }
}
