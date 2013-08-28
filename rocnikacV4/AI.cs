#region

using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace rocnikacV4
{
    public class AI
    {
        #region Pomocné proměnné

        private readonly Random random = new Random();
        private readonly Rules rules = new Rules();

        #endregion Pomocné proměnné

        #region Minimax

        /// <summary>
        ///     Funkce minimaxu
        /// </summary>
        /// <param name="gb">instance tridy hraci desky</param>
        /// <param name="depth">hloubka minimaxu</param>
        /// <returns></returns>
        private double minimax(Gameboard gb, int depth)
        {
            // pokud je na tahu bily hrac
            if (gb.PlayingWhite)
            {
                // a vitez je nastaven na bileho hrace vrat MAX
                if (gb.Winner == status.whitePlayer)
                    return -GlobalVariables.MAX;
                // a vitez je nastaven na cerneho hrace vrat -MAX
                if (gb.Winner == status.blackPlayer)
                    return GlobalVariables.MAX;
                // a je remiza, vrat 0
                if (gb.Winner == status.draw)
                    return 0;
            }
                // pokud je na tahu cerny hrac
            else
            {
                // a je vitez nastaven na cerneho hrace, vrat MAX
                if (gb.Winner == status.blackPlayer)
                    return -GlobalVariables.MAX;
                // a je vitez nastaven na bileho hrace, vrat -MAX
                if (gb.Winner == status.whitePlayer)
                    return GlobalVariables.MAX;
                // a je remiza, vrat 0
                if (gb.Winner == status.draw)
                    return 0;
            }

            if (depth == 0)
                return evalFunction(gb);
            else
            {
                List<Fairway> moves = generateMoves(gb);
                double rank = -GlobalVariables.MAX;

                for (int i = 0; i < moves.Count; i += 2)
                {
                    // potomek <- zahraj(pozice, tah)
                    Gameboard potomek = gb.gameboardCopy();
                    rules.makeMove(moves[i], moves[i + 1], potomek, true);
                    rank = Math.Max(rank, -minimax(potomek, depth - 1));
                }
                return rank;
            }
        }

        /// <summary>
        ///     Fuknce generujici nejlepsi tah
        /// </summary>
        /// <param name="gb">instance tridy hraci deska</param>
        /// <param name="depth">hloubka minimaxu</param>
        /// <returns>Vraci list dvou hracich poli, kde na 1. miste je tah odkud a na 2. poli kam</returns>
        public List<Fairway> bestMove(Gameboard gb, int depth)
        {
            Gameboard potomek = gb.gameboardCopy();
            rules.generateMoves(potomek);

            List<Fairway> moves = generateMoves(potomek);
            var bestMove = new List<Fairway>();
            double bestRank = -GlobalVariables.MAX;

            for (int i = 0; i < moves.Count; i += 2)
            {
                // potomek <- zahraj(pozice, tah)
                rules.makeMove(moves[i], moves[i + 1], potomek, true);
                double rank;

                if (depth == 0)
                    rank = evalFunction(potomek);
                else
                    rank = -minimax(potomek, depth - 1);

                if (rank > bestRank)
                {
                    bestMove = new List<Fairway>();
                    bestRank = rank;
                    bestMove.Add(moves[i]);
                    bestMove.Add(moves[i + 1]);
                }
                if (rank == bestRank)
                {
                    bestMove.Add(moves[i]);
                    bestMove.Add(moves[i + 1]);
                }
            }
            return bestMove;
        }

        /// <summary>
        ///     Ohodnocovaci funkce minimaxu
        /// </summary>
        /// <param name="gb">instance tridy hraci deska</param>
        /// <returns>hodnotu typu double predstavujici hodnotu pozice</returns>
        private double evalFunction(Gameboard gb)
        {
            int smallRand = random.Next(-1, 1);
            int mediumRand = random.Next(-2, 2);
            int largeRand = random.Next(-3, 3);
            int xLargeRand = random.Next(-4, 4);

            double result = 0;
            status playerOnMove = gb.PlayingWhite ? status.whitePlayer : status.blackPlayer;

            foreach (var fw in gb.Board)
            {
                string name = fw.Name;
                int x = Convert.ToInt32(name.Substring(1));
                char y = name[0];

                if (fw.Player != status.free)
                {
                    // pozice u kraje desky - vodorovne
                    if (x == 1 || x == GlobalVariables.size)
                    {
                        result += fw.Player == playerOnMove ? -5 : 5;
                        result += smallRand;
                    }
                    // pozice u kraje desky - svisle
                    if (y == 'A' || y == (char) ('A' + GlobalVariables.size - 1))
                    {
                        result += fw.Player == playerOnMove ? -15 : 15;
                        result += largeRand;
                    }
                    // pokud je figurka kralovna
                    if (fw.Queen)
                    {
                        result += fw.Player == playerOnMove ? -20 : 20;
                        result += xLargeRand;
                    }
                        // pokud jde o obycejny kamen
                    else
                    {
                        result += fw.Player == playerOnMove ? -5 : 5;
                        result += smallRand;
                    }

                    //pokud kamen muze skakat
                    if (fw.Jump)
                    {
                        foreach (var jump in fw.Moves)
                        {
                            result += fw.Player == playerOnMove ? -10 : 10;
                            result += mediumRand;
                        }
                    }
                    else if (fw.StoneMove)
                    {
                        foreach (var move in fw.Moves)
                            result += fw.Player == playerOnMove ? -2.5 : 2.5;
                    }

                        // pokud je kamen zablokovany
                    else
                    {
                        result += fw.Player == playerOnMove ? 15 : -15;
                        result += largeRand;
                    }
                }
            }
            return result;
        }

        #endregion Minimax

        #region Pomocné metody minimaxu

        /// <summary>
        ///     Funkce generujici veskere mozne tahy na hraci desce
        /// </summary>
        /// <param name="gb">Instance tridy hraci deska</param>
        /// <returns>Vraci pole hracich poli, kde na lichem miste je ulozeno pole odkud bylo tazeno a na sudem miste pole, kam bylo tazeno</returns>
        private List<Fairway> generateMoves(Gameboard gb)
        {
            List<Fairway> movable = rules.choosableFigures(gb);
            var moves = new List<Fairway>();

            foreach (var fw in movable)
            {
                List<string> dests = gb.possibleMoves(fw);

                foreach (var destination in dests)
                {
                    moves.Add(fw);
                    moves.Add(gb.getFigure(destination));
                }
            }
            return moves;
        }

        /// <summary>
        ///     Funkce generujici pole hracich poli, na ktere jde tahnout
        /// </summary>
        /// <param name="gb">Instrance tridy hraci deska</param>
        /// <param name="fw">Instance tridy hraci pole</param>
        /// <returns>Vraci List hracich poli, na ktere je mozno figurkou tahnout</returns>
        private List<Fairway> generateDests(Gameboard gb, Fairway fw)
        {
            var dest = new List<Fairway>();
            var temp = new List<String>();

            if (fw.Jump)
            {
                for (int i = 0; i < fw.Dests.Count; i++)
                {
                    foreach (var jump in fw.Dests)
                    {
                        string[] jumps = jump.Split('-');
                        temp.Add(jumps[0]);
                    }
                }
            }
            else temp = fw.Moves;

            foreach (var fair in gb.Board)
            {
                if (temp.Contains(fair.Name))
                    dest.Add(fair);
            }
            return dest;
        }

        #endregion Pomocné metody minimaxu

        #region Random AI

        private Fairway randomFrom(Gameboard gb)
        {
            rules.generateMoves(gb);
            List<Fairway> stones = rules.choosableFigures(gb);
            int position = random.Next(0, stones.Count);

            return stones[position];
        }

        private Fairway randomTo(Gameboard gb, Fairway fw)
        {
            List<string> destinations = gb.possibleMoves(fw);
            int position = random.Next(0, destinations.Count);
            string name = destinations[position];

            return gb.getFigure(name);
        }

        private void randomAI(Gameboard gb)
        {
            Fairway from = randomFrom(gb);
            Fairway to = randomTo(gb, from);

            gb.addHistory(from, to);

            rules.makeMove(from, to, gb, true);
        }

        #endregion Random AI

        #region Správa AI

        private Gameboard aiSwitcher(Gameboard gb, int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    randomAI(gb);
                    break;

                case 2:
                    gb = MiniMaxAI(gb, 2);
                    break;

                case 3:
                    gb = MiniMaxAI(gb, 3);
                    break;
            }
            return gb;
        }

        public Gameboard playAI(Gameboard gb, BackgroundWorker worker)
        {
            if (gb.PlayingWhite)
                gb = aiSwitcher(gb, gb.WhitePlayer);
            else
                gb = aiSwitcher(gb, gb.BlackPlayer);

            return gb;
        }

        private Gameboard MiniMaxAI(Gameboard gb, int depth)
        {
            List<Fairway> bMove = bestMove(gb, depth);
            string f = bMove[0].Name;
            string t = bMove[1].Name;

            var from = new Fairway();
            var to = new Fairway();

            foreach (var fw in gb.Board)
            {
                from = fw.Name == f ? fw : from;
                to = fw.Name == t ? fw : to;
            }

            rules.generateMoves(gb, from);

            gb.addHistory(from, to);
            rules.makeMove(from, to, gb, true);

            return gb;
        }

        #endregion Správa AI
    }
}