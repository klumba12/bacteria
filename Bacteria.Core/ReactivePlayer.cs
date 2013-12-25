using System;
using Bacteria.Core.Utility;

namespace Bacteria.Core
{
   public sealed class ReactivePlayer : Player
   {
      private readonly Player player;
      private Action wait;

      private ReactivePlayer(Player player)
      {
         this.player = player;
      }

      public event EventHandler Ready;

      public void Wait()
      {
         wait();
      }

      private void OnReady()
      {
         Ready.Raise(this);
      }

      public static ReactivePlayer Create(AIPlayer player)
      {
         var reactivePlayer = new ReactivePlayer(player);
         reactivePlayer.wait = () => reactivePlayer.OnReady();
         return reactivePlayer;
      }

      public static ReactivePlayer Create(HumanPlayer player)
      {
         var reactivePlayer = new ReactivePlayer(player);
         reactivePlayer.wait = () => player.Think(() => reactivePlayer.OnReady());
         return reactivePlayer;
      }

      internal override bool MakeMove(ThinkBox box)
      {
         return player.MakeMove(box);
      }
   }
}