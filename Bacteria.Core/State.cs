
namespace Bacteria.Core
{
   public sealed class State
   {
      internal State(Player player, Piece piece)
      {
         Player = player;
         Piece = piece;
      }

      public Player Player
      {
         get;
         private set;
      }

      public Piece Piece
      {
         get;
         private set;
      }

      public static State Void()
      {
         return Player.VoidState;
      }

      public static State Alive(Player player)
      {
         return player.AliveState;
      }

      public static State Zombie(Player player)
      {
         return player.ZombieState;
      }

      public static State AliveZombie(Player player)
      {
         return player.AliveZombieState;
      }

      public override string ToString()
      {
         return string.Format("{0} {1}", Player, Piece);
      }
   }
}