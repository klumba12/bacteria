using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bacteria.Core.Infrastructure;
using Bacteria.Core.Utility;

namespace Bacteria.Core
{
   [DebuggerDisplay("Player = {Player}, Length = {Length}")]
   public sealed class Board : IEnumerable<Node>, IHistoryBoard, IBoard
   {
      private readonly State[][] data;
      private readonly Player[] players;
      private Player player = Player.None;

      internal Board(GameRules rules, Player[] players)
      {
         Rules = rules;
         this.players = players;
         data = new State[Length][];
         for (int x = 0; x < Length; x++)
         {
            data[x] = new State[Length];
            for (int y = 0; y < Length; y++)
               data[x][y] = State.Void();
         }
      }

      public GameRules Rules { get; private set; }

      public Player Player
      {
         get
         {
            return player;
         }
         internal set
         {
            player = value ?? Player.None;
         }
      }

      public IEnumerable<Player> Players
      {
         get { return players; }
      }

      public int Length
      {
         get { return Rules.BoardLength; }
      }

      public State this[int x, int y]
      {
         get
         {
            return data[x][y];
         }
         set
         {
            data[x][y] = value;
         }
      }

      public int GetTag(Player player)
      {
         return Array.IndexOf(players, player);
      }

      public State Occupy(int x, int y)
      {
         Debug.Assert(data[x][y].ToUnit(Player) == Unit.None || data[x][y].ToUnit(Player) == Unit.AlienAlive);
         //Debug.Assert(Heuristic.HasRoot(this, new Node(x, y, State.Zombie(Player))));

         var state =
            data[x][y] =
               data[x][y].ToUnit(Player) == Unit.AlienAlive
                  ? Heuristic.HasRoot(this, new Node(x, y, State.Zombie(Player)))
                     ? State.AliveZombie(Player)
                     : State.Zombie(Player)
                  : State.Alive(Player);

         var node = new Node(x, y, state);
         if (state.Piece == Piece.Alive || state.Piece == Piece.AliveZombie)
            CoerceAllyZombies(node);

         if (state.Piece != Piece.Void)
            CoerceAlienZombies(node);

         return state;
      }

      public IDisposable Flip(Player player)
      {
         var temp = Player;
         Player = player;
         return new DisposableMethodHandler(() => Player = temp);
      }

      public IBoard Clone()
      {
         var clone =
            new Board(Rules, players)
            {
               Player = Player,
            };

         for (int x = 0; x < Length; x++)
            for (int y = 0; y < Length; y++)
               clone.data[x][y] = data[x][y];

         return clone;
      }

      public override string ToString()
      {
         int ident =
            (players.Length * 3 + 2)
               .ToString().Length + 1;

         Func<int, string> writeTag =
            tag => tag.ToString().PadRight(ident);

         return
            string.Join(Environment.NewLine,
               Enumerable.Range(0, Length)
                  .Select(x =>
                     string.Concat(
                        Enumerable.Range(0, Length)
                           .Select(y =>
                           {
                              var state = data[x][y];
                              switch (state.Piece)
                              {
                                 case Piece.Alive:
                                    return writeTag(GetTag(state.Player) * 3);
                                 case Piece.Zombie:
                                    return writeTag(GetTag(state.Player) * 3 + 1);
                                 case Piece.AliveZombie:
                                    return writeTag(GetTag(state.Player) * 3 + 2);
                                 default:
                                    return new string(' ', ident);
                              }
                           }))));
      }

      public IEnumerator<Node> GetEnumerator()
      {
         return
            data
               .SelectMany(
                  (ys, x) =>
                     ys.Select(
                        (_, y) =>
                           new Node(x, y, data[x][y])))
               .GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      internal void Commit(IBoard board)
      {
         for (var x = 0; x < Length; x++)
            for (var y = 0; y < Length; y++)
               data[x][y] = board[x, y];
      }

      private void CoerceAllyZombies(Node hitNode)
      {
         var nodes =
            Heuristic
               .GetAdjoinedNodes(this, hitNode)
               .Where(node => node.State == State.Zombie(Player));

         foreach (var node in nodes)
         {
            data[node.X][node.Y] = State.AliveZombie(Player);
            CoerceAllyZombies(node);
         }
      }

      private void CoerceAlienZombies(Node hitNode)
      {
         var nodes =
             Heuristic
                .GetAdjoinedNodes(this, hitNode)
                .Where(node =>
                   node.State.ToUnit(Player) == Unit.AlienAliveZombie &&
                   !Heuristic.HasRoot(this, node));

         foreach (var node in nodes)
         {
            data[node.X][node.Y] = State.Zombie(node.State.Player);
            CoerceAlienZombies(node);
         }
      }
   }
}