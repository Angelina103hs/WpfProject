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
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        private int DlgMode;
        private string Name_buf;
        private string Surname_buf;
        private string Patronymic_buf;
        private string Group_buf;

        public StudentPage()
        {
            InitializeComponent();
            DataContext = this;
            StudentDataGrid.ItemsSource = BaseTesting.college.STUDENTS.ToList();
            GroupComboBox.ItemsSource = BaseTesting.college.GROUPS.ToList();
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

        private void StudentAddRollback_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false);
            StudentDataGrid.IsHitTestVisible = true;
        }

        private void StudentAddCommit_Click(object sender, RoutedEventArgs e)
        {
            var NewStudent = new Data.STUDENTS();
            NewStudent.SURNAME_STUDENT = TextSurnameStudent.Text;
            NewStudent.NAME_STUDENT = TextNameStudent.Text;
            NewStudent.PATRONYMIC_STUDENT = TextPatronymicStudent.Text;
            NewStudent.GROUPS = (Data.GROUPS)GroupComboBox.SelectedItem;
            if (DlgMode == 0)
            {
                BaseTesting.college.STUDENTS.Add(NewStudent);
            }

            BaseTesting.college.SaveChanges();
            StudentDataGrid.IsHitTestVisible = true;
            BookDlgLoad(false);
        }

    private void ClickAddStudent(object sender, RoutedEventArgs e)
        {
            //процедура, обеспечивающая отображение и скрытие колонки для работы с данными
            BookDlgLoad(true);
            DlgMode = 0;
            //запрет на доступ к строкам DataGrid
            StudentDataGrid.IsHitTestVisible = false;
            StudentDataGrid.SelectedItem = null;
            StudentLabel.Content = "Добавить студента";
            StudentAddCommit.Content = "Добавить студента";
            TextSurnameStudent.Text = "";
            TextNameStudent.Text = "";
            TextPatronymicStudent.Text = "";
        }

        private void ClickCopyStudent(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true);
                DlgMode = 0;
                StudentLabel.Content = "Копировать - добавить студента на основе выбранной";
                StudentAddCommit.Content = "Копировать";
                StudentDataGrid.IsHitTestVisible = false;
                //использование буферных переменных для «отрыва» от данных выбранной строки (чтобы не сработал Binding)
                Surname_buf = TextSurnameStudent.Text;
                Name_buf = TextNameStudent.Text;
                Patronymic_buf = TextPatronymicStudent.Text;
                Group_buf = GroupComboBox.Text;
                //убрать фокус с выделенной строки
                StudentDataGrid.SelectedItem = null;
                TextSurnameStudent.Text = Surname_buf;
                TextNameStudent.Text = Name_buf;
                TextPatronymicStudent.Text = Patronymic_buf;
                GroupComboBox.Text = Group_buf;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }

        }

        private void ClickEditStudent(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem != null)
            {
                DlgMode = 1;
                BookDlgLoad(true);
                StudentLabel.Content = "Изменить";
                StudentAddCommit.Content = "Изменить";
                StudentDataGrid.IsHitTestVisible = false;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void ClickDeleteStudent(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                BaseTesting.college.STUDENTS.Remove((Data.STUDENTS)StudentDataGrid.SelectedItem);
                BaseTesting.college.SaveChanges();
            }
        }
    }
}
