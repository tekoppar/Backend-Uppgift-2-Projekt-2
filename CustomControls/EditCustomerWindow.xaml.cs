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

namespace Projekt2FuckAll.CustomControls
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        public Customer dp { get; set; }

        public EditCustomerWindow(Customer customer)
        {
            InitializeComponent();
            this.dp = customer;
            this.listContactInfo.ItemsSource = this.dp.ContactInfos;
            this.listContactInfo.DisplayMemberPath = nameof(ContactInfo.ContactData);
            this.tbFirstName.Text = this.dp.FirstName;
            this.tbLastName.Text = this.dp.LastName;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnContactInfo":
                    ContactInfoWindow infoW = new ContactInfoWindow();
                    infoW.ShowDialog();

                    if (infoW.info != null)
                    {
                        this.dp.AddContactInfo(infoW.info);
                        this.listContactInfo.Items.Refresh();
                    }
                    break;
                case "btnRemoveContactIfno":
                    if (this.listContactInfo.SelectedItem != null)
                    {
                        this.dp.RemoveContactInfo((ContactInfo)this.listContactInfo.SelectedItem);
                        this.listContactInfo.Items.Refresh();
                    }
                    break;
                case "btnAccept":
                    this.dp.FirstName = tbFirstName.Text;
                    this.dp.LastName = tbLastName.Text;
                    this.dp.UpdateDB();
                    this.Close();
                    break;
                case "btnCancel": this.Close(); break;
            }
        }
    }
}
