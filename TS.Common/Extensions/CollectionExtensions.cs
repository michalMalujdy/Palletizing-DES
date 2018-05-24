using System.Collections.Generic;
using System.Linq;

namespace TS.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) where T : class
        {
            return collection == null || !collection.Any();
        }
    }
}
