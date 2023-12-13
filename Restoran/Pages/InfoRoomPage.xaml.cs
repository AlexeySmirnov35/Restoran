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
    /// Логика взаимодействия для InfoRoomPage.xaml
    /// </summary>
    public partial class InfoRoomPage
    {
        private Rooms _rooms= new Rooms();
        public InfoRoomPage(Rooms selrooms)
        {
            InitializeComponent();
            if (selrooms != null)
            {
                _rooms = selrooms;
            }
           
            DataContext = _rooms;
            
        }

        private void Btn_Reserv_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FormPage());
        }
    }
}
