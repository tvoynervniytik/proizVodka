using mop.DB;
using mop.Functions;
using mop.Windows;
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

namespace mop.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public static List<Employees> employees { get; set; }
        public EmployeesPage()
        {
            InitializeComponent();
            employees = new List<Employees>(DBConnection.mop.Employees.Where(i => i.PostID != 4).ToList());
           
            this.DataContext = this;
        }
        public void Refresh()
        {
            if (surnameTb.Text == "") 
            {
                employeesLv.ItemsSource = new List<Employees>(DBConnection.mop.Employees.Where(i => i.PostID != 4).ToList());
            }
            else
                employeesLv.ItemsSource = new List<Employees>(DBConnection.mop.Employees.Where(i=> i.PostID != 4 && 
            i.Surname.ToLower().StartsWith(surnameTb.Text.Trim().ToLower())).ToList());
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddWindow employeeAddWindow = new EmployeeAddWindow();
            employeeAddWindow.Show();
        }
        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var del = employeesLv.SelectedItem as Employees;
            if (employeesLv.SelectedItem != null)
            {
                DBConnection.mop.Employees.Remove(del);
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void surnameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
