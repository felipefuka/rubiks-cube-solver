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
        rubiks.PrintStatus();
        
        BFS(rubiks);
        // List<int> minMoves = new List<int>();
        // DFS(rubiks, minMoves, new HashSet<int>(), new List<int>());

    }

    private static void DFS(RubiksCube rubiks, List<int> minMoves, HashSet<int> visited, List<int> moves) 
    {
        if (rubiks.IsSolved())
        {
            Console.WriteLine($"rubiks.IsSolved(): {string.Join(',', moves.Select(x => rubiks.moveDictionary[x]))}");
            if (minMoves.Count == 0 || moves.Count < minMoves.Count)
            {
                minMoves = new List<int>(moves);
            }
            return;
        } else if (moves.Count > 30)
        {
            return;
        }

        for (int i = 0; i < 12; i++)
        {
            moves.Add(i);
            rubiks.Move(i);
            if (visited.Add(rubiks.GetHash()))
            {
                DFS(rubiks, minMoves, visited, moves);
            }
            if (i % 2 == 0)
            {
                rubiks.Move(i + 1);
            } else
            {
                rubiks.Move(i - 1);
            }
            rubiks.RemoveLastMove();
            moves.RemoveAt(moves.Count - 1);
        }
    }

    private static void BFS(RubiksCube rubiks)
    {
        // BFS
        var currQ = new Queue<RubiksCube>();
        var nextQ = new List<RubiksCube>();
        var hashes = new HashSet<int>
        {
            rubiks.GetHash()
        };
        currQ.Enqueue(rubiks);
        int prevSteps = 0;
        int prevSolved = rubiks.CalculateSolved();
        int prevCrosses = rubiks.CalculateSolved();
        int currSteps = 0;
        while (currQ.Count > 0)
        {
            var cube = currQ.Dequeue();
            if (currSteps < cube.steps)
            {
                Console.WriteLine($"currSteps: {currSteps}");
                currSteps = cube.steps;
            }
            for (int i = 0; i < 18; i++)
            {
                var nextCube = new RubiksCube(cube);
                nextCube.Move(i);
                if (nextCube.IsSolved())
                {
                    Console.WriteLine($"Found a solution!");
                    nextCube.PrintMoveSet();
                    return;
                }
                // nextCube.PrintMoveSet();
                // nextCube.PrintStatus();
                var hashCode = nextCube.GetHash();
                if (!hashes.Add(nextCube.GetHash()))
                {
                    continue;
                }
                int currCrosses = rubiks.CalculateCrosses();
                int currSolved = rubiks.CalculateSolved();
                if (prevCrosses < 6 * 4 && prevCrosses < currCrosses)
                {
                    nextQ.Add(nextCube);
                    prevCrosses = currCrosses;
                    prevSolved = currSolved;
                    currQ = new();
                    Console.WriteLine($"Found crossed solution!");
                } else if (prevCrosses == 6 * 4 &&  prevSolved < currSolved)
                {
                    nextQ.Add(nextCube);
                    prevSolved = currSolved;
                } else
                {
                    currQ.Enqueue(nextCube);
                }
            }
            if (currQ.Count == 0)
            {
                currQ = new Queue<RubiksCube>(nextQ.OrderByDescending(x => x.CalculateCrosses()).ThenByDescending(x => x.CalculateSolved()));
                nextQ = new();
                prevSteps = cube.steps;
                Console.WriteLine($"step: {prevSteps}. currQ.Count: {currQ.Count}");
            }
        }
        Console.WriteLine($"no solution found after {prevSteps} steps");
    }
}