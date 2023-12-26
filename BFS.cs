public class BFS {
    public static void Search(RubiksCube rubiks)
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
            for (int i = 0; i < RubiksCube.COMPLETE_MOVE_SET; i++)
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
                if (!hashes.Add(hashCode))
                {
                    continue;
                }
                int currCrosses = rubiks.CalculateCrosses();
                int currSolved = rubiks.CalculateSolved();
                if (prevCrosses < RubiksCube.MAX_CROSSES_COUNT && prevCrosses < currCrosses)
                {
                    Console.WriteLine($"Found crossed solution!");
                    nextQ.Add(nextCube);
                    prevCrosses = currCrosses;
                    prevSolved = currSolved;
                    currQ = new();
                } else if (prevCrosses == RubiksCube.MAX_CROSSES_COUNT &&  prevSolved < currSolved)
                {
                    Console.WriteLine($"Found an improved solution!");
                    nextQ.Add(nextCube);
                    prevSolved = currSolved;
                    currQ = new();
                } else
                {
                    currQ.Enqueue(nextCube);
                }
            }
            if (currQ.Count == 0)
            {
                currQ = new Queue<RubiksCube>(nextQ);
                nextQ = new();
                prevSteps = cube.steps;
                Console.WriteLine($"step: {prevSteps}. currQ.Count: {currQ.Count}");
            }
        }
        Console.WriteLine($"no solution found after {prevSteps} steps");
    }    
}