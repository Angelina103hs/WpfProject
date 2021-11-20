using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.Windows.Shapes;

namespace BaseWPFClientEx
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// 
    /// Captcha:
    /// https://blog.foolsoft.ru/c-pishem-prostuyu-captcha-zashhitu/
    /// https://calmsen.ru/delaem-svoyu-kapchu-asp-net-c/
    /// http://www.softez.pp.ua/2014/05/17/get-image-with-captcha-in-geckofx-csharp/

    /// </summary>
    public partial class RegistrationWindow : Window
    {
        // Контекст бызы данных
        private Data.LibraryExEntities Database;

        // Длина капчи
        private static int CaptchaTextLength = 5;
        
        // Капча
        private String CaptchaText;

        // Конструктор
        public RegistrationWindow(Data.LibraryExEntities Database)
        {
            InitializeComponent();
            this.Database = Database;
            // Изначальное создание капчи
            CaptchaImage.Source = CreateCaptcha();
        }

        // Метод преобразования изображения типа Bitmap в изображение 
        // типа BitmapImage
        public static BitmapImage BitmapToBitmapImage(Bitmap Bitmap)
        {
            // Создание потока
            MemoryStream MemoryStream = new MemoryStream();
            // Сохранение исходного изображения в поток
            Bitmap.Save(MemoryStream, ImageFormat.Png);
            // Установка курсора в начало потока в качестве указателя,
            // откуда начинать читать данные
            MemoryStream.Position = 0;
            // Создание "пустого" изображения класса BitmapImage
            BitmapImage BitmapImage = new BitmapImage();
            // Вход в режим инициализации экземпляра класса
            BitmapImage.BeginInit();
            // Непосредственно чтение содержимого изображения из потока
            BitmapImage.StreamSource = MemoryStream;
            // Задание режима кэширования всего изображения в памяти
            BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            // Выход из режима инициализации экземпляра класса
            BitmapImage.EndInit();
            // Запрет на дальнейшие изменения изображения
            BitmapImage.Freeze();
            // Возврат результата
            return BitmapImage;
        }
        
        // Метод создания капчи
        private BitmapImage CreateCaptcha()
        {
            // ВНИМАНИЕ! Чтобы воспользоваться методами работы с классом Bitmap 
            // и инструментами рисования на нем, требуется подключить модуль System.Drawing!

            // Создание генератора случайных значений
            Random Generator = new Random();
            // Генерация капчи
            CaptchaText = "";
            string Alphabet = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int I = 0; I < CaptchaTextLength; I++)
            {
                CaptchaText += Alphabet[Generator.Next(Alphabet.Length)];
            }

            // ВНИМАНИЕ! Размеры CaptchaImage назначаются с некоторым запаздыванием,
            // поэтому даже дри вызове данной подпрограммы из обработчика события
            // Window_Loaded даст нулевые размеры для CaptchaImage!

            // Задание размеров создаваемого изображения
            // (чтобы изображение не заставляло растягиваться CaptchaImage по вертикали,
            // в его соотношении сторон высота должна быть меньше, чем в соотношении
            // сторон CaptchaImage!)
            int BitmapWidth = 200;  
            int BitmapHeight = 100; 
            // Создание полотна заданных размеров
            Bitmap Bitmap = new Bitmap(BitmapWidth, BitmapHeight);
            // Создание интерфеса для инструментов рисования на полотне
            Graphics Graphics = Graphics.FromImage(Bitmap);
            // Задание словаря используемых цветов
            System.Drawing.Color[] Colors = {System.Drawing.Color.Black, System.Drawing.Color.Blue,
                System.Drawing.Color.Green, System.Drawing.Color.White,  System.Drawing.Color.Brown,
                System.Drawing.Color.Cornsilk, System.Drawing.Color.DeepPink};
            // Заливка фона изображения
            Graphics.Clear(System.Drawing.Color.Gray);
            // Задание позиции текста на изображении
            int X = Generator.Next(0, BitmapWidth / 5);
            int Y = Generator.Next(10, BitmapHeight / 3);
            // Рисование текста
            Graphics.DrawString(CaptchaText, new Font("Arial Black", 30),
                new SolidBrush(Colors[Generator.Next(Colors.Length)]), new PointF(X, Y));
            // Помехи в виде точек
            for (X = 0; X < BitmapWidth; X++)
            {
                for (Y = 0; Y < BitmapHeight; Y++)
                {
                    if (Generator.Next() % 10 == 0)
                    {
                        Bitmap.SetPixel(X, Y, Colors[Generator.Next(Colors.Length)]);
                    }
                }
            }
            // Помехи в виде линий
            for (int I = 0; I < 20; I++)
            {
                Graphics.DrawLine(new System.Drawing.Pen(Colors[Generator.Next(Colors.Length)], 1),
                    Generator.Next(BitmapWidth), Generator.Next(BitmapHeight),
                    Generator.Next(BitmapWidth), Generator.Next(BitmapHeight));
            }
            // Преобразование созданного изображения к типу BitmapImage для передачи в CaptchaImage
            return BitmapToBitmapImage(Bitmap);
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

        // Метод обработки события нажатия на кнопку ОК
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaText == CaptchaTextBox.Text)
            {
                // Создание и инициализация нового пользователя системы
                Data.Users User = new Data.Users();
                User.Login = LoginTextBox.Text;
                User.Password = PasswordPasswordBox.Password != "" ? PasswordPasswordBox.Password : PasswordTextBox.Text;
                User.IdRole = 0;
                User.FriendlyName = "";
                // Добавление его в базу данных
                Database.Users.Add(User);
                // Сохранение изменений
                Database.SaveChanges();
            }
            else
            {
                // Вывод сообщения о неверно введенной капче
                MessageBox.Show("Неверно указан текст капчи!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Close();
        }

        // Метод обработки события нажатия на кнопку Отмена
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Метод обработки события нажатия на кнопку Сменить
        private void ChangeCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            CaptchaImage.Source = CreateCaptcha();
        }

        // Метод обработки события нажатия на кнопку Озвучить
        private void PronounceCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание объекта для озвучивания
            System.Speech.Synthesis.SpeechSynthesizer Synthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
            // Установка громкости
            Synthesizer.Volume = 100;
            // Побуквенное озвучивание
            foreach (char C in CaptchaText)
            {
                Synthesizer.Speak(C.ToString());
            }
        }
    }
}
