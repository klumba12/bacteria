using System;

namespace Bacteria.Core
{
   public abstract class HumanPlayer : Player
   {
      protected HumanPlayer() 
      { 
      }

      public abstract void Think(Action ready);
   }
}