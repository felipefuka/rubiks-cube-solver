using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

internal class Program
{
    public class RubiksCube {

        public int size = 3;

        public List<int> moves;
        public class Face {
            public char[][] face;
            public Face(char[][] cube) 
            {
                face = cube;
            }

            public Face(Face face) 
            {
                this.face = new char[face.face.Length][];
                for (int i = 0; i < face.face.Length; i++)
                {
                    this.face[i] = new char[face.face[i].Length];
                    Array.Copy(face.face[i], this.face[i], face.face[i].Length);
                }
            }

            public bool IsSolved()
            {
                char baseValue = face[0][0];
                for (int i = 0; i < face.Length; i++)
                {
                    for (int j = 0; j < face[i].Length; j++)
                    {
                        if (baseValue != face[i][j])
                            return false;
                    }
                }

                return true;
            }
        }

        public Face front;
        public Face back;
        public Face left;
        public Face top;
        public Face right;
        public Face bottom;

        public RubiksCube(int size = 3)
        {
            moves = new List<int>();
        }

        public RubiksCube(RubiksCube original, int size = 3)
        {
            top = new Face(original.top);
            left = new Face(original.left);
            front = new Face(original.front);
            right = new Face(original.right);
            back = new Face(original.back);
            bottom = new Face(original.bottom);
            moves = new List<int>(original.moves);
        }

        string[] moveDictionary = new string[]{
            "MoveTopLeft", "MoveTopRight", "MoveBottomLeft", "MoveBottomRight", 
            "MoveLeftUp",  "MoveLeftDown", "MoveRightUp", "MoveRightDown", 
            "TwistFrontLeft", "TwistFrontRight", "TwistBackLeft", "TwistBackRight"
        };

        public void Move(int move)
        {
            moves.Add(move);
            if (move < 0 || 11 < move)
                return;

            switch (move)
            {
                case 0:
                    MoveTopLeft();
                    break;
                case 1:
                    MoveTopRight();
                    break;
                case 2:
                    MoveBottomLeft();
                    break;
                case 3:
                    MoveBottomRight();
                    break;
                case 4:
                    MoveLeftUp();
                    break;
                case 5:
                    MoveLeftDown();
                    break;
                case 6:
                    MoveRightUp();
                    break;
                case 7:
                    MoveRightDown();
                    break;
                case 8:
                    TwistFrontLeft();
                    break;
                case 9:
                    TwistFrontRight();
                    break;
                case 10:
                    TwistBackLeft();
                    break;
                case 11:
                    TwistBackRight();
                    break;
            }
        }

        public void MoveTopLeft()
        {
            MoveLeft(0);
        }

        public void MoveBottomLeft()
        {
            MoveLeft(size - 1);
        }

        public void MoveTopRight()
        {
            MoveRight(0);
        }

        public void MoveBottomRight()
        {
            MoveRight(size - 1);
        }
        public void MoveLeftUp()
        {
            MoveUp(0);
        }

        public void MoveLeftDown()
        {
            MoveDown(0);
        }

        public void MoveRightUp()
        {
            MoveUp(size - 1);
        }

        public void MoveRightDown()
        {
            MoveDown(size - 1);
        }

        public void MoveUp(int col)
        {
            if (col < 0 || size <= col)
                return;

            for (int i = 0; i < size; i++)
            {
                char tmp = front.face[i][col];
                front.face[i][col] = bottom.face[i][col];
                bottom.face[i][col] = back.face[size - 1 - i][size - 1 - col];
                back.face[size - 1 - i][size - 1 - col] = top.face[i][col];
                top.face[i][col] = tmp;
            }
            if (col == 0)
            {
                TurnCounterClockwise(left);
            } else
            {
                TurnClockwise(right);
            }
        }

        public void MoveDown(int col)
        {
            if (col < 0 || size <= col)
                return;

            for (int i = 0; i < size; i++)
            {
                char tmp = front.face[i][col];
                front.face[i][col] = top.face[i][col];
                top.face[i][col] = back.face[size - 1 - i][size - 1 - col];
                back.face[size - 1 - i][size - 1 - col] = bottom.face[i][col];
                bottom.face[i][col] = tmp;
            }
            if (col == 0)
            {
                TurnClockwise(left);
            } else
            {
                TurnCounterClockwise(right);
            }
        }

        public void MoveLeft(int row)
        {
            if (row < 0 || size <= row)
                return;

            for (int i = 0; i < size; i++)
            {
                char tmp = front.face[row][i];
                front.face[row][i] = right.face[row][i];
                right.face[row][i] = back.face[row][i];
                back.face[row][i] = left.face[row][i];
                left.face[row][i] = tmp;
            }
            if (row == 0)
            {
                TurnClockwise(top);
            } else if (row == size - 1)
            {
                TurnCounterClockwise(bottom);
            }
        }

        public void MoveRight(int row)
        {
            if (row < 0 || size <= row)
                return;

            for (int i = 0; i < size; i++)
            {
                char tmp = front.face[row][i];
                front.face[row][i] = left.face[row][i];
                left.face[row][i] = back.face[row][i];
                back.face[row][i] = right.face[row][i];
                right.face[row][i] = tmp;
            }
            if (row == 0)
            {
                TurnCounterClockwise(top);
            } else
            {
                TurnClockwise(bottom);
            }
        }

        public void TwistFrontLeft()
        {
            TwistLeft(0);
        }

