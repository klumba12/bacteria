using System;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Utility;

namespace Bacteria.Sandbox
{
   public class AIPlayerR : AIPlayer
   {
      public AIPlayerR()
      {
      }

      protected override void Think(ThinkBox box)
      {
         var moves =
            Heuristic
               .GetPossibleMoves(box.Board)
               .ToArray();

         if (moves.Length > 0)
         {
            var random = new Random();
            var index = random.Next(moves.Length - 1);
            box.Result = moves[index];
         }
      }
   }
}