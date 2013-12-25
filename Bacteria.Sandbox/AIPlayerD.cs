using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Sandbox
{
   public class AIPlayerD : AIPlayer
   {
      private readonly int maxDepth;

      public AIPlayerD()
         : this(2)
      {
      }

      internal AIPlayerD(int maxDepth)
      {
         this.maxDepth = maxDepth;
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
            //.Where(node => node.State.ToUnit(board.Player) > 0)
               .Aggregate(0,
                  (score, node) =>
                     score + (int)node.State.ToUnit(board.Player));
      }

      private void MakeMove(ThinkBox box)
      {
         Func<IBoard, float, float, int, float> makeMove = null;
         var baseline = box.Board;
         makeMove = (position, a, b, depth) =>
         {
            if (depth == maxDepth)
               return Score(position);

            var moves = Heuristic.GetPossibleMoves(position);
            if (!moves.Any())
               return Score(position);

            foreach (var move in moves)
            {
               var score =
                  move
                     .Players
                     .Where(player => player != move.Player)
                     .Sum(player =>
                        {
                           using (move.Flip(player))
                              return -makeMove(move, -b, -a, depth + 1);
                        });

               if (score > a)
               {
#if DEBUG
                  Trace.WriteLine(new BoardTraceMessage(
                     string.Format(
                        "a: {0}              \n\r" +
                        "s: {1}              \n\r" +
                        "b: {2}                  ",
                        a, score, b),
                     move));
#endif
                  a = score;
                  if (depth == 0) box.Result = move;
                  if (b <= a) break;
               }
            }

            return a;
         };

         makeMove(box.Board, float.NegativeInfinity, float.PositiveInfinity, 0);
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