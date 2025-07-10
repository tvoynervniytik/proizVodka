using mop.DB;
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

namespace mop.Pages.editingPages
{
    /// <summary>
    /// Логика взаимодействия для AddressesEditPage.xaml
    /// </summary>
    public partial class AddressesEditPage : Page
    {
        public static List<Clients> clients { get; set; }
        private Address address1;
        public AddressesEditPage(Address address)
        {
            InitializeComponent();
            address1 = address;
            clients = new List<Clients>(DBConnection.mop.Clients.ToList());
            clientsCb.ItemsSource = clients;
            clientsCb.SelectedItem = clients.FirstOrDefault(i=>i.ID == address.ClientID);
            streetTb.Text = address1.Street;
            houseNumberTb.Text = address1.HouseDigit;
            houseNameTb.Text = address1.HouseLetter;
            roomTb.Text = address1.RoomNumber;
            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = clientsCb.SelectedItem as Clients;
            if (roomTb.Text == "" || streetTb.Text == "" || houseNumberTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (client != null) 
                    address1.ClientID = client.ID;
                address1.Street = streetTb.Text;
                address1.HouseNumber = address1.HouseCalc(houseNumberTb.Text.Trim(), houseNameTb.Text.Trim());
                address1.RoomNumber = roomTb.Text;
                DBConnection.mop.SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new AddressesPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddressesPage());

        }

        private void houseNumberTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(houseNumberTb.Text.Where(c => char.IsDigit(c)).ToArray());

            if (newText != houseNumberTb.Text)
            {
                int cursorPosition = houseNumberTb.SelectionStart;
                houseNumberTb.Text = newText;
                houseNumberTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }

        }
        private void houseNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(houseNameTb.Text.Where(c => char.IsLetter(c) || c == ' ').ToArray());

            if (newText != houseNameTb.Text)
            {
                int cursorPosition = houseNameTb.SelectionStart;
                houseNameTb.Text = newText;
                houseNameTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }
        }

        private void roomTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = new string(roomTb.Text.Where(c => char.IsDigit(c)).ToArray());

            if (newText != roomTb.Text)
            {
                int cursorPosition = houseNumberTb.SelectionStart;
                roomTb.Text = newText;
                roomTb.SelectionStart = cursorPosition > 0 ? cursorPosition - 1 : 0;
            }
        }
    }
}
