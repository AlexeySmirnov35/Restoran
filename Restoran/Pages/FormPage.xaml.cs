using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace Restoran.Page
{
    /// <summary>
    /// Логика взаимодействия для FormPage.xaml
    /// </summary>
    public partial class FormPage
    {
        Reservations _reservations=new Reservations();
        private int _idRole;
        private bool IsEditMode;

        public FormPage(Reservations selres,int t)
        {
            InitializeComponent();
            cbRoom.ItemsSource = RestoranEntities.GetContext().Rooms.ToList();
            for (int i = 1; i <= 10; i++)
            {
                cbHour.Items.Add(i);
            }
            for (int i = 9; i <= 20; i++)
            {
                cbTime.Items.Add(new DateTime(1, 1, 1, i, 0, 0).ToString("HH:mm"));
            }
            if (selres != null)
            {
                IsEditMode = true;
                _reservations =selres;
                tbName.Text = _reservations.User.Name;
                tbSur.Text = _reservations.User.Surname;
                tbPat.Text = _reservations.User.Patranomic;
                tbPhone.Text = _reservations.User.PhoneNumber;
                cbTime.SelectedItem = _reservations.DateTimeReserv.Value.ToString("HH:mm");
                cbHour.SelectedItem = _reservations.Hours;
                cbRoom.SelectedItem = _reservations.Rooms;
                tbDop.Text = _reservations.DopTimeMinut.ToString();


            }
            _idRole = t;
            DataContext= _reservations;
            datePicker.DisplayDateStart = DateTime.Now;
            datePicker.DisplayDateEnd = DateTime.Now.AddMonths(1);
            cbRoom.SelectionChanged += RoomComboBox_SelectionChanged;
            datePicker.SelectedDateChanged += DatePicker_SelectedChanged;
            
            if (_idRole == 2)
            {
                tbDop.Visibility = Visibility.Collapsed;
                tblock.Visibility = Visibility.Collapsed;
                datePicker.IsEnabled = false;
                cbTime.IsEnabled = false;
            }
        }

        private void CreateReq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User existingUser = RestoranEntities.GetContext().User.FirstOrDefault(x => x.PhoneNumber == tbPhone.Text);
                string pattern = @"^\+7\s?\d{10}$";
                Regex regex = new Regex(pattern);

                if (!regex.IsMatch(tbPhone.Text))
                {
                    MessageBox.Show("Неверный номер телефона");
                    return;
                }

             
                    // В режиме создания новой записи
                    if (existingUser == null)
                    {
                        // Создаем нового пользователя, если не существует
                        User newUser = new User()
                        {
                            Name = tbName.Text,
                            Surname = tbSur.Text,
                            Patranomic = tbPat.Text,
                            PhoneNumber = tbPhone.Text,
                            RoleID = 2,
                        };

                        RestoranEntities.GetContext().User.Add(newUser);
                        RestoranEntities.GetContext().SaveChanges();

                        CreateReservation(newUser.UserID);
                    }
                    else
                    {
                        // Используем существующего пользователя
                        CreateReservation(existingUser.UserID);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении бронирования: {ex.Message}");
            }
        }

        private void DatePicker_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate.HasValue && !IsEditMode)
            {
                var room = cbRoom.SelectedItem as Rooms;

                if (room != null)
                {
                    int roomId = room.RoomID;
                    var reservations = RestoranEntities.GetContext().Reservations
                        .Where(r => r.RoomID == roomId && DbFunctions.TruncateTime(r.DateTimeReserv) == selectedDate)
                        .ToList();
                    var allHours = Enumerable.Range(9, 12).ToList();
                    foreach (var reservation in reservations)
                    {
                        int startHour = reservation.DateTimeReserv.Value.Hour;
                        int endHour = reservation.DateEndReserv.Value.Hour;
                        if (reservation.DopTimeMinut > 0)
                        {
                            allHours.RemoveAll(h => h >= startHour && h <= endHour);
                        }
                        allHours.RemoveAll(h => h >= startHour && h < endHour);
                    }
                    cbTime.IsEnabled = true;
                    cbTime.Items.Clear();
                    foreach (var hour in allHours)
                    {
                        cbTime.Items.Add(new DateTime(1, 1, 1, hour, 0, 0).ToString("HH:mm"));
                    }
                }
                else
                {
                    MessageBox.Show("Выберите комнату перед выбором даты.");
                }
            }
        }

        private void CreateReservation(int userId)
        {
            try
            {
                StringBuilder errors = new StringBuilder();

                var hour = cbTime.SelectedItem;
                var room = cbRoom.SelectedItem as Rooms;
                var hourbron = cbHour.SelectedItem;
                int selectedHours;
                if (room == null || hour == null || hourbron == null || string.IsNullOrWhiteSpace(tbNum.Text)) 
                {
                    errors.AppendLine("Выберите комнату, время и количество часов.");
                }
                if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbSur.Text) )
                {
                    errors.AppendLine("Напишите имя и фамилию");
                }
                if (string.IsNullOrWhiteSpace(tbDop.Text) && _idRole!=2)
                {
                    errors.AppendLine("Не ввели доп время");
                }
                if (!int.TryParse(cbHour.SelectedItem?.ToString(), out selectedHours) || selectedHours <= 0)
                {
                    errors.AppendLine("Выберите количество часов.");
                }

                if (!int.TryParse(tbNum.Text, out int numberOfPeople) || numberOfPeople < 1)
                {
                    errors.AppendLine("Количество людей должно быть больше 1.");
                }
                if (room != null && room.NumberPeopleMax < numberOfPeople)
                {
                    errors.AppendLine("Количество людей не может превышать максимальное количество людей в комнате.");
                }
                int selectedMinutes;
                if (!int.TryParse(tbDop.Text, out selectedMinutes) || selectedMinutes < 0)
                {
                    errors.AppendLine("Введите корректное значение для дополнительных минут.");
                }
                if(cbTime.SelectedItem == null)
                {
                    errors.AppendLine("Нужно выбрать количество времени");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                string selectedTimeString = cbTime.SelectedItem.ToString();
                DateTime selectedTime = DateTime.ParseExact(hour.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                int selectedHour = selectedTime.Hour;
                
                string fios = $"{tbSur.Text} {tbName.Text} {tbPat.Text}";
                Reservations newd = new Reservations();
                DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now.Date;
                DateTime combinedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedTime.Hour, selectedTime.Minute, 0);
                newd.DateTimeReserv = combinedDateTime;
                if (selectedHours + selectedHour > 22)
                {
                    MessageBox.Show("Выбранное количество часов не может превышать 22 часа.");
                }
                var endDate = newd.DateTimeReserv.Value.AddHours(selectedHours).AddMinutes(selectedMinutes);
                
                _reservations.DateTimeReserv = combinedDateTime;
                _reservations.Hours = selectedHours;
                _reservations.DopTimeMinut = selectedMinutes;
                _reservations.DateEndReserv = endDate;
                _reservations.FIO = $"{tbSur.Text} {tbName.Text} {tbPat.Text}";
                

                if (IsEditMode)
                {
                    var editOverlappingReservations = RestoranEntities.GetContext().Reservations
                .Where(r => r.RoomID == room.RoomID && r.ReservationsID != _reservations.ReservationsID &&
                    ((combinedDateTime >= r.DateTimeReserv && combinedDateTime < r.DateEndReserv) ||
                    (endDate > r.DateTimeReserv && endDate <= r.DateEndReserv) ||
                    (r.DateTimeReserv >= combinedDateTime && r.DateTimeReserv < endDate) ||
                    (r.DateEndReserv > combinedDateTime && r.DateEndReserv <= endDate)))
                .ToList();
                    if (editOverlappingReservations.Any())
                    {
                        MessageBox.Show("Выбранное количество часов и минут пересекается с другим бронированием.");
                        return;
                    }

                    // Вносим изменения в текущую запись
                    _reservations.DateTimeReserv = combinedDateTime;
                    _reservations.Hours = selectedHours;
                    _reservations.DopTimeMinut = selectedMinutes;
                    _reservations.DateEndReserv = endDate;
                    _reservations.FIO = $"{tbSur.Text} {tbName.Text} {tbPat.Text}";

                    RestoranEntities.GetContext().Entry(_reservations).State = EntityState.Modified;
                    RestoranEntities.GetContext().SaveChanges();
                }
                else
                {
                    // В режиме создания новой записи выполняем проверку на перекрывающиеся бронирования
                    var overlappingReservations = RestoranEntities.GetContext().Reservations
                        .Where(r => r.RoomID == room.RoomID &&
                            ((newd.DateTimeReserv >= r.DateTimeReserv && newd.DateTimeReserv < r.DateEndReserv) ||
                            (endDate > r.DateTimeReserv && endDate <= r.DateEndReserv)))
                        .ToList();

                    if (overlappingReservations.Any())
                    {
                        MessageBox.Show("Выбранное количество часов и минут не может быть забронировано.");
                        return;
                    }

                    // Создаем новую запись
                    Reservations reservation = new Reservations()
                    {
                        UserID = userId,
                        FIO = fios,
                        RoomID = room.RoomID,
                        NumberOfPeople = Convert.ToInt32(tbNum.Text),
                        Hours = selectedHours,
                        DopTimeMinut = selectedMinutes,
                        DateTimeReserv = newd.DateTimeReserv,
                        DateEndReserv = endDate
                    };

                    RestoranEntities.GetContext().Reservations.Add(reservation);
                    RestoranEntities.GetContext().SaveChanges();
                    MessageBox.Show("Бронирование успешно сохранено");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении бронирования: {ex.Message}");
            }

            DatePicker_SelectedChanged(datePicker, null);
        }
      
        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!IsEditMode)
            {
            datePicker.IsEnabled = cbRoom.SelectedItem != null;
            cbTime.IsEnabled = false;
            }
           
        }
    }
}
