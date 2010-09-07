namespace Integrator.HelloWorld
{
    public static class ConfigurationExtensions
    {
        public static DatabaseExpression UseDefaultConfiguration(this DatabaseExpression expression)
        {
            expression
                .AutoDrop(true)
                .Use("HelloWorld")
                .ConnectWith("Integrator");

            return expression;
        }
    }
}