using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MegaMillionAuto
{
    public static class Program
    {
        public static AppRandom appRand = new AppRandom();

        public static void Main(string[] args)
        {
            var sb = new StringBuilder();

            Statistics.Retrieve(out SortedList<int, Ball> balls, out SortedList<int, Ball> megas);
            if (balls.Count != 75 || megas.Count != 15)
            {
                sb.AppendLine("Bad statistics data");
            }
            else
            {
                Statistics.PopulateRange(balls);
                Statistics.PopulateRange(megas);

                sb.Append("Balls: ");
                for (var i = 1; i < 75; i++)
                {
                    if (i > 1)
                        sb.Append('.');

                    sb.Append(balls[i].Id);
                    sb.Append(':');
                    sb.Append(balls[i].Freq);
                    if (balls[i].Stat != 'c')
                        sb.Append(balls[i].Stat);
                }

                sb.AppendLine();

                sb.Append("Megas: ");
                for (var i = 1; i < 15; i++)
                {
                    if (i > 1)
                        sb.Append('.');

                    sb.Append(megas[i].Id);
                    sb.Append(':');
                    sb.Append(megas[i].Freq);
                    if (megas[i].Stat != 'c')
                        sb.Append(megas[i].Stat);
                }

                sb.AppendLine();
                sb.AppendLine();

                for (var i = 0; i < 5; i++)
                {
                    if (i > 0)
                        sb.AppendLine();

                    var regularNumbers = new SortedList<int, Ball>(balls);
                    var normals = GetBalls(regularNumbers, 5);
                    var megaNumbers = new SortedList<int, Ball>(megas);
                    var mega = GetBalls(megaNumbers, 1);

                    for (var j = 0; j < normals.Length; j++)
                    {
                        if (j > 0)
                            sb.Append('-');

                        var id = normals[j];
                        sb.Append(id);
                        if (balls[id].Stat != 'c')
                            sb.Append(balls[id].Stat);
                    }

                    sb.Append("---");
                    var mid = mega[0];
                    sb.Append(mid);
                    if (megas[mid].Stat != 'c')
                        sb.Append(megas[mid].Stat);
                }
            }

            File.WriteAllText("result.txt", sb.ToString());
        }

        private static int[] GetBalls(SortedList<int, Ball> balls, int count)
        {
            var drops = new List<int>();
            var workballs = new List<Ball>(balls.Values);
            var total = 0;
            foreach (var ball in workballs)
                total += ball.Freq;

            for (var i = 0; i < count; i++)
            {
                do
                {
                    var r = appRand.Next(total);
                    var index = 0;
                    while (index < workballs.Count)
                    {
                        if (r < workballs[index].Freq)
                            break;

                        index++;
                        r -= workballs[index].Freq;
                    }

                    var id = workballs[index].Id;
                    if (drops.Contains(id))
                        continue;

                    drops.Add(id);
                    break;
                }
                while (true);
            }

            drops.Sort();
            return drops.ToArray();
        }
    }
}
