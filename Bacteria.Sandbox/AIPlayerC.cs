using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;
using System.Collections.Generic;

namespace Bacteria.Sandbox
{
   public class AIPlayerC : AIPlayerP
   {
      private readonly Dictionary<Unit, float> scores;
      public AIPlayerC(Dictionary<Unit, float> scores)
      {
         this.scores = scores;
      }

      protected override float Score(IBoard board)
      {
         return
             board
                .Aggregate(0f,
                   (score, node) =>
                   {
                      var unit = node.State.ToUnit(board.Player);
                      return score + scores[unit];
                    });
      }

   }
}