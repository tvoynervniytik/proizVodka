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
    /// Логика взаимодействия для ClientsEditPage.xaml
    /// </summary>
    public partial class ClientsEditPage : Page
    {
        public static List<ClientTypes> types { get; set; }
        private Clients clien;
        public ClientsEditPage(Clients client)
        {
            InitializeComponent();
            clien = client;
            types = new List<ClientTypes>(DBConnection.mop.ClientTypes.ToList());
            surnameTb.Text = clien.Surname;
            nameTb.Text = clien.Name;
            patronymicTb.Text = clien.Patronymic;
            phoneTb.Text = clien.Phone;
            emailTb.Text = clien.Email;
            typesCb.ItemsSource = types;
            typesCb.SelectedItem = types.FirstOrDefault(i=>i.ID == client.ClientTypeID);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (surnameTb.Text == "" ||
                nameTb.Text == "" ||
                patronymicTb.Text == "" ||
                emailTb.Text == "" ||
                phoneTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else 
            {
                var a = typesCb.SelectedItem as ClientTypes;
                clien.Name = nameTb.Text;
                clien.Email = emailTb.Text;
                clien.Phone = phoneTb.Text;
                clien.Surname = surnameTb.Text;
                clien.Patronymic = patronymicTb.Text;
                if (a != null)
                    clien.ClientTypeID = a.ID; 
                DBConnection.mop.SaveChanges();

                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new ClientsPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }
    }
}
