using System.Collections.Generic;
using System.Linq;

namespace Bacteria.Core
{
   public sealed class Game
   {
      internal Game(Board board)
      {
         Board = board;
      }

      public Board Board { get; private set; }

      public IEnumerable<Player> Play()
      {
         var loop =
            new GameLoop(
               Board.Players,
               player =>
               {
                  using (Board.Flip(player))
                     return Heuristic.GetPossibleMoves(Board).Any();
               });

         var history = new List<IBoard>();
         foreach (var player in loop)
         {
            Board.Player = player;
            var box = new ThinkBox(loop.Round, Board.Clone(), history.ToArray());
            if (!player.MakeMove(box))
               break;

            Board.Commit(box.Result);
            history.Add(box.Result);
            yield return player;
         }

         yield return GetResult();
      }

      private Player GetResult()
      {
         var winner =
            Board.Players
               .FirstOrDefault(
                  player =>
                  {
                     using (Board.Flip(player))
                        return Heuristic.GetPossibleMoves(Board).Any();
                  });

         if (null == winner)
         {
            var results =
               Board.Players
                  .Select(
                     player =>
                        new
                        {
                           Player = player,
                           Score =
                              Board
                                 .Where(node =>
                                    node.State == State.Alive(player) ||
                                    node.State == State.AliveZombie(player))
                                 .Count(),
                        });

            //
            // TODO: Add possibility to return several players as winners
            //

            winner =
               results
                  .GroupBy(result => result.Score)
                  .OrderByDescending(group => group.Key)
                  .First()
                  .First()
                  .Player;
         }

         return winner;
      }
   }
}