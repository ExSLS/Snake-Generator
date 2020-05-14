using System.IO;
using System.Text;

namespace SnakeGenerator
{
    public class Program
    {
        private static StreamWriter _file;

        static void Main(string[] args)
        {
            var numSteps = 10000; // TODO: Read num steps from input
            _file = File.CreateText("snake.txt");
            
            var board = new Board();
            for (var i = 0; i < numSteps; ++i)
            {
                OutputState(board.Frame);
                board.GenerateNextFrame();
            }
        }

        private static void OutputState(byte[] boardFrame)
        {
            var line = GenerateLine(boardFrame);
            _file.WriteLine(line);
        }

        private static string GenerateLine(byte[] boardFrame)
        {
            var sb = new StringBuilder();
            foreach (var colour in boardFrame) sb.Append(colour.ToString());
            sb.Append(100); // TODO: Make time configurable
            return sb.ToString();
        }
    }
}