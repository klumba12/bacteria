using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Utility;
using Bacteria.Core.Infrastructure;
using Bacteria.Sandbox;

namespace Bacteria.Test
{
   internal static class Helper
   {
      private static readonly Player player1 = new AIPlayerD();
      private static readonly Player player2 = new AIPlayerD();

      public const string EmptyView =
         "+ + + + + + + + + m" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "e + + + + + + + + +";

      public static Board ToBoard(string view, GameRules rules)
      {
         var players =
            new[] { player1, player2, };

         var board =
            new Board(rules, players)
            {
               Player = player1,
            };

         view =
            view
               .Replace(Environment.NewLine, string.Empty)
               .Replace(" ", string.Empty);

         if (view.Length != board.Length * board.Length)
            throw new InvalidOperationException("Invalid view size");

         for (int i = 0; i < board.Length; i++)
         {
            for (int j = 0; j < board.Length; j++)
            {
               var token = view[i * board.Length + j];
               switch (token)
               {
                  case 'm':
                     board[i, j] = State.Alive(player1);
                     break;
                  case 'M':
                     board[i, j] = State.Zombie(player1);
                     break;
                  case 'N':
                     board[i, j] = State.AliveZombie(player1);
                     break;
                  case 'e':
                     board[i, j] = State.Alive(player2);
                     break;
                  case 'E':
                     board[i, j] = State.Zombie(player2);
                     break;
                  case 'F':
                     board[i, j] = State.AliveZombie(player2);
                     break;
                  case '+':
                     board[i, j] = State.Void();
                     break;
                  default:
                     throw new InvalidOperationException(
                        string.Format("Invalid token '{0}'", token));
               }
            }
         }

         return board;
      }

      public static Board Intersect(this IEnumerable<IBoard> boards)
      {
         Debug.Assert(boards.Any());

         var firstBoard = boards.First();

         return
            boards.Aggregate(
               new Board(firstBoard.Rules, firstBoard.Players.ToArray()) { Player = firstBoard.Player },
               (intersection, board) =>
               {
                  foreach (var node in board)
                     if (Math.Abs((int)intersection[node.X, node.Y].ToUnit(firstBoard.Player)) < Math.Abs((int)node.State.ToUnit(firstBoard.Player)))
                        intersection[node.X, node.Y] = node.State;

                  return intersection;
               });
      }

      public static bool IsEqual(this IBoard board1, IBoard board2)
      {
         return
            Enumerable.SequenceEqual(
               board1.Select(node => node.State),
               board2.Select(node => node.State));
      }

      public static IDisposable TrackTime(Action<TimeSpan> track)
      {
         var stopwatch = new Stopwatch();
         stopwatch.Start();

         return
            new DisposableMethodHandler(() =>
            {
               stopwatch.Stop();
               track(stopwatch.Elapsed);
            });
      }
   }

   internal class BoardEqualityComparer : IEqualityComparer<IBoard>
   {
      public bool Equals(IBoard x, IBoard y)
      {
         return x.IsEqual(y);
      }

      public int GetHashCode(IBoard board)
      {
         return
            board
               .Sum(node =>
                  node.X * (int)node.State.ToUnit(board.Player)
                + node.Y * (int)node.State.ToUnit(board.Player));
      }
   }
}