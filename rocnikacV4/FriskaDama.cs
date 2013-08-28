using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace rocnikacV4
{
    public partial class Friska_dama : Form
    {
        #region Proměnné

        private Gameboard gb = new Gameboard();
        private Rules r = new Rules();
        private AI ai = new AI();

        #endregion Proměnné

        #region Konstanty

        private const int FAIRWAY_HEIGHT = 50;
        private const int FAIRWAY_WIDTH = 50;
        private const int FAIRWAY_SHIFT = 20;
        private const int LETTER_SHIFT_X = 46;
        private const int LETTER_SHIFT_Y = 59;
        private const int LETTER_Y_PADDING = 25;
        private string nl = Environment.NewLine;

        #endregion Konstanty

        #region Getry/Setry a metody spravujici mainDialog

        public BackgroundWorker bw { get; set; }

        public Gameboard GB
        {
            get { return this.gb; }
            set { this.gb = value; }
        }

        public Friska_dama()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = false;
        }

        public Button UndoButton
        {
            get { return this.bUndo; }
        }

        public Button RedoButton
        {
            get { return this.bRedo; }
        }

        public ListBox History
        {
            get { return this.lbHistory; }
            set { this.lbHistory = value; }
        }

        public Button ButtonPause
        {
            get { return this.pauseButton; }
            set { this.pauseButton = value; }
        }

        public void cleanHistory()
        {
            this.History.Items.Clear();
        }

        public void disableSettings()
        {
            this.settings.Enabled = false;
        }

        public void enableSettings()
        {
            this.settings.Enabled = true;
        }

        public void disableUndoButton()
        {
            UndoButton.Enabled = false;
        }

        public void enableUndoButton()
        {
            UndoButton.Enabled = true;
        }

        public void disableRedoButton()
        {
            RedoButton.Enabled = false;
        }

        public void enableRedoButton()
        {
            RedoButton.Enabled = true;
        }


        #endregion Getry/Setry a metody spravujici mainDialog

        #region Inicializace okna / přidání popisků aj.

        private void addDescriptions()
        {
            Label l;
            char letter = 'A';

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                // pismena
                l = new Label();
                l.AutoSize = true;
                l.Location = new Point(LETTER_SHIFT_X + i * FAIRWAY_WIDTH, LETTER_Y_PADDING);
                l.Text = letter.ToString();
                Controls.Add(l);

                // cisla
                l = new Label();
                l.Location = new Point(0, LETTER_SHIFT_Y + i * FAIRWAY_HEIGHT);
                l.AutoSize = true;
                l.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                l.Text = (i + 1).ToString();
                Controls.Add(l);

                letter = (char)((int)letter + 1);
            }
        }

        private void FriskaDama_Load(object sender, EventArgs e)
        {
            Fairway fw;
            char letter = 'A';
            addDescriptions();

            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    fw = new Fairway();
                    fw.Size = new Size(FAIRWAY_WIDTH, FAIRWAY_HEIGHT);
                    fw.Enabled = false;
                    fw.Location = new Point(FAIRWAY_SHIFT + (i * FAIRWAY_HEIGHT), 2 * FAIRWAY_SHIFT + (j * FAIRWAY_WIDTH));
                    fw.BackgroundImage = ((i + j) % 2 == 1) ? Properties.Resources.board_dark : Properties.Resources.board_light;
                    gb.Board[j, i] = fw;
                    Controls.Add(fw);
                }
                letter = (char)((int)letter + 1);
            }
            this.gb.Winner = status.free;
            this.gb.mainDialog = this;
            statusBarLabel.Text = "Pro spuštění nové hry nebo načtení uložené hry stiskněte Hra v levém horním rohu";
        }

        public void FriskaDama_Activated(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            this.Activated -= this.FriskaDama_Activated;
        }

        #endregion Inicializace okna / přidání popisků aj.

        #region Pozastavení a znovuspuštení hry

        private void pauseGame()
        {
            if (bw.IsBusy)
                bw.CancelAsync();
            this.pauseButton.Click -= pauseButtonResume_Click;
            this.pauseButton.Click += new System.EventHandler(this.pauseButtonResume_Click);
        }

        private void resumeGame()
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            this.pauseButton.Click -= pauseButtonResume_Click;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.Activated -= this.FriskaDama_Activated;
        }

        #endregion Pozastavení a znovuspuštení hry

        #region Nápověda (nejlepšího tahu)

        private void pravidlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseGame();
            string directory = Path.GetDirectoryName(Application.ExecutablePath);
            string path = string.Format("file://{0}", Path.Combine(directory, "Help.chm"));

            // pokud se soubor podarilo nalezt, zobrazime jej
            if (File.Exists(path))
                Help.ShowHelp(this, path);
            // v opacnem pripade zobrazime chybovou hlasku
            else
            {
                MessageBox.Show("Nápovědu se nepodařilo nalézt. Soubor je buď poškozen nebo chybí",
                    "Nápověda nenalezena!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void bestMove_Click(object sender, EventArgs e)
        {
            pauseGame();
            List<Fairway> bestMove = ai.bestMove(this.gb, 3);
            string move = String.Format("Jako nejlepší tah z krátkodobého hlediska se může jevit tah z {0} na {1}", bestMove[0].Name, bestMove[1].Name);
            MessageBox.Show(move);
        }

        #endregion Nápověda (nejlepšího tahu)

        #region Ukladani / Načítání hry aj.

        private void newGame_Click(object sender, EventArgs e)
        {
            pauseGame();
            GameSettings gs = new GameSettings(this.gb, true);
            gs.mainDialog = this;
            gs.ShowDialog();

            if (gs.DialogResult == DialogResult.OK)
                this.bestMove.Enabled = true;

            this.Activated += new System.EventHandler(FriskaDama_Activated);

            FriskaDama_Activated(sender, e);
        }

        private void saveGame_Click(object sender, EventArgs e)
        {
            SaveLoad sl = new SaveLoad();
            pauseGame();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML soubor (*.xml)|*.xml";
            sfd.Title = "Uložení hry";

            List<Move> moves = new List<Move>();

            foreach (string item in History.Items)
            {
                List<string> values = new List<string>(item.Split(' '));
                values.RemoveAll(c => c.Equals(""));
                values.RemoveAll(c => c.Equals("->"));
                // odstranime typ tahu
                values.RemoveAt(4);
                // odstranime slovo hrac
                values.RemoveAt(1);

                string player = values[0];
                string from = values[1];
                string to = values[2];

                Move move = new Move(from, to, player);
                moves.Add(move);
            }
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    sl.saveGame(moves, gb.WhitePlayer, gb.BlackPlayer, gb.StartsWhite, gb.ShowMoveHelp, sfd.FileName);
                    MessageBox.Show("Hra byla uložena");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Hru se nepodařilo uložit");
            }
        }

        private void loadGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML soubor (*.xml)|*.xml";
            ofd.Title = "Načtení uložené hry";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cleanHistory();
                    SaveLoad sl = new SaveLoad();
                    this.gb = sl.loadGame(ofd.FileName, gb);
                    this.gb.drawBoard();
                    this.gb.colorUp();
                    pauseGame();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Hru se nepodařilo načíst!\n{0}", ex.Message.ToString()));
                    cleanHistory();
                }
            }
            else
                MessageBox.Show("Hru se nepodařilo načíst!");
        }

        private void settings_Click(object sender, EventArgs e)
        {
            pauseGame();
            GameSettings gs = new GameSettings(gb, false);
            gs.mainDialog = this;
            gs.ShowDialog();

            if (gs.DialogResult == DialogResult.OK)
                resumeGame();
        }

        private void exitGame_Click(object sender, EventArgs e)
        {
            // TO-DO ulozeni hry pred ukoncenim
            //pauseGame();

            Application.Exit();
        }

        #endregion Ukladani / Načítání hry aj.

        #region Ovladaci prvky / tlacitka

        private void bUndo_Click(object sender, EventArgs e)
        {
            if (this.History.Items.Count >= 2 && this.History.SelectedIndex > -1)
            {
                this.History.SelectedIndex -= 1;
                gb.drawBoard();
                gb.colorUp();
            } if (this.History.SelectedIndex == 0)
            {
                this.bUndo.Enabled = false;
            }
        }

        private void bRedo_Click(object sender, EventArgs e)
        {
            if (this.History.SelectedIndex < this.History.Items.Count - 1)
            {
                this.History.SelectedIndex += 1;
            }
        }

        private void lbHistory_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            int index = lbHistory.IndexFromPoint(e.Location);
            statusBarLabel.Text = lbHistory.IndexFromPoint(e.Location).ToString();

            if (index == 0)
                this.bUndo.Enabled = false;
            if (index == lbHistory.Items.Count)
                this.bRedo.Enabled = false;
        }

        private void pauseButtonResume_Click(object sender, EventArgs e)
        {
            this.pauseButton.Text = "Zastavit hru";
            gb.drawBoard();
            this.pauseButton.Click -= pauseButtonResume_Click;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            gb.drawBoard();
            this.pauseButton.Click -= pauseButtonResume_Click;
            this.pauseButton.Click += new System.EventHandler(this.pauseButtonResume_Click);

            if (bw.IsBusy)
                bw.CancelAsync();
        }

        #endregion Ovladaci prvky / tlacitka

        #region BackgroundWorker a s ním spojene funkce/metody

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            gb.drawBoard();
            Action disableHistory = () => this.History.Enabled = false;
            Action disablePauseButton = () => this.ButtonPause.Enabled = false;
            Action enablePauseButton = () => this.ButtonPause.Enabled = true;
            History.Invoke(disableHistory);

            while (gb.Winner == status.free)
            {
                string player = gb.PlayingWhite ? "Hraje bílý hráč" : "Hraje černý hráč";
                this.statusBarLabel.Text = player;

                if (gb.PlayingWhite)
                {
                    if (gb.WhitePlayer != 0)
                    {
                        ButtonPause.Invoke(enablePauseButton);
                        History.Invoke(disableHistory);
                        disableAllFairways(gb);
                        ai.playAI(gb, this.bw);
                    }
                    else
                    {
                        enableallFairways(gb);
                        ButtonPause.Invoke(disablePauseButton);
                        break;
                    }
                    gb.colorUp();
                }
                else
                {
                    if (gb.BlackPlayer != 0)
                    {
                        ButtonPause.Invoke(enablePauseButton);
                        History.Invoke(disableHistory);
                        disableAllFairways(gb);
                        ai.playAI(gb, this.bw);
                    }

                    else
                    {
                        enableallFairways(gb);
                        ButtonPause.Invoke(disablePauseButton);
                        break;
                    }
                    gb.colorUp();
                }
                gb.From = null;
                Action enableHistory = () => this.History.Enabled = true;
                this.History.Invoke(enableHistory);

                if (gb.Winner != status.free)
                {
                    gb.drawBoard();
                    string winner = "";

                    switch (gb.Winner)
                    {
                        case status.draw:
                            winner = "Hra skočila remízou!";
                            break;

                        case status.whitePlayer:
                            winner = "Bílý hráč zvítězil";
                            break;

                        case status.blackPlayer:
                            winner = "Černý hráč zvítězil";
                            break;
                    }
                    e.Result = winner;
                    this.Activated -= this.FriskaDama_Activated;
                }
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                    gb.drawBoard();
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string winner = null;
            Rules rules = new Rules();
            rules.checkWinner(gb);
            if (gb.Winner != status.free)
            {
                switch (gb.Winner)
                {
                    case status.draw:
                        winner = "Hra skočila remízou!";
                        break;

                    case status.whitePlayer:
                        winner = "Bílý hráč zvítězil";
                        break;

                    case status.blackPlayer:
                        winner = "Černý hráč zvítězil";
                        break;
                }
            }

            if (e.Cancelled)
            {
                Action enableHistory = () => this.History.Enabled = true;
                Action enablePause = () => this.ButtonPause.Enabled = true;
                History.Invoke(enableHistory);
                ButtonPause.Invoke(enablePause);

                gb.drawBoard();
                MessageBox.Show("Hra byla pozastavena");
                Action setResumeText = () => this.ButtonPause.Text = "Pokračovat";
                pauseButton.Invoke(setResumeText);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(String.Format("Doslo k chybe pri vykonavani vypoctu {0}", e.Error));
            }
            else if (winner != null)
            {
                MessageBox.Show(winner);
                this.pauseButton.Enabled = false;
            }
            else
            {
                this.Activated += this.FriskaDama_Activated;
            }
        }

        private void disableAllFairways(Gameboard gb)
        {
            Action<Fairway> disable = (fw) => fw.Enabled = false;
            foreach (Fairway fw in gb.Board)
                fw.Invoke(disable, fw);
        }

        private void enableallFairways(Gameboard gb)
        {
            Action<Fairway> enable = (fw) => fw.Enabled = true;
            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    if ((i + j) % 2 == 1)
                        gb.Board[i, j].Invoke(enable, gb.Board[i, j]);
                }
            }
        }

        #endregion BackgroundWorker a s ním spojene funkce/metody
    }
}