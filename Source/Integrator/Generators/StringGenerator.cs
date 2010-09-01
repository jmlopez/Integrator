using System;
using System.Text;

namespace Integrator.Generators
{
    [GeneratorFor(typeof(string))]
    public class StringGenerator : IGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);
        public object Generate()
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 32; ++i)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}