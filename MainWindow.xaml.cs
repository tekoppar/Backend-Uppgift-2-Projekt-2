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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt2.Classes;
using Projekt2FuckAll.CustomControls;
using Projekt2FuckAll.Classes;

namespace Projekt2FuckAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public List<Customer> Customers = new List<Customer>();
        static public BookingSystem BKSystem = new BookingSystem();
        static public RoomSystem RMSystem = new RoomSystem();
        private bool ListCustomersUpdated = false;

        public MainWindow()
        {
            InitializeComponent();
            //Customer customer = new Customer("Test1", "Test3");
            this.listCustomers.ItemsSource = MainWindow.Customers;
            this.listCustomers.DisplayMemberPath = nameof(Customer.CustomerName);
            List<DPCustomer> customers = DatabaseSystem.DBS.Select<DPCustomer>("customer", new List<string>() { "Id", "FirstName", "LastName", "ContactInfoId" });

            foreach (DPCustomer customer in customers)
            {
                MainWindow.Customers.Add(new Customer(customer));
            }
            this.listCustomers.Items.Refresh();
            MainWindow.BKSystem.GetBookings();
            MainWindow.BKSystem.AddCustomers(MainWindow.Customers);
        }

        private void UpdateCustomerBookings()
        {
            Customer customer = (Customer)this.listCustomers.SelectedItem;
            this.listCustomerBookings.ItemsSource = MainWindow.BKSystem.GetCustomerBookings(customer.GetId());
            this.listCustomerBookings.DisplayMemberPath = nameof(CustomerBookings.CBName);
            this.listCustomerBookings.Items.Refresh();
            this.ListCustomersUpdated = false;
        }

        private void UpdateBookings()
        {
            CustomerBookings bookings = (CustomerBookings)this.listCustomerBookings.SelectedItem;

            if (bookings != null)
            {
                this.listBookings.ItemsSource = bookings.ListBookings;
                this.listBookings.DisplayMemberPath = nameof(Booking.BookingName);
                this.listBookings.Items.Refresh();
            } else
            {
                this.listBookings.Items.Clear();
                this.listBookings.Items.Refresh();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnNewCustomer":
                    NewCustomerWindow customer = new NewCustomerWindow();
                    customer.ShowDialog();
                    //this.Close();
                    break;

                case "btnEditCustomer":
                    EditCustomerWindow editWindow = new EditCustomerWindow((Customer)this.listCustomers.SelectedItem);
                    editWindow.ShowDialog();
                    break;

                case "btnNewBooking":
                    Customer newBookingCustomer = (Customer)this.listCustomers.SelectedItem;
                    CustomerBookings newBookings = new CustomerBookings(newBookingCustomer.GetId());
                    newBookings.NewBookings();
                    MainWindow.BKSystem.RegisterBooking(newBookings, newBookingCustomer.GetId());

                    NewBookingWindow newBookingWindow = new NewBookingWindow(newBookings, newBookingCustomer);
                    newBookingWindow.ShowDialog();
                    newBookings.UpdateDB();
                    this.UpdateCustomerBookings();
                    this.UpdateBookings();
                    break;

                case "btnAddBooking":
                    CustomerBookings bookings;
                    Customer tempCustomer = (Customer)this.listCustomers.SelectedItem;

                    if (tempCustomer == null)
                        break;

                    if (this.listCustomerBookings.SelectedIndex != -1)
                        bookings = (CustomerBookings)this.listCustomerBookings.SelectedItem;
                    else if (this.listCustomers.SelectedIndex != -1)
                    {
                        bookings = new CustomerBookings(tempCustomer.GetId());
                    }
                    else
                        break;

                    NewBookingWindow bookingWindow = new NewBookingWindow(bookings, tempCustomer);
                    bookingWindow.ShowDialog();
                    bookings.UpdateDB();
                    this.UpdateCustomerBookings();
                    this.UpdateBookings();
                    break;

                case "btnEditBooking":
                    if (this.listBookings.SelectedIndex != -1)
                    {
                        EditBookingWindow editBookingWindow = new EditBookingWindow((Booking)this.listBookings.SelectedItem);
                        editBookingWindow.ShowDialog();
                    }
                    break;
            }
        }

        private void listCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ListCustomersUpdated = true;
            this.UpdateCustomerBookings();
            this.listBookings.ItemsSource = null;
            this.listBookings.Items.Refresh();
        }

        private void listBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListCustomersUpdated == false)
            {
                this.UpdateBookings();
            }
        }

        public ListView GetCustomerList()
        {
            return this.listCustomers;
        }
    }
}
