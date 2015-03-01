using System.Collections.Generic;
using NUnit.Framework;

namespace SnakesAndLadders
{
    public class GivenAStartedGameWithOnePlayer
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 1);
            _board.AddPlayer();
            _board.StartGame();
        }

        [Test]
        public void ThenThePlayerTokenIsInPositionOne()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(1));
        }
    }
    
    public class GivenThePlayerIsOnSquareOne
    {
        private GameBoard _board;

        [SetUp]
        public void WhenTheyMoveThreeSpaces()
        {
            _board = new GameBoard(() => 3);
            _board.AddPlayer();
            _board.StartGame();

            Assert.That(_board.NextPlayerTurn(), Is.EqualTo(3));
        }

        [Test]
        public void ThenTheyShouldBeOnSquareFour()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(4));
        }
    }

    public class GivenThePlayerIsOnSquareOneWhenRollingADie
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 4);
            _board.AddPlayer();
            _board.StartGame();

            _board.NextPlayerTurn();
        }

        [Test]
        public void ThenTheyShouldBeOnSquareFive()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(5));
        }
    }

    public class GivenThePlayerIsOnSquareOneWhenMovedTwice
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            var rolls = new Queue<int>(new[] {4, 5});
            _board = new GameBoard(rolls.Dequeue);
            _board.AddPlayer();
            _board.StartGame();

            Assert.That(_board.NextPlayerTurn(), Is.EqualTo(4));
            Assert.That(_board.NextPlayerTurn(), Is.EqualTo(5));
        }

        [Test]
        public void ThenTheyShouldBeOnSquareFour()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(10));
        }
    }

    public class GivenThePlayerLandsOnALadderBeginningAtTwoEndingAtTwelve
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 1);
            _board.AddPlayer();
            _board.AddLadder(2, 12);
            _board.StartGame();

            _board.NextPlayerTurn();
        }

        [Test]
        public void ThenTheyShouldBeOnSquareTwelve()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(12));
        }
    }

    public class GivenThePlayerLandsOnASnakeBeginningAt8EndingAtThree
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 4);
            _board.AddPlayer();
            _board.AddSnake(8, 3);
            _board.StartGame();
            _board.GetPlayer(0).MoveTo(4);

            _board.NextPlayerTurn();
        }

        [Test]
        public void ThenTheyShouldBeOnSquareThree()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(3));
        }
    }

    public class GivenThePlayerIsOnSquare97
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 3);
            _board.AddPlayer();
            _board.StartGame();
            _board.GetPlayer(0).MoveTo(97);

            _board.NextPlayerTurn();
        }

        [Test]
        public void ThenTheyShouldBeOnSquare100()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(100));
        }

        [Test]
        public void ThenTheGameIsOver()
        {
            Assert.That(_board.State, Is.EqualTo(GameState.Finished));
        }

        [Test]
        public void ThenThePlayerHasWon()
        {
            Assert.That(_board.GetPlayer(0).HasWon(), Is.True);
        }
    }

    public class GivenThePlayerIsOnSquare97AndOverRolls
    {
        private GameBoard _board;

        [SetUp]
        public void SetUp()
        {
            _board = new GameBoard(() => 4);
            _board.AddPlayer();
            _board.StartGame();

            _board.GetPlayer(0).MoveTo(97);
            _board.NextPlayerTurn();
        }

        [Test]
        public void ThenTheyShouldBeOnSquareNinetySeven()
        {
            Assert.That(_board.GetPlayer(0).Position, Is.EqualTo(97));
        }

        [Test]
        public void ThenTheGameIsNotOver()
        {
            Assert.That(_board.State, Is.EqualTo(GameState.Playing));
        }
    }
}
