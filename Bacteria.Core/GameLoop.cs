using System;
using System.Collections;
using System.Collections.Generic;

namespace Bacteria.Core
{
   internal sealed class GameLoop : IEnumerable<Player>
   {
      private readonly IEnumerable<Player> players;
      private readonly Predicate<Player> until;

      public GameLoop(IEnumerable<Player> players, Predicate<Player> until)
      {
         this.players = players;
         this.until = until;
      }

      public int Round { get; private set; }

      public IEnumerator<Player> GetEnumerator()
      {
         return new PlayersHoopEnumerator(this);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      private sealed class PlayersHoopEnumerator : IEnumerator<Player>
      {
         private readonly IEnumerator<Player> enumerator;
         private readonly GameLoop loop;

         public PlayersHoopEnumerator(GameLoop loop)
         {
            this.loop = loop;
            enumerator = loop.players.GetEnumerator();
         }

         public Player Current
         {
            get
            {
               return enumerator.Current;
            }
         }

         object IEnumerator.Current
         {
            get { return Current; }
         }

         public bool MoveNext()
         {
            if (!enumerator.MoveNext())
            {
               loop.Round++;
               enumerator.Reset();
               return enumerator.MoveNext() && loop.until(Current);
            }
            return loop.until(Current);
         }

         public void Reset()
         {
            enumerator.Reset();
         }

         public void Dispose()
         {
            enumerator.Dispose();
         }
      }
   }
}
