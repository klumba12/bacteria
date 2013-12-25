using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using System.Collections.Generic;

namespace Bacteria.Sandbox
{
   class Program
   {
      static void Main(string[] args)
      {
         var rules = new GameRules(moveLimit: 3, boardLength: 6);
         var timeout = TimeSpan.FromSeconds(60);
         var players =
            new Func<Player>[]
            {
             //  () => new AIPlayerR{ Name = "PlayerR#0", Timeout = timeout,  },
                 () => new AIPlayerP{ Name = "PlayerP#1", Timeout = timeout,  },
                 () => new AIPlayerP1{ Name = "PlayerP1#2", Timeout = timeout,  },
                 () => new AIPlayerP2{ Name = "PlayerP2#3", Timeout = timeout,  },
            //     () => new AIPlayerD{ Name ="PlayerD#2", Timeout = timeout,  },
            //   () => new AIPlayerD1{ Name = "PlayerD1#3", Timeout = timeout, },
            };

         var color = 8;
         var colors = players.ToDictionary(p => p().Name, _ => ++color);
         Action<string> write =
            p =>
            {
               var temp = Console.ForegroundColor;
               if (p != "None")
                  Console.ForegroundColor = (ConsoleColor)colors[p];

               Console.Write(p);
               Console.ForegroundColor = temp;
            };

         var stats = new Dictionary<string, int>();
         foreach (var p1 in players)
            foreach (var p2 in players)
            {
               var ps = new[] { p1(), p2() };
               if (ps[0].Name == ps[1].Name)
                  continue;

               var rounds = 10;
               write(ps[0].Name);
               Console.Write(" vs. ");
               write(ps[1].Name);
               Console.WriteLine();

               while (rounds-- > 0)
               {
                  Console.Write("round " + rounds + ": ");

                  var game =
                     new GameBuilder(rules)
                        .Build(ps);

                  var startPositions =
                     new[]
                     {
                        new {X = 0, Y = game.Board.Length - 1},
                        new {X = game.Board.Length - 1, Y = 0},
                     };

                  int i = 0;
                  foreach (var player in game.Board.Players)
                     using (game.Board.Flip(player))
                        game.Board.Occupy(startPositions[i].X, startPositions[i++].Y);

                  var stopwatch = new Stopwatch();
                  stopwatch.Start();
                  int moves = 0;
                  var winner =
                    game.Play()
                       .Select(
                           player =>
                           {
                              moves++;
                              return player;
                           })
                       .Last();
                  stopwatch.Stop();

                  write(winner.Name);
                  Console.WriteLine(" won, in " + moves + " moves[" + stopwatch.Elapsed + "]");

                  int numberOfWins;
                  if (!stats.TryGetValue(winner.Name, out numberOfWins))
                     stats[winner.Name] = 1;
                  else
                     stats[winner.Name] = numberOfWins + 1;
               }

               Console.WriteLine();
            }

         Console.WriteLine("Stat: ");
         var place = 0;
         foreach (var pair in stats.OrderByDescending(p => p.Value))
         {
            Console.Write(++place + ".");
            write(pair.Key);
            Console.WriteLine(": " + pair.Value + " wins");
         }
         Console.ReadKey();
      }
   }
}