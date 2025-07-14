using System;
using NCalc;

namespace ExamApi
{
    public static class Utils
    {
        public const char keySymbol = '~';
        public const string keyWord = "equ";

        public static void GetRandomExamAndValues(this Exam exam)
        {
            exam.GetRandomExam();

            Random random = new Random();

            for (int i = 0; i < exam.formulas.Length; i++)
            {
                ref string formula = ref exam.formulas[i];
                ref string task = ref exam.questions[i];

                Interval[] interval = exam.intervals[i];

                int arrayIndex = 0;

                while (formula.IndexOf($"{keySymbol}{arrayIndex}") >= 0)
                {
                    int randomValue = random.Next(interval[arrayIndex]._from, interval[arrayIndex]._to);

                    task = task.Replace($"{keySymbol}{arrayIndex}", randomValue.ToString());

                    formula = formula.Replace($"{keySymbol}{arrayIndex}", randomValue.ToString());

                    arrayIndex++;
                }

                if (task.IndexOf($"{keySymbol}{keyWord}") >= 0)
                {
                    int randomValue = random.Next(interval[arrayIndex]._from, interval[arrayIndex]._to);

                    formula = formula.Replace($"X", randomValue.ToString());

                    Expression expression = new Expression(formula);

                    task = task.Replace($"{keySymbol}{keyWord}", expression.Evaluate().ToString());

                    formula = randomValue.ToString();
                }

                task = task.Replace("- -", "+ ");
                task = task.Replace("-1X", "-X");
                task = task.Replace("1X", "X");
                task = task.Replace("- +", "- ");
                task = task.Replace("+ -", "- ");
                task = task.Replace("-1(", "-(");
                task = task.Replace("1(", "(");
            }
        }
        public static void GetRandomExam(this Exam exam)
        {
            string[] randomQuestions = new string[exam.availableQuestions];
            string[] randomFormulas = new string[exam.availableQuestions];
            Interval[][] randomIntervals = new Interval[exam.availableQuestions][];

            Random rand = new Random();

            for (int i = 0; i < exam.availableQuestions; i++)
            {
                int randomIndex = rand.Next(0, exam.questions.Length);

                randomQuestions[i] = exam.questions[randomIndex];
                exam.questions[randomIndex] = null;

                randomFormulas[i] = exam.formulas[randomIndex];
                exam.formulas[randomIndex] = null;

                randomIntervals[i] = exam.intervals[randomIndex];
                exam.intervals[randomIndex] = null;

                string[] av_Questions = new string[exam.intervals.Length - 1];
                string[] av_Formulas = new string[exam.formulas.Length - 1];
                Interval[][] av_intervals = new Interval[exam.intervals.Length - 1][];
                int addIndex = 0;

                for (int j = 0; j < exam.questions.Length - 1; j++)
                {
                    if (exam.questions[j] == null)
                    {
                        addIndex++;
                    }

                    av_Questions[j] = exam.questions[j + addIndex];
                    av_Formulas[j] = exam.formulas[j + addIndex];
                    av_intervals[j] = exam.intervals[j + addIndex];
                }

                exam.questions = av_Questions;
                exam.formulas = av_Formulas;
                exam.intervals = av_intervals;
            }

            exam.questions = randomQuestions;
            exam.formulas = randomFormulas;
            exam.intervals = randomIntervals;
        }
        public static void CheckForIllegalSymbols(ref string[] anwsers)
        {
            for (int i = 0; i < anwsers.Length; i++)
            {
                ref string anwser = ref anwsers[i];
                anwser = anwser == null || anwser == string.Empty ? "0" : anwser;

                foreach (char c in anwser)
                {
                    if (!(char.IsDigit(c) || c == '.' || c == '/' || c == '-'))
                    {
                        anwser = "0";
                        break;
                    }
                }
            } 
            
        }
    }
}
