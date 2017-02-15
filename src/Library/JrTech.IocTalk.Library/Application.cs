using JrTech.IocTalk.Library.Utilities;
using JrTech.IocTalk.Library.Utilities.Logging;

namespace JrTech.IocTalk.Library
{
    public class Application
    {
        private readonly ICalculator _calculator;
        private readonly ILogger _logger;

        public Application(ICalculator calculator, ILogger logger)
        {
            _calculator = calculator;
            _logger = logger;
        }

        public void Run()
        {
            _logger.Log("Hello World!");
        }
    }
}
