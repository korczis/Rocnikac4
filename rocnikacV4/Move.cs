namespace rocnikacV4
{
    public class Move
    {
        private readonly string _from;
        private readonly string _player;
        private readonly string _to;

        public Move(string from, string to, string player)
        {
            _from = from;
            _to = to;
            _player = player;
        }

        public string From
        {
            get { return _from; }
        }

        public string To
        {
            get { return _to; }
        }

        public string Player
        {
            get { return _player; }
        }
    }
}