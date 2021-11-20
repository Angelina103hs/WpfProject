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
    /// Логика взаимодействия для ReadingsPage.xaml
    /// </summary>
    public partial class ReadingsPage : Page
    {
        // Контекст бызы данных
        private Data.LibraryExEntities Database;

        // Хранилище идентификатора роли текущего пользователя
        private int IdRole;

        // Компоненты для организации работы с данными в таблицах DataGrid
        private CollectionViewSource ReadersViewModel { get; set; }
        private CollectionViewSource ReadingsViewModel { get; set; }

        public ReadingsPage(Data.LibraryExEntities Database, int IdRole)
        {
            InitializeComponent();
            // Создание внутренних структур данных страницы и инициализация переменных
            this.Database = Database;
            this.IdRole = IdRole;
            ReadersViewModel = new CollectionViewSource();
            // Привязка к ReadersViewModel метода фильтации данных в таблице
            ReadersViewModel.Filter += Readers_Filter;
            ReadingsViewModel = new CollectionViewSource();
            // Первоначальный вывод данных в таблицу
            UpdateReadersDataGrid(null);
        }

        // Метод настроки страницы после ее загрузки
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Первоначальная настройка фильтра данных для быстрого поиска
            FilterComboBox.ItemsSource = ReadersDataGrid.Columns;
            FilterComboBox.DisplayMemberPath = "Header";
            FilterComboBox.SelectedIndex = 0;
        }
        
        // Метод установки доступности элементов управления интерфейса страницы
        private void SetControlsEnabled()
        {
            AddReadingButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            CopyReadingButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            EditReadingButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
            DeleteReadingButton.IsEnabled = (DialogScrollViewer.Visibility == Visibility.Hidden) && (IdRole >= 1);
        }

        // Метод обновления данных в таблице Readers страницы
        public void UpdateReadersDataGrid(Data.Readers Reader)
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
            // Обновление данных в зависимой таблице произойдет по событию 
            // ReadersDataGrid_SelectionChanged!
            // Установка доступности элементов управления в интерфейсе страницы
            SetControlsEnabled();
        }

        // Метод обновления данных в таблице Readings страницы
        public void UpdateReadingsDataGrid(Data.Readings Reading)
        {
            // Если через входной параметр не указан объект, на который необходимо
            // установить курсор в таблице после отработки метода, то при наличии 
            // возможности необходимо запомнить ссылку на объект, являющйся
            // текущим положением курсора
            if ((Reading == null) && (ReadingsViewModel.Source != null))
            {
                Reading = (Data.Readings)ReadingsDataGrid.SelectedItem;
            }
            // Обновление данных из базы данных
            ObservableCollection<Data.Readings> Readings;
            Data.Readers Reader = (Data.Readers)ReadersDataGrid.SelectedItem;
            if (Reader != null)
            {
                Readings = new ObservableCollection<Data.Readings>(Database.Readings.Where(P => P.IdReader == Reader.IdReader).ToList());
            }
            else
            {
                Readings = new ObservableCollection<Data.Readings>();
            }
            ReadingsViewModel.Source = Readings;
            // Передача данных в таблицу
            ReadingsDataGrid.ItemsSource = ReadingsViewModel.View;
            // Установка курсора в таблице при наличии данных в ней
            if (Readings.Count > 0)
            {
                // Попытка установить курсор на заданный объект (или в его прежнее положение)
                ReadingsDataGrid.SelectedItem = Reading;
                // Если заданный объект не найден в таблице, то курсор необходимо установить
                // на первую запись в ней
                if (ReadingsDataGrid.SelectedIndex < 0)
                {
                    ReadingsDataGrid.SelectedIndex = 0;
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

        // Метод обработки события смены строки в таблице Readers
        private void ReadersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateReadingsDataGrid(null);
        }

        // Метод отображения диалога редактирования данных
        private void ShowDialog(Page Page)
        {
            // Уменьшение размеров таблицы
            Grid.SetColumnSpan(ReadingsDataGridDockPanel, 1);
            // Вывод на экран диалоговой страницы
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
            // Усановка доступности элементов управления интерфейса
            SetControlsEnabled();
            // Установка фокуса на таблицу
            ReadersDataGrid.Focus();
        }

        // Метод сокрытия диалога редактирования данных
        public void HideDialog()
        {
            // Увеличение размеров таблицы
            Grid.SetColumnSpan(ReadingsDataGridDockPanel, 3);
            // Сокрытие диалоговой страницы
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
            // Усановка доступности элементов управления интерфейса
            SetControlsEnabled();
        }
    }
}
