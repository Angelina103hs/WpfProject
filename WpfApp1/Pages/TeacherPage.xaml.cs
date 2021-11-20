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
    /// Interaction logic for TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {
        private int DlgMode;
        private string Name_buf;
        private string Surname_buf;
        private string Patronymic_buf;
        public TeacherPage()
        {
            InitializeComponent();
            DataContext = this;
            TeacherDataGrid.ItemsSource = BaseTesting.college.TEACHERS.ToList();
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

        private void ClickAddTeacher(object sender, RoutedEventArgs e)
        {
            //процедура, обеспечивающая отображение и скрытие колонки для работы с данными
            BookDlgLoad(true);
            DlgMode = 0;
            //запрет на доступ к строкам DataGrid
            TeacherDataGrid.IsHitTestVisible = false;
            TeacherDataGrid.SelectedItem = null;
            TeacherLabel.Content = "Добавить преподавателя";
            TeacherAddCommit.Content = "Добавить преподавателя";
            TextSurnameTeacher.Text = "";
            TextNameTeacher.Text = "";
            TextPatronymicTeacher.Text = "";
        }

        private void ClickCopyTeacher(object sender, RoutedEventArgs e)
        {
            if (TeacherDataGrid.SelectedItem != null)
            {
                BookDlgLoad(true);
                DlgMode = 0;
                TeacherLabel.Content = "Копировать - добавить преподавателя на основе выбранной";
                TeacherAddCommit.Content = "Копировать";
                TeacherDataGrid.IsHitTestVisible = false;
                //использование буферных переменных для «отрыва» от данных выбранной строки (чтобы не сработал Binding)
                Surname_buf = TextSurnameTeacher.Text;
                Name_buf = TextNameTeacher.Text;
                Patronymic_buf = TextPatronymicTeacher.Text;
                //убрать фокус с выделенной строки
                TeacherDataGrid.SelectedItem = null;
                TextSurnameTeacher.Text = Surname_buf;
                TextNameTeacher.Text = Name_buf;
                TextPatronymicTeacher.Text = Patronymic_buf;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void ClickEditTeacher(object sender, RoutedEventArgs e)
        {
            if (TeacherDataGrid.SelectedItem != null)
            {
                DlgMode = 1;
                BookDlgLoad(true);
                TeacherLabel.Content = "Изменить";
                TeacherAddCommit.Content = "Изменить";
                TeacherDataGrid.IsHitTestVisible = false;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void ClickDeleteTeacher(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                BaseTesting.college.TEACHERS.Remove((Data.TEACHERS)TeacherDataGrid.SelectedItem);
                BaseTesting.college.SaveChanges();
            }
        }

        private void TeacherAddRollback_Click(object sender, RoutedEventArgs e)
        {
            BookDlgLoad(false);
            TeacherDataGrid.IsHitTestVisible = true;
        }

        private void TeacherAddCommit_Click(object sender, RoutedEventArgs e)
        {
            var NewTeacher = new Data.TEACHERS();
            NewTeacher.SURNAME_TEACHER = TextSurnameTeacher.Text;
            NewTeacher.NAME_TEACHER = TextNameTeacher.Text;
            NewTeacher.PATRONYMIC_TEACHER = TextPatronymicTeacher.Text;
            if (DlgMode == 0)
            {
                BaseTesting.college.TEACHERS.Add(NewTeacher);
            }

            BaseTesting.college.SaveChanges();
            TeacherDataGrid.IsHitTestVisible = true;
            BookDlgLoad(false);
        }
    }
}
