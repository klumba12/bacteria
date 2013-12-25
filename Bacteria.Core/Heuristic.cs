using System;
using System.Collections.Generic;
using System.Linq;
using Bacteria.Core.Utility;

namespace Bacteria.Core
{
   using Nodes = IEnumerable<Node>;

   public static class Heuristic
   {
      public static IEnumerable<IBoard> GetPossibleMoves(IBoard board)
      {
         return
            GetPossibleNodes(board)
               .Select(nodes => Occupy(board, nodes));
      }

      public static Nodes GetAdjoinedNodes(IBoard board, Node node)
      {
         var left = Math.Max(0, node.X - 1);
         var right = Math.Min(board.Length - 1, node.X + 1);
         var top = Math.Max(0, node.Y - 1);
         var bottom = Math.Min(board.Length - 1, node.Y + 1);

         return
            Enumerable.Range(left, right - left + 1)
               .SelectMany(x =>
                  Enumerable.Range(top, bottom - top + 1)
                    .Where(y => x != node.X || y != node.Y),
                  (x, y) => new Node(x, y, board[x, y]));
      }

      public static bool HasRoot(IBoard board, Node node)
      {
         return HasRoot(board, node, new HashSet<Node>());
      }

      private static bool HasRoot(IBoard board, Node hitNode, HashSet<Node> visitedNodes)
      {
         visitedNodes.Add(hitNode);

         return
            (from node in GetAdjoinedNodes(board, hitNode)
             where 
               !visitedNodes.Contains(node) &&
               node.State.CorrelatesWith(hitNode.State)
             let unit = node.State.ToUnit()
             where
                (unit == Unit.AllyAlive ||
                ((unit == Unit.AllyZombie || unit == Unit.AllyAliveZombie) &&
                HasRoot(board, node, visitedNodes)))
             select true)
             .Any();
      }

      private static Nodes GetBindedNodes(IBoard board)
      {
         return 
            from node in board
            let unit = node.State.ToUnit(board.Player)
            where 
               unit == Unit.AllyAlive || 
               unit == Unit.AllyAliveZombie ||
               unit == Unit.AllyZombie && HasRoot(board, node)
            select node;
      }

      private static Nodes GetMovableNodes(IBoard board, Nodes hitNodes)
      {
         return
            (from hitNode in hitNodes
            from node in GetAdjoinedNodes(board, hitNode)
            let unit = node.State.ToUnit(board.Player)
            where unit == Unit.None || unit == Unit.AlienAlive
            select node)
            .Distinct();
      }

      private static IEnumerable<Nodes> GetPossibleNodes(IBoard board)
      {
         var blasts =
            GenerateBlasts(board)
               .Take(board.Rules.MoveLimit)
               .ToArray();

         var result = Enumerable.Empty<Nodes>();
         foreach (var nextKs in GenerateKs(board.Rules.MoveLimit))
         {
            var ks = nextKs.ToArray();
            Func<IEnumerable<Nodes>, int, IEnumerable<Nodes>> generateKsCombs = null;

            generateKsCombs =
               (nss, i) =>
               {
                  if (i == ks.Length)
                     return nss;

                  if (i == 0)
                  {
                     return
                        generateKsCombs(
                          nss.SelectMany(ns =>
                             GenerateCombinations(
                                GetMovableNodes(blasts[i], ns), ks[i])),
                          i + 1);
                  }

                  return
                     generateKsCombs(
                        nss.SelectMany(ns =>
                           GenerateCombinations(
                              GetMovableNodes(blasts[i], ns), ks[i]),
                           (ns1, ns2) => ns1.Concat(ns2)),
                        i + 1);
               };

            result =
               result.Concat(
                  generateKsCombs(
                     GetBindedNodes(board).ToEnumerable(), 
                  0));
         }

         return result;
      }

      private static IEnumerable<IEnumerable<int>> GenerateKs(int limit)
      {
         if (limit == 0)
            return Enumerable.Empty<int>().ToEnumerable();

         return
            Enumerable.Range(1, limit)
              .SelectMany(i =>
                 GenerateKs(limit - i),
                 (term, terms) => term.ToEnumerable().Concat(terms));
      }

      private static IEnumerable<IBoard> GenerateBlasts(IBoard board)
      {
         yield return board.Clone();
         
         var nodes =  GetBindedNodes(board);
         while (nodes.Any())
         {
            nodes = GetMovableNodes(board, nodes);
            yield return board = Occupy(board, nodes);
         }
      }

      private static IEnumerable<Nodes> GenerateCombinations(Nodes nodes, int k)
      {
         return 
            k == 0
               ? Enumerable.Empty<Node>().ToEnumerable()
               : nodes.SelectMany((a, i) =>
                    GenerateCombinations(nodes.Skip(i + 1), k - 1)
                       .Select(c => a.ToEnumerable().Concat(c)));
      }

      private static IBoard Occupy(IBoard board, Nodes nodes)
      {
         return
            nodes.Aggregate(
               board.Clone(),
               (move, node) => 
               {
                  move.Occupy(node.X, node.Y); 
                  return move;
               });
      }
   }
}