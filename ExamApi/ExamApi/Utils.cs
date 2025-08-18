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
    public static class Utils
    {
        public static char keySymbol = '~';
        public static string keyWord = "equ";

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
