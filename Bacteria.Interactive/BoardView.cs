using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bacteria.Core;
using Bacteria.Core.Utility;

namespace Bacteria.Interactive
{
   internal partial class BoardView : Form
   {
      private Control[][] cells;
      private int moveCount;
      private Action ready;
      private bool canMove = true;

      public BoardView()
      {
         InitializeComponent();
      }

      public IBoard Board { get; set; }
      public event EventHandler Start;

      public GameMode GameMode
      {
         get
         {
            if (menuItemOptionsHH.Checked) return GameMode.HumanVsHuman;
            if (menuItemOptionsHA.Checked) return GameMode.HumanVsComputer;
            if (menuItemOptionsAH.Checked) return GameMode.ComputerVsHuman;

            throw new InvalidOperationException();
         }
      }

      public GameLevel GameLevel
      {
         get
         {
            if (menuItemLevelRookie.Checked) return GameLevel.Rookie;
            if (menuItemLevelintermediate.Checked) return GameLevel.Intermediate;
            if (menuItemLevelProfessional.Checked) return GameLevel.Professional;
            if (menuItemLevelWorldClass.Checked) return GameLevel.WorldClass;

            throw new InvalidOperationException();
         }
      }

      public void WaitForMove(Action ready)
      {
         Replicate();
         if (!Heuristic.GetPossibleMoves(Board).Any())
         {
            canMove = false;
            ready();
            return;
         }

         moveCount = 0;

         progressBar.Style = ProgressBarStyle.Blocks;
         progressBar.Value = 0;
         panel.Enabled = true;

         this.ready =
            () =>
            {
               panel.Enabled = false;
               Application.DoEvents();

               progressBar.Style = ProgressBarStyle.Marquee;
               ready();
            };
      }

      public void UpdateUnitStatus(Player player)
      {
         labelWho.Text = player.Name;
         labelWho.ForeColor =
            player == Player1
               ? Color.Blue : Color.Red;
      }

      public bool MakeMove(IBoard board)
      {
         return canMove;
      }

      protected virtual void OnStart()
      {
         Start.Raise(this);

         if (null == Board)
            throw new InvalidOperationException("Board is not set on start");

         int cellWidth = panel.Width / Board.Length;
         int cellHeight = panel.Height / Board.Length;

         SuspendLayout();
         panel.Controls.Clear();

         cells = new Control[Board.Length][];
         for (int i = 0; i < Board.Length; i++)
         {
            cells[i] = new Control[Board.Length];
            for (int j = 0; j < Board.Length; j++)
            {
               var x = i;
               var y = j;

               var cell =
                  new Button()
                  {
                     Size = new Size(cellWidth, cellHeight),
                     Location = new Point(x * cellWidth, y * cellHeight),
                     FlatStyle = FlatStyle.Flat,
                  };

               cell.Click +=
                  (o, args) =>
                  {
                     var unit = Board[x, y].ToUnit(Board.Player);
                     if ((unit == Unit.None || unit == Unit.AlienAlive) &&
                        Heuristic.HasRoot(Board, new Node(x, y, State.Zombie(Board.Player))))
                     {
                        Board.Occupy(x, y);
                        Replicate();

                        progressBar.Value = 100 * (moveCount + 1) / Board.Rules.MoveLimit;

                        if (++moveCount == Board.Rules.MoveLimit) ready();
                     }
                  };

               panel.Controls.Add(cell);
               cells[i][j] = cell;
            }
         }
         ResumeLayout();
      }

      //
      // Assumes that we have only 2 players
      //

      private Player Player1
      {
         get { return Board.Players.ToArray()[0]; }
      }

      private Player Player2
      {
         get { return Board.Players.ToArray()[1]; }
      }

      private void Replicate()
      {
         for (int i = 0; i < Board.Length; i++)
            for (int j = 0; j < Board.Length; j++)
            {
               Color color = Color.White;
               switch (Board[i, j].ToUnit(Player1))
               {
                  case Unit.None:
                     color = Color.White;
                     break;
                  case Unit.AlienAlive:
                     color = Color.FromArgb(0xFF, 0xB6, 0xC1);
                     break;
                  case Unit.AlienZombie:
                     color = Color.FromArgb(0x8B, 0x00, 0x00);
                     break;
                  case Unit.AlienAliveZombie:
                     color = Color.FromArgb(0xFF, 0x00, 0x00);
                     break;
                  case Unit.AllyAlive:
                     color = Color.FromArgb(0xAD, 0xD8, 0xE6);
                     break;
                  case Unit.AllyZombie:
                     color = Color.FromArgb(0x4B, 0x00, 0x82);
                     break;
                  case Unit.AllyAliveZombie:
                     color = Color.FromArgb(0x00, 0x00, 0xFF);
                     break;
               }

               cells[i][j].BackColor = color;
            }
      }

      private void menuItemOptions_Click(object sender, EventArgs e)
      {
         menuItemOptionsAH.Checked =
            menuItemOptionsHA.Checked =
               menuItemOptionsHH.Checked = false;

         ((ToolStripMenuItem)sender).Checked = true;
      }

      private void menuItemLevel_Click(object sender, EventArgs e)
      {
         menuItemLevelRookie.Checked =
            menuItemLevelintermediate.Checked =
               menuItemLevelProfessional.Checked =
                  menuItemLevelWorldClass.Checked = false;

         ((ToolStripMenuItem)sender).Checked = true;
      }

      private void menuItemStart_Click(object sender, EventArgs e)
      {
         OnStart();
      }
   }
}