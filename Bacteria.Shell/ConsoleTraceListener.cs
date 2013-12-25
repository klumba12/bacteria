using System;
using System.Linq;
using System.Text;
using Bacteria.Core;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Shell
{
   internal class ConsoleTraceListener : System.Diagnostics.ConsoleTraceListener
   {
      public ConsoleTraceListener()
         : base()
      {
      }

      public override void Write(object o)
      {
         var message = o as BoardTraceMessage;
         if (null != message)
         {
            Write(message);
         }
         else
         {
            base.Write(o);
         }
      }

      public override void WriteLine(object o)
      {
         var message = o as BoardTraceMessage;
         if (null != message)
         {
            Write(message);
         }
         else
         {
            base.WriteLine(o);
         }
      }

      private void Write(BoardTraceMessage message)
      {
         var cursorTop = System.Console.CursorTop;
         Write( 
            new StringBuilder()
               .AppendLine()
               .AppendLine(message.Board.ToString())
               .AppendLine(message.Text)
               .AppendLine(
                  string.Join(
                     Environment.NewLine,
                     message.Board.Players
                        .Select(player => GetState(message.Board, player))
                        .ToArray()))
               .ToString());

         System.Console.CursorTop = cursorTop;
      }

      private static string GetState(IBoard board, Player player)
      {
         return
            string.Format(
               "{0}: {1}",
               player,
               board
                  .Where(node => node.State.ToUnit(player) > 0)
                  .Count());
      }
   }
}
