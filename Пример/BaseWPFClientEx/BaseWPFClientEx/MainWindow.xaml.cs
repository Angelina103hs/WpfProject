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

namespace BaseWPFClientEx
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// 
    /// Для всех элементов графического интерфейса базовое стилевое оформление 
    /// описано в модуле App.xaml отдельными стилями. 
    /// 
    /// ВНИМАНИЕ! В базовый стиль оформления кнопок недопустимо влючать свойства 
    /// Height и Width, потому что после этого все кнопки без исключения приобретают 
    /// заданные размеры, включая внутренние кнопки компонентов DatePicker!
    /// 
    /// ВНИМАНИЕ! Стиль для компонентов ComboBox должен быть описан до стиля 
    /// компонентов TextBox для устранения их взаимного влияния друг на друга!
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        // Корневое хранилище контекста базы данных
        private Data.LibraryExEntities Database;

        // Корневое хранилище идентификатора роли текущего пользователя
        private int IdRole;

        // Список активных основных страниц (не диалогов!), открытых в окне приложения
        private List<Page> ActivePages;

        // Индекс текущей активной страницы в окне приложения
        private int CurrentPageIndex;

        // Конструктор
        public MainWindow(Data.LibraryExEntities Database, int IdRole)
        {
            InitializeComponent();
            // Создание внутренних структур данных окна приложения и инициализация
            // переменных
            ActivePages = new List<Page>();
            CurrentPageIndex = -1;
            this.Database = Database;
            this.IdRole = IdRole;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Начальная установка состояния элементов управления окна приложения
            SetControlsEnabled();
            // Загрузка наименования роли текущего пользователя
            String RoleName = Database.Roles.Find(IdRole).RoleName;
            // Вывод наименования роли в интерфейсе
            RoleLabel.Content = "Интерфейс\n" + (RoleName != null ? RoleName.Replace(" ", "/n") : "");
        }

        // Метод установки доступности элементов управления интерфейса окна приложения
        private void SetControlsEnabled()
        {
            BooksButton.IsEnabled = IdRole >= 0;
            CopiesButton.IsEnabled = IdRole >= 2;
            ReadersButton.IsEnabled = IdRole >= 1;
            ReadingsButton.IsEnabled = IdRole >= 1;

            PreviosPageButton.IsEnabled = (CurrentPageIndex > 0);
            NextPageButton.IsEnabled = (CurrentPageIndex < ActivePages.Count - 1);
            ClosePageButton.IsEnabled = (ActivePages.Count > 0);
        }

        // Метод поиска активной страницы в окне приложения по заданному типу страницы
        private int GetActivePageIndexByType(Type PageType)
        {
            int Index = ActivePages.Count - 1;
            while ((Index >= 0) && (ActivePages[Index].GetType() != PageType))
            {
                Index--;
            }
            return Index;
        }

        // Метод отображения страницы заданного типа в окне приложения
        private void ShowPage(Type PageType)
        {
            Page Page;
            if (PageType != null)
            {
                // Поиск страницы заданного типа среди активных страниц приложения
                int Index = GetActivePageIndexByType(PageType);
                if (Index < 0)
                {
                    // Если страница не найдена среди активных, создание страницы 
                    // заданного типа при помощи метода класса Activator. Последние два 
                    // параметра передаются на вход конструктора страницы
                    Page = (Page)Activator.CreateInstance(PageType, Database, IdRole);
                    // Добавление созданной страницы в список активных
                    ActivePages.Add(Page);
                    CurrentPageIndex = ActivePages.Count - 1;
                }
                else
                {
                    // Переход к существующей активной странице
                    Page = ActivePages[Index];
                    CurrentPageIndex = Index;
                }
            }
            else
            {
                // Подготовка параметра для метода Navigate компонента RootFrame,
                // чтобы он "закрыл" текущую активную страницу
                Page = null;
            }
            // Отображение в фрейме текущей выбранной страницы
            RootFrame.Navigate(Page);
        }

        // Метод удаления из фрейма окна приложения всех незадействованных 
        // на текущий момент страниц
        private void RootFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            while (RootFrame.CanGoBack)
            {
                RootFrame.RemoveBackEntry();
            }
            SetControlsEnabled();
        }

        // Переход к предыдущей активной странице окна приложения
        private void PreviosPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex--;
            RootFrame.Navigate(ActivePages[CurrentPageIndex]);
        }

        // Переход к следующей активной странице окна приложения
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPageIndex++;
            RootFrame.Navigate(ActivePages[CurrentPageIndex]);
        }

        // Закрытие текущей активной страницы окна приложения
        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            Page Page;
            // Непосредственно удаление текущей страницы из списка активных
            // страниц окна приложения
            ActivePages.RemoveAt(CurrentPageIndex);
            // Переход к соседней активной странице, если она существует
            if (CurrentPageIndex > 0)
            {
                CurrentPageIndex--;
                Page = ActivePages[CurrentPageIndex];
            }
            else
            {
                if (CurrentPageIndex < ActivePages.Count)
                {
                    Page = ActivePages[CurrentPageIndex];
                }
                else
                {
                    Page = null;
                }
            }
            // Отображение в фрейме новой выбранной страницы
            RootFrame.Navigate(Page);
        }

        // Переход к странице справочника Books
        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.BooksPage));
        }

        // Переход к странице справочника Readers
        private void ReadersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.ReadersPage));
        }

        // Переход к странице справочника Readings
        private void ReadingsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.ReadingsPage));
        }
    }
}
