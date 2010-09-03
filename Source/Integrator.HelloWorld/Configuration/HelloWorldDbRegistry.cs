namespace Integrator.HelloWorld.Configuration
{
    public class HelloWorldDbRegistry : DatabaseRegistry
    {
        public HelloWorldDbRegistry()
        {
            Database
                .Use("HelloWorld")
                .ConnectWith("Integrator");
        }
    }
}