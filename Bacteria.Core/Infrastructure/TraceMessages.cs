using System;
using System.Diagnostics;

namespace Bacteria.Core.Infrastructure
{  
   [DebuggerDisplay("Text = {Text}")]
   public sealed class BoardTraceMessage
   {
      public BoardTraceMessage(string text, IBoard board)
      {
         Text = text;
         Board = board;
      }

      public IBoard Board { get; private set; }
      public string Text { get; private set; }

      public override string ToString()
      {
         return Board.ToString() + Environment.NewLine + Text;
      }
   }
}