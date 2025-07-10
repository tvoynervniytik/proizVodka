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

namespace mop.Pages.addingPages
{
    /// <summary>
    /// Логика взаимодействия для ClientsAddPage.xaml
    /// </summary>
    public partial class ClientsAddPage : Page
    {
        public static List<ClientTypes> types {  get; set; }
        public ClientsAddPage()
        {
            InitializeComponent();
            types = new List<ClientTypes>(DBConnection.mop.ClientTypes.ToList());
            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients();
            var t = typesCb.SelectedItem as ClientTypes;
            if (t == null || surnameTb.Text == "" ||
                nameTb.Text == "" ||
                patronymicTb.Text == "" ||
                emailTb.Text == "" ||
                phoneTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                client.Name = nameTb.Text;
                client.Surname = surnameTb.Text;
                client.Patronymic = patronymicTb.Text;
                client.Phone = phoneTb.Text;
                client.Email = emailTb.Text;
                client.ClientTypeID = t.ID;
                DBConnection.mop.Clients.Add(client);
                DBConnection.mop.SaveChanges();
                MessageBox.Show($"Клиент {surnameTb.Text} {nameTb.Text.First()}. {patronymicTb.Text.First()}. успешно добавлен");
                NavigationService.Navigate(new ClientsPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }
    }
}
