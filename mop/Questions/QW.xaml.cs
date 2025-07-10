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
using System.Windows.Shapes;

namespace mop.Questions
{
    /// <summary>
    /// Логика взаимодействия для QW.xaml
    /// </summary>
    public partial class QW : Window
    {
        public QW()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorizationFunc.SecondPass == pasportTb.Password)
            {
                this.Close();
                AuthorizationFunc.Question = true;
            }
            else
                MessageBox.Show("Неверные данные");
        }
    }
}
