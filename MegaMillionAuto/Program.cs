using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MegaMillionAuto
{
    public static class Program
    {
        public static Random Rand = new Random();

        public static void Main(string[] args)
        {
            var regularNumbersSource = new SortedList<int, int>
            {
                {01, 18},
                {02, 28},
                {03, 26},
                {04, 17},
                {05, 18},
                {06, 21},
                {07, 27},
                {08, 26},
                {09, 23},
                {10, 23},
                {11, 30},
                {12, 23},
                {13, 18},
                {14, 27},
                {15, 21},
                {16, 22},
                {17, 25},
                {18, 22},
                {19, 22},
                {20, 32},
                {21, 25},
                {22, 21},
                {23, 21},
                {24, 23},
                {25, 33},
                {26, 27},
                {27, 24},
                {28, 28},
                {29, 35},
                {30, 24},
                {31, 33},
                {32, 19},
                {33, 26},
                {34, 21},
                {35, 32},
                {36, 23},
                {37, 24},
                {38, 24},
                {39, 25},
                {40, 18},
                {41, 33},
                {42, 20},
                {43, 15},
                {44, 30},
                {45, 31},
                {46, 27},
                {47, 23},
                {48, 22},
                {49, 32},
                {50, 27},
                {51, 31},
                {52, 19},
                {53, 26},
                {54, 27},
                {55, 14},
                {56, 26},
                {57, 24},
                {58, 27},
                {59, 23},
                {60, 18},
                {61, 18},
                {62, 24},
                {63, 21},
                {64, 18},
                {65, 21},
                {66, 26},
                {67, 15},
                {68, 29},
                {69, 23},
                {70, 20},
                {71, 20},
                {72, 17},
                {73, 21},
                {74, 25},
                {75, 21}
            };

            var megaNumbersSource = new SortedList<int, int>
            {
                {01, 25},
                {02, 23},
                {03, 26},
                {04, 25},
                {05, 17},
                {06, 24},
                {07, 30},
                {08, 21},
                {09, 27},
                {10, 24},
                {11, 19},
                {12, 22},
                {13, 20},
                {14, 22},
                {15, 33}
            };

            var sb = new StringBuilder();
            for (var i = 0; i < 5; i++)
            {
                if (i > 0)
                    sb.AppendLine();

                var regularNumbers = new SortedList<int, int>(regularNumbersSource);
                var normals = GetBalls(regularNumbers, 5);
                var megaNumbers = new SortedList<int, int>(megaNumbersSource);
                var mega = GetBalls(megaNumbers, 1);

                for (var j = 0; j < normals.Length; j++)
                {
                    if (j > 0)
                        sb.Append('-');

                    sb.Append(normals[j]);
                }

                sb.Append("---");
                sb.Append(mega[0]);
            }

            File.WriteAllText("result.txt", sb.ToString());
        }

        private static int[] GetBalls(SortedList<int, int> list, int count)
        {
            var balls = new List<int>();
            for (var i = 0; i < count; i++)
            {
                var sum = list.Values.Sum();
                var r = Rand.Next(sum);
                var index = 0;
                while (index < list.Count)
                {
                    if (r < list.Values[index])
                        break;

                    index++;
                    r -= list.Values[index];
                }

                var ball = list.Keys[index];
                balls.Add(ball);
                list.Remove(ball);
            }

            balls.Sort();
            return balls.ToArray();
        }
    }
}
