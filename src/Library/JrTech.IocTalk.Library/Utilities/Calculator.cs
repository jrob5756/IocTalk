using System.Linq;

namespace JrTech.IocTalk.Library.Utilities
{
    public class Calculator : ICalculator
    {
        public int Sum(params int[] numbers)
        {
            if (numbers == null)
            {
                return 0;
            }

            return numbers.Sum();
        }
    }
}
