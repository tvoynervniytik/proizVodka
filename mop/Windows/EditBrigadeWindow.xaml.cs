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
using System.Windows.Shapes;

namespace mop.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditBrigadeWindow.xaml
    /// </summary>
    public partial class EditBrigadeWindow : Window
    {
        public static List<Employees> employees { get; set; }
        private static Brigades brigades;
        public EditBrigadeWindow(Brigades brigade)
        {
            InitializeComponent();
            brigades = brigade;
            employees = new List<Employees>(DBConnection.mop.Employees.Where(i=>i.BrigadeID == brigade.ID).ToList());
            this.DataContext = this;
        }
        private void Refresh()
        {
            brigadesLv.ItemsSource = new List<Employees>(DBConnection.mop.Employees.Where(i => i.BrigadeID == brigades.ID).ToList());
            emplCb.ItemsSource = new List<Employees>(DBConnection.mop.Employees.Where(i => i.BrigadeID == brigades.ID).ToList());
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = emplCb.SelectedItem as Employees;
            if (a != null)
            {
                brigades.HeadID = a.ID;
                DBConnection.mop.SaveChanges();
                this.Close();
            }
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeesBrigade addEmployeesBrigade = new AddEmployeesBrigade(brigades);
            addEmployeesBrigade.Show();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var del = brigadesLv.SelectedItem as Employees;
            if (del != null)
            {
                del.BrigadeID = null;
                DBConnection.mop.SaveChanges();
                Refresh();
            }
        }

        private void udateBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
