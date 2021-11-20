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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GroupsButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.GroupsPage());
        }

        private void TeachersButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.TeacherPage());
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.StudentPage());
        }

        private void SubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new Pages.SubjectPage());
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
