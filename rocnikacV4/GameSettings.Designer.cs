namespace rocnikacV4
{
  partial class GameSettings
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
        this.whitePlayer = new System.Windows.Forms.GroupBox();
        this.whitePlayerPc = new System.Windows.Forms.RadioButton();
        this.whitePlayerHuman = new System.Windows.Forms.RadioButton();
        this.blackPlayer = new System.Windows.Forms.GroupBox();
        this.blackPlayerPc = new System.Windows.Forms.RadioButton();
        this.blackPlayerHuman = new System.Windows.Forms.RadioButton();
        this.settingsStorno = new System.Windows.Forms.Button();
        this.settingsSave = new System.Windows.Forms.Button();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.gameSettingsInfo = new System.Windows.Forms.ToolStripStatusLabel();
        this.moveHelp = new System.Windows.Forms.GroupBox();
        this.moveHelpNo = new System.Windows.Forms.RadioButton();
        this.moveHelpYes = new System.Windows.Forms.RadioButton();
        this.startingPlayer = new System.Windows.Forms.GroupBox();
        this.startingPlayerBlack = new System.Windows.Forms.RadioButton();
        this.startingPlayerWhite = new System.Windows.Forms.RadioButton();
        this.whiteComputerDifficulty = new System.Windows.Forms.TrackBar();
        this.blackComputerDifficulty = new System.Windows.Forms.TrackBar();
        this.saveSettingsStartNewGame = new System.Windows.Forms.Button();
        this.lWhiteEasy = new System.Windows.Forms.Label();
        this.lWhiteNormal = new System.Windows.Forms.Label();
        this.lWhiteHard = new System.Windows.Forms.Label();
        this.lBlackEasy = new System.Windows.Forms.Label();
        this.lBlackNormal = new System.Windows.Forms.Label();
        this.lBlackHard = new System.Windows.Forms.Label();
        this.whitePlayer.SuspendLayout();
        this.blackPlayer.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.moveHelp.SuspendLayout();
        this.startingPlayer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.whiteComputerDifficulty)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.blackComputerDifficulty)).BeginInit();
        this.SuspendLayout();
        // 
        // whitePlayer
        // 
        this.whitePlayer.Controls.Add(this.whitePlayerPc);
        this.whitePlayer.Controls.Add(this.whitePlayerHuman);
        this.whitePlayer.Location = new System.Drawing.Point(12, 12);
        this.whitePlayer.Name = "whitePlayer";
        this.whitePlayer.Size = new System.Drawing.Size(204, 75);
        this.whitePlayer.TabIndex = 1;
        this.whitePlayer.TabStop = false;
        this.whitePlayer.Text = "Bílý hráč";
        // 
        // whitePlayerPc
        // 
        this.whitePlayerPc.AutoSize = true;
        this.whitePlayerPc.Location = new System.Drawing.Point(7, 44);
        this.whitePlayerPc.Name = "whitePlayerPc";
        this.whitePlayerPc.Size = new System.Drawing.Size(63, 17);
        this.whitePlayerPc.TabIndex = 1;
        this.whitePlayerPc.TabStop = true;
        this.whitePlayerPc.Text = "Počítač";
        this.whitePlayerPc.UseVisualStyleBackColor = true;
        this.whitePlayerPc.CheckedChanged += new System.EventHandler(this.whitePlayerPc_CheckedChanged);
        // 
        // whitePlayerHuman
        // 
        this.whitePlayerHuman.AutoSize = true;
        this.whitePlayerHuman.Location = new System.Drawing.Point(7, 20);
        this.whitePlayerHuman.Name = "whitePlayerHuman";
        this.whitePlayerHuman.Size = new System.Drawing.Size(57, 17);
        this.whitePlayerHuman.TabIndex = 0;
        this.whitePlayerHuman.TabStop = true;
        this.whitePlayerHuman.Text = "člověk";
        this.whitePlayerHuman.UseVisualStyleBackColor = true;
        this.whitePlayerHuman.CheckedChanged += new System.EventHandler(this.whitePlayerHuman_CheckedChanged);
        // 
        // blackPlayer
        // 
        this.blackPlayer.Controls.Add(this.blackPlayerPc);
        this.blackPlayer.Controls.Add(this.blackPlayerHuman);
        this.blackPlayer.Location = new System.Drawing.Point(222, 12);
        this.blackPlayer.Name = "blackPlayer";
        this.blackPlayer.Size = new System.Drawing.Size(204, 75);
        this.blackPlayer.TabIndex = 2;
        this.blackPlayer.TabStop = false;
        this.blackPlayer.Text = "Černý hráč";
        // 
        // blackPlayerPc
        // 
        this.blackPlayerPc.AutoSize = true;
        this.blackPlayerPc.Location = new System.Drawing.Point(7, 44);
        this.blackPlayerPc.Name = "blackPlayerPc";
        this.blackPlayerPc.Size = new System.Drawing.Size(63, 17);
        this.blackPlayerPc.TabIndex = 1;
        this.blackPlayerPc.TabStop = true;
        this.blackPlayerPc.Text = "Počítač";
        this.blackPlayerPc.UseVisualStyleBackColor = true;
        this.blackPlayerPc.CheckedChanged += new System.EventHandler(this.blackPlayerPc_CheckedChanged);
        // 
        // blackPlayerHuman
        // 
        this.blackPlayerHuman.AutoSize = true;
        this.blackPlayerHuman.Location = new System.Drawing.Point(7, 20);
        this.blackPlayerHuman.Name = "blackPlayerHuman";
        this.blackPlayerHuman.Size = new System.Drawing.Size(57, 17);
        this.blackPlayerHuman.TabIndex = 0;
        this.blackPlayerHuman.TabStop = true;
        this.blackPlayerHuman.Text = "člověk";
        this.blackPlayerHuman.UseVisualStyleBackColor = true;
        this.blackPlayerHuman.CheckedChanged += new System.EventHandler(this.blackPlayerHuman_CheckedChanged);
        // 
        // settingsStorno
        // 
        this.settingsStorno.Location = new System.Drawing.Point(292, 227);
        this.settingsStorno.Name = "settingsStorno";
        this.settingsStorno.Size = new System.Drawing.Size(134, 25);
        this.settingsStorno.TabIndex = 8;
        this.settingsStorno.Text = "Zrušit";
        this.settingsStorno.UseVisualStyleBackColor = true;
        this.settingsStorno.Click += new System.EventHandler(this.settingsStorno_Click);
        // 
        // settingsSave
        // 
        this.settingsSave.Location = new System.Drawing.Point(152, 227);
        this.settingsSave.Name = "settingsSave";
        this.settingsSave.Size = new System.Drawing.Size(134, 25);
        this.settingsSave.TabIndex = 7;
        this.settingsSave.Text = "Uložit změny";
        this.settingsSave.UseVisualStyleBackColor = true;
        this.settingsSave.Click += new System.EventHandler(this.settingsSave_Click);
        // 
        // statusStrip1
        // 
        this.statusStrip1.AllowMerge = false;
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameSettingsInfo});
        this.statusStrip1.Location = new System.Drawing.Point(0, 256);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(432, 22);
        this.statusStrip1.SizingGrip = false;
        this.statusStrip1.TabIndex = 4;
        this.statusStrip1.Text = "statusStrip1";
        // 
        // gameSettingsInfo
        // 
        this.gameSettingsInfo.Name = "gameSettingsInfo";
        this.gameSettingsInfo.Size = new System.Drawing.Size(0, 17);
        // 
        // moveHelp
        // 
        this.moveHelp.Controls.Add(this.moveHelpNo);
        this.moveHelp.Controls.Add(this.moveHelpYes);
        this.moveHelp.Location = new System.Drawing.Point(222, 144);
        this.moveHelp.Name = "moveHelp";
        this.moveHelp.Size = new System.Drawing.Size(204, 75);
        this.moveHelp.TabIndex = 6;
        this.moveHelp.TabStop = false;
        this.moveHelp.Text = "Zobrazovat nápovědu tahu";
        // 
        // moveHelpNo
        // 
        this.moveHelpNo.AutoSize = true;
        this.moveHelpNo.Location = new System.Drawing.Point(7, 44);
        this.moveHelpNo.Name = "moveHelpNo";
        this.moveHelpNo.Size = new System.Drawing.Size(39, 17);
        this.moveHelpNo.TabIndex = 1;
        this.moveHelpNo.TabStop = true;
        this.moveHelpNo.Text = "Ne";
        this.moveHelpNo.UseVisualStyleBackColor = true;
        this.moveHelpNo.CheckedChanged += new System.EventHandler(this.moveHelpNo_CheckedChanged);
        // 
        // moveHelpYes
        // 
        this.moveHelpYes.AutoSize = true;
        this.moveHelpYes.Location = new System.Drawing.Point(7, 20);
        this.moveHelpYes.Name = "moveHelpYes";
        this.moveHelpYes.Size = new System.Drawing.Size(44, 17);
        this.moveHelpYes.TabIndex = 0;
        this.moveHelpYes.TabStop = true;
        this.moveHelpYes.Text = "Ano";
        this.moveHelpYes.UseVisualStyleBackColor = true;
        this.moveHelpYes.CheckedChanged += new System.EventHandler(this.moveHelpYes_CheckedChanged);
        // 
        // startingPlayer
        // 
        this.startingPlayer.Controls.Add(this.startingPlayerBlack);
        this.startingPlayer.Controls.Add(this.startingPlayerWhite);
        this.startingPlayer.Location = new System.Drawing.Point(12, 144);
        this.startingPlayer.Name = "startingPlayer";
        this.startingPlayer.Size = new System.Drawing.Size(204, 75);
        this.startingPlayer.TabIndex = 5;
        this.startingPlayer.TabStop = false;
        this.startingPlayer.Text = "Začínající hráč";
        // 
        // startingPlayerBlack
        // 
        this.startingPlayerBlack.AutoSize = true;
        this.startingPlayerBlack.Location = new System.Drawing.Point(7, 44);
        this.startingPlayerBlack.Name = "startingPlayerBlack";
        this.startingPlayerBlack.Size = new System.Drawing.Size(76, 17);
        this.startingPlayerBlack.TabIndex = 1;
        this.startingPlayerBlack.TabStop = true;
        this.startingPlayerBlack.Text = "Černý hráč";
        this.startingPlayerBlack.UseVisualStyleBackColor = true;
        this.startingPlayerBlack.CheckedChanged += new System.EventHandler(this.startingPlayerBlack_CheckedChanged);
        // 
        // startingPlayerWhite
        // 
        this.startingPlayerWhite.AutoSize = true;
        this.startingPlayerWhite.Location = new System.Drawing.Point(7, 20);
        this.startingPlayerWhite.Name = "startingPlayerWhite";
        this.startingPlayerWhite.Size = new System.Drawing.Size(67, 17);
        this.startingPlayerWhite.TabIndex = 0;
        this.startingPlayerWhite.TabStop = true;
        this.startingPlayerWhite.Text = "Bílý hráč";
        this.startingPlayerWhite.UseVisualStyleBackColor = true;
        this.startingPlayerWhite.CheckedChanged += new System.EventHandler(this.startingPlayerWhite_CheckedChanged);
        // 
        // whiteComputerDifficulty
        // 
        this.whiteComputerDifficulty.Enabled = false;
        this.whiteComputerDifficulty.Location = new System.Drawing.Point(12, 93);
        this.whiteComputerDifficulty.Maximum = 3;
        this.whiteComputerDifficulty.Minimum = 1;
        this.whiteComputerDifficulty.Name = "whiteComputerDifficulty";
        this.whiteComputerDifficulty.Size = new System.Drawing.Size(204, 45);
        this.whiteComputerDifficulty.TabIndex = 3;
        this.whiteComputerDifficulty.Value = 1;
        // 
        // blackComputerDifficulty
        // 
        this.blackComputerDifficulty.Enabled = false;
        this.blackComputerDifficulty.Location = new System.Drawing.Point(222, 93);
        this.blackComputerDifficulty.Maximum = 3;
        this.blackComputerDifficulty.Minimum = 1;
        this.blackComputerDifficulty.Name = "blackComputerDifficulty";
        this.blackComputerDifficulty.Size = new System.Drawing.Size(204, 45);
        this.blackComputerDifficulty.TabIndex = 4;
        this.blackComputerDifficulty.Value = 1;
        // 
        // saveSettingsStartNewGame
        // 
        this.saveSettingsStartNewGame.Location = new System.Drawing.Point(12, 227);
        this.saveSettingsStartNewGame.Name = "saveSettingsStartNewGame";
        this.saveSettingsStartNewGame.Size = new System.Drawing.Size(134, 25);
        this.saveSettingsStartNewGame.TabIndex = 0;
        this.saveSettingsStartNewGame.Text = "Uložit a začít novou hru";
        this.saveSettingsStartNewGame.UseVisualStyleBackColor = true;
        this.saveSettingsStartNewGame.Click += new System.EventHandler(this.saveSettingsStartNewGame_Click);
        // 
        // lWhiteEasy
        // 
        this.lWhiteEasy.AutoSize = true;
        this.lWhiteEasy.Enabled = false;
        this.lWhiteEasy.Location = new System.Drawing.Point(10, 115);
        this.lWhiteEasy.Name = "lWhiteEasy";
        this.lWhiteEasy.Size = new System.Drawing.Size(36, 13);
        this.lWhiteEasy.TabIndex = 10;
        this.lWhiteEasy.Text = "Lehký";
        this.lWhiteEasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lWhiteEasy.Click += new System.EventHandler(this.lWhiteEasy_Click);
        // 
        // lWhiteNormal
        // 
        this.lWhiteNormal.AutoSize = true;
        this.lWhiteNormal.Enabled = false;
        this.lWhiteNormal.Location = new System.Drawing.Point(94, 115);
        this.lWhiteNormal.Name = "lWhiteNormal";
        this.lWhiteNormal.Size = new System.Drawing.Size(43, 13);
        this.lWhiteNormal.TabIndex = 11;
        this.lWhiteNormal.Text = "Střední";
        this.lWhiteNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lWhiteNormal.Click += new System.EventHandler(this.lWhiteNormal_Click);
        // 
        // lWhiteHard
        // 
        this.lWhiteHard.AutoSize = true;
        this.lWhiteHard.Enabled = false;
        this.lWhiteHard.Location = new System.Drawing.Point(181, 115);
        this.lWhiteHard.Name = "lWhiteHard";
        this.lWhiteHard.Size = new System.Drawing.Size(36, 13);
        this.lWhiteHard.TabIndex = 12;
        this.lWhiteHard.Text = "Těžký";
        this.lWhiteHard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lWhiteHard.Click += new System.EventHandler(this.lWhiteHard_Click);
        // 
        // lBlackEasy
        // 
        this.lBlackEasy.AutoSize = true;
        this.lBlackEasy.Enabled = false;
        this.lBlackEasy.Location = new System.Drawing.Point(219, 115);
        this.lBlackEasy.Name = "lBlackEasy";
        this.lBlackEasy.Size = new System.Drawing.Size(36, 13);
        this.lBlackEasy.TabIndex = 13;
        this.lBlackEasy.Text = "Lehký";
        this.lBlackEasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lBlackEasy.Click += new System.EventHandler(this.lBlackEasy_Click);
        // 
        // lBlackNormal
        // 
        this.lBlackNormal.AutoSize = true;
        this.lBlackNormal.Enabled = false;
        this.lBlackNormal.Location = new System.Drawing.Point(300, 115);
        this.lBlackNormal.Name = "lBlackNormal";
        this.lBlackNormal.Size = new System.Drawing.Size(43, 13);
        this.lBlackNormal.TabIndex = 14;
        this.lBlackNormal.Text = "Střední";
        this.lBlackNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lBlackNormal.Click += new System.EventHandler(this.lBlackNormal_Click);
        // 
        // lBlackHard
        // 
        this.lBlackHard.AutoSize = true;
        this.lBlackHard.Enabled = false;
        this.lBlackHard.Location = new System.Drawing.Point(396, 115);
        this.lBlackHard.Name = "lBlackHard";
        this.lBlackHard.Size = new System.Drawing.Size(36, 13);
        this.lBlackHard.TabIndex = 15;
        this.lBlackHard.Text = "Těžký";
        this.lBlackHard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lBlackHard.Click += new System.EventHandler(this.lBlackHard_Click);
        // 
        // GameSettings
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(432, 278);
        this.Controls.Add(this.lBlackHard);
        this.Controls.Add(this.lBlackNormal);
        this.Controls.Add(this.lBlackEasy);
        this.Controls.Add(this.lWhiteHard);
        this.Controls.Add(this.lWhiteNormal);
        this.Controls.Add(this.lWhiteEasy);
        this.Controls.Add(this.saveSettingsStartNewGame);
        this.Controls.Add(this.blackComputerDifficulty);
        this.Controls.Add(this.whiteComputerDifficulty);
        this.Controls.Add(this.startingPlayer);
        this.Controls.Add(this.moveHelp);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.settingsSave);
        this.Controls.Add(this.settingsStorno);
        this.Controls.Add(this.blackPlayer);
        this.Controls.Add(this.whitePlayer);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "GameSettings";
        this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.Text = "Nastavení hry";
        this.whitePlayer.ResumeLayout(false);
        this.whitePlayer.PerformLayout();
        this.blackPlayer.ResumeLayout(false);
        this.blackPlayer.PerformLayout();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.moveHelp.ResumeLayout(false);
        this.moveHelp.PerformLayout();
        this.startingPlayer.ResumeLayout(false);
        this.startingPlayer.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.whiteComputerDifficulty)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.blackComputerDifficulty)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox whitePlayer;
    private System.Windows.Forms.GroupBox blackPlayer;
    private System.Windows.Forms.Button settingsStorno;
    private System.Windows.Forms.Button settingsSave;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel gameSettingsInfo;
    private System.Windows.Forms.RadioButton whitePlayerPc;
    private System.Windows.Forms.RadioButton whitePlayerHuman;
    private System.Windows.Forms.RadioButton blackPlayerPc;
    private System.Windows.Forms.RadioButton blackPlayerHuman;
    private System.Windows.Forms.GroupBox moveHelp;
    private System.Windows.Forms.RadioButton moveHelpNo;
    private System.Windows.Forms.RadioButton moveHelpYes;
    private System.Windows.Forms.GroupBox startingPlayer;
    private System.Windows.Forms.RadioButton startingPlayerBlack;
    private System.Windows.Forms.RadioButton startingPlayerWhite;
    private System.Windows.Forms.TrackBar whiteComputerDifficulty;
    private System.Windows.Forms.TrackBar blackComputerDifficulty;
    private System.Windows.Forms.Button saveSettingsStartNewGame;
    private System.Windows.Forms.Label lWhiteEasy;
    private System.Windows.Forms.Label lWhiteNormal;
    private System.Windows.Forms.Label lWhiteHard;
    private System.Windows.Forms.Label lBlackEasy;
    private System.Windows.Forms.Label lBlackNormal;
    private System.Windows.Forms.Label lBlackHard;
  }
}