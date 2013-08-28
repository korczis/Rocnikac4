using System;
using System.Collections.Generic;
using System.Xml;

namespace rocnikacV4
{
    public class SaveLoad
    {
        #region Konstruktor

        public SaveLoad()
        {
        }

        #endregion Konstruktor

        #region Pomocné funkce realizující zápis do XML soubor

        /// <summary>
        /// Funkce pridavajici tah jako xml element
        /// </summary>
        /// <param name="writer">XML writer</param>
        /// <param name="move">Instance tridy Move reprezentujici tah</param>
        private void addMove(XmlWriter writer, Move move)
        {
            writer.WriteStartElement("MOVE");
            writer.WriteElementString("FROM", move.From);
            writer.WriteElementString("TO", move.To);
            writer.WriteElementString("PLAYER", move.Player);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Funkce pridavajici do xml souboru element HRACI
        /// </summary>
        /// <param name="writer">XML writer</param>
        /// <param name="whitePlayer">Obtiznost bileho hrace</param>
        /// <param name="blackPlayer">Obtiznost cerneho hrace</param>
        private void addPlayers(XmlWriter writer, int whitePlayer, int blackPlayer)
        {
            writer.WriteStartElement("PLAYERS");
            writer.WriteElementString("WHITE", whitePlayer.ToString());
            writer.WriteElementString("BLACK", blackPlayer.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// Funkce pridavajici do xml souboru parametry hraci desky
        /// </summary>
        /// <param name="writer">XML writer</param>
        /// <param name="startsWhite">Zacinajici hrac == bily hrac</param>
        /// <param name="showHelp">Zobrayovani napovedy</param>
        private void addBoardSettings(XmlWriter writer, bool startsWhite, bool showHelp)
        {
            writer.WriteStartElement("BOARD");
            writer.WriteElementString("STARTS", startsWhite.ToString());
            writer.WriteElementString("SHOWHELP", showHelp.ToString());
            writer.WriteEndElement();
        }

        #endregion Pomocné funkce realizující zápis do XML soubor

        #region Uložení hry

        /// <summary>
        /// Funkce ukladajici rozehratou / dokoncenou hru jako xml soubor
        /// </summary>
        /// <param name="history">List tahu</param>
        /// <param name="whitePlayer">Obtiznost bileho hrace</param>
        /// <param name="blackPlayer">Obtiznost cerneho hrace</param>
        /// <param name="startsWhite">Zacinajici bily hrac</param>
        /// <param name="showHelp">Zobrazeni napovedy</param>
        /// <param name="fileName">Soubor, do ktereho ma byt zapsano</param>
        public void saveGame(List<Move> history, int whitePlayer, int blackPlayer, bool startsWhite, bool showHelp, string fileName)
        {
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create(fileName, setting))
            {
                writer.WriteStartDocument();

                // root element
                writer.WriteStartElement("GAME");

                // ulozime nastaveni hracu
                addPlayers(writer, whitePlayer, blackPlayer);

                // ulozime nastaveni hraci desky
                addBoardSettings(writer, startsWhite, showHelp);

                // ulozime historii / tahy
                writer.WriteStartElement("HISTORY");

                // pridame vsechny prvky historie
                foreach (Move move in history)
                    addMove(writer, move);

                // uzavreme tag historie
                writer.WriteEndElement();

                //uzavreme tag game
                writer.WriteEndElement();

                // uzavreme xml
                writer.WriteEndDocument();
            }
        }

        #endregion Uložení hry

        #region Načtení hry

        /// <summary>
        /// Funkce provadejici nacteni hry a zkontrolovani, zda se jedna o korektni ulozeni
        /// </summary>
        /// <param name="filePath">cesta k souboru</param>
        /// <param name="gb">Hraci deska, na niz maji byt zmeny provedeny</param>
        /// <returns>Hraci desku s odehratou hrou</returns>
        public Gameboard loadGame(string filePath, Gameboard gb)
        {
            string from = "";
            string to = "";
            Fairway fwFrom;
            Fairway fwTo;
            Rules rules = new Rules();

            gb.Board = gb.newBoard();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            // nacteme si obtiznost bileho hrace a nastavime
                            case "WHITE":
                                if (reader.Read())
                                {
                                    int whitePlayer = Convert.ToInt32(reader.Value.Trim());
                                    gb.WhitePlayer = whitePlayer;
                                }
                                break;
                            // nacteme si obtiznost cerneho hrace a nastavime
                            case "BLACK":
                                if (reader.Read())
                                {
                                    int blackPlayer = Convert.ToInt32(reader.Value.Trim());
                                    gb.BlackPlayer = blackPlayer;
                                }
                                break;

                            // nacteme si zacinajiciho hrace a nastavime
                            case "STARTS":
                                if (reader.Read())
                                {
                                    bool startsWhite = Convert.ToBoolean(reader.Value.Trim());
                                    gb.StartsWhite = startsWhite;
                                }
                                break;
                            //
                            case "SHOWHELP":
                                if (reader.Read())
                                {
                                    bool showHelp = Convert.ToBoolean(reader.Value.Trim());
                                    gb.ShowMoveHelp = showHelp;
                                }
                                break;

                            case "FROM":
                                if (reader.Read())
                                    from = (string)reader.Value.Trim();
                                break;

                            case "TO":
                                if (reader.Read())
                                    to = (string)reader.Value.Trim();
                                fwFrom = gb.getFigure(from);
                                fwTo = gb.getFigure(to);
                                rules.generateMoves(gb);
                                rules.makeMove(fwFrom, fwTo, gb, false);
                                gb.addHistory(fwFrom, fwTo);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return gb;
        }

        #endregion Načtení hry
    }
}