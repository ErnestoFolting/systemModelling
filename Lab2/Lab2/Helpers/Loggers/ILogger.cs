namespace Lab3.Helpers.Loggers
{
    public interface ILogger : IDisposable
    {
        public void Log(string message);
    }
}
