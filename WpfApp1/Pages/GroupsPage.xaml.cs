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
using WpfApp1.Data;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for GroupsPage.xaml
    /// </summary>
    
    public partial class GroupsPage : Page
    {
        private int DlgMode;
        private string Name_buf;

        public GroupsPage()
        {
            InitializeComponent();
            DataContext = this;
            GroupDataGrid.ItemsSource = BaseTesting.college.GROUPS.ToList();
        }

        private void ClickAddGroup(object sender, RoutedEventArgs e)
        {
            //процедура, обеспечивающая отображение и скрытие колонки для работы с данными
            BookDlgLoad(true);
            DlgMode = 0;
            //запрет на доступ к строкам DataGrid
            GroupDataGrid.IsHitTestVisible = false;
            GroupDataGrid.SelectedItem = null;
            GroupLabel.Content = "Добавить группу";
            GroupAddCommit.Content = "Добавить группу";
            TextGroups.Text = "";

        }

        public void BookDlgLoad(bool b)
        {
            if (b == true)
            {
                ChangeColumn.Width = new GridLength(400);
            }
            else
            {
                ChangeColumn.Width = new GridLength(0);
            }

        }

        private void GroupAddRollback_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false);
            GroupDataGrid.IsHitTestVisible = true;
        }

        private void ClickCopyGroup(object sender, RoutedEventArgs e)
        {
            if (GroupDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true);
                DlgMode = 0;
                GroupLabel.Content = "Копировать - добавить группу на основе выбранной";
                GroupAddCommit.Content = "Копировать";
                GroupDataGrid.IsHitTestVisible = false;
                //использование буферных переменных для «отрыва» от данных выбранной строки (чтобы не сработал Binding)
                Name_buf = TextGroups.Text;
                //убрать фокус с выделенной строки
                GroupDataGrid.SelectedItem = null;
                TextGroups.Text = Name_buf;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }

        }

        private void ClickEditGroup(object sender, RoutedEventArgs e)
        {
            if (GroupDataGrid.SelectedItem != null)
            {
                DlgMode = 1;
                BookDlgLoad(true);
                GroupLabel.Content = "Изменить";
                GroupAddCommit.Content = "Изменить";
                GroupDataGrid.IsHitTestVisible = false;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }

        }

        private void ClickDeleteGroup(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                BaseTesting.college.GROUPS.Remove((Data.GROUPS)GroupDataGrid.SelectedItem);
                BaseTesting.college.SaveChanges();
            }

        }

        private void GroupAddCommit_Click(object sender, RoutedEventArgs e)
        {
            var NewGroup = new Data.GROUPS();
            NewGroup.NAME_GROUP = TextGroups.Text;
            if (DlgMode == 0)
            {
                BaseTesting.college.GROUPS.Add(NewGroup);
            }

            BaseTesting.college.SaveChanges();
            GroupDataGrid.IsHitTestVisible = true;
            BookDlgLoad(false);

        }
    }
}
