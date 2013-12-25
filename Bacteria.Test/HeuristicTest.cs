using System;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using Bacteria.Core.Utility;
using NUnit.Framework;

namespace Bacteria.Test
{
   [TestFixture]
   public class HeuristicTest
   {
      private readonly GameRules rules3 = new GameRules(moveLimit: 3);
      private readonly GameRules rules2 = new GameRules(moveLimit: 2);

      [Test]
      public void Test_Has_Root_When_All_Nodes_Are_Rooted()
      {
         var view =
            "+ + + + + + + M M m" +
            "+ + + + + + + + + m" +
            "+ + + + + + + + M +" +
            "+ + + + + + + + M +" +
            "+ + + + + + + M + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board =
            Helper.ToBoard(view, rules3);

         var nodes =
            board.Where(node => node.State.ToUnit(board.Player) == Unit.AllyZombie);

         foreach (var node in nodes)
            Assert.IsTrue(Heuristic.HasRoot(board, node), node.ToString());
      }

      [Test]
      public void Test_Has_Root_When_All_Nodes_Are_Rooted_And_They_Are_Closed()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + M" +
            "+ + + M M M M M M +" +
            "+ + + M + + + + + M" +
            "+ + + M + + M M + M" +
            "+ + + M M M + + + M" +
            "+ + + M M M + + + M" +
            "+ + + M M M M M M M" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var board =
            Helper.ToBoard(view, rules3);

         var nodes =
            board.Where(node => node.State.ToUnit(board.Player) == Unit.AllyZombie);

         foreach (var node in nodes)
            Assert.IsTrue(Heuristic.HasRoot(board, node), node.ToString());
      }


      [Test]
      public void Test_Has_Root_When_All_Nodes_Are_Not_Rooted()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + M + + + + M" +
            "+ + M M + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e M + + + + + + + M";

         var board =
            Helper.ToBoard(view, rules3);

         var nodes =
            board.Where(node => node.State.ToUnit(board.Player) == Unit.AllyZombie);

