using System;

namespace SnakesAndLadders
{
    public class GameDie
    {
        private readonly int _sides;
        private readonly Random _random;

        public GameDie(int sides)
        {
            _sides = sides;
            _random = new Random((int) DateTime.Now.Ticks);
        }

        public int Roll()
        {
            return _random.Next(1, _sides + 1);
        }
    }
}