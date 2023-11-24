namespace Lab3.Helpers.Loggers
{
    public class Logger : ILogger, IDisposable
    {
        private StreamWriter writer;
        private bool _isActive;
        public Logger(bool isActive)
        {
            writer = new StreamWriter("log.txt", false);
            _isActive = isActive;

        }

        public void Log(string message)
        {
            if (_isActive)
            {
                writer.WriteLine(message);
                Console.WriteLine(message);
            }
        }

        public void Dispose()
        {
            writer?.Close();
        }
    }
}
