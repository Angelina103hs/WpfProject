using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для BooksPage.xaml
    /// 
    /// Вывод коллекции картинок:
    /// https://www.cyberforum.ru/wpf-silverlight/thread2398834.html
    /// https://ru.stackoverflow.com/questions/944397/%D0%9F%D0%BE%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%87%D0%BD%D1%8B%D0%B9-%D0%B2%D1%8B%D0%B2%D0%BE%D0%B4-%D0%B8%D0%BD%D1%84%D0%BE%D1%80%D0%BC%D0%B0%D1%86%D0%B8%D0%B8-%D0%B8%D0%B7-%D0%B1%D0%B4
    /// 
    /// Экспорт в PDF
    /// https://metanit.com/sharp/articles/25.php
    /// 
    /// </summary>
    public partial class BooksPage : Page
    {
        // Контекст бызы данных
        private Data.LibraryExEntities Database;

        // Хранилище идентификатора роли текущего пользователя
        private int IdRole;

        // Компонент для организации работы с данными в таблице DataGrid
        private CollectionViewSource BooksViewModel { get; set; }

        // Текущий номер блока информации в таблице
        private int _BlockNum = 1;
        public int BlockNum
        {
            get
            {
                return _BlockNum;
            }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                else
                {
                    if (value > BlockCount)
                    {
                        value = BlockCount;
                    }
                }
                if (_BlockNum != value)
                {
                    _BlockNum = value;
                    BlockNumLabel.GetBindingExpression(Label.ContentProperty).UpdateTarget();
                }
                UpdateDataGrid(null);
            }
        }

        // Количество записей в блоке информации в таблице
        private int _BlockRecordsCount = 5;
        public int BlockRecordsCount
        {
            get
            {
                return _BlockRecordsCount;
            }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                if (_BlockRecordsCount != value)
                {
                    _BlockRecordsCount = value;
                    BlockCountLabel.GetBindingExpression(Label.ContentProperty).UpdateTarget();
                    BlockNum = _BlockNum;
                }
            }
        }

        // Количество блоков информации в таблице всего
        public int BlockCount
        {
            get { return (Database.Books.Count() - 1) / BlockRecordsCount + 1; }
        }

        // Конструктор
        public BooksPage(Data.LibraryExEntities Database, int IdRole)
        {
            InitializeComponent();
            // Создание внутренних структур данных страницы и инициализация переменных
            this.Database = Database;
            this.IdRole = IdRole;
            BooksViewModel = new CollectionViewSource();
            // Привязка к ReadersViewModel метода фильтации данных в таблице
            BooksViewModel.Filter += Books_Filter;
            // Первоначальный вывод данных в таблицу
            UpdateDataGrid(null);
            // Контекст поиска данных для механизма Binding
            DataContext = this;
        }

        // Метод настроки страницы после ее загрузки
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска,
            // при этом из фильтрации нужно исключить столбец "Управление"
            // Создание собствнного списка заголовков столбцов
            List<String> Columns = new List<string>();
            // Перебор и добавление в новый список только необходимых заголовков
            // Исключен столбец 4
            for (int I = 0; I < 4; I++)
            {
                Columns.Add(BooksDataGrid.Columns[I].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;
            // Запрет на управление сортировкой щелчком по заголовку столбца
            foreach (DataGridColumn Column in BooksDataGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        // Метод установки доступности элементов управления интерфейса страницы
        private void SetControlsEnabled()
        {
            AddBookButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 2);
            CopyBookButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 2);
            EditBookButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 2);
            DeleteBookButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 2);
            FirstBlockButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (BlockNum > 1);
            PreviosBlockButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (BlockNum > 1);
            NextBlockButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (BlockNum < BlockCount);
            LastBlockButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (BlockNum < BlockCount);
            BlockRecordsCountTextBox.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden);
            // Изменение видимости колонки "Управление" в таблице на момент открытия диалоговой страницы
            // с целью исключения доступа к кнопкам "Изменить" и "Удалить", находящимся в ней
            BooksDataGrid.Columns[4].Visibility = DialogScrollViewer.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }

        // Метод обновления данных в таблице страницы
        public void UpdateDataGrid(Data.Books Book)
        {
            // Если через входной параметр не указан объект, на который необходимо
            // установить курсор в таблице после отработки метода, то при наличии 
            // возможности необходимо запомнить ссылку на объект, являющйся
            // текущим положением курсора
            if ((Book == null) && (BooksViewModel.Source != null))
            {
                Book = (Data.Books)BooksDataGrid.SelectedItem;
            }
            // Обновление данных из базы данных
            ObservableCollection<Data.Books> Books =
                new ObservableCollection<Data.Books>(Database.Books.Where(P => P.IdBook >= 0).OrderBy(P => P.Name).Skip((BlockNum - 1) * BlockRecordsCount).Take(BlockRecordsCount).ToList());
            BooksViewModel.Source = Books;
            // Передача данных в таблицу
            BooksDataGrid.ItemsSource = BooksViewModel.View;
            // Обновление счетчика страниц данных в интерфейсе
            BlockCountLabel.GetBindingExpression(Label.ContentProperty).UpdateTarget();
            // Установка курсора в таблице при наличии данных в ней
            if (Books.Count > 0)
            {
                // Попытка установить курсор на заданный объект (или в его прежнее положение)
                BooksDataGrid.SelectedItem = Book;
                // Если заданный объект не найден в таблице, то курсор необходимо установить
                // на первую запись в ней
                if (BooksDataGrid.SelectedIndex < 0)
                {
                    BooksDataGrid.SelectedIndex = 0;
                }
            }
            // Установка доступности элементов управления в интерфейсе страницы
            SetControlsEnabled();
        }

        // Метод быстрого поиска (фильтрации данных) в таблице
        void Books_Filter(object sender, FilterEventArgs e)
        {
            // Преобразование входных данных в объект необходимого типа
            Data.Books Book = (Data.Books)e.Item;
            // Проверка на наличие объекта для фильтрации
            if (Book != null)
            {
                // Текст, введенный пользователем в строку быстрого поиска
                String Text = FilterTextBox.Text.ToUpper();
                // Формирование результата по объекту в зависимости от выбранного столбца
                switch (FilterComboBox.SelectedIndex)
                {
                    case 0:
                        e.Accepted = Book.Name.ToUpper().Contains(Text);
                        break;
                    case 1:
                        e.Accepted = Book.Authors.ToUpper().Contains(Text);
                        break;
                    case 2:
                        e.Accepted = Book.Publishers.PublisherName.ToUpper().Contains(Text);
                        break;
                    case 3:
                        e.Accepted = Book.PublishYear.ToString().Contains(Text);
                        break;
                }
            }
            else
            {
                e.Accepted = true;
            }
        }

        // Метод вызова фильтрации данных при наборе строки
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BooksViewModel.View.Refresh();
        }

        // Метод вызова фильтрации данных при смене целевого столбца таблицы
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BooksViewModel.View.Refresh();
        }

        // Метод отображения диалога редактирования данных
        private void ShowDialog(Page Page)
        {
            // Уменьшение размеров таблицы
            Grid.SetColumnSpan(BooksDataGridDockPanel, 1);
            // Вывод на экран диалоговой страницы
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
            // Усановка доступности элементов управления интерфейса
            SetControlsEnabled();
            // Установка фокуса на таблицу
            BooksDataGrid.Focus();
        }

        // Метод сокрытия диалога редактирования данных
        public void HideDialog()
        {
            // Увеличение размеров таблицы
            Grid.SetColumnSpan(BooksDataGridDockPanel, 3);
            // Сокрытие диалоговой страницы
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
            SetControlsEnabled();
        }

        // Метод вызова диалога редактирования данных в режиме создания новой книги
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new Pages.BooksDlgPage(this, Database, null));
        }

        // Метод вызова диалога редактирования данных в режиме создания новой книги
        // на основе выбранной
        private void CopyBookButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // Метод вызова диалога редактирования данных в режиме изменения данных книги
        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Так как данный метод может быть вызван двойным щелчком по строке таблицы,
            // не допускается его использование, когда кнопка заблокирована (то есть
            // когда диалог редактирования данных уже открыт)
            if (EditBookButton.IsEnabled)
            {
                Data.Books Book = (Data.Books)BooksDataGrid.SelectedItem;
                ShowDialog(new Pages.BooksDlgPage(this, Database, Book));
            }
        }

        // Метод удаления выбранной книги
        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                try
                {
                    // Ссылка на удаляемую книгу
                    Data.Books DeletingBook = (Data.Books)BooksDataGrid.SelectedItem;
                    if (BooksDataGrid.SelectedIndex < BooksDataGrid.Items.Count - 1)
                    {
                        BooksDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (BooksDataGrid.SelectedIndex > 0)
                        {
                            BooksDataGrid.SelectedIndex--;
                        }
                    }
                    // Ссылка на книгу, на которую требуется установить курсор в таблице после
                    // удаления (следующую по списку или предшествующкю)
                    Data.Books SelectingBook = (Data.Books)BooksDataGrid.SelectedItem;
                    // Возвращение курсора в таблице в исходное состояние на тот случай,
                    // если запись не удастся удалить
                    BooksDataGrid.SelectedItem = DeletingBook;
                    // Непосредственно удаление книги
                    Database.Books.Remove(DeletingBook);
                    // Сохранение изменений в базу данных
                    Database.SaveChanges();
                    // Обновление данных в таблице
                    UpdateDataGrid(SelectingBook);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                // Установка фокуса на таблицу
                BooksDataGrid.Focus();
            }
        }

        // Метод перехода к первому блоку данных в таблице
        private void FirstBlockButton_Click(object sender, RoutedEventArgs e)
        {
            BlockNum = 1;
        }

        // Метод перехода к предыдущему блоку данных в таблице
        private void PreviosBlockButton_Click(object sender, RoutedEventArgs e)
        {
            BlockNum--;
        }

        // Метод перехода к следующему блоку данных в таблице
        private void NextBlockButton_Click(object sender, RoutedEventArgs e)
        {
            BlockNum++;
        }

        // Метод перехода к последнему блоку данных в таблице
        private void LastBlockButton_Click(object sender, RoutedEventArgs e)
        {
            BlockNum = BlockCount;
        }
    }
}
