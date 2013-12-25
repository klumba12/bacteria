using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using System;
using Bacteria.Sandbox;

namespace Bacteria.Shell
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.SetWindowSize(40, 30);
         Trace.Listeners.Add(new ConsoleTraceListener());

         var rules = new GameRules(moveLimit: 3, boardLength: 6);
         var game =
            new GameBuilder(rules)
               .Build(
                  new Player[] 
                  {
                     new AIPlayerP1{ Name = "PlayerP1" },
                     new AIPlayerP{ Name = "PlayerP" },
                     //new AIPlayerD{ Name = "PlayerD" },
                    // new AIPlayerD{ Name = "Player4" },
                  });

         var startPositions = new[]
         {
            new {X = 0, Y = game.Board.Length - 1},
            new {X = game.Board.Length - 1, Y = 0},
            new {X = 0, Y = 0},
            new {X = game.Board.Length - 1, Y = game.Board.Length - 1},
         };

         int i = 0;
         foreach (var player in game.Board.Players)
            using (game.Board.Flip(player))
               game.Board.Occupy(startPositions[i].X, startPositions[i++].Y);

         int round = 0;
         var winner =
           game.Play()
              .Select(
                  player =>
                  {
                     Console.Clear();
                     Console.WriteLine(player + ": " + round++);
                     DrawBoard(game.Board);
                     Console.ReadLine();
                     return player;
                  })
              .Last();

         Console.WriteLine(winner + " Win!");
         Console.ReadKey();
      }

      static void DrawBoard(Board board)
      {
         var color = 8;
         var colors =
            board.Players
               .ToDictionary(p => p, _ => ++color);

         var ident =
            (colors.Count * 3 + 2)
               .ToString().Length + 1;

         Action<Player, string> writeTag =
            (player, tag) =>
            {
               var temp = Console.ForegroundColor;
               if (player != Player.None)
                  Console.ForegroundColor = (ConsoleColor)colors[player];

               Console.Write(tag);
               Console.Write(' ');
               Console.ForegroundColor = temp;
            };

         for (var x = 0; x < board.Length; x++)
         {
            for (var y = 0; y < board.Length; y++)
            {
               var state = board[x, y];
               switch (state.Piece)
               {
                  case Piece.Alive:
                     writeTag(state.Player, "O");
                     break;
                  case Piece.Zombie:
                     writeTag(state.Player, "X");
                     break;
                  case Piece.AliveZombie:
                     writeTag(state.Player, "Z");
                     break;
                  default:
                     writeTag(state.Player, " ");
                     break;
               }
            }
            System.Console.WriteLine();
         }
      }
   }
}
