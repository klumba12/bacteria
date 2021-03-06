﻿using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Sandbox
{
   public class AIPlayerP2 : AIPlayerP
   {
      public AIPlayerP2()
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
                        case Unit.AlienAlive: return score - 2;
                        case Unit.AlienAliveZombie: return score - 100;
                        case Unit.AlienZombie: return score + 5;
                        case Unit.AllyAlive: return score + 5;
                        case Unit.AllyAliveZombie: return score + 100;
                        case Unit.AllyZombie: return score - 5;
                        case Unit.None: return score;
                        default: return score;
                     }
                  });
      }
   }
}