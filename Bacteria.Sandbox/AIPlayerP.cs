using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Sandbox
{
   public class AIPlayerP : AIPlayer
   {
      public AIPlayerP()
      {
      }

      protected override void Think(ThinkBox box)
      {
         if (box.Round == 0)
            MakeFirstMove(box);
         else
            MakeMove(box);
      }

      protected virtual float Score(IBoard board)
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
                         case Unit.AlienAliveZombie: return score - 4;
                         case Unit.AlienZombie: return score + 1;
                         case Unit.AllyAlive: return score + 2;
                         case Unit.AllyAliveZombie: return score + 4;
                         case Unit.AllyZombie: return score - 1;
                         case Unit.None: return score;
                         default: return score;
                      }
                   });
      }

      private void MakeMove(ThinkBox box)
      {
         var moves = Heuristic.GetPossibleMoves(box.Board);
         var a = float.NegativeInfinity;
         foreach (var move in moves)
         {
            var score = Score(move);
            if (score > a)
            {
#if DEBUG
               Trace.WriteLine(new BoardTraceMessage(
                  string.Format(
                     "a: {0}              \n\r" +
                     "s: {1}              \n\r",
                     a, score),
                  move));
#endif
               a = score;
               box.Result = move;
            }
         }
      }

      private void MakeFirstMove(ThinkBox box)
      {
         var moves =
            Heuristic
               .GetPossibleMoves(box.Board)
               .ToArray();

         var random = new Random();
         var index = random.Next(moves.Length - 1);
         foreach (var node in moves[index].Where(node => node.State.ToUnit(box.Board.Player) > 0))
            box.Board[node.X, node.Y] = node.State;

         box.Result = box.Board;
      }
   }
}