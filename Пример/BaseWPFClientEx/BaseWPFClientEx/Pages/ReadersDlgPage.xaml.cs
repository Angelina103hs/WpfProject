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

namespace BaseWPFClientEx.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadersDlgPage.xaml
    /// </summary>
    public partial class ReadersDlgPage : Page
    {
        // Полный путь к хранилищу фотографий
        private static String ImagesPath = Directory.GetCurrentDirectory() + "\\Images\\";
        // Ссылка на вызывавшую диалог страницу
        private Page Page;
        // Контекст базы данных
        private Data.LibraryExEntities Database;
        // Редактируемый объект (читатель)
        public Data.Readers Reader { get; set; }
        // Источник данных для комбинированного списка "Пол"
        public List<Data.Sexes> Sexes { get; set; }
        // Диалог для работы с файлами
        private OpenFileDialog OpenFileDialog;

        public ReadersDlgPage(Page Page, Data.LibraryExEntities Database, Data.Readers Reader)
        {
            InitializeComponent();
            // Создание внутренних структур данных страницы и инициализация переменных
            this.Page = Page;
            this.Database = Database;
            OpenFileDialog = new OpenFileDialog();
            // Загрузка списков данных
            LoadSexes();
            // Выбор режима работы диалога
            if (Reader == null)
            {
                CaptionLabel.Content = "Новый читатель";
                // Создание нового читателя
                Reader = new Data.Readers();
                // Формирование признака нового читателя в экземпляре объекта
                Reader.IdReader = -1;
                // Установка значения внешнего ключа
                Reader.Sexes = Database.Sexes.Find(-1);
                // Еще вариант: Reader.Sexes = Database.Sexes.First();
                // Еще вариант: Reader.Sexes = Database.Sexes.Single(P => P.IdSex == -1);
            }
            else
            {
                CaptionLabel.Content = "Изменение данных читателя";
            }
            this.Reader = Reader;
            // В качестве контекста для работы с данными будет использован данный экземпляр класса
            this.DataContext = this;
        }

        // Метод загрузки списка "Пол"
        private void LoadSexes()
        {
            Sexes = new List<Data.Sexes>(Database.Sexes.OrderBy(P => P.IdSex).ToList());
        }

        // Метод проверки корректности информации диалога перед записью в базу данных
        private bool CheckInfo()
        {
            if (SurnameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Не указана фамилия читателя.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                SurnameTextBox.Focus();
                return false;
            }
            if (NameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Не указано имя читателя.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                NameTextBox.Focus();
                return false;
            }
            if (((Data.Sexes)SexesComboBox.SelectedItem).IdSex < 0)
            {
                MessageBox.Show("Не указан пол читателя.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                SexesComboBox.Focus();
                return false;
            }
            if (BirthdateDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Не указана дата рождения читателя.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                BirthdateDatePicker.Focus();
                return false;
            }
            return true;
        }

        // Метод записи информации в базу данных
        private bool SaveInfo()
        {
            // Добавление фотографии к информации читателя
            if (OpenFileDialog.FileName != "")
            {
                try
                {
                    // Создание папки хранилища фотографий, если оно до этого не существовало
                    if (!Directory.Exists(ImagesPath))
                    {
                        Directory.CreateDirectory(ImagesPath);
                    }
                    // Формирование уникального полного имени фотографии для хранилища
                    Reader.Photo = ImagesPath + DateTime.Now.ToString("fffssmmHHddMMyyyy");
                    // Копирование выбранной пользователем фотографии в хранилище
                    File.Copy(OpenFileDialog.FileName, Reader.Photo);
                }
                catch
                {
                    // Сообщение об ошибке
                    MessageBox.Show("Не удалось скопировать выбранную фотографию в хранилище.",
                       "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                    return false;
                }
            }
            else
            {
                // Удаление ссылки на фотографию в хранилище (сама фотография из хранилища не удаляется)
                if (PhotoImage.Source == null)
                {
                    Reader.Photo = null;
                }
            }
            try
            {
                // Добавление нового читателя в базу данных
                if (Reader.IdReader < 0)
                {
                    Database.Readers.Add(Reader);
                }
                // Сохранение данных в базе данных
                Database.SaveChanges();
            }
            catch
            {
                // Сообщение об ошибке
                MessageBox.Show("Не удалось сохранить информацию в базе данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                return false;
            }
            return true;
        }

        // Метод закрытия страницы диалога
        private void CloseDialog(bool NeedUpdate)
        {
            if (Page is Pages.ReadersPage)
            {
                // Вызов метода закрытия диалоговой страницы у класса родительской страницы
                (Page as Pages.ReadersPage).HideDialog();
                // При необходимости вызов метода обнавления данных в таблице 
                // на родительской странице
                if (NeedUpdate)
                {
                    (Page as Pages.ReadersPage).UpdateDataGrid(Reader);
                }
            }
        }

        // Метод кнопки ОК
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfo() && SaveInfo())
            {
                CloseDialog(true);
            }
        }

        // Метод кнопки Отмена
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CloseDialog(false);
        }

        // Метод кнопки Загрузить фотографию
        private void LoadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == true)
            {
                PhotoImage.Source = new BitmapImage(new Uri(OpenFileDialog.FileName));
            }
        }

        // Метод кнопки Очистить фотографию
        private void ClearPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            PhotoImage.Source = null;
        }
    }
}
