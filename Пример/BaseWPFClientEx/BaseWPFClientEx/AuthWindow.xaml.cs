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
using System.Windows.Threading;

namespace BaseWPFClientEx
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        // Контекст бызы данных
        private Data.LibraryExEntities Database;

        // Максимальное количество попыток ввода пароля до блокировки
        private static int StandartAttempts = 3;

        // Количество оставшихся попыток ввода пароля до блокировки
        private int _Attempts = StandartAttempts;
        public int Attempts
        {
            get
            {
                return _Attempts;
            }
            set
            {
                if (_Attempts != value)
                {
                    _Attempts = value;
                    // Обновление значения количество оставшихся попыток в интерфейсе
                    AttemptsLabel.Content = _Attempts;
                }
            }
        }

        // Время блокировки интерфейса в случае достижения максимального количества
        // попыток ввода пароля
        private static TimeSpan StandartBlockingTime = new TimeSpan(0, 1, 0);

        // Оставшееся время блокировки интерфейса
        private TimeSpan _BlockingTime;
        public TimeSpan BlockingTime
        {
            get
            {
                return _BlockingTime;
            }
            set
            {
                if (value.TotalSeconds < 0)
                {
                    // Сброс в нуль времени блокировки, если оно отрицательное
                    // (такая ситуация почти всегда возникает при выводе окна на экран, 
                    // когда зафиксированное время начала блокировки очень давнее и
                    // оно явно перекрывает стандартный период блокировки)
                    value = new TimeSpan(0, 0, 0);
                }
                if (_BlockingTime != value)
                {
                    _BlockingTime = value;
                    if (_BlockingTime.TotalSeconds > 0)
                    {
                        // Вывод в окне информации об оставшемся времени блокировки, если оно еше не закончилось
                        ShowBlockingMessage();
                    }
                    else
                    {
                        // Сокрытие информации в окне о времени блокировки после его окончания
                        HideBlockingMessage();
                    }
                    // Вывод времени в окно
                    // ВНИМАНИЕ! Строка форматирования должна иметь специфичный вид, так как,
                    // формально, значение часов в данном случае не ограничено 24!
                    TimerLabel.Content = _BlockingTime.ToString("hh':'mm':'ss");
                }
            }
        }

        // Таймер обновления времени в интерфейсе
        private DispatcherTimer Timer;

        // Конструктор
        public AuthWindow()
        {
            InitializeComponent();
            // Создание внутренних структур данных окна приложения и инициализация
            // переменных
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += BlockingTimer_Tick;
            // Подключение к базе данных
            try
            {
                Database = new Data.LibraryExEntities();
            }
            catch
            {
                // Вывод сообщения о неверно введенном пароле
                MessageBox.Show("Не удалось подключиться к базе данных. Проверьте настройки подключения приложения.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        // Метод подготовки окна к работе после его загрузки
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Вычисление оставшегося времени блокировки
            BlockingTime = StandartBlockingTime.Subtract(DateTime.Now - Properties.Settings.Default.StartBlockingTime);
        }

        // Метод запуска блокировки интефеса
        private void ShowBlockingMessage()
        {
            // Вывод сообщения 
            TimerDockPanel.Visibility = Visibility.Visible;
            Height = 200;
            // Блокировка интерфейса
            OkButton.IsEnabled = false;
            // Запуск таймера
            Timer.Start();
        }

        // Метод завершения блокировки
        private void HideBlockingMessage()
        {
            // Остановка таймера
            Timer.Stop();
            // Сокрытие сообщения 
            TimerDockPanel.Visibility = Visibility.Hidden;
            Height = 180;
            // Разблокировка интерфейса
            OkButton.IsEnabled = true;
        }

        // Метод таймера
        private void BlockingTimer_Tick(object sender, EventArgs e)
        {
            // Уменьшение оставшегося времени блокировки (в интерфейсе пользователя!)
            BlockingTime = BlockingTime.Subtract(new TimeSpan(0, 0, 1));
        }

        // Метод для показа и сокрытия пароля
        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Переброска необходимой информации во временные буферы
            String Password = PasswordPasswordBox.Password;
            Visibility Visibility = PasswordPasswordBox.Visibility;
            double Width = PasswordPasswordBox.ActualWidth;
            // Изменение подписи на кнопке
            PasswordButton.Content = Visibility == Visibility.Visible ? "Скрыть" : "Показать";
            // Переброска информации из TextBox'а в PasswordBox
            PasswordPasswordBox.Password = PasswordTextBox.Text;
            PasswordPasswordBox.Visibility = PasswordTextBox.Visibility;
            PasswordPasswordBox.Width = PasswordTextBox.Width;
            // Возврат информации из временных буферов в TextBox
            PasswordTextBox.Text = Password;
            PasswordTextBox.Visibility = Visibility;
            PasswordTextBox.Width = Width;
        }

        // Метод кнопки ОК
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Выборка пароля из PasswordBox'а или TextBox'а
            String Password = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
            // Проверка валидности пароля
            Data.Users User = Database.Users.SingleOrDefault(P => P.Login == LoginTextBox.Text && P.Password == Password);
            if (User != null)
            {
                // При соответствии пароля в настройку приложения StartBlockingTime производится запись
                // минимально возможного времени (вроде того, что блокировка случилась очень давно...)
                Properties.Settings.Default.StartBlockingTime = DateTime.MinValue;
                // Сохранение настроек приложения
                Properties.Settings.Default.Save();
                // Вывод главного окна приложения на экран
                MainWindow Window = new MainWindow(Database, (int)User.IdRole);
                Window.Show();
                // Закрытие окна
                Close();
            }
            else
            {
                // Вывод сообщения о неверно введенном пароле
                MessageBox.Show("Неверно указан пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Уменьшение количества попыток
                Attempts--;
                // Вывод (точнее - показ) в окне сообщения об оставшемся количестве попыток ввода пароля
                AttemptsDockPanel.Visibility = Visibility.Visible;
                // Если попытки закончились, то...
                if (Attempts == 0)
                {
                    // В настройку приложения StartBlockingTime производится запись текущего времени,
                    // которое будет считаться моментом начала блокировки
                    Properties.Settings.Default.StartBlockingTime = DateTime.Now;
                    // Сохранение настроек приложения (на случай его перезагрузки!)
                    Properties.Settings.Default.Save();
                    // Непосредственно установка времени блокировки в пользовательском интерфейсе,
                    // время блокировки будет выведено в окно в методе set свойства BlockingTime
                    BlockingTime = StandartBlockingTime;
                    // Сокрытие в окне сообщения об оставшемся количестве попыток ввода пароля
                    AttemptsDockPanel.Visibility = Visibility.Hidden;
                    // Сброс количества попыток ввода пароля в начальное значение
                    Attempts = StandartAttempts;
                }
            }
        }

        // Метод кнопки Отмена
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Метод кнопки Регистрация
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow Window = new RegistrationWindow(Database);
            Window.ShowDialog();
        }
    }
}
