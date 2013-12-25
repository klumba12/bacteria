using System;
using System.Collections.Generic;

namespace Bacteria.Core
{
   public interface IBoard : IHistoryBoard
   {
      // Remove due to sandbox
      new State this[int x, int y] { get; set; }

      IEnumerable<Player> Players { get; }
      IDisposable Flip(Player player);
      State Occupy(int x, int y);
      IBoard Clone();
   }
}