using System;

namespace Bacteria.Core.Utility
{
   public static class EventExtensions
   {
      public static void Raise(this EventHandler handler, object sender)
      {
         if (null != handler)
            handler(sender, EventArgs.Empty);
      }
   }
}