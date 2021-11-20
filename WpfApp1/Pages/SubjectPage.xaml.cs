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
    /// Interaction logic for SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Page
    {
        private int DlgMode;
        private string Surname_buf;
        private string Subject_buf;
        public SubjectPage()
        {
            InitializeComponent();
            DataContext = this;
            SubjectDataGrid.ItemsSource = BaseTesting.college.SUBJECTS.ToList();
            SurnameTeacherComboBox.ItemsSource = BaseTesting.college.TEACHERS.ToList();
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

        private void ClickAddSubject(object sender, RoutedEventArgs e)
        {
            //процедура, обеспечивающая отображение и скрытие колонки для работы с данными
            BookDlgLoad(true);
            DlgMode = 0;
            //запрет на доступ к строкам DataGrid
            SubjectDataGrid.IsHitTestVisible = false;
            SubjectDataGrid.SelectedItem = null;
            SubjectLabel.Content = "Добавить дисциплину";
            SubjectAddCommit.Content = "Добавить дисциплину";
            TextSubject.Text = "";
        }

        private void ClickCopySubject(object sender, RoutedEventArgs e)
        {
            if (SubjectDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true);
                DlgMode = 0;
                SubjectLabel.Content = "Копировать - добавить дисциплину на основе выбранной";
                SubjectAddCommit.Content = "Копировать";
                SubjectDataGrid.IsHitTestVisible = false;
                //использование буферных переменных для «отрыва» от данных выбранной строки (чтобы не сработал Binding)
                Subject_buf = TextSubject.Text;
                Surname_buf = SurnameTeacherComboBox.Text;
                //убрать фокус с выделенной строки
                SubjectDataGrid.SelectedItem = null;
                TextSubject.Text = Subject_buf;
                SurnameTeacherComboBox.Text = Surname_buf;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void ClickEditSubject(object sender, RoutedEventArgs e)
        {
            if (SubjectDataGrid.SelectedItem != null)
            {
                DlgMode = 1;
                BookDlgLoad(true);
                SubjectLabel.Content = "Изменить";
                SubjectAddCommit.Content = "Изменить";
                SubjectDataGrid.IsHitTestVisible = false;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void ClickDeleteSubject(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                BaseTesting.college.SUBJECTS.Remove((Data.SUBJECTS)SubjectDataGrid.SelectedItem);
                BaseTesting.college.SaveChanges();
            }
        }

        private void SubjectAddRollback_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false);
            SubjectDataGrid.IsHitTestVisible = true;
        }

        private void SubjectAddCommit_Click(object sender, RoutedEventArgs e)
        {
            var NewSubject = new Data.SUBJECTS();
            NewSubject.SUBJECT = TextSubject.Text;
            NewSubject.TEACHERS = (Data.TEACHERS)SurnameTeacherComboBox.SelectedItem;
            if (DlgMode == 0)
            {
                BaseTesting.college.SUBJECTS.Add(NewSubject);
            }

            BaseTesting.college.SaveChanges();
            SubjectDataGrid.IsHitTestVisible = true;
            BookDlgLoad(false);
        }
    }
}