        public void TwistBackLeft()
        {
            TwistLeft(size - 1);
        }

        public void TwistLeft(int layer)
        {
            if (layer < 0 || size <= layer)
            return;

            for (int i = 0; i < size; i++)
            {
                char tmp = top.face[size - 1 - layer][i];
                top.face[size - 1 - layer][i] = right.face[i][layer];
                right.face[i][layer] = bottom.face[layer][size - 1 - i];
                bottom.face[layer][size - 1 - i] = left.face[size - 1 - i][size - 1 - layer];
                left.face[size - 1 - i][size - 1 - layer] = tmp;
            }

            if (layer == 0)
            {
                TurnCounterClockwise(front);
            } else if (layer == size - 1)
            {
                TurnClockwise(back);
            }
        }

        private void TurnClockwise(Face face)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    char tmp = face.face[i][j];
                    face.face[i][j] = face.face[j][i];
                    face.face[j][i] = tmp;
                }
            }
            for (int i = 0; i < size; i++)
            {
                int l = 0, r = size - 1;
                while (l < r)
                {
                    char tmp = face.face[i][r];
                    face.face[i][r] = face.face[i][l];
                    face.face[i][l] = tmp;
                    l++;
                    r--;
                }
            }
        }

        private void TurnCounterClockwise(Face face)
        {
            for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        char tmp = face.face[i][j];
                        face.face[i][j] = face.face[j][i];
                        face.face[j][i] = tmp;
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    int l = 0, r = size - 1;
                    while (l < r)
                    {
                        char tmp = face.face[l][i];
                        face.face[l][i] = face.face[r][i];
                        face.face[r][i] = tmp;
                        l++;
                        r--;
                    }
                }
        }

        public void TwistFrontRight()
        {
            TwistRight(0);
        }

        public void TwistBackRight()
        {
            TwistRight(size - 1);
        }

        public void TwistRight(int layer)
        {
            if (layer < 0 || size <= layer)
            return;

            for (int i = 0; i < size; i++)
            {
                char tmp = top.face[size - 1 - layer][i];
                top.face[size - 1 - layer][i] = left.face[size - 1 - i][size - 1 - layer];
                left.face[size - 1 - i][size - 1 - layer] = bottom.face[layer][size - 1 -i];
                bottom.face[layer][size - 1 - i] = right.face[i][layer];
                right.face[i][layer] = tmp;
            }

            if (layer == 0)
            {
                TurnClockwise(front);
            } else if (layer == size - 1)
            {
                TurnCounterClockwise(back);
            }
        }

        public bool IsSolved()
        {
            return front.IsSolved() && back.IsSolved() && top.IsSolved() && 
            left.IsSolved() && right.IsSolved() && bottom.IsSolved();
        }

        public void PrintStatus()
        {
            Console.WriteLine($"=====================================================================");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"         {string.Join(", ", top.face[i])}                        ");
            }
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"{string.Join(", ", left.face[i])}, {string.Join(", ", front.face[i])}, {string.Join(", ", right.face[i])}, {string.Join(", ", back.face[i])}");
            }
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"         {string.Join(", ", bottom.face[i])}                        ");
            }
            Console.WriteLine($"=====================================================================");
        }

        public void PrintMoveSet()
        {
            Console.WriteLine($"moves: {string.Join(", ", moves.Select(x => $"{x} : {moveDictionary[x]}"))}");
        }

        public bool IsValid()
        {
            int[] charCount = new int[26];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    charCount[Char.ToLower(top.face[i][j]) - 'a']++;
                    charCount[Char.ToLower(left.face[i][j]) - 'a']++;
                    charCount[Char.ToLower(front.face[i][j]) - 'a']++;
                    charCount[Char.ToLower(right.face[i][j]) - 'a']++;
                    charCount[Char.ToLower(back.face[i][j]) - 'a']++;
                    charCount[Char.ToLower(bottom.face[i][j]) - 'a']++;
                }
            }

            int correctCount = 0;
            foreach (var count in charCount)
            {
                if (count == size * size)
                {
                    correctCount++;
                }
            }

            return correctCount == 6;
        }

        internal int GetHash()
        {
            StringBuilder hash = new();
            for (int i = 0; i < size; i++) {
                hash.Append($"{string.Join(',', top.face[i])},{string.Join(',', left.face[i])},{string.Join(',', front.face[i])},");
                hash.Append($"{string.Join(',', right.face[i])},{string.Join(',', back.face[i])},{string.Join(',', bottom.face[i])},");
            }

            return hash.ToString().GetHashCode();
        }
    }

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
        
        // BFS
        var currQ = new Queue<RubiksCube>();
        var nextQ = new Queue<RubiksCube>();
        var hashes = new HashSet<int>
        {
            rubiks.GetHash()
        };
        currQ.Enqueue(rubiks);
        int steps = 0;
        while (steps < 30 && currQ.Count > 0)
        {
            var cube = currQ.Dequeue();
            for (int i = 0; i < 12; i++)
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
                nextQ.Enqueue(nextCube);
            }
            if (currQ.Count == 0)
            {
                currQ = nextQ;
                nextQ = new();
                steps++;
                Console.WriteLine($"step: {steps}. currQ.Count: {currQ.Count}. aprox {System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(currQ.Count), 11)} calculations");
            }
        }
        Console.WriteLine($"no solution found after {steps} steps");

    }
}