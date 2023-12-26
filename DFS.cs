public class DFS {
    public static void Search(RubiksCube rubiks, List<int> minMoves, HashSet<int> visited, List<int> moves) 
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
}