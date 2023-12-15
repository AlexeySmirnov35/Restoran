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
        
        public int userRoleId;

        public RoomsPage(int to)
        {
            InitializeComponent();
            LVieew.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
            
            userRoleId = to;
        }
        
      


        private void Btn_Podr(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InfoRoomPage((sender as Button).DataContext as Rooms,userRoleId));
            //NavigationService.Navigate(new FormPage(null,userRoleId));
        }
        private void UpdateRooms()
        {
            string searchText = TboxSerch.Text.ToLower();
            var allRooms = RestoranEntities.GetContext().Rooms.ToList();

            var filteredRooms = allRooms
                .Where(room =>
                    room.NameRoom.ToLower().Contains(searchText) ||
                    room.Description.ToLower().Contains(searchText))
                .ToList();
            switch (ComboFilter.SelectedIndex)
            {

                case 1:
                    
                    filteredRooms = filteredRooms.OrderBy(room => room.Price).ToList();
                    break;
                case 2:
                    
                    filteredRooms = filteredRooms.OrderByDescending(room => room.Price).ToList();
                    break;
                case 3:
                    
                    filteredRooms = filteredRooms.OrderBy(room => room.NameRoom).ToList();
                    break;
            }
            LVieew.ItemsSource = filteredRooms;
        }
        private void Tbox_Search(object sender, TextChangedEventArgs e)
        {
            UpdateRooms();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRooms();
        }
    }
}
