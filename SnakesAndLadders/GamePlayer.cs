namespace SnakesAndLadders
{
    internal class GamePlayer
    {
        private readonly int _startPosition;
        private readonly int _winPosition;

        public GamePlayer(int startPosition, int winPosition)
        {
            _startPosition = startPosition;
            _winPosition = winPosition;
        }

        public int Position { get; private set; }

        public bool HasWon()
        {
            return Position == _winPosition;
        }

        public void MoveToStart()
        {
            MoveTo(_startPosition);
        }

        public void MoveTo(int newPosition)
        {
            Position = newPosition;
        }
    }
}