using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rocnikacV4
{
    public class Move
    {
        string _from;
        string _to;
        string _player;

        public Move(string from, string to, string player)
        {
            this._from = from;
            this._to = to;
            this._player = player;
        }

        public string From { get { return _from; }}
        public string To { get { return _to; } }
        public string Player { get { return _player; } }
    }
}
