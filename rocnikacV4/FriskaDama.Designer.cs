namespace rocnikacV4
{
  partial class Friska_dama
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Friska_dama));
        this.bUndo = new System.Windows.Forms.Button();
        this.bRedo = new System.Windows.Forms.Button();
        this.gbHistory = new System.Windows.Forms.GroupBox();
        this.lbHistory = new System.Windows.Forms.ListBox();
        this.menu = new System.Windows.Forms.MenuStrip();
        this.tsGame = new System.Windows.Forms.ToolStripMenuItem();
        this.newGame = new System.Windows.Forms.ToolStripMenuItem();
        this.saveGame = new System.Windows.Forms.ToolStripMenuItem();
        this.loadGame = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.settings = new System.Windows.Forms.ToolStripMenuItem();
        this.exitGame = new System.Windows.Forms.ToolStripMenuItem();
        this.tsHelp = new System.Windows.Forms.ToolStripMenuItem();
        this.pravidlaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.bestMove = new System.Windows.Forms.ToolStripMenuItem();
        this.statusBar = new System.Windows.Forms.StatusStrip();
        this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
        this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
        this.pauseButton = new System.Windows.Forms.Button();
        this.gbHistory.SuspendLayout();
        this.menu.SuspendLayout();
        this.statusBar.SuspendLayout();
        this.SuspendLayout();
        // 
        // bUndo
        // 
        resources.ApplyResources(this.bUndo, "bUndo");
        this.bUndo.Name = "bUndo";
        this.bUndo.UseVisualStyleBackColor = true;
        this.bUndo.Click += new System.EventHandler(this.bUndo_Click);
        // 
        // bRedo
        // 
        resources.ApplyResources(this.bRedo, "bRedo");
        this.bRedo.Name = "bRedo";
        this.bRedo.UseVisualStyleBackColor = true;
        this.bRedo.Click += new System.EventHandler(this.bRedo_Click);
        // 
        // gbHistory
        // 
        this.gbHistory.Controls.Add(this.lbHistory);
        this.gbHistory.Controls.Add(this.bUndo);
        this.gbHistory.Controls.Add(this.bRedo);
        resources.ApplyResources(this.gbHistory, "gbHistory");
        this.gbHistory.Name = "gbHistory";
        this.gbHistory.TabStop = false;
        // 
        // lbHistory
        // 
        this.lbHistory.FormattingEnabled = true;
        resources.ApplyResources(this.lbHistory, "lbHistory");
        this.lbHistory.Name = "lbHistory";
        this.lbHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbHistory_SelectedIndexChanged);
        // 
        // menu
        // 
        this.menu.BackColor = System.Drawing.SystemColors.Menu;
        this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsGame,
            this.tsHelp});
        resources.ApplyResources(this.menu, "menu");
        this.menu.Name = "menu";
        this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
        // 
        // tsGame
        // 
        this.tsGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGame,
            this.saveGame,
            this.loadGame,
            this.toolStripSeparator1,
            this.settings,
            this.exitGame});
        this.tsGame.Name = "tsGame";
        resources.ApplyResources(this.tsGame, "tsGame");
        // 
        // newGame
        // 
        this.newGame.Name = "newGame";
        resources.ApplyResources(this.newGame, "newGame");
        this.newGame.Click += new System.EventHandler(this.newGame_Click);
        // 
        // saveGame
        // 
        this.saveGame.Image = global::rocnikacV4.Properties.Resources.ico7;
        this.saveGame.Name = "saveGame";
        resources.ApplyResources(this.saveGame, "saveGame");
        this.saveGame.Click += new System.EventHandler(this.saveGame_Click);
        // 
        // loadGame
        // 
        this.loadGame.Image = global::rocnikacV4.Properties.Resources.ico46;
        this.loadGame.Name = "loadGame";
        resources.ApplyResources(this.loadGame, "loadGame");
        this.loadGame.Click += new System.EventHandler(this.loadGame_Click);
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
        // 
        // settings
        // 
        resources.ApplyResources(this.settings, "settings");
        this.settings.Image = global::rocnikacV4.Properties.Resources.ico114;
        this.settings.Name = "settings";
        this.settings.Click += new System.EventHandler(this.settings_Click);
        // 
        // exitGame
        // 
        this.exitGame.Image = global::rocnikacV4.Properties.Resources.ico240;
        this.exitGame.Name = "exitGame";
        resources.ApplyResources(this.exitGame, "exitGame");
        this.exitGame.Click += new System.EventHandler(this.exitGame_Click);
        // 
        // tsHelp
        // 
        this.tsHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pravidlaToolStripMenuItem,
            this.bestMove});
        this.tsHelp.Name = "tsHelp";
        resources.ApplyResources(this.tsHelp, "tsHelp");
        // 
        // pravidlaToolStripMenuItem
        // 
        this.pravidlaToolStripMenuItem.Image = global::rocnikacV4.Properties.Resources.ico263;
        this.pravidlaToolStripMenuItem.Name = "pravidlaToolStripMenuItem";
        resources.ApplyResources(this.pravidlaToolStripMenuItem, "pravidlaToolStripMenuItem");
        this.pravidlaToolStripMenuItem.Click += new System.EventHandler(this.pravidlaToolStripMenuItem_Click);
        // 
        // bestMove
        // 
        resources.ApplyResources(this.bestMove, "bestMove");
        this.bestMove.Name = "bestMove";
        this.bestMove.Click += new System.EventHandler(this.bestMove_Click);
        // 
        // statusBar
        // 
        this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
        resources.ApplyResources(this.statusBar, "statusBar");
        this.statusBar.Name = "statusBar";
        this.statusBar.SizingGrip = false;
        // 
        // statusBarLabel
        // 
        this.statusBarLabel.Name = "statusBarLabel";
        resources.ApplyResources(this.statusBarLabel, "statusBarLabel");
        // 
        // pauseButton
        // 
        resources.ApplyResources(this.pauseButton, "pauseButton");
        this.pauseButton.Name = "pauseButton";
        this.pauseButton.UseVisualStyleBackColor = true;
        this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
        // 
        // Friska_dama
        // 
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.pauseButton);
        this.Controls.Add(this.statusBar);
        this.Controls.Add(this.gbHistory);
        this.Controls.Add(this.menu);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MainMenuStrip = this.menu;
        this.MaximizeBox = false;
        this.Name = "Friska_dama";
        this.Load += new System.EventHandler(this.FriskaDama_Load);
        this.gbHistory.ResumeLayout(false);
        this.menu.ResumeLayout(false);
        this.menu.PerformLayout();
        this.statusBar.ResumeLayout(false);
        this.statusBar.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button bUndo;
    private System.Windows.Forms.Button bRedo;
    private System.Windows.Forms.GroupBox gbHistory;
    private System.Windows.Forms.MenuStrip menu;
    private System.Windows.Forms.ToolStripMenuItem tsGame;
    private System.Windows.Forms.ToolStripMenuItem newGame;
    private System.Windows.Forms.ToolStripMenuItem exitGame;
    private System.Windows.Forms.ToolStripMenuItem saveGame;
    private System.Windows.Forms.ToolStripMenuItem loadGame;
    private System.Windows.Forms.ToolStripMenuItem tsHelp;
    private System.Windows.Forms.ToolStripMenuItem pravidlaToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusBar;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    public System.Windows.Forms.ListBox lbHistory;
    public System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
    private System.Windows.Forms.ToolStripMenuItem settings;
    private System.Windows.Forms.ToolStripMenuItem bestMove;
    private System.ComponentModel.BackgroundWorker backgroundWorker;
    private System.Windows.Forms.Button pauseButton;
  }
}

