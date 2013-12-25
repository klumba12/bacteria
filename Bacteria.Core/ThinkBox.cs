
namespace Bacteria.Core
{
   public sealed class ThinkBox
   {
      public ThinkBox(int round, IBoard board, IHistoryBoard[] history) 
      {
         Round = round;
         Board = board;
         History = history;
      }

      public int Round { get; private set; }
      public IHistoryBoard[] History { get; private set; }
      
      public IBoard Board { get; private set; }
      public IBoard Result { get; set; }
   }
}