
namespace Bacteria.Core
{
   public sealed class GameRules
   {
      public GameRules(int moveLimit = 3, int boardLength = 10)
      {
         MoveLimit = moveLimit;
         BoardLength = boardLength;
      }

      public int MoveLimit { get; private set; }
      public int BoardLength { get; private set; }
   }
}