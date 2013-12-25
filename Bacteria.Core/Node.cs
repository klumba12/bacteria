using System.Collections.Generic;

namespace Bacteria.Core
{
   public struct Node : IEqualityComparer<Node>
   {
      private readonly int x;
      private readonly int y;
      private readonly State state;

      public Node(int x, int y, State state)
      {
         this.x = x;
         this.y = y;
         this.state = state;
      }

      public int X { get { return x; } }
      public int Y { get { return y; } }
      public State State { get { return state ?? State.Void(); } }

      public static bool operator ==(Node node1, Node node2)
      {
         if (object.ReferenceEquals(null, node1))
            return object.ReferenceEquals(null, node2);

         return node1.Equals(node2);
      }

      public static bool operator !=(Node node1, Node node2)
      {
         return !(node1 == node2);
      }

      public override string ToString()
      {
         return string.Format("({0},{1}), {2}", X, Y, State);
      }

      public override int GetHashCode()
      {
         return (x << 32 + x) ^ y;
      }

      public override bool Equals(object obj)
      {
         if (!(obj is Node)) return false;

         return Equals((Node)obj);
      }

      public bool Equals(Node node)
      {
         return state == node.state && x == node.x && y == node.y;
      }

      public bool Equals(Node node1, Node node2)
      {
         return node1 == node2;
      }

      public int GetHashCode(Node obj)
      {
         return obj.GetHashCode();
      }
   }
}