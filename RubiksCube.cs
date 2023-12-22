using System.Text;

public class RubiksCube {

        public int size = 3;

        public List<int> moves;

        public int steps = 0;
        
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

            internal int CalculateCrosses()
            {
                int count = 0;
                char baseColor = face[1][1];
                if (face[0][1] == baseColor)
                    count++;
                if (face[1][0] == baseColor)
                    count++;
                if (face[1][2] == baseColor)
                    count++;
                if (face[2][1] == baseColor)
                    count++;

                return count;
            }

            internal int CalculateSolved()
            {
                int count = 0;
                char baseChar = face[1][1];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (baseChar == face[i][j])
                            count++;
                    }
                }

                return count;
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
            this.steps = original.steps;
        }

        public string[] moveDictionary = new string[]{
            "MoveTopLeft", "MoveTopRight", "MoveBottomLeft", "MoveBottomRight", 
            "MoveLeftUp",  "MoveLeftDown", "MoveRightUp", "MoveRightDown", 
            "TwistFrontLeft", "TwistFrontRight", "TwistBackLeft", "TwistBackRight",
            "MoveTopLeft x 2", "MoveBottomLeft x 2", "MoveLeftUp x 2", "MoveRightUp x 2",
            "TwistFrontLeft x 2", "TwistBackLeft x 2"
        };

        public void Move(int move)
        {
            moves.Add(move);
            steps++;
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
                case 12:
                    MoveTopLeft();
                    MoveTopLeft();
                    break;
                case 13:
                    MoveBottomLeft();
                    MoveBottomLeft();
                    break;
                case 14:
                    MoveLeftUp();
                    MoveLeftUp();
                    break;
                case 15:
                    MoveRightUp();
                    MoveRightUp();
                    break;
                case 16:
                    TwistFrontLeft();
                    TwistFrontLeft();
                    break;
                case 17:
                    TwistBackLeft();
                    TwistBackLeft();
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
        
        public int CalculateSolved()
        {
            return front.CalculateSolved() + back.CalculateSolved() + top.CalculateSolved() +
            left.CalculateSolved() + right.CalculateSolved() + bottom.CalculateSolved();
        }

        public int CalculateCrosses()
        {
            return front.CalculateCrosses() + back.CalculateCrosses() + top.CalculateCrosses() +
            left.CalculateCrosses() + right.CalculateCrosses() + bottom.CalculateCrosses();
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

        internal void RemoveLastMove()
        {
            moves.RemoveAt(moves.Count - 1);
        }
    }
