using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Restoran.Page
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage 
    {
        public RegPage()
        {
            InitializeComponent();
            
            

            
        }

        private void Reg_Btn_Click(object sender, RoutedEventArgs e)
        {
           
            User newd = new User();
            /* DateTime selectedDate = datePicker.SelectedDate.HasValue ? datePicker.SelectedDate.Value : DateTime.Now.Date;
            DateTime selectedTime = DateTime.ParseExact(comboBoxTime.Text, "HH:mm", CultureInfo.InvariantCulture);

            DateTime combinedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedTime.Hour, selectedTime.Minute, 0);
            newd.Data = combinedDateTime;*/
            if (RestoranEntities.GetContext().User.Count(x => x.login == tbLog.Text) > 0)
            {
                MessageBox.Show("Такой пользователь уже есть", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Close();
                return;
            }
            try
            {
                User sotrrud = new User()
                {
                    login = tbLog.Text,
                    passwod = tbPas.Password,
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    RoleID = 2,
                  //  Data=newd.Data
                    

                };
                RestoranEntities.GetContext().User.Add(sotrrud);
                RestoranEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно регистрация");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
