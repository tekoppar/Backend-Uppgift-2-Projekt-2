using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekt2.Classes;
using Projekt2FuckAll.Classes;

namespace Projekt2FuckAll.CustomControls
{
    /// <summary>
    /// Interaction logic for NewBookingWindow.xaml
    /// </summary>
    public partial class NewBookingWindow : Window
    {
        private CustomerBookings bks;
        private Customer customer;
        public NewBookingWindow(CustomerBookings bookings, Customer customer)
        {
            InitializeComponent();
            this.bks = bookings;
            this.customer = customer;

            this.paymentMethods.ItemsSource = Enum.GetNames(typeof(PaymentMethod));
            this.paymentMethods.Items.Refresh();

            this.listRooms.ItemsSource = MainWindow.RMSystem.ListRooms;
            this.listRooms.DisplayMemberPath = nameof(Room.RoomName);
            this.listRooms.Items.Refresh();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnAccept":
                    if (this.listRooms.SelectedIndex == -1 || this.paymentMethods.SelectedIndex == -1)
                    {
                        this.ShowMessage("Missing information, please make sure everything is filled in.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    }

                    Room room = (Room)this.listRooms.SelectedItem;
                    BookingDates dates = new BookingDates(this.checkInDate.SelectedDate.Value, this.checkOutDate.SelectedDate.Value);
                    PaymentMethod payment = Enum.Parse<PaymentMethod>((string)this.paymentMethods.SelectedItem);
                    Booking newBooking = new Booking(this.bks.Id, this.customer.GetId(), room.GetRoomDB(), payment, dates);
                    this.bks.AddBooking(newBooking);

                    newBooking.SetDates(dates);
                    newBooking.SetPaymentMethod(payment);
                    newBooking.SetRoom(room.GetRoomDB());
                    newBooking.UpdateDB();
                    this.Close();
                    break;

                case "btnCancel":
                    this.Close();
                    break;
            }
        }

        private void UpdateBlackoutDates()
        {
            this.checkOutDate.BlackoutDates.Clear();
            this.checkInDate.BlackoutDates.Clear();
            Room selectedRoom = (Room)this.listRooms.SelectedItem;
            List<Booking> bookings = MainWindow.BKSystem.GetBookinsByRoom(selectedRoom.RoomNumber);

            foreach (Booking booking in bookings)
            {
                BookingDates dpDates = booking.GetDates();
                this.checkOutDate.BlackoutDates.Add(new CalendarDateRange(dpDates.CheckInDate, dpDates.CheckOutDate));
                this.checkInDate.BlackoutDates.Add(new CalendarDateRange(dpDates.CheckInDate, dpDates.CheckOutDate));
            }
        }

        private void DatePicker_DataContextChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateBlackoutDates();
            this.checkOutDate.BlackoutDates.Add(new CalendarDateRange(DateTime.UnixEpoch, this.checkInDate.SelectedDate.Value));
        }

        private void ShowMessage(string message, string caption, MessageBoxButton buttonTypes = MessageBoxButton.OK, MessageBoxImage type = MessageBoxImage.Asterisk)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, buttonTypes, type);
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    break;

                case MessageBoxResult.No:
                    break;

                case MessageBoxResult.None:
                    break;

                case MessageBoxResult.OK:
                    break;

                case MessageBoxResult.Yes:
                    break;
            }
        }

        private void listRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateBlackoutDates();
        }
    }
}
