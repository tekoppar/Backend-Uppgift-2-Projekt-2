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
    /// Interaction logic for NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        public Customer dp { get; set; }
        public List<ContactInfo> contactInfo = new List<ContactInfo>();

        public NewCustomerWindow()
        {
            InitializeComponent();
            this.listContactInfo.ItemsSource = this.contactInfo;
            this.listContactInfo.DisplayMemberPath = nameof(ContactInfo.ContactData);
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
                        this.contactInfo.Add(infoW.info);
                        this.listContactInfo.Items.Refresh();
                    }
                    break;
                case "btnAccept":
                    this.dp = new Customer(tbFirstName.Text, tbLastName.Text);
                    List<ContactInfo> infos = (List<ContactInfo>)this.listContactInfo.ItemsSource;
                    this.dp.AddContactInfo(infos);
                    this.Close();
                    break;
                case "btnCancel": this.Close(); break;
            }
        }
    }
}
