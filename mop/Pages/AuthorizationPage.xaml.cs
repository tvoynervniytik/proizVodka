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
using System.Drawing;
using System.Windows.Interop;
namespace mop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login;
            string password;
            login = loginTb.Text.Trim();
            password = passwordTb.Password.Trim();

            AuthorizationFunc.Authorization(login, password);

            if (AuthorizationFunc.loggedUser != null & AuthorizationFunc.Question)
            {
                if (AuthorizationFunc.loggedUser.PostID == 3)
                    NavigationService.Navigate(new MenuSecondPage());
                else
                    NavigationService.Navigate(new MenuFirstPage());
            }
        }
    }
}
