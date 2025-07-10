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
    /// Логика взаимодействия для ServicesEditPage.xaml
    /// </summary>
    public partial class ServicesEditPage : Page
    {
        private Services servic;
        public ServicesEditPage(Services service)
        {
            InitializeComponent();
            servic = service;
            descriptionTb.Text = service.Description;
            nameTb.Text = service.Name;
            priceTb.Text = service.Price.ToString();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (priceTb.Text == "" || nameTb.Text == "" || descriptionTb.Text == "")
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                servic.Name = nameTb.Text;
                servic.Description = descriptionTb.Text;
                servic.Price = int.Parse(priceTb.Text);
                DBConnection.mop.SaveChanges();

                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new ServicesPage());
            }
        }
    }
}
