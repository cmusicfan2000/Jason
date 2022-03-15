using System;
using System.Collections.Generic;
using System.Linq;

namespace Jason.Views.Helpers
{
    public static class BindingFuncs
    {
        public static IEnumerable<object> Subset(IEnumerable<object> set, IEnumerable<int> indexes)
        {
            if (indexes == null ||
                set?.Any() != true)
                return Enumerable.Empty<object>();

            return set.Where((o, i) => indexes.Contains(i));
        }
    }
}
