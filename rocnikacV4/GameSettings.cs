using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace rocnikacV4
{
    public partial class GameSettings : Form
    {
        #region Pomocné proměnné

        private Gameboard gb;

        #endregion Pomocné proměnné

        #region Getry a Setry

        public Friska_dama mainDialog { get; set; }

        #endregion Getry a Setry

        #region Konstruktor třídy

        public GameSettings()
        {
            InitializeComponent();
        }

        public GameSettings(Gameboard gb, bool newGame)
        {
            InitializeComponent();
            this.gb = gb;
            setSettingsWindow(gb, newGame);
        }

        #endregion Konstruktor třídy

        #region Pomocné metody nastavující okno nastavení

        /// <summary>
        /// Metoda starajici se o nastaveni okna s nastavením v závislosti na parametrech hry
        /// </summary>
        /// <param name="gb">Instance tridy hraci deska</param>
        /// <param name="newGame">hodnota reprezentujici, zda bylo okno vyvolano pomoci menu itemu nova hra nebo nastaveni</param>
        private void setSettingsWindow(Gameboard gb, bool newGame)
        {
            List<RadioButton> _whitePlayer = new List<RadioButton>();
            List<RadioButton> _blackPlayer = new List<RadioButton>();
            List<RadioButton> _moveHelp = new List<RadioButton>();
            List<RadioButton> _startingPlayer = new List<RadioButton>();

            foreach (Control c in this.whitePlayer.Controls)
                _whitePlayer.Add(c as RadioButton);

            foreach (Control c in this.blackPlayer.Controls)
                _blackPlayer.Add(c as RadioButton);

            foreach (Control c in this.moveHelp.Controls)
                _moveHelp.Add(c as RadioButton);

            foreach (Control c in this.startingPlayer.Controls)
                _startingPlayer.Add(c as RadioButton);

            this.startingPlayer.Enabled = newGame ? true : false;

            // nastaveni hodnoty zacinajiciho hrace
            if (gb.PlayingWhite)
                this.startingPlayerWhite.Checked = true;
            else this.startingPlayerBlack.Checked = true;

            // nastaveni hodnoty bileho hrace
            if (gb.WhitePlayer == 0)
            {
                this.whitePlayerHuman.Checked = true;
                this.whiteComputerDifficulty.Enabled = false;
                this.lWhiteEasy.Enabled = false;
                this.lWhiteNormal.Enabled = false;
                this.lWhiteHard.Enabled = false;
            }
            else
            {
                this.whitePlayerPc.Checked = true;
                this.whiteComputerDifficulty.Enabled = true;
                this.whiteComputerDifficulty.Value = gb.WhitePlayer;
                this.lWhiteEasy.Enabled = true;
                this.lWhiteNormal.Enabled = true;
                this.lWhiteHard.Enabled = true;
            }

            // nastaveni hodnoty cerneho hrace
            if (gb.BlackPlayer == 0)
            {
                this.blackPlayerHuman.Checked = true;
                this.blackComputerDifficulty.Enabled = false;
            }
            else
            {
                this.blackPlayerPc.Checked = true;
                this.blackComputerDifficulty.Enabled = true;
                this.blackComputerDifficulty.Value = gb.BlackPlayer;
            }

            // nastaveni zobrazovani napovedy
            if (gb.ShowMoveHelp)
                this.moveHelpYes.Checked = true;
            else this.moveHelpNo.Checked = true;

            this.gameSettingsInfo.Text = "";
            if (newGame)
            {
                this.settingsSave.Enabled = false;
                this.saveSettingsStartNewGame.Enabled = true;
                this.Text = "Nová hra - nastavení";
            }
            else
            {
                this.Text = "Nastavení hry";
            }
            this.AcceptButton = newGame ? this.saveSettingsStartNewGame : this.settingsSave;
            this.CancelButton = this.settingsStorno;
            this.gb = gb;
        }

        /// <summary>
        /// Metoda deaktivujici radiobuttony
        /// </summary>
        /// <param name="l">Seznam radiobuttonu</param>
        /// <param name="chosen">Vybrany radiobutton, ktery ma zustat aktivni</param>
        private void deactiveOther(List<RadioButton> l, RadioButton chosen)
        {
            foreach (RadioButton rb in l)
            {
                if (rb.Equals(chosen))
                    rb.Checked = true;
                else rb.Checked = false;
            }
        }

        #endregion Pomocné metody nastavující okno nastavení

        #region Metody reagujici na klik

        private void saveSettingsStartNewGame_Click(object sender, EventArgs e)
        {
            Rules r = new Rules();
            this.gb.Winner = status.free;
            this.mainDialog.cleanHistory();
            this.gb.newBoard();
            bool playingWhite = this.startingPlayerWhite.Checked;
            this.gb.StartsWhite = this.startingPlayerWhite.Checked;
            int whitePlayer = this.whitePlayerHuman.Checked ? 0 : this.whiteComputerDifficulty.Value;
            int blackPlayer = this.blackPlayerHuman.Checked ? 0 : this.blackComputerDifficulty.Value;
            bool showMoveHelp = this.moveHelpYes.Checked;

            mainDialog.enableSettings();

            this.gb.ShowMoveHelp = showMoveHelp;
            this.gb.WhitePlayer = whitePlayer;
            this.gb.BlackPlayer = blackPlayer;
            this.gb.PlayingWhite = playingWhite;

            foreach (Fairway fw in gb.Board)
                fw.Enabled = true;

            r.startGame(gb);
            this.DialogResult = DialogResult.OK;
        }

        private void settingsStorno_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // pri kliknuti na tlacitko ulozit zmeny provest ulozeni vsech hodnot
        private void settingsSave_Click(object sender, EventArgs e)
        {
            this.gb.ShowMoveHelp = this.moveHelpYes.Checked;
            this.gb.PlayingWhite = this.startingPlayerWhite.Checked;
            this.gb.StartsWhite = this.startingPlayerWhite.Checked;
            this.gb.WhitePlayer = this.whitePlayerHuman.Checked ? 0 : this.whiteComputerDifficulty.Value;
            this.gb.BlackPlayer = this.blackPlayerHuman.Checked ? 0 : this.blackComputerDifficulty.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void lWhiteEasy_Click(object sender, EventArgs e)
        {
            this.whiteComputerDifficulty.Value = 1;
        }

        private void lWhiteNormal_Click(object sender, EventArgs e)
        {
            this.whiteComputerDifficulty.Value = 2;
        }

        private void lWhiteHard_Click(object sender, EventArgs e)
        {
            this.whiteComputerDifficulty.Value = 3;
        }

        private void lBlackEasy_Click(object sender, EventArgs e)
        {
            this.blackComputerDifficulty.Value = 1;
        }

        private void lBlackNormal_Click(object sender, EventArgs e)
        {
            this.blackComputerDifficulty.Value = 2;
        }

        private void lBlackHard_Click(object sender, EventArgs e)
        {
            this.blackComputerDifficulty.Value = 3;
        }

        #endregion Metody reagujici na klik

        #region Metody reagujicí na změnu

        // nastaveni textu informacniho panelu pri zmene zacinajiciho hrace
        private void startingPlayerWhite_CheckedChanged(object sender, EventArgs e)
        {
            this.gameSettingsInfo.Text = "Hru bude začínat bílý hráč";
        }

        // nastaveni textu informacniho panelu pri zmene zacinajiciho hrace
        private void startingPlayerBlack_CheckedChanged(object sender, EventArgs e)
        {
            this.gameSettingsInfo.Text = "Hru bude začínat černý hráč";
        }

        // nastaveni textu informacniho panelu pri zmene nastaveni napovedy tahu
        private void moveHelpYes_CheckedChanged(object sender, EventArgs e)
        {
            this.gameSettingsInfo.Text = "Zobrazování možných tahů zapnuto";
        }

        // nastaveni textu informacniho panelu pri zmene nastaveni napovedy tahu
        private void moveHelpNo_CheckedChanged(object sender, EventArgs e)
        {
            this.gameSettingsInfo.Text = "Zobrazování možných tahů vypnuto";
        }

        // nastaveni textu informacniho panelu pri zmene typu bileho hrace
        // zobrazeni listy s nastavitelnou obtiznosti
        private void whitePlayerPc_CheckedChanged(object sender, EventArgs e)
        {
            this.whiteComputerDifficulty.Enabled = true;
            this.lWhiteEasy.Enabled = true;
            this.lWhiteNormal.Enabled = true;
            this.lWhiteHard.Enabled = true;
            this.gameSettingsInfo.Text = "Počítač hraje bílého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu bileho hrace
        // skryti listy s nastavitelnou obtiznosti
        private void whitePlayerHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.whiteComputerDifficulty.Enabled = false;
            this.lWhiteEasy.Enabled = false;
            this.lWhiteNormal.Enabled = false;
            this.lWhiteHard.Enabled = false;
            this.gameSettingsInfo.Text = "Člověk hraje bílého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu cerneho hrace
        // zobrazeni listy s nastavitelnou obtiznosti
        private void blackPlayerPc_CheckedChanged(object sender, EventArgs e)
        {
            this.blackComputerDifficulty.Enabled = true;
            this.lBlackEasy.Enabled = true;
            this.lBlackNormal.Enabled = true;
            this.lBlackHard.Enabled = true;
            this.gameSettingsInfo.Text = "Počítač hraje černého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu cerneho hrace
        // skryti listy s nastavitelnou obtiznosti
        private void blackPlayerHuman_CheckedChanged(object sender, EventArgs e)
        {
            this.blackComputerDifficulty.Enabled = false;
            this.lBlackEasy.Enabled = false;
            this.lBlackNormal.Enabled = false;
            this.lBlackHard.Enabled = false;
            this.gameSettingsInfo.Text = "Člověk hraje černého hráče";
        }

        #endregion Metody reagujicí na změnu
    }
}