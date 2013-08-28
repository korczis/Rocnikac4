using System.Collections.Generic;
using System.Windows.Forms;

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
        private status player;          // slot predstavujici, zda se jedna o cerneho/bileho hrace nebo volne pole

        private bool queen;             // slot jedna se o kralovnu
        private bool jump;              // slot muze figurka skakat
        private bool stoneMove;         // slot muze se figurka hybat
        private List<string> moves;     // slot seznam moznych tahu
        private List<string> dests;
        private List<string> overs;
        private List<int> ranks;

        // getry a setry
        public status Player
        {
            get { return player; }
            set { player = value; }
        }

        public bool Queen
        {
            get { return queen; }
            set { queen = value; }
        }

        public bool Jump
        {
            get { return jump; }
            set { jump = value; }
        }

        public bool StoneMove
        {
            get { return stoneMove; }
            set { stoneMove = value; }
        }

        public List<string> Dests
        {
            get { return this.dests; }
            set { this.dests = value; }
        }

        public List<string> Overs
        {
            get { return this.overs; }
            set { this.overs = value; }
        }

        public List<int> Ranks
        {
            get { return this.ranks; }
            set { this.ranks = value; }
        }

        public List<string> Moves
        {
            get { return moves; }
            set { moves = value; }
        }

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

        public Fairway Clone()
        {
            Fairway result = new Fairway();

            result.player = this.Player;
            result.Queen = this.Queen;
            result.Jump = this.Jump;
            result.StoneMove = this.StoneMove;
            result.Moves = this.Moves;
            result.Name = this.Name;
            result.Dests = this.Dests;
            result.Overs = this.Overs;
            result.Ranks = this.Ranks;

            return result;
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
    }
}