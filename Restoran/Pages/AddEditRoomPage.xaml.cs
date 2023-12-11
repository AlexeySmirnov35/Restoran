using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditRoomPage.xaml
    /// </summary>
    public partial class AddEditRoomPage 

    {
        private Rooms _rooms = new Rooms();
        private byte[] img = null;
        public AddEditRoomPage(Rooms selrooms)
        {
            InitializeComponent();
            if (selrooms != null)
            {
                _rooms = selrooms;
            }

            DataContext = _rooms;
           
        }
        OpenFileDialog fileOpen = new OpenFileDialog();
        private void Image_Load(object sender, RoutedEventArgs e)
        {
            fileOpen.Title = "imgSele";
            fileOpen.Multiselect = false;
            fileOpen.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (fileOpen.ShowDialog() == true)
            {

                img = File.ReadAllBytes(fileOpen.FileName);
                ImageV.Source = new ImageSourceConverter()
                   .ConvertFrom(img) as ImageSource;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_rooms.RoomID == 0)
            {
                var room = new Rooms
                {
                   NameRoom=tbName.Text,
                   Description=tbDesc.Text,
                   Photo=img
                };
                RestoranEntities.GetContext().Rooms.Add(room);

            }


            try
            {
                RestoranEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно сохранено");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
