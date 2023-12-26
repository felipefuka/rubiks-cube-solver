using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        // rubiks cube solver
        var rubiks = new RubiksCube();
        
        rubiks.top = new RubiksCube.Face(new char[][]{
            new char[]{ 'W', 'W', 'W' },
            new char[]{ 'W', 'W', 'W' },
            new char[]{ 'W', 'W', 'W' }
        });
        rubiks.left = new RubiksCube.Face(new char[][]{
            new char[]{ 'O', 'O', 'O' },
            new char[]{ 'O', 'O', 'Y' },
            new char[]{ 'B', 'B', 'Y' }
        });
        rubiks.front = new RubiksCube.Face(new char[][]{
            new char[]{ 'G', 'G', 'G' },
            new char[]{ 'R', 'G', 'B' },
            new char[]{ 'R', 'Y', 'Y' }
        });
        rubiks.right = new RubiksCube.Face(new char[][]{
            new char[]{ 'R', 'R', 'R' },
            new char[]{ 'R', 'R', 'G' },
            new char[]{ 'G', 'Y', 'O' }
        });
        rubiks.back = new RubiksCube.Face(new char[][]{
            new char[]{ 'B', 'B', 'B' },
            new char[]{ 'R', 'B', 'G' },
            new char[]{ 'G', 'Y', 'Y' }
        });
        rubiks.bottom = new RubiksCube.Face(new char[][]{
            new char[]{ 'B', 'G', 'R' },
            new char[]{ 'O', 'Y', 'B' },
            new char[]{ 'O', 'O', 'Y' }
        });

        if (!rubiks.IsValid())
            Console.WriteLine($"invalid cube!");

        // check if its solved
        // var result = rubiks.IsSolved();
        // Console.WriteLine($"rubiks.IsSolved() == {result}");

        // BFS.Search(rubiks);
        // List<int> minMoves = new List<int>();
        // DFS.Search(rubiks, minMoves, new HashSet<int>(), new List<int>());
    }

    
}