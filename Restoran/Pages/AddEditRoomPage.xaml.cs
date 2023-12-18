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
            StringBuilder errors = new StringBuilder();

            if (tbPrice.Text.Contains(",") || tbDopMinut.Text.Contains(",") || tbNumberMax.Text.Contains(","))
            {
                errors.AppendLine("Пожалуйста, не используйте запятую.");
            }

            if (string.IsNullOrWhiteSpace(tbDesc.Text))
            {
                errors.AppendLine("Напишите описание!");
            }

            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                errors.AppendLine("Напишите name!");
            }

            if (!int.TryParse(tbPrice.Text, out int price) || price < 1000)
            {
                errors.AppendLine("Цена должна быть целым числом и больше 1000!");
            }

            if (!int.TryParse(tbDopMinut.Text, out int dopPriceMinut) || dopPriceMinut < 1 || dopPriceMinut > 5)
            {
                errors.AppendLine("Дополнительная цена в минуту должна быть целым числом, больше 1 и меньше 5!");
            }

            if (!int.TryParse(tbNumberMax.Text, out int numberPeopleMax) || numberPeopleMax < 1 || numberPeopleMax >= 30)
            {
                errors.AppendLine("Количество людей должно быть целым числом, больше 1 и меньше 30!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_rooms.RoomID == 0)
            {
                var room = new Rooms
                {
                    NameRoom = tbName.Text,
                    Description = tbDesc.Text,
                    Price = Convert.ToInt32(tbPrice.Text),
                    DopPriceMinut = Convert.ToInt32(tbDopMinut.Text),
                    NumberPeopleMax = Convert.ToInt32(tbNumberMax.Text),
                    Photo = img
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

        private void Btn_GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
