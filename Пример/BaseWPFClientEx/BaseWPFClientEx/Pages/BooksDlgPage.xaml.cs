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

namespace BaseWPFClientEx.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksDlgPage.xaml
    /// </summary>
    public partial class BooksDlgPage : Page
    {
        // Ссылка на вызывавшую диалог страницу
        private Page Page;
        // Контекст базы данных
        private Data.LibraryExEntities Database;
        // Редактируемый объект (книга)
        public Data.Books Book { get; set; }
        // Источник данных для комбинированного списка "Пол"
        public List<Data.Publishers> Publishers { get; set; }

        public BooksDlgPage(Page Page, Data.LibraryExEntities Database, Data.Books Book)
        {
            InitializeComponent();
            // Создание внутренних структур данных страницы и инициализация переменных
            this.Page = Page;
            this.Database = Database;
            // Загрузка списков данных
            LoadPublishers();
            // Выбор режима работы диалога
            if (Book == null)
            {
                CaptionLabel.Content = "Новая книга";
                // Создание нового читателя
                Book = new Data.Books();
                // Формирование признака нового читателя в экземпляре объекта
                Book.IdBook = -1;
                // Установка значения внешнего ключа
                Book.Publishers = Database.Publishers.Find(-1);
            }
            else
            {
                CaptionLabel.Content = "Изменение данных книги";
            }
            this.Book = Book;
            // В качестве контекста для работы с данными будет использован данный экземпляр класса
            this.DataContext = this;
        }

        // Метод загрузки списка "Издатели"
        private void LoadPublishers()
        {
            Publishers = new List<Data.Publishers>(Database.Publishers.OrderBy(P => P.PublisherName).ToList());
        }

        // Метод проверки корректности информации диалога перед записью в базу данных
        private bool CheckInfo()
        {
            if (NameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Не указано наименование книги.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                NameTextBox.Focus();
                return false;
            }
            if (((Data.Publishers)PublishersComboBox.SelectedItem).IdPublisher < 0)
            {
                MessageBox.Show("Не выбрано издательство.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                PublishersComboBox.Focus();
                return false;
            }
            if (int.TryParse(PublishYearTextBox.Text.Trim(), out int Year) && (Year < 1900))
            {
                MessageBox.Show("Не указан год издания книги.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                PublishYearTextBox.Focus();
                return false;
            }
            return true;
        }

        // Метод записи информации в базу данных
        private bool SaveInfo()
        {
            try
            {
                // Добавление новой книги в базу данных
                if (Book.IdBook < 0)
                {
                    Database.Books.Add(Book);
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

        delegate bool FindRecordType(Data.Books CurrentRecord);
        private bool FindRecord(Data.Books CurrentRecord)
        {
            return (CurrentRecord != Book);
        }

        // Метод закрытия страницы диалога
        private void CloseDialog(bool NeedUpdate)
        {
            if (Page is Pages.BooksPage)
            {
                // Вызов метода закрытия диалоговой страницы у класса родительской страницы
                (Page as Pages.BooksPage).HideDialog();
                // При необходимости вызов метода обнавления данных в таблице 
                // на родительской странице
                if (NeedUpdate)
                {
                    int IdBook = Book.IdBook;
                    int RecordsCount = Database.Books.Where(P => (P.IdBook >= 0) && (String.Compare(P.Name, Book.Name) < 0)).Count();
                    (Page as Pages.BooksPage).BlockNum = RecordsCount / (Page as Pages.BooksPage).BlockRecordsCount + 1;
                    (Page as Pages.BooksPage).UpdateDataGrid(Book);
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
    }
}
