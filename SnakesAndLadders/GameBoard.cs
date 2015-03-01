using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadders
{
    internal class GameBoard
    {
        private const int StartPosition = 1;
        private const int WinPosition = 100;
        private readonly List<GamePlayer> _players = new List<GamePlayer>();
        private readonly List<Tuple<int, int, FeatureType>> _features = new List<Tuple<int, int, FeatureType>>();
        private readonly Func<int> _rollFunc;
        private int _currentPlayer;

        private enum FeatureType
        {
            Ladder,
            Snake
        }

        public GameBoard(Func<int> rollFunc)
        {
            _rollFunc = rollFunc;
        }

        public GameState State { get; private set; }

        public void AddPlayer()
        {
            _players.Add(new GamePlayer(StartPosition, WinPosition));
        }

        public GamePlayer GetPlayer(int index)
        {
            return _players[index];
        }

        public void AddLadder(int start, int end)
        {
            AddFeature(start, end, FeatureType.Ladder);
        }

        public void AddSnake(int start, int end)
        {
            AddFeature(start, end, FeatureType.Snake);
        }

        private void AddFeature(int start, int end, FeatureType featureType)
        {
            _features.Add(new Tuple<int, int, FeatureType>(start, end, featureType));
        }

        public void StartGame()
        {
            _players.ForEach(p => p.MoveToStart());
            _currentPlayer = 0;
            State = GameState.Playing;
        }

        public int NextPlayerTurn()
        {
            var roll = _rollFunc.Invoke();
            var player = GetPlayer(_currentPlayer);
            MovePlayer(player, roll);

            if (player.HasWon())
            {
                State = GameState.Finished;
            }

            _currentPlayer = ++_currentPlayer%_players.Count;
            return roll;
        }

        private void MovePlayer(GamePlayer player, int roll)
        {
            var newPosition = player.Position + roll;

            var feature = _features.FirstOrDefault(f => f.Item1 == newPosition);
            if (feature != null)
            {
                newPosition = feature.Item2;
            }

            if (newPosition <= WinPosition)
            {
                player.MoveTo(newPosition);
            }
        }
    }
}