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
    /// Interaction logic for ContactInfoWindow.xaml
    /// </summary>
    public partial class ContactInfoWindow : Window
    {
        public ContactInfo info { get; set; }
        public ContactInfoWindow()
        {
            InitializeComponent();
            this.listContactTypes.ItemsSource = Enum.GetNames(typeof(ContactType));
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnAccept":
                    ContactType type = (ContactType)Enum.Parse(typeof(ContactType), (string)this.listContactTypes.SelectedItem);
                    this.info = new ContactInfo(this.textBox.Text, type);
                    this.Close();
                    break;
                case "btnCancel": this.Close(); break;
            }
        }
    }
}
