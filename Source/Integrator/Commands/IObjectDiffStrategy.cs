namespace Integrator.Commands
{
    public interface IObjectDiffStrategy
    {
        DiffResult Diff<T>(T x, T y) where T : class;
    }
}