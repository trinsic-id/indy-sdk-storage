using System.Collections.Generic;
using System.Linq;

namespace Indy.Sdk.Storage.Tests
{
    public static class Extensions
    {
        public static int MaxOrDefault(this IEnumerable<int> enumerable)
        {
            return enumerable.Any() ? enumerable.Max() : 0;
        }
    }
}