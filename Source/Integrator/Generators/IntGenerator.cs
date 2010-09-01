using System;

namespace Integrator.Generators
{
    [GeneratorFor(typeof(int))]
    public class IntGenerator : IGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);
        public object Generate()
        {
            return Random.Next(int.MinValue, int.MaxValue);
        }
    }
}