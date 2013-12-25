using System;

namespace Bacteria.Core
{
   public abstract class Player
   {
      public readonly static Player None = new NullPlayer() { Name = "None", };

      //
      // Intended for use only by State type 
      //

      internal static readonly State VoidState = new State(None, Piece.Void);
      internal readonly State AliveZombieState;
      internal readonly State ZombieState;
      internal readonly State AliveState;

      protected Player()
      {
         AliveState = new State(this, Piece.Alive);
         ZombieState = new State(this, Piece.Zombie);
         AliveZombieState = new State(this, Piece.AliveZombie);
      }

      public string Name { get; set; }

      public override string ToString()
      {
         return Name;
      }

      internal abstract bool MakeMove(ThinkBox box);

      #region Nested Types

      internal sealed class NullPlayer : Player
      {
         internal override bool MakeMove(ThinkBox box)
         {
            throw new InvalidOperationException("Null player can't make moves");
         }
      }

      #endregion
   }
}