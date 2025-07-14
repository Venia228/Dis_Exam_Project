using ExamApi;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfMath.Controls;

namespace DIS_Exams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            GetFiles(Exam.filePath);
        }

        private void GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".json")
                {
                    fileChoose.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
        }
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (fileChoose.SelectedIndex == -1)
            {
                MessageBox.Show( "Файл не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            try
            {
                string fileData = File.ReadAllText($"{Exam.filePath}{fileChoose.SelectedItem.ToString()}.json");

                Exam exam = JsonConvert.DeserializeObject<Exam>(fileData);

                string _studentName = studentName.Text;
                string _className = className.Text;

                MainWindow_1 window_1 = new MainWindow_1(exam, _studentName, _className);
                window_1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Close();
            }
        }

    }
}