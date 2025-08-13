using ExamApi;
using ExamApi.Main;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;

namespace DIS_Exams
{

    /// <summary>
    /// Логика взаимодействия для MainWindow_1.xaml
    /// </summary>
    public partial class MainWindow_1 : Window
    {
        private StudentData student;

        private ExamData exam;

        private string[] studentAnwsers;

        private int maxLineLength = 90;

        private ExamTimer timer;

        private Color[] colors;

        private bool examIsDone;
        public MainWindow_1(ExamData exam, StudentData student)
        {
            InitializeComponent();

            this.exam = exam;
            this.student = student;

            timer = new ExamTimer(exam);

            timer.OnTimerUpdate += UpdateText;
            timer.Start();

            StartExam();
        }

        private void UpdateText(string timerText)
        {
            Timer.Text = timerText;
        }

        private void StartExam()
        {
            studentAnwsers = new string[exam.availableQuestions];

            if (exam.useRandomValues)
                exam.GetRandomExamAndValues();
            else
                exam.GetRandomExam();

            for (int i = 0; i < exam.availableQuestions; i++)
            {
                taskSelection.Items.Add($"Задание {i + 1}");
            }
        }
        private void GetCondition()
        {
            string task = exam.questions[taskSelection.SelectedIndex];
            int startIndex = 0;

            int indexOfFormula = task.IndexOf($"{Utils.keySymbol}F");

            string textPart = indexOfFormula < 0 ? task : task.Substring(0, indexOfFormula).Trim();
            string formulaPart = indexOfFormula < 0 ? string.Empty : task.Substring(indexOfFormula + 2).Trim();

            taskCondition_formulaPart.Formula = formulaPart;

            int length = maxLineLength > textPart.Length ? textPart.Length : maxLineLength;

            int lineBreakCounts = exam.questions[taskSelection.SelectedIndex].Length / maxLineLength;

            if (lineBreakCounts == 0)
                lineBreakCounts = 1;

            for (int i = 0; i < taskCondition_textPart.Items.Count; i++)
            {
                taskCondition_textPart.Items.RemoveAt(i);
            }

            for (int i = 0; i < lineBreakCounts; i++)
            {
                if (task.Length - startIndex < length)
                {
                    length = textPart.Length - startIndex;
                }

                taskCondition_textPart.Items.Add(textPart.Substring(startIndex, length));

                startIndex = length;
                length += maxLineLength;
            }
        }
        private void taskSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCondition();

            anwserField.Text = studentAnwsers[taskSelection.SelectedIndex];

            if (examIsDone)
                anwserField.Background = new SolidColorBrush(colors[taskSelection.SelectedIndex]);

            pageLabel.Content = $"Задание {taskSelection.SelectedIndex + 1}";

            taskPage.Visibility = Visibility.Visible;
        }

        private void anwserField_TextChanged(object sender, TextChangedEventArgs e)
        {
            studentAnwsers[taskSelection.SelectedIndex] = anwserField.Text;
        }

        private void endButton_Click(object sender, RoutedEventArgs e)
        {
            examIsDone = true;

            Timer.Visibility = Visibility.Hidden;
            endButton.Visibility = Visibility.Hidden;

            anwserField.IsEnabled = false;

            timer.Stop();

            int rightAnwsersCount = 0;

            student.Mark = (sbyte)GetMark(ref rightAnwsersCount);
            student.rightAnwsersCount = rightAnwsersCount;
            student.exam = exam;
            student.wastedTimeOnExam = timer.GetWastedTime();

            MessageBox.Show($"Оценка: {student.Mark}\n" +
                            $"Правильных ответов: {student.rightAnwsersCount} / {exam.availableQuestions}\n" +
                            $"Время потрачено: {student.wastedTimeOnExam}");

            student.SaveFile();
        }

        private int GetMark(ref int rightAnwsersCount)
        {
            colors = new Color[exam.availableQuestions];

            Utils.CheckForIllegalSymbols(ref studentAnwsers);  

            for (int i = 0; i < studentAnwsers.Length; i++)
            {               
                NCalc.Expression anwser = new NCalc.Expression(studentAnwsers[i]);
                NCalc.Expression formula = new NCalc.Expression(exam.anwsersOrFormulas[i]);

                formula.EvaluateFunction += (name, args) =>
                {
                    if (name == "Abs")
                        args.Result = Math.Abs(Convert.ToDouble(args.Parameters[0].Evaluate()));
                };

                try
                {
                    string studentResult = anwser.Evaluate().ToString();
                    string rightResult = formula.Evaluate().ToString();

                    if (studentResult == rightResult)
                    {
                        rightAnwsersCount++;
                        colors[i] = Color.FromRgb(180, 255, 180);
                    }
                    else
                    {
                        colors[i] = Color.FromRgb(255, 180, 180);
                    }
                }
                catch (NCalc.EvaluationException)
                {
                    MessageBox.Show("Wrong or damaged formula was found!", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);

                    Close();
                }
            }

            float percentage = (float)rightAnwsersCount / exam.availableQuestions;

            if (percentage < 0.5f)
                return 2;
            if (percentage < 0.7f)
                return 3;
            if (percentage < 0.9f)
                return 4;

            return 5;
        }
    }
}
