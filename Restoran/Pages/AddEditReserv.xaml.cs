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

namespace Restoran.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditReserv.xaml
    /// </summary>
    public partial class AddEditReserv 
    {
        
        public AddEditReserv(int role)
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
            datePicker.DisplayDateStart = DateTime.Now;
            datePicker.DisplayDateEnd = DateTime.Now.AddMonths(1);
            cbRoom.SelectionChanged += RoomComboBox_SelectionChanged;
            datePicker.SelectedDateChanged += DatePicker_SelectedChanged;
            datePicker.IsEnabled = false;
            cbTime.IsEnabled = false;
            if (role == 2)
            {
                tbDop.Visibility = Visibility.Collapsed;
                
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
                    MessageBox.Show("number");
                    return;
                }
                if (existingUser == null)
                {
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

            if (selectedDate.HasValue)
            {
                var room = cbRoom.SelectedItem as Rooms;

                if (room != null)
                {
                    int roomId = room.RoomID;

                    var reservations = RestoranEntities.GetContext().Reservations
                        .Where(r => r.RoomID == roomId && DbFunctions.TruncateTime(r.DateTimeReserv) == selectedDate)
                        .ToList();
                    var reserhour = RestoranEntities.GetContext().Reservations.Where(r => r.RoomID == roomId);
                    var hours = reserhour.Select(r => r.Hours).ToList();
                    var bookedHours = reservations.SelectMany(r => Enumerable.Range(r.DateTimeReserv.Value.Hour, (int)r.Hours));
                    var hourst = reservations.Select(r => r.DateTimeReserv.Value.Hour).ToList();
                    var allHours = Enumerable.Range(9, 12).ToList();
                    var availableHours = allHours.Except(bookedHours);

                    cbTime.IsEnabled = true;

TimeSpan totalReservationTime = GetTotalReservationTime(room.RoomID, selectedDate.Value);
            TimeSpan averageReservationTime = GetAverageReservationTime(room.RoomID, selectedDate.Value);

            // Обновление значений в TextBlock
            txtTotalReservationTime.Text = $"Общее время бронирования: {totalReservationTime.TotalHours} часов";
            txtAverageReservationTime.Text = $"Среднее время бронирования: {averageReservationTime.TotalHours} часов";
                    cbTime.Items.Clear();
                    if (reservations.Any())
                    {
                        
                        for (int i = 9; i <= 20; i++)
                        {
                            if (!bookedHours.Contains(i))
                            {
                                cbTime.Items.Add(new DateTime(1, 1, 1, i, 0, 0).ToString("HH:mm"));
                            }
                        }
                    }
                    else
                    {
                        for (int x = 9; x <= 20; x++)
                        {
                            cbTime.Items.Add(new DateTime(1, 1, 1, x, 0, 0).ToString("HH:mm"));
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Выберите комнату перед выбором даты.");
                }

            }
        }

        private TimeSpan GetTotalReservationTime(int roomId, DateTime selectedDate)
        {
            var reservationsForRoomAndDate = RestoranEntities.GetContext().Reservations
                .Where(r => r.RoomID == roomId && DbFunctions.TruncateTime(r.DateTimeReserv) == selectedDate)
                .ToList();

            // Вычислить общее время бронирования для выбранной комнаты и даты
            TimeSpan totalReservationTime = TimeSpan.Zero;
            foreach (var reservation in reservationsForRoomAndDate)
            {
                totalReservationTime += (reservation.DateEndReserv - reservation.DateTimeReserv).Value;
            }

            return totalReservationTime;
        }

        // Метод для вывода среднего времени бронирования для выбранной комнаты и даты
        private TimeSpan GetAverageReservationTime(int roomId, DateTime selectedDate)
        {
            var reservationsForRoomAndDate = RestoranEntities.GetContext().Reservations
                .Where(r => r.RoomID == roomId && DbFunctions.TruncateTime(r.DateTimeReserv) == selectedDate)
                .ToList();

            // Проверить, есть ли бронирования, чтобы избежать деления на ноль
            if (reservationsForRoomAndDate.Count > 0)
            {
                // Вычислить среднее время бронирования для выбранной комнаты и даты
                TimeSpan totalReservationTime = TimeSpan.Zero;
                foreach (var reservation in reservationsForRoomAndDate)
                {
                    totalReservationTime += (reservation.DateEndReserv - reservation.DateTimeReserv).Value;
                }

                // Используйте TotalMinutes для получения общего времени в минутах и затем выполните деление на количество бронирований
                double averageMinutes = totalReservationTime.TotalMinutes / reservationsForRoomAndDate.Count;

                TimeSpan averageReservationTime = TimeSpan.FromMinutes(averageMinutes);
                return averageReservationTime;
            }

            return TimeSpan.Zero;
        }



        private void CreateReservation(int userId)
        {
            try
            {
                StringBuilder errors = new StringBuilder();

                var hour = cbTime.SelectedItem;
                var room = cbRoom.SelectedItem as Rooms;
                if (room == null || hour == null)
                {
                    errors.AppendLine("Выберите комнату и время");
                }
                if (room.NumberPeopleMax < Convert.ToInt32(tbNum.Text))
                {
                    errors.AppendLine("aaaa");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                var hourbron = cbHour.SelectedItem;
                int hour1 = Convert.ToInt32(hourbron.ToString());
                string fios = $"{tbSur.Text} {tbName.Text} {tbPat.Text}";
                Reservations newd = new Reservations();
                DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now.Date;
                DateTime selectedTime = DateTime.ParseExact(hour.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                DateTime combinedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedTime.Hour, selectedTime.Minute, 0);
                newd.DateTimeReserv = combinedDateTime;

                int selectedHours = Convert.ToInt32(hourbron.ToString());
                int selectedMinutes = Convert.ToInt32(tbDop.Text);

                var endDate = newd.DateTimeReserv.Value.AddHours(selectedHours).AddMinutes(selectedMinutes);

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
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении бронирования: {ex.Message}");
            }
            DatePicker_SelectedChanged(datePicker, null);
        }

        private void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker.IsEnabled = cbRoom.SelectedItem != null;
            cbTime.IsEnabled = false;
        }
    }
}
