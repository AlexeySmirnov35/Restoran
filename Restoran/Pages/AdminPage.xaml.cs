using Restoran.Page;
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

namespace Restoran.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage 
    {
        private int _role;
        public AdminPage(int roleId)
        {
            InitializeComponent();
            _role = roleId;
        }

        private void Btn_Room_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPage());
        }

        private void Btn_Resrev_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReservInfoPage(_role));
        }
        private void Reg_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("pages/RegPage.xaml", UriKind.Relative));
        }
    }
}
