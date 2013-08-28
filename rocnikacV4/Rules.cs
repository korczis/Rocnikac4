using System;
using System.Collections.Generic;

namespace rocnikacV4
{
    public class Rules
    {
        #region Pomocné proměnné

        private int[] xs = { 2, -2, 0, 0, 1, 1, -1, -1 };
        private int[] ys = { 0, 0, 2, -2, 1, -1, -1, 1 };

        #endregion Pomocné proměnné

        #region Metody generujicí tahy pěšce

        private void walkersMoves(Gameboard gb, Fairway fw, int dx, int dy)
        {
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';

            if (x + dx >= 0 &&
                x + dx < GlobalVariables.size &&
                y + dy >= 0 &&
                y + dy < GlobalVariables.size &&
                gb.Board[x + dx, y + dy].Player == 0)
            {
                List<String> moves = fw.Moves;
                moves.Add((char)(y + dy + 'A') + "" + (x + dx + 1));
                fw.StoneMove = moves.Count > 0 ? true : false;
                fw.Moves = moves;
            }
            fw.StoneMove = fw.Moves.Count > 0 ? true : false;
        }

        private void genWalMoves(Gameboard gb, Fairway fw)
        {
            if (!gb.PlayingWhite)
            {
                walkersMoves(gb, fw, 1, -1);
                walkersMoves(gb, fw, 1, 1);
            }
            else
            {
                walkersMoves(gb, fw, -1, -1);
                walkersMoves(gb, fw, -1, 1);
            }
        }

        /// <summary>
        /// Funkce kontrolujici, zda v danem smeru je za figurkou volno
        /// </summary>
        /// <param name="gb">Instance tridy hraci deska</param>
        /// <param name="fw">Instance tridy hraci pole reprezentujici odkud kontrolujeme</param>
        /// <param name="dx">X smer kterym kontrolujeme</param>
        /// <param name="dy">Y smer kterym kontrolujeme</param>
        /// <returns>True, pokud je za figurou v danem smeru volno, jinak false</returns>
        private bool checkPossibleJump(Gameboard gb, Fairway fw, int dx, int dy)
        {
            status playerOnMove = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';

            if (x + 2 * dx < GlobalVariables.size &&
                x + 2 * dx >= 0 &&
                y + 2 * dy < GlobalVariables.size &&
                y + 2 * dy >= 0 &&
                (int)gb.Board[x + dx, y + dy].Player == -(int)playerOnMove &&
                gb.Board[x + 2 * dx, y + 2 * dy].Player == 0)
                return true;
            else return false;
        }

        private TreeNode genWalJumpTree(Gameboard gb, Fairway fw)
        {
            TreeNode root = new TreeNode();
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';

            for (int i = 0; i < 8; i++)
            {
                if (checkPossibleJump(gb, fw, xs[i], ys[i]))
                {
                    Gameboard potomek = gb.gameboardCopy();
                    Fairway from = potomek.Board[x, y];
                    Fairway over = potomek.Board[x + xs[i], y + ys[i]];
                    Fairway to = potomek.Board[x + 2 * xs[i], y + 2 * ys[i]];

                    TreeNode jump;

                    bool bQ = from.Queen;
                    makeJump(from, over, to);
                    bool aQ = to.Queen;

                    if (bQ == aQ)
                        jump = genWalJumpTree(potomek, to);
                    else
                        jump = new TreeNode();

                    jump.over = over.Name;
                    jump.dest = to.Name;
                    root.childs.Add(jump);
                    jump.rank = over.Queen ? 3 : 2;
                }
            }
            return root;
        }

        #endregion Metody generujicí tahy pěšce

        #region Metody generujicí tahy královny

        private void genQueMoves(Gameboard gb, Fairway fw)
        {
            status playerOnMove = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;
            List<string> moves = fw.Moves;
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';
            int temp = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 1;
                    x + j * xs[i] < GlobalVariables.size &&
                    x + j * xs[i] >= 0 &&
                    y + j * ys[i] >= 0 &&
                    y + j * ys[i] < GlobalVariables.size &&
                    gb.Board[x + j * xs[i], y + j * ys[i]].Player == status.free; j++)
                {
                    if (gb.Board[x + j * xs[i], y + j * ys[i]].Player == 0)
                    {
                        string move = (char)(y + j * ys[i] + 'A') + "" + (x + j * xs[i] + 1);
                        moves.Add(move);
                    }
                    temp = j + 1;
                }
                fw.StoneMove = moves.Count > 0 ? true : false;

