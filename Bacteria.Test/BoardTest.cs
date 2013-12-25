using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using NUnit.Framework;

namespace Bacteria.Test
{
   [TestFixture]
   public class BoardTest
   {
      private readonly GameRules rules3 = new GameRules(moveLimit: 3);

      [Test]
      public void Test_Occupy_Empty_Node()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(0, 8);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Node_That_Nearby_Allien_Alive_Zombie()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + m +" +
            "+ + + + + + + m + +" +
            "+ + + + + + F + + +" +
            "+ + + + + e + + + +" +
            "+ + + + e + + + + +" +
            "+ + + e + + + + + +" +
            "+ + e + + + + + + +" +
            "+ e + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + + m" +
            "+ + + + + + + + m +" +
            "+ + + + + + + m + +" +
            "+ + + + + + F m + +" +
            "+ + + + + e + + + +" +
            "+ + + + e + + + + +" +
            "+ + + e + + + + + +" +
            "+ + e + + + + + + +" +
            "+ e + + + + + + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(3, 7);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Occupy_Node_Is_Not_Rooted()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "M + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(9, 0);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Occupy_Node_Is_Rooted()
      {
         var view =
            "+ + + + + + + + e m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + N m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(0, 8);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Empty_Node_When_Occupy_Node_Connects_With_Not_Rooted_Nodes()
      {
         var view =
            "+ + + + + + + M + m" +
            "+ + + + + M M + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + N m m" +
            "+ + + + + N N + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(0, 8);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Occupy_Node_Connects_With_Not_Rooted_Nodes()
      {
         var view =
            "+ + + + + + + M e m" +
            "+ + + + + M M + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + N N m" +
            "+ + + + + N N + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(0, 8);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Not_Rooted_Occupy_Node_Connects_With_Not_Rooted_Nodes()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + M + + + + + + +" +
            "+ M + + + + + + + +" +
            "e M + + + + + + + +";

         var etalonView =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + M + + + + + + +" +
            "+ M + + + + + + + +" +
            "M M + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(9, 0);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Enemy_Node_Is_Root()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + F + + + + + + +" +
            "+ + F + + + + + + +" +
            "+ F + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + E + + + + + + +" +
            "+ + E + + + + + + +" +
            "+ E + + + + + + + +" +
            "M + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(9, 0);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Enemy_Node_When_Not_Rooted_Occupy_Node_Connects_With_Not_Rooted_Nodes1()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + m + + + + + + +" +
            "+ + N + + + + + + +" +
            "+ + F + + M + + + +" +
            "+ + + F M F M M F e" +
            "+ + + M e + + + + +" +
            "+ + + M F + N F + +" +
            "+ + E + M F F + + +" +
            "+ + + + + + M + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + + + + m" +
            "+ + m + + + + + + +" +
            "+ + N + + + + + + +" +
            "+ + E + + N + + + +" +
            "+ + + E N E N N F e" +
            "+ + + N N m + + + +" +
            "+ + + N E + N E + +" +
            "+ + E + N E E + + +" +
            "+ + + + + + M + + +" +
            "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         board.Occupy(5, 5);
         board.Occupy(5, 4);

         Assert.IsTrue(board.IsEqual(Helper.ToBoard(etalonView, rules3)));
      }

      [Test]
      public void Test_Occupy_Performance()
      {
         var view =
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +";

         for (int i = 0; i < 10; i++)
         {
            var boards =
               Enumerable
                  .Range(0, 10)
                  .Select(_ => Helper.ToBoard(view, rules3))
                  .ToArray();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var board in boards)
            {
               for (int x = 0; x < board.Length; x++)
                  for (int y = 0; y < board.Length; y++)
                     board.Occupy(x, y);
            }

            Trace.WriteLine(
               string.Format(
                  "Board occupy performance: {0}ms",
                     stopwatch.ElapsedMilliseconds));
         }
      }
   }
}