         foreach (var node in nodes)
            Assert.IsFalse(Heuristic.HasRoot(board, node), node.ToString());
      }

      [Test]
      public void Test_Get_Possible_Moves_When_Board_Is_In_Initial_State()
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
            "+ + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_Board_Is_Not_In_Initial_State_With_Unrooted_Nodes()
      {
         var view =
            "M + + + + M + + + m" +
            "+ + + + + M + + + +" +
            "+ + + M + M + + + +" +
            "+ + + + + M + + + +" +
            "+ + + + + M M M M M" +
            "+ M + + M + + M + +" +
            "+ + + + + + + + + +" +
            "+ + + M + + + + + +" +
            "+ + + + + + M + + +" +
            "e M + + + + + + + M";

         var etalonView =
            "M + + + + N m m m m" +
            "+ + + + + N m m m m" +
            "+ + + M + N m m m m" +
            "+ + + + + N m m m m" +
            "+ + + + + N N N N N" +
            "+ M + + N + + N + +" +
            "+ + + + + + + + + +" +
            "+ + + M + + + + + +" +
            "+ + + + + + M + + +" +
            "e M + + + + + + + M";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_Board_Is_Not_In_Initial_State_With_Unrooted_Node()
      {
         var view =
            "M + + + + + + + + m" +
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
            "M + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + m m m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_There_Board_Is_Not_In_Initial_State()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + m +" +
            "+ + + + + + + + m +" +
            "+ + + + + m m m + +" +
            "+ + + + + m + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_There_Board_Is_Not_In_Initial_State2()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + m + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m m" +
            "+ + m m m m m m m +" +
            "+ + m m m m m m m +" +
            "+ + m m m m m m m +" +
            "+ + m m m m m m m +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }


      [Test]
      public void Test_Get_Possible_Moves_When_There_Board_Is_Not_In_Initial_State3()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + + + m +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }


      [Test]
      public void Test_Get_Possible_Moves_When_Player1_Nodes_Are_Nearby_Player2_Nodes()
      {
         var view =
            "+ + + + + + + + + m" +
            "+ + + + + + e + e +" +
            "+ + + + + + + e e +" +
            "+ + + + + + e + + +" +
            "+ + + + + e + + + +" +
            "+ + + + e + + + + +" +
            "+ + + e + + + + + +" +
            "+ + e + + + + + + +" +
            "+ e + + + + + + + +" +
            "e + + + + + + + + +";

         var etalonView =
            "+ + + + + + m m m m" +
            "+ + + + + + N m N m" +
            "+ + + + + + m N N m" +
            "+ + + + + + N m m m" +
            "+ + + + + e + + + +" +
            "+ + + + e + + + + +" +
            "+ + + e + + + + + +" +
            "+ + e + + + + + + +" +
            "+ e + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_Board_Has_Explicitly_Rooted_Strong_Node()
      {
         var view =
            "+ + + + + + + + M m" +
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
            "+ + + + + m m m N m" +
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + m m m m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_Get_Possible_Moves_When_Board_Has_Implicitly_Rooted_Strong_Node()
      {
         var view =
            "+ + + + + + + M M m" +
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
            "+ + + + m m m N N m" +
            "+ + + + m m m m m m" +
            "+ + + + m m m m m m" +
            "+ + + + m m m m m m" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "+ + + + + + + + + +" +
            "e + + + + + + + + +";

         Assert.IsTrue(AreEqualAfterGettingPossibleMoves3(etalonView, view));
      }

      [Test]
      public void Test_That_Get_Possible_Moves_Returns_Disperse_Positions()
      {
         var view =
               "+ + + + + + + + + m" +
               "+ + + + + + + + m +" +
               "+ + + + + + + m + +" +
               "+ + + + + + m + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "e + + + + + + + + +";

         var etalonView =
               "+ + + + + + + + m m" +
               "+ + + + + + + + m +" +
               "+ + + + + + m m + +" +
               "+ + + + + + m + + +" +
               "+ + + + + m + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);
         var etalonBoard = Helper.ToBoard(etalonView, rules3);

         Assert.IsTrue(
            Heuristic
               .GetPossibleMoves(board)
               .Any(move => move.IsEqual(etalonBoard)));
      }

      [Test]
      public void Test_That_Get_Possible_Moves_Returns_Disperse_Positions2()
      {
         var view =
               "+ + + + + + + m m m" +
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
               "+ + + + + + m m m m" +
               "+ + + + + + + + + m" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules2);
         var etalonBoard = Helper.ToBoard(etalonView, rules2);

         Assert.IsTrue(
            Heuristic
               .GetPossibleMoves(board)
               .Any(move => move.IsEqual(etalonBoard)));
      }

      [Test]
      public void Test_That_Get_Possible_Moves_Returns_Distinct_Positions()
      {
         var view =
               "+ + + + + + + + + m" +
               "+ + + + + + + + m +" +
               "+ + + + + + + m + +" +
               "+ + + + + + m + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "+ + + + + + + + + +" +
               "e + + + + + + + + +";

         var board = Helper.ToBoard(view, rules3);

         Assert.AreEqual(
            Heuristic
               .GetPossibleMoves(board)
               .Distinct(new BoardEqualityComparer())
               .Count(),
            Heuristic
               .GetPossibleMoves(board)
               .Count()
            );
      }

      [Test]
      public void Test_That_Get_Possible_Moves_Returns_Distinct_Positions2()
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

         var board = Helper.ToBoard(view, rules2);

         Assert.AreEqual(
            Heuristic
               .GetPossibleMoves(board)
               .Distinct(new BoardEqualityComparer())
               .Count(),
            Heuristic
               .GetPossibleMoves(board)
               .Count());
      }

      [Test]
      //[Ignore]
      public void Test_Get_Possible_Moves_Perfomance()
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

         TrackGetPossibleMovesTime3(view, time => Trace.WriteLine("Position 1: " + time));

         view =
         "+ + + + + + + + + m" +
         "+ + + + + + + + m +" +
         "+ + + + + + + m + +" +
         "+ + + + + + m + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "+ + + + + + + + + +" +
         "e + + + + + + + + +";

         TrackGetPossibleMovesTime3(view, time => Trace.WriteLine("Position 2: " + time));

         view =
         "+ + e + + + m + + m" +
         "+ + e + + + m + m +" +
         "+ + e + + + m m + +" +
         "+ + e + + + m + + +" +
         "+ + e N N N m + + +" +
         "+ + e + + + m + + +" +
         "+ + e + + + m + + +" +
         "+ + e F F F m + + +" +
         "+ e e + + + m + + +" +
         "e e e + + + m + + +";

         TrackGetPossibleMovesTime3(view, time => Trace.WriteLine("Position 3: " + time));

         view =
         "m + + + + + + + + m" +
         "+ m + + + + + + m +" +
         "+ + m + m m + m + +" +
         "+ + + m + + m + + +" +
         "+ + m + + + + m + +" +
         "+ + m + + + + m + +" +
         "+ + + m + + m + + +" +
         "+ + m + m m + m + +" +
         "+ m + + + + + + m +" +
         "e + + + + + + + + m";

         TrackGetPossibleMovesTime3(view, time => Trace.WriteLine("Position 4: " + time));

      }

      private void TrackGetPossibleMovesTime3(string view, Action<TimeSpan> track)
      {
         var board = Helper.ToBoard(view, rules3);
         int range = 4;
         using (Helper.TrackTime(time => track(new TimeSpan(time.Ticks / range))))
         {
            for (int i = 0; i < range; i++)
               Heuristic.GetPossibleMoves(board).ToArray();
         }
      }

      private bool AreEqualAfterGettingPossibleMoves3(string etalonView, string view)
      {
         var intersection =
            Heuristic
               .GetPossibleMoves(Helper.ToBoard(view, rules3))
               .Intersect();

         return intersection.IsEqual(Helper.ToBoard(etalonView, rules3));
      }
   }
}