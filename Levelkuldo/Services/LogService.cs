namespace Levelkuldo.Services
{
    public class LogService
    {
        private List<string> _logs = new List<string>();
        public List<string> Logs => _logs;

        public void Insert(string text)
        {
            _logs.Add(text);
        }
    }
}
