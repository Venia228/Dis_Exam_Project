using ExamApi.Main;
using System;

namespace ExamApi
{
    public static class Extensions
    {
        public static void GetRandomExamAndValues(this ExamData exam)
        {
            exam.GetRandomExam();

            Random random = new Random();

            for (int i = 0; i < exam.anwsersOrFormulas.Length; i++)
            {
                ref string formula = ref exam.anwsersOrFormulas[i];
                ref string task = ref exam.questions[i];

                Interval[] interval = exam.intervals[i];

                int arrayIndex = 0;

                while (formula.IndexOf($"{Utils.keySymbol}{arrayIndex}") >= 0)
                {
                    int randomValue = random.Next(interval[arrayIndex]._from, interval[arrayIndex]._to);

                    task = task.Replace($"{Utils.keySymbol}{arrayIndex}", randomValue.ToString());

                    formula = formula.Replace($"{Utils.keySymbol}{arrayIndex}", randomValue.ToString());

                    arrayIndex++;
                }

                if (task.IndexOf($"{Utils.keySymbol}{Utils.keyWord}") >= 0)
                {
                    int randomValue = random.Next(interval[arrayIndex]._from, interval[arrayIndex]._to);

                    formula = formula.Replace($"X", randomValue.ToString());

                    NCalc.Expression expression = new NCalc.Expression(formula);

                    task = task.Replace($"{Utils.keySymbol}{Utils.keyWord}", expression.Evaluate().ToString());

                    formula = randomValue.ToString();
                }

                task = task.Replace("- -", "+ ");
                task = task.Replace("-1x", "-x ");
                task = task.Replace("1x", "x ");
                task = task.Replace("- +", "- ");
                task = task.Replace("+ -", "- ");
            }
        }
        public static void GetRandomExam(this ExamData exam)
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

                randomFormulas[i] = exam.anwsersOrFormulas[randomIndex];
                exam.anwsersOrFormulas[randomIndex] = null;

                randomIntervals[i] = exam.intervals[randomIndex];
                exam.intervals[randomIndex] = null;

                string[] av_Questions = new string[exam.intervals.Length - 1];
                string[] av_Formulas = new string[exam.anwsersOrFormulas.Length - 1];
                Interval[][] av_intervals = new Interval[exam.intervals.Length - 1][];
                int addIndex = 0;

                for (int j = 0; j < exam.questions.Length - 1; j++)
                {
                    if (exam.questions[j] == null)
                    {
                        addIndex++;
                    }

                    av_Questions[j] = exam.questions[j + addIndex];
                    av_Formulas[j] = exam.anwsersOrFormulas[j + addIndex];
                    av_intervals[j] = exam.intervals[j + addIndex];
                }

                exam.questions = av_Questions;
                exam.anwsersOrFormulas = av_Formulas;
                exam.intervals = av_intervals;
            }

            exam.questions = randomQuestions;
            exam.anwsersOrFormulas = randomFormulas;
            exam.intervals = randomIntervals;
        }
    }
}
