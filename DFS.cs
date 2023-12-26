public class DFS {
    public static void Search(RubiksCube rubiks) 
        {
            int depth = 25; // TODO: get DB-backed depth;
            HashSet<int> visited = new();
            for (int i = 0; i < depth; i++)
            {
                var (found, remaining) = DLS(rubiks, depth, visited);
                if (found != null)
                    // return found;
                    return;
                else if (!remaining)
                    return ;
            }
        }

        private static (RubiksCube, bool) DLS(RubiksCube rubiks, int depth, HashSet<int> visited) {
            if (depth == 0)
            {
                if (rubiks.IsSolved())
                {
                    rubiks.PrintMoveSet();
                    return (rubiks, true);
                } else
                {
                    return (null, true);
                }
            } else {
                bool anyRemaining = false;
                // TODO: implement A*
                // foreach (var nextRubiks in rubiks.AStar())
                for (int i = 0; i < 12; i++)
                {
                    var child = new RubiksCube(rubiks);
                    child.Move(i);
                    if (!visited.Add(child.GetHash()))
                        continue;
                    var (found, remaining) = DLS(child, depth - 1, visited);
                    if (found != null)
                    {
                        return (found, true);
                    }
                    if (remaining == true) {
                        anyRemaining = true;
                    }
                }

                return (null, anyRemaining);
            }
        }
}