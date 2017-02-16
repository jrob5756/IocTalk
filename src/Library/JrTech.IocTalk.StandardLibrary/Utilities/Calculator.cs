using System.Linq;

namespace JrTech.IocTalk.StandardLibrary.Utilities
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
