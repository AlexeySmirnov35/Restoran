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
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage 
    {
        private int _roleId;
        public int userRoleId;

        public RoomsPage(int to)
        {
            InitializeComponent();
            LVieew.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
            TboxSerch.Text=to.ToString();
            userRoleId = to;
        }
        
      


        private void Btn_Podr(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new InfoRoomPage((sender as Button).DataContext as Rooms));
            NavigationService.Navigate(new FormPage(userRoleId));
        }

        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {

        }
    }
}
