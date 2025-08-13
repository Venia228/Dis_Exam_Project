using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using ExamApi;
using ExamApi.Main;

namespace DIS_Exams_Admin
{
    class Program
    {
        private static void Main()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("ПРОГРАММА ДЛЯ СОЗДАНИЯ ЭКЗАМЕНОВ ОТ ШЕИНА ВЕНИАМИНА");
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine();

            Console.WriteLine("[1] Тестовый экзамен");
            Console.WriteLine("[2] Тестовый экзамен (с генерацией случайных чисел)");
            Console.WriteLine("[3] Экзамен с открытыми вопросами");
            Console.WriteLine("[4] Экзамен с открытыми вопросами (с генерацией случайных чисел)");

            ConsoleKey key = Console.ReadKey().Key;

            Console.Clear();

            switch (key)
            {
                case ConsoleKey.D1:

                    NewExam(useRandom: false);

                    break;

                case ConsoleKey.D2:

                    NewExam(useRandom: true);

                    break;

                case ConsoleKey.D3:

                    NewExam(useRandom: false);

                    break;

                case ConsoleKey.D4:

                    NewExam(useRandom: true);

                    break;
            }
        }
        private static void NewExam(bool useRandom)
        {
            ExamData ex = new ExamData();

            Console.Write("Название файла экзамена: ");
            ex.fileName = Console.ReadLine() ?? ex.fileName;

            ex.useRandomValues = useRandom;

            Console.Write("Общие количество вопросов: ");
            int qCount = int.Parse(Console.ReadLine());

            Console.Write("Количество отображаемых вопросов: ");
            int avalaibleQuestions = int.Parse(Console.ReadLine());

            Console.Write("Время выполнения экзамена (в секундах): ");
            int timeToSolve = int.Parse(Console.ReadLine());

            ex.availableQuestions = avalaibleQuestions;
            ex.timeToSolve = timeToSolve;
            ex.questions = new string[qCount];
            ex.anwsersOrFormulas = new string[qCount];
            ex.intervals = new Interval[qCount][];

            for (int i = 0; i < ex.questions.Length; i++)
            {
                Console.Clear();

                Console.Write($"Задание {i + 1}: ");
                string task = Console.ReadLine();

                ex.questions[i] = task;

                Console.Write("Формула для вычисления правильного ответа: ");
                ex.anwsersOrFormulas[i] = Console.ReadLine();

                int arrayIndex = 0;

                if (!ex.useRandomValues)
                    continue;

                List<Interval> intervalList = new List<Interval>();

                while (ex.anwsersOrFormulas[i].IndexOf($"{Utils.keySymbol}{arrayIndex}") >= 0)
                {
                    Console.WriteLine($"Интервал числа {Utils.keySymbol}{arrayIndex}");
                    Console.Write("От: ");
                    int _from = int.Parse(Console.ReadLine());

                    Console.Write("До: ");
                    int _to = int.Parse(Console.ReadLine());

                    Interval interval = new Interval(_from, _to);

                    intervalList.Add(interval);

                    arrayIndex++;
                }

                if (ex.anwsersOrFormulas[i].IndexOf("X") >= 0)
                {
                    Console.WriteLine($"Интервал числа X");
                    Console.Write("От: ");
                    int _from = int.Parse(Console.ReadLine());

                    Console.Write("До: ");
                    int _to = int.Parse(Console.ReadLine());

                    Interval interval = new Interval(_from, _to);

                    intervalList.Add(interval);
                }

                ex.intervals[i] = intervalList.ToArray();
            }

            ex.SaveFile();

            Console.Clear();

            Console.WriteLine("Нажмите на любую клавишу для завершения работы программы...");

            Console.ReadKey();
        }
    }
}

