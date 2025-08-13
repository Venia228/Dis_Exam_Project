using ExamApi;
using ExamApi.Main;
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
            GetExamFiles();
        }

        private void GetExamFiles()
        {
            string[] files = Directory.GetFiles(new ExamData().filePath);

            foreach (string file in files)
            {
                if (Path.GetExtension(file) == $".{new ExamData().fileExtension}")
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
                ExamData examData = new ExamData();

                string fileData = File.ReadAllText($"{examData.filePath}{fileChoose.SelectedItem.ToString()}.{examData.fileExtension}");

                examData = JsonConvert.DeserializeObject<ExamData>(fileData);

                StudentData studentData = new StudentData();

                studentData.Name = studentName.Text;
                studentData.ClassName = className.Text;

                MainWindow_1 window_1 = new MainWindow_1(examData, studentData);
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