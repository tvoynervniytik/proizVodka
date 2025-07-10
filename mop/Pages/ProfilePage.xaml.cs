using mop.DB;
using mop.Functions;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public static List<Education> educations { get; set; }
        private string name;
        private string surname;
        private string patronymic;
        private string email;
        private string phone;
        private string passport;
        private string login;
        private string password;
        private int education;
        public ProfilePage()
        {
            InitializeComponent();
            educations = new List<Education>(DBConnection.mop.Education.ToList());

            if (AuthorizationFunc.loggedUser.PostID == 3) //empl mng
            {
                requestBr.Visibility = Visibility.Hidden;
                editBtn.Content = "сохранить изменения";
            }

            Employees employee = AuthorizationFunc.loggedUser;
            nameTb.Text = employee.Name;
            name = employee.Name;
            //
            surnameTb.Text = employee.Surname;
            surname = employee.Surname;
            //
            emailTb.Text = employee.Email;
            email = employee.Email;
            //
            Education education = educations.FirstOrDefault(x => x.ID == employee.EducationID);
            educationCb.SelectedItem = education;
            //
            patronymicTb.Text = employee.Patronymic;
            patronymic = employee.Patronymic;
            //
            passportTb.Text = employee.Passport;
            passport = employee.Passport;
            //
            passwordTb.Text = employee.Password;
            password = employee.Password;
            //
            loginTb.Text = employee.Login;
            login = employee.Login;
            //
            phoneTb.Text = employee.Phone;
            phone = employee.Phone;

            educationCb.ItemsSource = educations;

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Employees employee = AuthorizationFunc.loggedUser;
            if (AuthorizationFunc.loggedUser.PostID == 3) //empl mng
            {
                MessageBox.Show("Данные сохранены");
                employee.Name = nameTb.Text.Trim();
                employee.Surname = surnameTb.Text.Trim();
                employee.Email = emailTb.Text.Trim();
                employee.EducationID = education;
                employee.Patronymic = patronymicTb.Text.Trim();
                employee.Passport = passportTb.Text.Trim();
                employee.Password = passwordTb.Text.Trim();
                employee.Login = loginTb.Text.Trim();
                employee.Phone = phoneTb.Text.Trim();

                DBConnection.mop.SaveChanges();
            }
            else
            {
                MessageBox.Show("Запрос отправлен");
                if (employee.Surname != surname)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "фамилия";
                    request.ItemEditted = surnameTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Name != name)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "имя";
                    request.ItemEditted = nameTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Patronymic != patronymic)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "отчество";
                    request.ItemEditted = patronymicTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Login != login)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "логин";
                    request.ItemEditted = loginTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Password != password)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "пароль";
                    request.ItemEditted = passwordTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Passport != passport)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "паспорт";
                    request.ItemEditted = passportTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Email != email)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "почта";
                    request.ItemEditted = emailTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.Phone != phone)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "телефон";
                    request.ItemEditted = phoneTb.Text.Trim();
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                if (employee.EducationID != education)
                {
                    Requests request = new Requests();
                    request.EmployeeID = employee.ID;
                    request.AtributeName = "образование";
                    request.Date = DateTime.Now;
                    request.Checking = false;
                    request.Done = false;
                    request.ItemEditted = DBConnection.mop.Education.FirstOrDefault(i=> i.ID == education).Name;
                    DBConnection.mop.Requests.Add(request);
                    DBConnection.mop.SaveChanges();
                }
                
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationFunc.loggedUser.PostID == 1)
                NavigationService.Navigate(new MenuFirstPage());
            else
                NavigationService.Navigate(new MenuSecondPage());
        }

        private void requestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestsPage());
        }

        private void surnameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname = surnameTb.Text.Trim();
        }

        private void nameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = nameTb.Text.Trim();
        }

        private void patronymicTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            patronymic = patronymicTb.Text.Trim();
        }

        private void passportTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            passport = passportTb.Text.Trim();
        }

        private void phoneTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            phone = phoneTb.Text.Trim();
        }

        private void educationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = educationCb.SelectedItem as Education;
            education = a.ID;
        }

        private void loginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            login = loginTb.Text.Trim();
        }

        private void passwordTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = passwordTb.Text.Trim();
        }

        private void emailTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = emailTb.Text.Trim();
        }
    }
}
