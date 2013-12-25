using System.Collections.Generic;

namespace Bacteria.Core.Utility
{
   public static class ObjectExtensions
   {
      public static IEnumerable<T> ToEnumerable<T>(this T instance)
      {
         yield return instance;
      }
   }
}