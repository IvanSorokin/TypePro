using System;
using System.Collections.Generic;
using System.Linq;

namespace TypePro
{
    public static class TypeProExtensions
    {
        public static T PickRandom<T>(this ICollection<T> collection) => collection.ElementAt(new Random().Next(0, collection.Count));
    }
}