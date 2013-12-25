using System.Collections.Generic;
using System.Diagnostics;

namespace Bacteria.Core
{
   public interface IHistoryBoard : IEnumerable<Node>
   {
      GameRules Rules { get; }
      int Length { get; }
      Player Player { get; }
      State this[int x, int y] { get; }
   }
}