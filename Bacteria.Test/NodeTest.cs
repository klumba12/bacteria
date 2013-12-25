using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core;
using NUnit.Framework;
using Bacteria.Sandbox;

namespace Bacteria.Test
{
   [TestFixture]
   public class NodeTest
   {
      [Test]
      [Ignore]
      public void Test_Dispersion_Of_Hash_Function()
      {
         Assert.AreEqual(
            GenerateNodes()
               .Count(),
            GenerateNodes()
               .Select(node => node.GetHashCode())
               .Distinct()
               .Count());
      }

      private static IEnumerable<Node> GenerateNodes()
      {
         var player = new AIPlayerD();
         var rules = new GameRules();
         for (int i = 0; i < rules.BoardLength; i++)
            for (int j = 0; j < rules.BoardLength; j++)
            {
               Trace.WriteLine(string.Format("({0}, {1}))={2}", i, j, new Node(i, j, State.Void()).GetHashCode()));
               yield return new Node(i, j, State.Void());
            }
      }
   }
}
