namespace Levelkuldo.Services
{
    public class LogService
    {
        private static List<string> _logs = new List<string>();
        public static List<string> Logs => _logs;

        public static void Insert(string text)
        {
            _logs.Add(text);
        }
    }
}
