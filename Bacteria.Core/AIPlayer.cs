using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bacteria.Core
{
   public abstract class AIPlayer : Player
   {
      public AIPlayer()
      {
         Timeout = TimeSpan.FromMilliseconds(-1);
      }

      public TimeSpan Timeout { get; set; }

      internal override bool MakeMove(ThinkBox box)
      {
         var timeout = new CancellationTokenSource();
         var task = new Task(() => Think(box), timeout.Token);

         task.Start();
         if (!task.Wait(Timeout))
            timeout.Cancel();

         return null != box.Result;
      }

      protected abstract void Think(ThinkBox box);
   }
}