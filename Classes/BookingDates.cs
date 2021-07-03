using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2.Classes
{
    public class DPBookingDates
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Id { get; set; }
    }

    public class BookingDates
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Id { get; set; } = -1;

        public BookingDates(DateTime checkIn, DateTime checkOut, int datesId = -1)
        {
            this.CheckInDate = checkIn;
            this.CheckOutDate = checkOut;

            if (datesId == -1)
                this.Id = -1;
        }

        public BookingDates(DPBookingDates dp)
        {
            this.CheckInDate = dp.CheckInDate;
            this.CheckOutDate = dp.CheckOutDate;
            this.Id = dp.Id;
        }

        public bool IsInDate()
        {
            return this.CheckInDate > DateTime.Now && this.CheckOutDate < DateTime.Now;
        }

        public void UpdateDB()
        {
            int bookExists = DatabaseSystem.DBS.SelectSingleWhere<int>("bookingDates", new List<string>() { "Id" }, new List<string>() { "Id" }, new List<string>() { this.Id.ToString() });

            if (bookExists == 0)
            {
                DatabaseSystem.DBS.InsertSingle("bookingDates",
                new List<string>() { "checkInDate", "checkOutDate" },
                new List<string>() { this.CheckInDate.ToString(), this.CheckOutDate.ToString() }
                );
                this.Id = DatabaseSystem.DBS.SelectSingle<int>("bookingDates",
                    new List<string>() { "max(Id)" }
                    );
            }
            else
            {

                DatabaseSystem.DBS.UpdateSingle("bookingDates",
                    new List<string>() { "checkInDate", "checkOutDate" },
                    new List<string>() { this.CheckInDate.ToString(), this.CheckOutDate.ToString() },
                    "Id", this.Id.ToString());
            }
        }
    }
}
