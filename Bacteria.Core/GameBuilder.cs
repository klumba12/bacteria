using System;
using System.Collections.Generic;
using System.Linq;

namespace Bacteria.Core
{
   public sealed class GameBuilder
   {
      private readonly GameRules rules;

      public GameBuilder(GameRules rules)
      {
         this.rules = rules;
      }

      public Game Build(IEnumerable<Player> players)
      {
         var units = players.ToArray();
         if (units.Length == 0)
            throw new ArgumentException("Invalid players count, must be greater than zero");

         var board =
            new Board(rules, units)
            {
               Player = units[0],
            };

         return new Game(board);
      }
   }
}