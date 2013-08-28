#region

using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace rocnikacV4
{
    public enum status
    {
        whitePlayer = -1,
        free,
        blackPlayer,
        draw
    };

    public class Fairway : PictureBox
    {
        // sloty
        private status player; // slot predstavujici, zda se jedna o cerneho/bileho hrace nebo volne pole

        public Fairway()
        {
            Queen = false;
            Jump = false;
            StoneMove = false;
            Moves = new List<string>();
            Dests = new List<string>();
            Overs = new List<string>();
            Ranks = new List<int>();
        }

        public Fairway(status player)
        {
            Player = player;
            Queen = false;
            Jump = false;
            StoneMove = false;
            Moves = new List<string>();
            Dests = new List<string>();
            Overs = new List<string>();
        }

        // getry a setry
        public status Player
        {
            get { return player; }
            set { player = value; }
        }

        public bool Queen { get; set; }

        public bool Jump { get; set; }

        public bool StoneMove { get; set; }

        public List<string> Dests { get; set; }

        public List<string> Overs { get; set; }

        public List<int> Ranks { get; set; }

        public List<string> Moves { get; set; }

        public Fairway Clone()
        {
            var result = new Fairway();

            result.player = Player;
            result.Queen = Queen;
            result.Jump = Jump;
            result.StoneMove = StoneMove;
            result.Moves = Moves;
            result.Name = Name;
            result.Dests = Dests;
            result.Overs = Overs;
            result.Ranks = Ranks;

            return result;
        }
    }
}