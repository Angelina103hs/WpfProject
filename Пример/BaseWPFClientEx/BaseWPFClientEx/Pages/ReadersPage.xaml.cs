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
    /// Логика взаимодействия для ReadersPage.xaml
    /// </summary>
    public partial class ReadersPage : Page
    {
        // Контекст бызы данных
        private Data.LibraryExEntities Database;

        // Хранилище идентификатора роли текущего пользователя
        private int IdRole;

        // Компонент для организации работы с данными в таблице DataGrid
        private CollectionViewSource ReadersViewModel { get; set; }
        
        // Конструктор
        public ReadersPage(Data.LibraryExEntities Database, int IdRole)
        {
            InitializeComponent();
            // Создание внутренних структур данных страницы и инициализация переменных
            this.Database = Database;
            this.IdRole = IdRole;
            ReadersViewModel = new CollectionViewSource();
            // Привязка к ReadersViewModel метода фильтации данных в таблице
            ReadersViewModel.Filter += Readers_Filter;
            // Первоначальный вывод данных в таблицу
            UpdateDataGrid(null);
        }

        // Метод настроки страницы после ее загрузки
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка отображения фотографии
            PhotoStackPanel.Width = 0;
            PhotoImage.Visibility = Visibility.Hidden;
            // Первоначальная настройка фильтра данных для быстрого поиска
            FilterComboBox.ItemsSource = ReadersDataGrid.Columns;
            FilterComboBox.DisplayMemberPath = "Header";
            FilterComboBox.SelectedIndex = 0;
        }

        // Метод установки доступности элементов управления интерфейса страницы
        private void SetControlsEnabled()
        {
            AddReaderButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            CopyReaderButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            EditReaderButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            DeleteReaderButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            PhotoButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
        }

        // Метод обновления данных в таблице страницы
        // (примеры кода на Link: https://metanit.com/sharp/tutorial/15.1.php)
        public void UpdateDataGrid(Data.Readers Reader)
        {
            // Если через входной параметр не указан объект, на который необходимо
            // установить курсор в таблице после отработки метода, то при наличии 
            // возможности необходимо запомнить ссылку на объект, являющйся
            // текущим положением курсора
            if ((Reader == null) && (ReadersViewModel.Source != null))
            {
                Reader = (Data.Readers)ReadersDataGrid.SelectedItem;
            }
            // Обновление данных из базы данных
            ObservableCollection<Data.Readers> Readers =
                new ObservableCollection<Data.Readers>(Database.Readers.Where(P => P.IdReader >= 0).ToList());
            ReadersViewModel.Source = Readers;
            // Передача данных в таблицу
            ReadersDataGrid.ItemsSource = ReadersViewModel.View;
            // Установка курсора в таблице при наличии данных в ней
            if (Readers.Count > 0)
            {
                // Попытка установить курсор на заданный объект (или в его прежнее положение)
                ReadersDataGrid.SelectedItem = Reader;
                // Если заданный объект не найден в таблице, то курсор необходимо установить
                // на первую запись в ней
                if (ReadersDataGrid.SelectedIndex < 0)
                {
                    ReadersDataGrid.SelectedIndex = 0;
                }
            }
            // Установка доступности элементов управления в интерфейсе страницы
            SetControlsEnabled();
        }

        // Метод быстрого поиска (фильтрации данных) в таблице
        void Readers_Filter(object sender, FilterEventArgs e)
        {
            // Преобразование входных данных в объект необходимого типа
            Data.Readers Reader = (Data.Readers)e.Item;
            // Проверка на наличие объекта для фильтрации
            if (Reader != null)
            {
                // Текст, введенный пользователем в строку быстрого поиска
                String Text = FilterTextBox.Text.ToUpper();
                // Формирование результата по объекту в зависимости от выбранного столбца
                switch (FilterComboBox.SelectedIndex)
                {
                    case 0:
                        e.Accepted = Reader.Surname.ToUpper().Contains(Text);
                        break;
                    case 1:
                        e.Accepted = Reader.Name.ToUpper().Contains(Text);
                        break;
                    case 2:
                        e.Accepted = Reader.Patronymic.ToUpper().Contains(Text);
                        break;
                    case 3:
                        e.Accepted = Reader.Birthdate.Value.ToString("dd.MM.yyyy").Contains(Text);
                        break;
                    case 4:
                        e.Accepted = Reader.Sexes.ShortName.ToUpper().Contains(Text);
                        break;
                }
            }
            else
            {
                e.Accepted = true;
            }
        }

        // Метод вызова фильтрации данных при наборе строки
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReadersViewModel.View.Refresh();
        }

        // Метод вызова фильтрации данных при смене целевого столбца таблицы
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReadersViewModel.View.Refresh();
        }

        // Метод вывода фотографии на экран после выбора строки в таблице
        private void ReadersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка на наличие выбранных объектов в таблице
            if (e.AddedItems.Count > 0)
            {
                // Полный путь к фотографии
                String FilePath = ((Data.Readers)e.AddedItems[0]).Photo;
                // Формирование фотографии по полному пути к ней
                try
                {
                    PhotoImage.Source = ((FilePath != null) && (FilePath != "") ? new BitmapImage(new Uri(FilePath)) : null);
                }
                catch
                {
                    PhotoImage.Source = null;
                }
            }
        }

        // Метод отображения диалога редактирования данных
        private void ShowDialog(Page Page)
        {
            // Уменьшение размеров таблицы
            Grid.SetColumnSpan(ReadersDataGridDockPanel, 1);
            // Вывод на экран диалоговой страницы
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
            // Сокрытие фотографии
            PhotoStackPanel.Width = 0;
            // Усановка доступности элементов управления интерфейса
            SetControlsEnabled();
            // Установка фокуса на таблицу
            ReadersDataGrid.Focus();
        }

        // Метод сокрытия диалога редактирования данных
        public void HideDialog()
        {
            // Увеличение размеров таблицы
            Grid.SetColumnSpan(ReadersDataGridDockPanel, 3);
            // Сокрытие диалоговой страницы
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
            // Восстановление исходного состояния фотографии
            if (PhotoImage.Visibility == Visibility.Visible)
            {
                // Установка ширины в значение Auto
                PhotoStackPanel.Width = Double.NaN;
            }
            // Усановка доступности элементов управления интерфейса
            SetControlsEnabled();
        }

        // Метод вызова диалога редактирования данных в режиме создания нового читателя
        private void AddReaderButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new Pages.ReadersDlgPage(this, Database, null));
        }

        // Метод вызова диалога редактирования данных в режиме создания нового читателя
        // на основе выбранного
        private void CopyReaderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // Метод вызова диалога редактирования данных в режиме изменения данных читателя
        private void EditReaderButton_Click(object sender, RoutedEventArgs e)
        {
            // Так как данный метод может быть вызван двойным щелчком по строке таблицы,
            // не допускается его использование, когда кнопка заблокирована (то есть
            // когда диалог редактирования данных уже открыт)
            if (EditReaderButton.IsEnabled)
            {
                Data.Readers Reader = (Data.Readers)ReadersDataGrid.SelectedItem;
                ShowDialog(new Pages.ReadersDlgPage(this, Database, Reader));
            }
        }
        
        // Метод удаления выбранного читателя
        private void DeleteReaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                try
                {
                    // Ссылка на удаляемого читателя
                    Data.Readers DeletingReader = (Data.Readers)ReadersDataGrid.SelectedItem;
                    if (ReadersDataGrid.SelectedIndex < ReadersDataGrid.Items.Count - 1)
                    {
                        ReadersDataGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (ReadersDataGrid.SelectedIndex > 0)
                        {
                            ReadersDataGrid.SelectedIndex--;
                        }
                    }
                    // Ссылка на читателя, на которого требуется установить курсор в таблице после
                    // удаления (следующий по списку или предшествующий)
                    Data.Readers SelectingReader = (Data.Readers)ReadersDataGrid.SelectedItem;
                    // Возвращение курсора в таблице в исходное состояние на тот случай,
                    // если запись не удастся удалить
                    ReadersDataGrid.SelectedItem = DeletingReader;
                    // Непосредственно удаление читателя
                    Database.Readers.Remove(DeletingReader);
                    // Сохранение изменений в базу данных
                    Database.SaveChanges();
                    // Обновление данных в таблице
                    UpdateDataGrid(SelectingReader);
                }
                catch
                {
                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
                // Установка фокуса на таблицу
                ReadersDataGrid.Focus();
            }
        }

        // Метод показа/скрытия фотографии
        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (PhotoStackPanel.ActualWidth == 0)
            {
                // Установка ширины в значение Auto
                PhotoStackPanel.Width = Double.NaN;
                PhotoButton.Content = "Скрыть фото";
                // Флаг видимости фотографии
                PhotoImage.Visibility = Visibility.Visible;
            }
            else
            {
                PhotoStackPanel.Width = 0;
                PhotoButton.Content = "Показать фото";
                // Флаг невидимости фотографии
                PhotoImage.Visibility = Visibility.Hidden;
            }
        }

        
    }
}
