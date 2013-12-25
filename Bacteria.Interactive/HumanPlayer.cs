using System;
using Bacteria.Core;

namespace Bacteria.Interactive
{
   internal sealed class HumanPlayer : Core.HumanPlayer
   {
      private readonly BoardView view;

      public HumanPlayer(BoardView view) 
      {
         this.view = view;
      }

      public override void Think(Action ready)
      {
         view.Invoke((Action)(() => view.WaitForMove(ready)));
      }

      internal override bool MakeMove(ThinkBox box)
      {
         box.Result = box.Board;
         return view.MakeMove(box.Board);
      }
   }
}