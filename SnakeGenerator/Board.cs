using System;

namespace SnakeGenerator
{
    public class Board
    {
        private const int MaxSnakeLength = 9;
        private readonly Random _rand;
        private byte[] _board;
        private int _applePosition;
        private byte _appleColour;
        private int _snakeHeadPosition;
        private int _snakeLength;
        private byte _snakeColour;

        public Board()
        {
            _rand = new Random();

            ClearBoard();
            
            InitialiseSnake();
            InitialiseApple();
            
            RenderSnake();
            RenderApple();
        }

        public byte[] Frame => _board;

        public void GenerateNextFrame()
        {
            ClearBoard();
            MoveSnake();
            
            if (CollisionDetect())
            {
                GrowSnake();
                InitialiseApple();

                if (SnakeOverLength()) ShedSnake();
            }
            
            RenderSnake();
            RenderApple();
        }

        private void ShedSnake()
        {
            ++_snakeColour;
            if (_snakeColour > 7) _snakeColour = 1;
            _snakeLength = 1;
        }

        private bool SnakeOverLength() => _snakeLength > MaxSnakeLength;

        private bool CollisionDetect() => _snakeHeadPosition == _applePosition;

        private void InitialiseSnake()
        {
            _snakeColour = 2; // Red snake
            _snakeHeadPosition = FindRandomSpaceWithNoSnake();
            _snakeLength = 1;
        }

        private void MoveSnake()
        {
            ++_snakeHeadPosition;
            _snakeHeadPosition %= _board.Length;
        }

        private void GrowSnake() => ++_snakeLength;

        private void InitialiseApple()
        {
            _appleColour = 1; // Green Apple
            _applePosition = FindRandomSpaceWithNoSnake();
        }

        private void ClearBoard() => _board = new byte[34];

        private void RenderSnake()
        {
            for (var i = 0; i < _snakeLength; ++i)
            {
                var position = (_board.Length + _snakeHeadPosition - i) % _board.Length;
                _board[position] = _snakeColour;
            }
        }

        private void RenderApple() => _board[_applePosition] = _appleColour;

        private int FindRandomSpaceWithNoSnake()
        {
            var searchSpace = _board.Length - _snakeLength;
            var offset = _rand.Next(searchSpace);
            var position = (_snakeHeadPosition + offset) % _board.Length;
            return position;
        }
    }
}