                genQueJumpTree(gb, fw);
            }
        }

        private List<Fairway> queAfterJumpDests(Gameboard gb, Fairway fw, int dx, int dy)
        {
            List<Fairway> result = new List<Fairway>();
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';

            for (int j = 0;
                   x + dx < GlobalVariables.size &&
                   x + dx >= 0 &&
                   y + dy < GlobalVariables.size &&
                   y + dy >= 0 &&
                   (gb.Board[x + dx, y + dy].Player == status.free);
                   j++)
            {
                result.Add(gb.Board[x + dx, y + dy]);
                x += dx;
                y += dy;
            }
            return result;
        }

        private TreeNode genQueJumpTree(Gameboard gb, Fairway fw)
        {
            TreeNode root = new TreeNode();
            List<string> moves = new List<string>();
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;
            int y = Convert.ToInt32(fw.Name[0]) - (int)'A';
            status playerOnMove = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;

            for (int i = 0; i < 8; i++)
            {
                bool jump = false;
                for (int j = 1;
                    x + j * xs[i] < GlobalVariables.size &&
                    x + j * xs[i] >= 0 &&
                    y + j * ys[i] < GlobalVariables.size &&
                    y + j * ys[i] >= 0; j++)
                {
                    // pokud je vedle volno, pridej do moves
                    if (gb.Board[x + j * xs[i], y + j * ys[i]].Player == status.free && !jump)
                    {
                        string move = (char)(y + j * ys[i] + 'A') + "" + (x + j * xs[i] + 1);
                    }
                    // pokud je vedle tvoje vlastni figurka, prerus
                    else if (gb.Board[x + j * xs[i], y + j * ys[i]].Player == playerOnMove)
                    {
                        break;
                    }
                    else if (
                        x + (j + 1) * xs[i] < GlobalVariables.size &&
                        x + (j + 1) * xs[i] >= 0 &&
                        y + (j + 1) * ys[i] >= 0 &&
                        y + (j + 1) * ys[i] < GlobalVariables.size &&
                        (int)gb.Board[x + j * xs[i], y + j * ys[i]].Player == -(int)playerOnMove &&
                        gb.Board[x + (j + 1) * xs[i], y + (j + 1) * ys[i]].Player == status.free)
                    {
                        jump = true;
                        Gameboard potomek = gb.gameboardCopy();
                        Fairway from = potomek.Board[x, y];
                        Fairway over = potomek.Board[x + j * xs[i], y + j * ys[i]];
                        Fairway to = potomek.Board[x + (j + 1) * xs[i], y + (j + 1) * ys[i]];

                        makeJump(from, over, to);
                        List<Fairway> destinies = queAfterJumpDests(potomek, to, xs[i], ys[i]);
                        destinies.Insert(0, to);

                        foreach (Fairway des in destinies)
                        {
                            TreeNode node = genQueJumpTree(potomek, des);
                            node.over = over.Name;
                            node.dest = des.Name;
                            node.rank = over.Queen ? 3 : 2;
                            root.childs.Add(node);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return root;
        }

        #endregion Metody generujicí tahy královny

        #region Obecné metody generující tahy

        public void generateMoves(Gameboard gb)
        {
            status playerOnMove = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;

            foreach (Fairway fw in gb.Board)
            {
                if (fw.Player == playerOnMove)
                    generateMoves(gb, fw);
            }
        }

        public void generateMoves(Gameboard gb, Fairway fw)
        {
            genJumps(gb, fw);
            List<string> newDests = new List<string>();
            List<string> newOvers = new List<string>();

            if (fw.Jump)
            {
                int bestRank = fw.Ranks[0];
                int rank = fw.Ranks[0];

                fw.Moves = new List<string>();

                for (int i = 0; i < fw.Ranks.Count; i++)
                {
                    rank = fw.Ranks[i];

                    string[] d = fw.Dests[i].Split('-');
                    string[] o = fw.Overs[i].Split('-');

                    if (rank == bestRank)
                    {
                        if (!newDests.Contains(d[0]))
                        {
                            newDests.Add(d[0]);
                            newOvers.Add(o[0]);
                        }

                        if (!fw.Moves.Contains(d[0]))
                            fw.Moves.Add(d[0]);
                    }
                    else if (rank > bestRank)
                    {
                        bestRank = rank;
                        fw.Moves = new List<string>();
                        newDests = new List<string>();
                        newOvers = new List<string>();

                        if (!newDests.Contains(d[0]))
                            newDests.Add(d[0]);

                        if (!newOvers.Contains(o[0]))
                            newOvers.Add(o[0]);

                        if (!fw.Moves.Contains(d[0]))
                            fw.Moves.Add(d[0]);
                    }
                }
            }
            else if (fw.Queen)
                genQueMoves(gb, fw);
            else
                genWalMoves(gb, fw);

            fw.Overs = newOvers;
            fw.Dests = newDests;
        }

        private void genJumps(Gameboard gb, Fairway fw)
        {
            int x, y;
            TreeNode root = fw.Queen ? genQueJumpTree(gb, fw) : genWalJumpTree(gb, fw);
            fw.Ranks = new List<int>();
            fw.Dests = new List<string>();
            fw.Overs = new List<string>();
            List<string> routes = root.allRoutes(root);

            foreach (string route in routes)
            {
                List<string> r = new List<string>(route.Split('-'));
                r.RemoveAll(c => c.Equals(""));
                int rank = 0;
                string dest = "";
                string over = "";
                for (int i = 0; i < r.Count; i += 2)
                {
                    x = Convert.ToInt32(r[i].Substring(1)) - 1;
                    y = Convert.ToInt32(r[i][0]) - (int)'A';
                    rank += gb.Board[x, y].Queen ? 3 : 2;
                    if (over == "")
                        over = r[i];
                    else over = over + "-" + r[i];

                    if (dest == "")
                        dest = r[i + 1];
                    else dest = dest + "-" + r[i + 1];
                }
                if (rank > 0 && dest != "" && over != "")
                {
                    fw.Ranks.Add(rank);
                    fw.Dests.Add(dest);
                    fw.Overs.Add(over);
                }
            }
            fw.Jump = fw.Ranks.Count > 0 ? true : false;
        }

        #endregion Obecné metody generující tahy

        #region Metody reprezentující tah, skok, povysení na královnu a uvolnení hracího pole

        public void makeMove(Fairway from, Fairway to, Gameboard gb, bool minimax)
        {
            if (!from.Moves.Contains(to.Name) && !minimax)
                throw new Exception("Neplatný tah");

            bool changePlayer = true;
            if (from.Jump)
            {
                gb.MovesWithoutJump = 0;
                Fairway over = gb.getFigureInBetween(from, to);

                bool bQ = from.Queen;
                makeJump(from, over, to);

                bool aQ = to.Queen;

                if (to.Jump && bQ == aQ)
                {
                    foreach (Fairway fw in gb.Board)
                    {
                        fw.Jump = false;
                        fw.StoneMove = false;
                        fw.Moves = new List<string>();
                        fw.Ranks = new List<int>();
                        fw.Dests = new List<string>();
                        fw.Overs = new List<string>();
                    }

                    generateMoves(gb, to);
                    gb.Winner = checkWinner(gb);
                    if (to.Jump)
                        changePlayer = false;
                    else
                        changePlayer = true;
                }

                if (bQ == aQ)
                {
                    generateMoves(gb, to);
                }
            }
            else
            {
                gb.MovesWithoutJump += 1;
                makeMove(from, to);
                setQueen(to);

                foreach (Fairway fw in gb.Board)
                {
                    fw.Jump = false;
                    fw.StoneMove = false;
                    fw.Moves = new List<string>();
                    fw.Ranks = new List<int>();
                    fw.Dests = new List<string>();
                    fw.Overs = new List<string>();
                }
            }

            if (changePlayer)
            {
                gb.PlayingWhite = !gb.PlayingWhite;
                generateMoves(gb);
                gb.Winner = checkWinner(gb);
            }
        }

        /// <summary>
        /// Funkce realizujici skok
        /// </summary>
        /// <param name="from">pole ze ktereho se tahne</param>
        /// <param name="over">pole pres ktere se tahne</param>
        /// <param name="to">pole kam se tahne</param>
        private void makeJump(Fairway from, Fairway over, Fairway to)
        {
            makeMove(from, to);
            setFree(over);
            setQueen(to);
        }

        /// <summary>
        /// Funkce reprezentujici provedeni tahu
        /// </summary>
        /// <param name="from">Instance tridy hraci pole - odkud se tahne</param>
        /// <param name="to">Instance tridy hraci pole - kam se tahne</param>
        private void makeMove(Fairway from, Fairway to)
        {
            to.Jump = from.Jump;
            to.Player = from.Player;
            to.Queen = from.Queen;
            to.Image = from.Image;
            to.Dests = new List<string>();
            to.Overs = new List<string>();
            from.Moves = new List<string>();
            to.Moves = new List<string>();

            setFree(from);
            setQueen(to);
        }

        /// <summary>
        /// Funkce uvolnujici hraci pole
        /// </summary>
        /// <param name="fw">Hraci pole, ktere ma byt uvolneno</param>
        private void setFree(Fairway fw)
        {
            fw.Dests = new List<string>();
            fw.Overs = new List<string>();
            fw.Player = status.free;
            fw.Queen = false;
            fw.Jump = false;
            fw.StoneMove = false;
            fw.Image = null;
        }

        /// <summary>
        /// Funkce nastavujici hraci figurku na kralovnu, pokud dosla na druhy konec hraci desky
        /// </summary>
        /// <param name="fw"></param>
        private void setQueen(Fairway fw)
        {
            // zjistime X-ovou souradnici pole
            int x = Convert.ToInt32(fw.Name.Substring(1)) - 1;

            // podle toho, o jakeho se jedna hrace
            switch (fw.Player)
            {
                case status.blackPlayer:
                    if (x == GlobalVariables.size - 1)
                    {
                        fw.Queen = true;
                        fw.Image = Properties.Resources.queen_black;
                    }
                    break;

                case status.whitePlayer:
                    if (x == 0)
                    {
                        fw.Queen = true;
                        fw.Image = Properties.Resources.queen_white;
                    }
                    break;
            }
        }

        #endregion Metody reprezentující tah, skok, povysení na královnu a uvolnení hracího pole

        #region Ostatní pomocné metody (kontrola vyhry, zahajeni hra,..)

        /// <summary>
        /// Funkce kontrolujici, zda uz byla hra dohrana
        /// </summary>
        /// <param name="gb">Instance tridy hraci deska</param>
        /// <returns>status.free pokud nikdo nevyhral, status.draw, pokud 60 tahu nedoslo ke skoku v ostatnich pripadech vraci status.[vyherce]</returns>
        public status checkWinner(Gameboard gb)
        {
            status result = status.free;
            // pokud se 60 tahu netahlo, remiza
            if (gb.MovesWithoutJump == GlobalVariables.draw)
                result = status.draw;
            // pokud pocet figur, se kterymi muze dany hrac tahnout = 0, vyhrava protihrac
            if (choosableFigures(gb).Count == 0)
            {
                if (gb.PlayingWhite)
                    result = status.blackPlayer;
                else
                    result = status.whitePlayer;
            }
            return result;
        }

        /// <summary>
        /// Funkce vracejici hracovi vybratelne figury
        /// </summary>
        /// <param name="gb">Instance hraci desky</param>
        /// <returns>pole hracich poli, jemiz je mozne tahnout</returns>
        public List<Fairway> choosableFigures(Gameboard gb)
        {
            List<Fairway> result = new List<Fairway>();
            status player = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;
            int rank = 0;

            // projdeme vsechny hraci policka
            foreach (Fairway fw in gb.Board)
            {
                // pokud se jedna o hracovu figurku
                if (fw.Player == player)
                {
                    // a pokud muze figurka skakat
                    if (fw.Jump)
                    {
                        // tak pro kazdy mozny skok
                        for (int i = 0; i < fw.Ranks.Count; i++)
                        {
                            // pokud je rank skoku vyssi, nez dosavadni rank, rank nastavime na novou hodnotu
                            // vymazeme dosavadni vysledek a pridame aktualni pole
                            if (fw.Ranks[i] > rank)
                            {
                                rank = fw.Ranks[i];
                                result = new List<Fairway>();
                                result.Add(fw);
                            }
                            // pokud je rank stejny, jak nynejsi hodnota, pridame pole mezi vysledky
                            else if (fw.Ranks[i] == rank)
                            {
                                result.Add(fw);
                            }
                        }
                    }
                    // pokud figurka nemuze skakat a rank je na hodnote 0 (neni znam zadny dosavadni skok)
                    // a figurka se muze hybat, pridame ji do vysledku
                    else if (rank == 0 && fw.StoneMove)
                    {
                        if (!result.Contains(fw))
                            result.Add(fw);
                    }
                }
            }
            // vratime vysledek
            return result;
        }

        public void startGame(Gameboard gb)
        {
            generateMoves(gb);
        }

        #endregion Ostatní pomocné metody (kontrola vyhry, zahajeni hra,..)
    }
}