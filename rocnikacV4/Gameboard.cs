using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace rocnikacV4
{
    public class Gameboard
    {
        #region Pomocné proměnné

        private Rules rules = new Rules();

        #endregion Pomocné proměnné

        #region Sloty třídy

        private Fairway[,] board = new Fairway[GlobalVariables.size, GlobalVariables.size];
        private bool playingWhite;
        private int whitePlayer;
        private int blackPlayer;
        private bool showMoveHelp;
        private int movesWithoutJump;
        private Fairway from;
        private status winner;
        private static List<string> missedHistory = new List<string>();

        #endregion Sloty třídy

        #region Getry a setry

        public Friska_dama mainDialog { get; set; }

        public List<string> MissedHistory
        {
            get;
            private set;
        }

        public Fairway[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        public bool PlayingWhite
        {
            get { return playingWhite; }
            set { playingWhite = value; }
        }

        public int WhitePlayer
        {
            get { return whitePlayer; }
            set { whitePlayer = value; }
        }

        public int BlackPlayer
        {
            get { return blackPlayer; }
            set { blackPlayer = value; }
        }

        public bool ShowMoveHelp
        {
            get { return showMoveHelp; }
            set { showMoveHelp = value; }
        }

        public bool StartsWhite { get; set; }

        public int MovesWithoutJump
        {
            get { return movesWithoutJump; }
            set { movesWithoutJump = value; }
        }

        public Fairway From
        {
            get { return from; }
            set { from = value; }
        }

        public status Winner
        {
            get { return winner; }
            set { winner = value; }
        }

        #endregion Getry a setry

        #region Konstruktory třídy

        public Gameboard()
        {
            Board = new Fairway[GlobalVariables.size, GlobalVariables.size];
            PlayingWhite = true;
            WhitePlayer = 0;
            BlackPlayer = 0;
            ShowMoveHelp = true;
            From = null;
            Winner = status.free;
        }

        public Gameboard(bool playingWhite, int whitePlayer, int blackPlayer, bool showMoveHelp)
        {
            Board = newBoard();
            PlayingWhite = playingWhite;
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
            ShowMoveHelp = showMoveHelp;
            From = null;
        }

        #endregion Konstruktory třídy

        #region Pomocné funkce realizující vykreslení hrací desky

        private bool isPlayer(int i, int j)
        {
            return ((i + j) % 2) == 1;
        }

        public Fairway[,] newBoard()
        {
            Fairway fw;
            char letter = 'A';

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    fw = this.Board[j, i];
                    fw.Dests = new List<string>();
                    fw.Overs = new List<string>();
                    fw.Moves = new List<string>();
                    fw.Queen = false;
                    fw.Jump = false;
                    fw.StoneMove = false;
                    fw.Name = (letter + "" + (j + 1));
                    fw.Click -= fw_2ndClick;
                    fw.Click -= fw_1stClick;

                    if (j < GlobalVariables.rowsOfPlayers && isPlayer(i, j))
                        fw.Player = status.blackPlayer;
                    else if (j >= GlobalVariables.size - GlobalVariables.rowsOfPlayers && isPlayer(i, j))
                        fw.Player = status.whitePlayer;

                    else
                        fw.Player = status.free;
                    if ((i + j) % 2 == 1) fw.Click += new EventHandler(fw_1stClick);
                }
                letter = (char)((int)letter + 1);
            }
            return board;
        }

        #endregion Pomocné funkce realizující vykreslení hrací desky

        #region Funkce vykreslující hrací desku

        // funkce obarvujici mozne tahy figurkou
        private void fairwayColorUp(Gameboard gb, Fairway fw)
        {
            List<string> moves = fw.Moves;

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    Fairway chosen = gb.Board[i, j];
                    if (moves.Contains(chosen.Name) && gb.ShowMoveHelp)
                        chosen.BackgroundImage = Properties.Resources.board_dark_active;
                    else chosen.BackgroundImage = ((i + j) % 2 == 1) ? Properties.Resources.board_dark : Properties.Resources.board_light;
                }
            }
            fw.BackgroundImage = Properties.Resources.board_dark_active_figure;
        }

        // funkce obarvujici figurky, jemiz je mozne tahnout
        public void colorUp()
        {
            List<Fairway> figures = rules.choosableFigures(this);

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    Fairway chosen = this.Board[i, j];
                    if (figures.Contains(chosen) && this.ShowMoveHelp)
                        chosen.BackgroundImage = Properties.Resources.board_dark_active_figure;
                    else chosen.BackgroundImage = ((i + j) % 2 == 1) ? Properties.Resources.board_dark : Properties.Resources.board_light;
                }
            }
        }

        /// <summary>
        /// funkce vykreslujici hraci desku
        /// </summary>
        public void drawBoard()
        {
            Fairway[,] board = this.Board;
            status white = status.whitePlayer;
            status black = status.blackPlayer;

            foreach (Fairway fw in board)
            {
                if (fw.Player == white)
                    fw.Image = fw.Queen ? Properties.Resources.queen_white : Properties.Resources.figure_white;
                else if (fw.Player == black)
                    fw.Image = fw.Queen ? Properties.Resources.queen_black : Properties.Resources.figure_black;
                else
                    fw.Image = null;
            }
        }

        #endregion Funkce vykreslující hrací desku

        #region Funkce pro nalezení hracího pole a možných tahů figurky

        public List<string> possibleMoves(Fairway fw)
        {
            List<string> possibleMoves = new List<string>();
            if (fw.Dests.Count > 0)
            {
                for (int i = 0; i < fw.Dests.Count; i++)
                {
                    string[] dests = fw.Dests[i].Split('-');
                    if (!possibleMoves.Contains(dests[0]))
                        possibleMoves.Add(dests[0]);
                }
            }
            else { possibleMoves = fw.Moves; }
            return possibleMoves;
        }

        public Fairway getFigure(string from)
        {
            int fx = Convert.ToInt32(from.Substring(1)) - GlobalVariables.line;
            int fy = Convert.ToInt32(from[0]) - GlobalVariables.column;
            return board[fx, fy];
        }

        public Fairway getFigureInBetween(Fairway f, Fairway t)
        {
            Fairway result = new Fairway();
            for (int i = 0; i < f.Dests.Count; i++)
            {
                if (f.Dests[i].Equals(t.Name))
                    return getFigure(f.Overs[i]);
            }
            return new Fairway();
        }

        /*
                public Fairway getFairway(string name)
                {
                    for (int i = 0; i < GlobalVariables.size; i++)
                    {
                        for (int j = 0; j < GlobalVariables.size; j++)
                        {
                            if (this.Board[j, i].Name == name)
                                return this.Board[j, i];
                        }
                    }
                    return null;
                }

                */

        #endregion Funkce pro nalezení hracího pole a možných tahů figurky

        #region Funkce pracující s hrací deskou

        /// <summary>
        /// Funkce nevytvari kopii historie (undo a redo), zobrazeni napovedy a slotu Fairway From
        /// </summary>
        /// <returns></returns>
        ///
        public Gameboard gameboardCopy()
        {
            Gameboard result = new Gameboard();

            //*
            result.Board = new Fairway[GlobalVariables.size, GlobalVariables.size];

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                    result.Board[i, j] = this.Board[i, j].Clone();
            }
            result.playingWhite = this.PlayingWhite;
            result.BlackPlayer = this.BlackPlayer;
            result.WhitePlayer = this.WhitePlayer;
            result.MovesWithoutJump = this.MovesWithoutJump;
            result.ShowMoveHelp = this.ShowMoveHelp;
            result.From = this.From;
            result.Winner = this.Winner;
            //*/

            return result;
        }

        #endregion Funkce pracující s hrací deskou

        #region Eventy pro klik myší

        private void fw_1stClick(object sender, EventArgs e)
        {
            var form = Friska_dama.ActiveForm as Friska_dama;
            Fairway fwClicked = (Fairway)sender;
            List<Fairway> chooseAvailable = rules.choosableFigures(this);

            if (fwClicked.Player == status.free)
            {
                // pokud klikl na volne pole ignoruj a vymaz text v informacni liste
                form.statusBarLabel.Text = "";
            }
            else if (chooseAvailable.Contains(fwClicked))
            {
                if (this.From == null || this.From != fwClicked)
                {
                    foreach (Fairway fw in this.Board)
                    {
                        fw.Click -= fw_1stClick;
                        fw.Click += fw_2ndClick;
                    }
                    From = fwClicked;
                    fairwayColorUp(this, From);
                    //form.statusBarLabel.Text = "Vyberte pole, kam chcete tahnout";
                }
                else
                {
                    form.statusBarLabel.Text = "Nejedná se o vaši figurku";
                }
            }
        }

        private void fw_2ndClick(object sender, EventArgs e)
        {
            var form = Friska_dama.ActiveForm as Friska_dama;
            BackgroundWorker bw = form.bw;
            Fairway fwClicked = (Fairway)sender;
            List<Fairway> playersFigures = rules.choosableFigures(this);
            List<string> posMoves = possibleMoves(this.From);

            if (fwClicked == this.From)
            {
                foreach (Fairway fw in this.Board)
                {
                    fw.Click -= fw_2ndClick;
                    fw.Click += fw_1stClick;
                }
                if (this.ShowMoveHelp)
                    this.colorUp();
                this.From = null;
                form.statusBarLabel.Text = "Vyberte figurku, kterou chcete táhnout";
            }
            else if (fwClicked != this.From && playersFigures.Contains(fwClicked))
            {
                this.From = fwClicked;
                fairwayColorUp(this, this.From);
            }
            else if (posMoves.Contains(fwClicked.Name))
            {
                addHistory(this.From, fwClicked);

                rules.makeMove(this.From, fwClicked, this, false);
                this.Winner = rules.checkWinner(this);

                from = null;
                foreach (Fairway fw in this.Board)
                {
                    fw.Click -= fw_2ndClick;
                    fw.Click += fw_1stClick;
                }
                if (this.ShowMoveHelp)
                    this.colorUp();
                form.Activate();
                form.FriskaDama_Activated(sender, e);
            }
        }

        #endregion Eventy pro klik myší

        #region Sprava historie

        public void addMissedHistory(string input)
        {
            ListBox history = mainDialog.History;
            Action<string> addHis = (s) => history.Items.Add(s);
            Action moveHis = () => history.SelectedIndex = history.Items.Count - 1;

            if (missedHistory.Count > 0)
            {
                foreach (string s in missedHistory)
                    history.Invoke(addHis, s);
                missedHistory.Clear();
            }
            history.Invoke(addHis, input);
            history.Invoke(moveHis);
        }

        public void addHistory(Fairway from, Fairway to)
        {
            string typ = from.Jump ? "skok" : "tah";
            string hrac = this.PlayingWhite ? "Bily hrac:           " : "Cerny hrac:       ";
            string input = string.Format("{0} {1} -> {2} ({3})", hrac, from.Name, to.Name, typ);

            addMissedHistory(input);
        }

        #endregion Sprava historie
    }
}