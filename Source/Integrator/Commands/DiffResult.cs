using System.Collections.Generic;
using System.Linq;

namespace Integrator.Commands
{
    public class DiffResult
    {
        private readonly IEnumerable<Diff> _diffs;

        public DiffResult(IEnumerable<Diff> diffs)
        {
            _diffs = diffs;
        }

        public bool IsEmpty { get { return !_diffs.Any(); } }
        public IEnumerable<Diff> Diffs { get { return _diffs; } }
    }
}