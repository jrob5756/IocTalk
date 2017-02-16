using System;

namespace JrTech.IocTalk.StandardLibrary.Utilities.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
