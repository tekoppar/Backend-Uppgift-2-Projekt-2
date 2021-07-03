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
    /// Interaction logic for EditBookingWindow.xaml
    /// </summary>
    public partial class EditBookingWindow : Window
    {
        private Booking booking;
        public EditBookingWindow(Booking booking)
        {
            InitializeComponent();
            this.booking = booking;

            this.paymentMethods.ItemsSource = Enum.GetNames<PaymentMethod>();
            this.paymentMethods.Items.Refresh();

            this.listRooms.ItemsSource = MainWindow.RMSystem.ListRooms;
            this.listRooms.DisplayMemberPath = nameof(Room.RoomName);
            this.listRooms.Items.Refresh();

            this.listRooms.SelectedItem = MainWindow.RMSystem.ListRooms[MainWindow.RMSystem.ListRoomsLUT[booking.GetRoomID()]];
            this.paymentMethods.SelectedItem = Enum.GetName<PaymentMethod>(booking.GetPaymentMethod());
            BookingDates dates = booking.GetDates();
            this.checkInDate.SelectedDate = this.checkInDate.DisplayDate = dates.CheckInDate;
            this.checkOutDate.SelectedDate = this.checkOutDate.DisplayDate = dates.CheckOutDate;
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

                    this.booking.SetDates(dates);
                    this.booking.SetPaymentMethod(payment);
                    this.booking.SetRoom(room.GetRoomDB());
                    this.booking.UpdateDB();
                    this.Close();
                    break;

                case "btnCancel":
                    this.Close();
                    break;
            }
        }

        private void DatePicker_DataContextChanged(object sender, RoutedEventArgs e)
        {
            this.checkOutDate.BlackoutDates.Clear();
            Room selectedRoom = (Room)this.listRooms.SelectedItem;
            List<Booking> bookings = MainWindow.BKSystem.GetBookinsByRoom(selectedRoom.RoomNumber);

            foreach (Booking booking in bookings)
            {
                BookingDates dpDates = booking.GetDates();
                this.checkOutDate.BlackoutDates.Add(new CalendarDateRange(dpDates.CheckInDate, dpDates.CheckOutDate));
            }

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
    }
}
