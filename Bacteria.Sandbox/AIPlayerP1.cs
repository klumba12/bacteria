using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Sandbox
{
   public class AIPlayerP1 : AIPlayerP
   {
      public AIPlayerP1()
      {
      }

      protected override float Score(IBoard board)
      {
         return
            board
               .Aggregate(0f,
                  (score, node) =>
                  {
                     var unit = node.State.ToUnit(board.Player);
                     switch (unit)
                     {
                        case Unit.AlienAlive: return score;
                        case Unit.AlienAliveZombie: return score;
                        case Unit.AlienZombie: return score;
                        case Unit.AllyAlive: return score;
                        case Unit.AllyAliveZombie: return score;
                        case Unit.AllyZombie: return score;
                        case Unit.None: return score;
                        default: return score;
                     }
                  });
      }
   }
}