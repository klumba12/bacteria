namespace Bacteria.Interactive
{
   partial class BoardView
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.progressBar = new System.Windows.Forms.ProgressBar();
         this.panel = new System.Windows.Forms.Panel();
         this.menuStrip = new System.Windows.Forms.MenuStrip();
         this.menuItemStart = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemOptionsHH = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemOptionsAH = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemOptionsHA = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemLevel = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemLevelRookie = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemLevelintermediate = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemLevelProfessional = new System.Windows.Forms.ToolStripMenuItem();
         this.menuItemLevelWorldClass = new System.Windows.Forms.ToolStripMenuItem();
         this.labelWho = new System.Windows.Forms.Label();
         this.menuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // progressBar
         // 
         this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
         this.progressBar.Location = new System.Drawing.Point(0, 24);
         this.progressBar.Name = "progressBar";
         this.progressBar.Size = new System.Drawing.Size(451, 14);
         this.progressBar.TabIndex = 0;
         // 
         // panel
         // 
         this.panel.BackColor = System.Drawing.SystemColors.Control;
         this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel.Enabled = false;
         this.panel.Location = new System.Drawing.Point(0, 38);
         this.panel.Margin = new System.Windows.Forms.Padding(0);
         this.panel.Name = "panel";
         this.panel.Size = new System.Drawing.Size(451, 412);
         this.panel.TabIndex = 1;
         // 
         // menuStrip
         // 
         this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemStart,
            this.menuItemOptions,
            this.menuItemLevel});
         this.menuStrip.Location = new System.Drawing.Point(0, 0);
         this.menuStrip.Name = "menuStrip";
         this.menuStrip.Size = new System.Drawing.Size(451, 24);
         this.menuStrip.TabIndex = 2;
         // 
         // menuItemStart
         // 
         this.menuItemStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemStart.Name = "menuItemStart";
         this.menuItemStart.ShortcutKeyDisplayString = "";
         this.menuItemStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
         this.menuItemStart.ShowShortcutKeys = false;
         this.menuItemStart.Size = new System.Drawing.Size(43, 20);
         this.menuItemStart.Text = "Start";
         this.menuItemStart.ToolTipText = "F5";
         this.menuItemStart.Click += new System.EventHandler(this.menuItemStart_Click);
         // 
         // menuItemOptions
         // 
         this.menuItemOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOptionsHH,
            this.menuItemOptionsAH,
            this.menuItemOptionsHA});
         this.menuItemOptions.Name = "menuItemOptions";
         this.menuItemOptions.Size = new System.Drawing.Size(61, 20);
         this.menuItemOptions.Text = "Options";
         // 
         // menuItemOptionsHH
         // 
         this.menuItemOptionsHH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemOptionsHH.Name = "menuItemOptionsHH";
         this.menuItemOptionsHH.Size = new System.Drawing.Size(173, 22);
         this.menuItemOptionsHH.Text = "Human-Human";
         this.menuItemOptionsHH.Click += new System.EventHandler(this.menuItemOptions_Click);
         // 
         // menuItemOptionsAH
         // 
         this.menuItemOptionsAH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemOptionsAH.Name = "menuItemOptionsAH";
         this.menuItemOptionsAH.Size = new System.Drawing.Size(173, 22);
         this.menuItemOptionsAH.Text = "Computer-Human";
         this.menuItemOptionsAH.Click += new System.EventHandler(this.menuItemOptions_Click);
         // 
         // menuItemOptionsHA
         // 
         this.menuItemOptionsHA.Checked = true;
         this.menuItemOptionsHA.CheckState = System.Windows.Forms.CheckState.Checked;
         this.menuItemOptionsHA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemOptionsHA.Name = "menuItemOptionsHA";
         this.menuItemOptionsHA.Size = new System.Drawing.Size(173, 22);
         this.menuItemOptionsHA.Text = "Human-Computer";
         this.menuItemOptionsHA.Click += new System.EventHandler(this.menuItemOptions_Click);
         // 
         // menuItemLevel
         // 
         this.menuItemLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLevelRookie,
            this.menuItemLevelintermediate,
            this.menuItemLevelProfessional,
            this.menuItemLevelWorldClass});
         this.menuItemLevel.Name = "menuItemLevel";
         this.menuItemLevel.Size = new System.Drawing.Size(46, 20);
         this.menuItemLevel.Text = "Level";
         // 
         // menuItemLevelRookie
         // 
         this.menuItemLevelRookie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemLevelRookie.Name = "menuItemLevelRookie";
         this.menuItemLevelRookie.Size = new System.Drawing.Size(152, 22);
         this.menuItemLevelRookie.Text = "Rookie";
         this.menuItemLevelRookie.Click += new System.EventHandler(this.menuItemLevel_Click);
         // 
         // menuItemLevelintermediate
         // 
         this.menuItemLevelintermediate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemLevelintermediate.Name = "menuItemLevelintermediate";
         this.menuItemLevelintermediate.Size = new System.Drawing.Size(152, 22);
         this.menuItemLevelintermediate.Text = "Intermediate";
         this.menuItemLevelintermediate.Click += new System.EventHandler(this.menuItemLevel_Click);
         // 
         // menuItemLevelProfessional
         // 
         this.menuItemLevelProfessional.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.menuItemLevelProfessional.Name = "menuItemLevelProfessional";
         this.menuItemLevelProfessional.Size = new System.Drawing.Size(152, 22);
         this.menuItemLevelProfessional.Text = "Professional";
         this.menuItemLevelProfessional.Click += new System.EventHandler(this.menuItemLevel_Click);
         // 
         // menuItemLevelWorldClass
         // 
         this.menuItemLevelWorldClass.Checked = true;
         this.menuItemLevelWorldClass.CheckState = System.Windows.Forms.CheckState.Checked;
         this.menuItemLevelWorldClass.Name = "menuItemLevelWorldClass";
         this.menuItemLevelWorldClass.Size = new System.Drawing.Size(152, 22);
         this.menuItemLevelWorldClass.Text = "World Class";
         this.menuItemLevelWorldClass.Click += new System.EventHandler(this.menuItemLevel_Click);
         // 
         // labelWho
         // 
         this.labelWho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.labelWho.BackColor = System.Drawing.Color.Transparent;
         this.labelWho.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.labelWho.Location = new System.Drawing.Point(352, 3);
         this.labelWho.Name = "labelWho";
         this.labelWho.Size = new System.Drawing.Size(97, 19);
         this.labelWho.TabIndex = 3;
         this.labelWho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // BoardView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(451, 450);
         this.Controls.Add(this.labelWho);
         this.Controls.Add(this.panel);
         this.Controls.Add(this.progressBar);
         this.Controls.Add(this.menuStrip);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MainMenuStrip = this.menuStrip;
         this.MaximizeBox = false;
         this.Name = "BoardView";
         this.ShowIcon = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Bacteria";
         this.menuStrip.ResumeLayout(false);
         this.menuStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ProgressBar progressBar;
      private System.Windows.Forms.Panel panel;
      private System.Windows.Forms.MenuStrip menuStrip;
      private System.Windows.Forms.ToolStripMenuItem menuItemStart;
      private System.Windows.Forms.ToolStripMenuItem menuItemOptions;
      private System.Windows.Forms.ToolStripMenuItem menuItemOptionsHH;
      private System.Windows.Forms.ToolStripMenuItem menuItemOptionsAH;
      private System.Windows.Forms.ToolStripMenuItem menuItemLevel;
      private System.Windows.Forms.ToolStripMenuItem menuItemOptionsHA;
      private System.Windows.Forms.ToolStripMenuItem menuItemLevelRookie;
      private System.Windows.Forms.ToolStripMenuItem menuItemLevelintermediate;
      private System.Windows.Forms.ToolStripMenuItem menuItemLevelProfessional;
      private System.Windows.Forms.ToolStripMenuItem menuItemLevelWorldClass;
      private System.Windows.Forms.Label labelWho;
   }
}

