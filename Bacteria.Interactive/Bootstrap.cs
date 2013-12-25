using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bacteria.Core;
using Bacteria.Sandbox;

namespace Bacteria.Interactive
{
   internal sealed class Bootstrap
   {
      public Bootstrap()
      {
      }

      public Form Run()
      {
         var view = new BoardView();

         //
         // TODO: If AI is thinking during starting new game, we must stop it
         //

         view.Start += (o, e) => StartGame(view);
         return view;
      }

      private void StartGame(BoardView view)
      {
         var players = 
            CreatePlayers(view);

         var game =
            new GameBuilder(new GameRules())
                .Build(players);

         //
         // TODO: Add more then 2 players support
         //

         using(game.Board.Flip(players[0]))
            game.Board.Occupy(0, game.Board.Length - 1);

         using (game.Board.Flip(players[1]))
            game.Board.Occupy(game.Board.Length - 1, 0);

         var gameStream =
            game.Play()
               .GetEnumerator();

         Action<ReactivePlayer> moveNext =
            player =>              
               Task.Factory.StartNew(() =>
               {
                  view.Invoke((Action)(() => view.UpdateUnitStatus(player)));
                  if (gameStream.MoveNext())
                  {
                     player.Wait();
                  }
                  else
                  {
                     MessageBox.Show(string.Format("{0} Win!", gameStream.Current.Name));
                     gameStream.Dispose();
                  }
               });

         for (int i = 0; i < players.Length; i++)
         {
            int j = i;
            players[j].Ready += (o, e) =>
               moveNext(players[(j + 1) % players.Length]);
         }

         view.Board = game.Board;
         moveNext(players[0]);
      }

      private ReactivePlayer[] CreatePlayers(BoardView view)
      {
         ReactivePlayer player1;
         ReactivePlayer player2;

         switch (view.GameMode)
         {
            case GameMode.HumanVsHuman:
               player1 = CreateHumanPlayer(view);
               player2 = CreateHumanPlayer(view);
               break;
            case GameMode.ComputerVsHuman:
               player1 = CreateAIPlayer(view);
               player2 = CreateHumanPlayer(view);
               break;
            case GameMode.HumanVsComputer:
               player1 = CreateHumanPlayer(view);
               player2 = CreateAIPlayer(view);
               break;
            default:
               player1 = CreateAIPlayer(view);
               player2 = CreateAIPlayer(view);
               break;
         }

         player1.Name = "Player1";
         player2.Name = "Player2";

         return new[] { player1, player2 };
      }

      private ReactivePlayer CreateHumanPlayer(BoardView view)
      {
         return ReactivePlayer.Create(new HumanPlayer(view));
      }

      private ReactivePlayer CreateAIPlayer(BoardView view)
      {
         return
            ReactivePlayer.Create(
               new AIPlayerD
               {
                  Timeout =
                     TimeSpan.FromMilliseconds(
                        view.GameLevel == GameLevel.WorldClass
                           ? -1
                           : (int)view.GameLevel * 5000),
               });
      }
   }
}