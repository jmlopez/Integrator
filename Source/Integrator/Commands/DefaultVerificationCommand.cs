using NUnit.Framework;

namespace Integrator.Commands
{
    public class DefaultVerificationCommand<TEntity> : IVerificationCommand<TEntity>
        where TEntity : class
    {
        private readonly IObjectDiffStrategy _diffStrategy;

        public DefaultVerificationCommand(IObjectDiffStrategy diffStrategy)
        {
            _diffStrategy = diffStrategy;
        }

        public void Verify(TEntity beforeInsert, TEntity afterInsert)
        {
            var diffResult = _diffStrategy.Diff(beforeInsert, afterInsert);
            if(diffResult.IsEmpty)
            {
                return;
            }

            foreach (var diff in diffResult.Diffs)
            {
                Assert.Fail("Verification failed on property: {0}. Expected \"{1}\" but found \"{2}\"", diff.Property.Name, 
                    diff.PreviousValue, diff.NewValue);
            }
        }
    }
}