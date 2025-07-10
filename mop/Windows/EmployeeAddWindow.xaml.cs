using mop.DB;
using mop.Pages;
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

namespace mop.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAddWindow.xaml
    /// </summary>
    public partial class EmployeeAddWindow : Window
    {
        public static List<Posts> posts { get; set; }
        public static List<Education> educations { get; set; }
        public static List<Brigades> brigades { get; set; }
        public EmployeeAddWindow()
        {
            InitializeComponent();
            posts = new List<Posts>(DBConnection.mop.Posts.ToList());
            educations = new List<Education>(DBConnection.mop.Education.ToList());
            brigades = new List<Brigades>(DBConnection.mop.Brigades.ToList());
            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var educ = educCb.SelectedItem as Education;
            var post = postsCb.SelectedItem as Posts;
            var c = brigadesCb.SelectedItem as Brigades;
            if (surnameTb.Text == "" ||
            nameTb.Text == "" ||
            patronymicTb.Text == "" ||
            loginTb.Text == "" ||
            passwordTb.Text == "" ||
            passportTb.Text == "" ||
            emailTb.Text == "" ||
            phoneTb.Text == "" ||
            educ == null ||
            posts == null || (c == null & post.ID==1))
                MessageBox.Show("Заполните все данные!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Employees employee = new Employees();
                employee.Name = nameTb.Text.Trim();
                employee.Surname = surnameTb.Text.Trim();
                employee.Patronymic = patronymicTb.Text.Trim();
                employee.Email = emailTb.Text.Trim();
                employee.EducationID = educ.ID;
                employee.PostID = post.ID;
                employee.Passport = passportTb.Text.Trim();
                employee.Password = passwordTb.Text.Trim();
                employee.Login = loginTb.Text.Trim();
                employee.Phone = phoneTb.Text.Trim();
                if (post.ID == 1)
                {
                    employee.BrigadeID = c.ID;
                }
                else
                    employee.BrigadeID = null;

                DBConnection.mop.Employees.Add(employee);
                DBConnection.mop.SaveChanges();
                this.Close();
                NavigationService.GetNavigationService(new EmployeesPage());
                MessageBox.Show($"Сотрудник {surnameTb.Text} {nameTb.Text.First()}. {patronymicTb.Text.First()}. успешно добавлен");
            }
        }

        private void postsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = postsCb.SelectedItem as Posts;
            if (a.ID == 1)
            {
                brigadeTb.Visibility = Visibility.Visible;
                brigadesCb.Visibility = Visibility.Visible;
            }
            else
            {
                brigadeTb.Visibility = Visibility.Hidden;
                brigadesCb.Visibility = Visibility.Hidden;
            }
        }
    }
}
