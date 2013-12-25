
namespace Bacteria.Core.Utility
{
   public static class StateExtensions
   {
      public static Unit ToUnit(this State state, Player player)
      {
         return
            player == state.Player
               ? (Unit)state.Piece
               : (Unit)(-(int)state.Piece);
      }

      public static Unit ToUnit(this State state)
      {
         return state.ToUnit(state.Player);
      }

      public static bool CorrelatesWith(this State state1, State state2)
      {
         return (state1.ToUnit() ^ state2.ToUnit(state1.Player)) >= 0;
      }
   }
}