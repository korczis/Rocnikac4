#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

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
        ///     Metoda starajici se o nastaveni okna s nastavením v závislosti na parametrech hry
        /// </summary>
        /// <param name="gb">Instance tridy hraci deska</param>
        /// <param name="newGame">hodnota reprezentujici, zda bylo okno vyvolano pomoci menu itemu nova hra nebo nastaveni</param>
        private void setSettingsWindow(Gameboard gb, bool newGame)
        {
            var _whitePlayer = new List<RadioButton>();
            var _blackPlayer = new List<RadioButton>();
            var _moveHelp = new List<RadioButton>();
            var _startingPlayer = new List<RadioButton>();

            foreach (Control c in whitePlayer.Controls)
                _whitePlayer.Add(c as RadioButton);

            foreach (Control c in blackPlayer.Controls)
                _blackPlayer.Add(c as RadioButton);

            foreach (Control c in moveHelp.Controls)
                _moveHelp.Add(c as RadioButton);

            foreach (Control c in startingPlayer.Controls)
                _startingPlayer.Add(c as RadioButton);

            startingPlayer.Enabled = newGame ? true : false;

            // nastaveni hodnoty zacinajiciho hrace
            if (gb.PlayingWhite)
                startingPlayerWhite.Checked = true;
            else startingPlayerBlack.Checked = true;

            // nastaveni hodnoty bileho hrace
            if (gb.WhitePlayer == 0)
            {
                whitePlayerHuman.Checked = true;
                whiteComputerDifficulty.Enabled = false;
                lWhiteEasy.Enabled = false;
                lWhiteNormal.Enabled = false;
                lWhiteHard.Enabled = false;
            }
            else
            {
                whitePlayerPc.Checked = true;
                whiteComputerDifficulty.Enabled = true;
                whiteComputerDifficulty.Value = gb.WhitePlayer;
                lWhiteEasy.Enabled = true;
                lWhiteNormal.Enabled = true;
                lWhiteHard.Enabled = true;
            }

            // nastaveni hodnoty cerneho hrace
            if (gb.BlackPlayer == 0)
            {
                blackPlayerHuman.Checked = true;
                blackComputerDifficulty.Enabled = false;
            }
            else
            {
                blackPlayerPc.Checked = true;
                blackComputerDifficulty.Enabled = true;
                blackComputerDifficulty.Value = gb.BlackPlayer;
            }

            // nastaveni zobrazovani napovedy
            if (gb.ShowMoveHelp)
                moveHelpYes.Checked = true;
            else moveHelpNo.Checked = true;

            gameSettingsInfo.Text = "";
            if (newGame)
            {
                settingsSave.Enabled = false;
                saveSettingsStartNewGame.Enabled = true;
                Text = "Nová hra - nastavení";
            }
            else
            {
                Text = "Nastavení hry";
            }
            AcceptButton = newGame ? saveSettingsStartNewGame : settingsSave;
            CancelButton = settingsStorno;
            this.gb = gb;
        }

        /// <summary>
        ///     Metoda deaktivujici radiobuttony
        /// </summary>
        /// <param name="l">Seznam radiobuttonu</param>
        /// <param name="chosen">Vybrany radiobutton, ktery ma zustat aktivni</param>
        private void deactiveOther(List<RadioButton> l, RadioButton chosen)
        {
            foreach (var rb in l)
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
            var r = new Rules();
            gb.Winner = status.free;
            mainDialog.cleanHistory();
            gb.newBoard();
            bool playingWhite = startingPlayerWhite.Checked;
            gb.StartsWhite = startingPlayerWhite.Checked;
            int whitePlayer = whitePlayerHuman.Checked ? 0 : whiteComputerDifficulty.Value;
            int blackPlayer = blackPlayerHuman.Checked ? 0 : blackComputerDifficulty.Value;
            bool showMoveHelp = moveHelpYes.Checked;

            mainDialog.enableSettings();

            gb.ShowMoveHelp = showMoveHelp;
            gb.WhitePlayer = whitePlayer;
            gb.BlackPlayer = blackPlayer;
            gb.PlayingWhite = playingWhite;

            foreach (var fw in gb.Board)
                fw.Enabled = true;

            r.startGame(gb);
            DialogResult = DialogResult.OK;
        }

        private void settingsStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        // pri kliknuti na tlacitko ulozit zmeny provest ulozeni vsech hodnot
        private void settingsSave_Click(object sender, EventArgs e)
        {
            gb.ShowMoveHelp = moveHelpYes.Checked;
            gb.PlayingWhite = startingPlayerWhite.Checked;
            gb.StartsWhite = startingPlayerWhite.Checked;
            gb.WhitePlayer = whitePlayerHuman.Checked ? 0 : whiteComputerDifficulty.Value;
            gb.BlackPlayer = blackPlayerHuman.Checked ? 0 : blackComputerDifficulty.Value;
            DialogResult = DialogResult.OK;
        }

        private void lWhiteEasy_Click(object sender, EventArgs e)
        {
            whiteComputerDifficulty.Value = 1;
        }

        private void lWhiteNormal_Click(object sender, EventArgs e)
        {
            whiteComputerDifficulty.Value = 2;
        }

        private void lWhiteHard_Click(object sender, EventArgs e)
        {
            whiteComputerDifficulty.Value = 3;
        }

        private void lBlackEasy_Click(object sender, EventArgs e)
        {
            blackComputerDifficulty.Value = 1;
        }

        private void lBlackNormal_Click(object sender, EventArgs e)
        {
            blackComputerDifficulty.Value = 2;
        }

        private void lBlackHard_Click(object sender, EventArgs e)
        {
            blackComputerDifficulty.Value = 3;
        }

        #endregion Metody reagujici na klik

        #region Metody reagujicí na změnu

        // nastaveni textu informacniho panelu pri zmene zacinajiciho hrace
        private void startingPlayerWhite_CheckedChanged(object sender, EventArgs e)
        {
            gameSettingsInfo.Text = "Hru bude začínat bílý hráč";
        }

        // nastaveni textu informacniho panelu pri zmene zacinajiciho hrace
        private void startingPlayerBlack_CheckedChanged(object sender, EventArgs e)
        {
            gameSettingsInfo.Text = "Hru bude začínat černý hráč";
        }

        // nastaveni textu informacniho panelu pri zmene nastaveni napovedy tahu
        private void moveHelpYes_CheckedChanged(object sender, EventArgs e)
        {
            gameSettingsInfo.Text = "Zobrazování možných tahů zapnuto";
        }

        // nastaveni textu informacniho panelu pri zmene nastaveni napovedy tahu
        private void moveHelpNo_CheckedChanged(object sender, EventArgs e)
        {
            gameSettingsInfo.Text = "Zobrazování možných tahů vypnuto";
        }

        // nastaveni textu informacniho panelu pri zmene typu bileho hrace
        // zobrazeni listy s nastavitelnou obtiznosti
        private void whitePlayerPc_CheckedChanged(object sender, EventArgs e)
        {
            whiteComputerDifficulty.Enabled = true;
            lWhiteEasy.Enabled = true;
            lWhiteNormal.Enabled = true;
            lWhiteHard.Enabled = true;
            gameSettingsInfo.Text = "Počítač hraje bílého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu bileho hrace
        // skryti listy s nastavitelnou obtiznosti
        private void whitePlayerHuman_CheckedChanged(object sender, EventArgs e)
        {
            whiteComputerDifficulty.Enabled = false;
            lWhiteEasy.Enabled = false;
            lWhiteNormal.Enabled = false;
            lWhiteHard.Enabled = false;
            gameSettingsInfo.Text = "Člověk hraje bílého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu cerneho hrace
        // zobrazeni listy s nastavitelnou obtiznosti
        private void blackPlayerPc_CheckedChanged(object sender, EventArgs e)
        {
            blackComputerDifficulty.Enabled = true;
            lBlackEasy.Enabled = true;
            lBlackNormal.Enabled = true;
            lBlackHard.Enabled = true;
            gameSettingsInfo.Text = "Počítač hraje černého hráče";
        }

        // nastaveni textu informacniho panelu pri zmene typu cerneho hrace
        // skryti listy s nastavitelnou obtiznosti
        private void blackPlayerHuman_CheckedChanged(object sender, EventArgs e)
        {
            blackComputerDifficulty.Enabled = false;
            lBlackEasy.Enabled = false;
            lBlackNormal.Enabled = false;
            lBlackHard.Enabled = false;
            gameSettingsInfo.Text = "Člověk hraje černého hráče";
        }

        #endregion Metody reagujicí na změnu
    }
}