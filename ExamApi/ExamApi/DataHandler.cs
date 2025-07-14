using System;

namespace ExamApi
{
    public struct Interval
    {
        public int _from;
        public int _to;

        public Interval(int _from, int _to) : this()
        {
            this._from = _from;
            this._to = _to;
        }
    }
    public enum ExamType
    {
        Test,

        OpenAnwser
    }

    public class Exam
    {
        public const string filePath = "Exams/";
        public string fileName = "New Exam";

        public int availableQuestions;
        public int timeToSolve;
        public string[] questions;
        public string[] formulas;
        public Interval[][] intervals;
        public ExamType examType;

        public bool useRandom = false;
    }
    public class Student
    {
        public string name;
        public string _class;

        public int mark;
        public int rightResults;
    }
}

