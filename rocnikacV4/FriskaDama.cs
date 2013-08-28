#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using rocnikacV4.Properties;

#endregion

namespace rocnikacV4
{
    public partial class Friska_dama : Form
    {
        #region Proměnné

        private readonly AI ai = new AI();
        private Gameboard gb = new Gameboard();
        private Rules r = new Rules();

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

        public Friska_dama()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = false;
        }

        public BackgroundWorker bw { get; set; }

        public Gameboard GB
        {
            get { return gb; }
            set { gb = value; }
        }

        public Button UndoButton
        {
            get { return bUndo; }
        }

        public Button RedoButton
        {
            get { return bRedo; }
        }

        public ListBox History
        {
            get { return lbHistory; }
            set { lbHistory = value; }
        }

        public Button ButtonPause
        {
            get { return pauseButton; }
            set { pauseButton = value; }
        }

        public void cleanHistory()
        {
            History.Items.Clear();
        }

        public void disableSettings()
        {
            settings.Enabled = false;
        }

        public void enableSettings()
        {
            settings.Enabled = true;
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
                l.Location = new Point(LETTER_SHIFT_X + i*FAIRWAY_WIDTH, LETTER_Y_PADDING);
                l.Text = letter.ToString();
                Controls.Add(l);

                // cisla
                l = new Label();
                l.Location = new Point(0, LETTER_SHIFT_Y + i*FAIRWAY_HEIGHT);
                l.AutoSize = true;
                l.TextAlign = ContentAlignment.MiddleRight;
                l.Text = (i + 1).ToString();
                Controls.Add(l);

                letter = (char) (letter + 1);
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
                    fw.Location = new Point(FAIRWAY_SHIFT + (i*FAIRWAY_HEIGHT), 2*FAIRWAY_SHIFT + (j*FAIRWAY_WIDTH));
                    fw.BackgroundImage = ((i + j)%2 == 1) ? Resources.board_dark : Resources.board_light;
                    gb.Board[j, i] = fw;
                    Controls.Add(fw);
                }
                letter = (char) (letter + 1);
            }
            gb.Winner = status.free;
            gb.mainDialog = this;
            statusBarLabel.Text = "Pro spuštění nové hry nebo načtení uložené hry stiskněte Hra v levém horním rohu";
        }

        public void FriskaDama_Activated(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            Activated -= FriskaDama_Activated;
        }

        #endregion Inicializace okna / přidání popisků aj.

        #region Pozastavení a znovuspuštení hry

        private void pauseGame()
        {
            if (bw.IsBusy)
                bw.CancelAsync();
            pauseButton.Click -= pauseButtonResume_Click;
            pauseButton.Click += pauseButtonResume_Click;
        }

        private void resumeGame()
        {
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
            pauseButton.Click -= pauseButtonResume_Click;
            pauseButton.Click += pauseButton_Click;
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Activated -= FriskaDama_Activated;
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
            List<Fairway> bestMove = ai.bestMove(gb, 3);
            string move = String.Format("Jako nejlepší tah z krátkodobého hlediska se může jevit tah z {0} na {1}",
                                        bestMove[0].Name, bestMove[1].Name);
            MessageBox.Show(move);
        }

        #endregion Nápověda (nejlepšího tahu)

        #region Ukladani / Načítání hry aj.

        private void newGame_Click(object sender, EventArgs e)
        {
            pauseGame();
            var gs = new GameSettings(gb, true);
            gs.mainDialog = this;
            gs.ShowDialog();

            if (gs.DialogResult == DialogResult.OK)
                bestMove.Enabled = true;

            Activated += FriskaDama_Activated;

            FriskaDama_Activated(sender, e);
        }

        private void saveGame_Click(object sender, EventArgs e)
        {
            var sl = new SaveLoad();
            pauseGame();
            var sfd = new SaveFileDialog();
            sfd.Filter = "XML soubor (*.xml)|*.xml";
            sfd.Title = "Uložení hry";

            var moves = new List<Move>();

            foreach (string item in History.Items)
            {
                var values = new List<string>(item.Split(' '));
                values.RemoveAll(c => c.Equals(""));
                values.RemoveAll(c => c.Equals("->"));
                // odstranime typ tahu
                values.RemoveAt(4);
                // odstranime slovo hrac
                values.RemoveAt(1);

                string player = values[0];
                string from = values[1];
                string to = values[2];

                var move = new Move(from, to, player);
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
            var ofd = new OpenFileDialog();
            ofd.Filter = "XML soubor (*.xml)|*.xml";
            ofd.Title = "Načtení uložené hry";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cleanHistory();
                    var sl = new SaveLoad();
                    gb = sl.loadGame(ofd.FileName, gb);
                    gb.drawBoard();
                    gb.colorUp();
                    pauseGame();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Hru se nepodařilo načíst!\n{0}", ex.Message));
                    cleanHistory();
                }
            }
            else
                MessageBox.Show("Hru se nepodařilo načíst!");
        }

        private void settings_Click(object sender, EventArgs e)
        {
            pauseGame();
            var gs = new GameSettings(gb, false);
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
            if (History.Items.Count >= 2 && History.SelectedIndex > -1)
            {
                History.SelectedIndex -= 1;
                gb.drawBoard();
                gb.colorUp();
            }
            if (History.SelectedIndex == 0)
            {
                bUndo.Enabled = false;
            }
        }

        private void bRedo_Click(object sender, EventArgs e)
        {
            if (History.SelectedIndex < History.Items.Count - 1)
            {
                History.SelectedIndex += 1;
            }
        }

        private void lbHistory_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            int index = lbHistory.IndexFromPoint(e.Location);
            statusBarLabel.Text = lbHistory.IndexFromPoint(e.Location).ToString();

            if (index == 0)
                bUndo.Enabled = false;
            if (index == lbHistory.Items.Count)
                bRedo.Enabled = false;
        }

        private void pauseButtonResume_Click(object sender, EventArgs e)
        {
            pauseButton.Text = "Zastavit hru";
            gb.drawBoard();
            pauseButton.Click -= pauseButtonResume_Click;
            pauseButton.Click += pauseButton_Click;
            if (!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            gb.drawBoard();
            pauseButton.Click -= pauseButtonResume_Click;
            pauseButton.Click += pauseButtonResume_Click;

            if (bw.IsBusy)
                bw.CancelAsync();
        }

        #endregion Ovladaci prvky / tlacitka

        #region BackgroundWorker a s ním spojene funkce/metody

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            gb.drawBoard();
            Action disableHistory = () => History.Enabled = false;
            Action disablePauseButton = () => ButtonPause.Enabled = false;
            Action enablePauseButton = () => ButtonPause.Enabled = true;
            History.Invoke(disableHistory);

            while (gb.Winner == status.free)
            {
                string player = gb.PlayingWhite ? "Hraje bílý hráč" : "Hraje černý hráč";
                statusBarLabel.Text = player;

                if (gb.PlayingWhite)
                {
                    if (gb.WhitePlayer != 0)
                    {
                        ButtonPause.Invoke(enablePauseButton);
                        History.Invoke(disableHistory);
                        disableAllFairways(gb);
                        ai.playAI(gb, bw);
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
                        ai.playAI(gb, bw);
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
                Action enableHistory = () => History.Enabled = true;
                History.Invoke(enableHistory);

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
                    Activated -= FriskaDama_Activated;
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
            var rules = new Rules();
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
                Action enableHistory = () => History.Enabled = true;
                Action enablePause = () => ButtonPause.Enabled = true;
                History.Invoke(enableHistory);
                ButtonPause.Invoke(enablePause);

                gb.drawBoard();
                MessageBox.Show("Hra byla pozastavena");
                Action setResumeText = () => ButtonPause.Text = "Pokračovat";
                pauseButton.Invoke(setResumeText);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(String.Format("Doslo k chybe pri vykonavani vypoctu {0}", e.Error));
            }
            else if (winner != null)
            {
                MessageBox.Show(winner);
                pauseButton.Enabled = false;
            }
            else
            {
                Activated += FriskaDama_Activated;
            }
        }

        private void disableAllFairways(Gameboard gb)
        {
            Action<Fairway> disable = (fw) => fw.Enabled = false;
            foreach (var fw in gb.Board)
                fw.Invoke(disable, fw);
        }

        private void enableallFairways(Gameboard gb)
        {
            Action<Fairway> enable = (fw) => fw.Enabled = true;
            for (int i = 0; i < GlobalVariables.size; i++)
            {
                for (int j = 0; j < GlobalVariables.size; j++)
                {
                    if ((i + j)%2 == 1)
                        gb.Board[i, j].Invoke(enable, gb.Board[i, j]);
                }
            }
        }

        #endregion BackgroundWorker a s ním spojene funkce/metody
    }
}