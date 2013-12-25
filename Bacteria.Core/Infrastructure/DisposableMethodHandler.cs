using System;

namespace Bacteria.Core.Infrastructure
{
   internal sealed class DisposableMethodHandler : IDisposable
   {
      private readonly Action action;
      private bool isDisposed;

      public DisposableMethodHandler(Action action)
      {
         this.action = action;
      }

      public void Dispose()
      {
         if (!isDisposed)
         {
            isDisposed = true;
            action();
         }
      }
   }
}