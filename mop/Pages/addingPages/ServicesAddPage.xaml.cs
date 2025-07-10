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
    /// Логика взаимодействия для ServicesAddPage.xaml
    /// </summary>
    public partial class ServicesAddPage : Page
    {
        public ServicesAddPage()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTb.Text == "" || priceTb.Text == "" || descriptionTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Services service = new Services();
                service.Name = nameTb.Text;
                service.Price = int.Parse(priceTb.Text);
                service.Description = descriptionTb.Text;

                DBConnection.mop.Services.Add(service);
                DBConnection.mop.SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new ServicesPage());
            }
        }
    }
}
