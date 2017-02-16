using System.IO;

namespace JrTech.IocTalk.StandardLibrary.Utilities.Logging
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            File.AppendAllText("output.log", message);
        }
    }
}