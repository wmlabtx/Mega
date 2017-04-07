using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MegaMillionAuto
{
    public static class Statistics
    {
        public static bool Retrieve(out SortedList<int, Ball> balls, out SortedList<int, Ball> megas)
        {
            balls = new SortedList<int, Ball>();
            megas = new SortedList<int, Ball>();

            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadData("http://www.txlottery.org/export/sites/lottery/Games/Mega_Millions/Number_Frequency.html");
                if (data == null)
                    return false;

                var html = Encoding.UTF8.GetString(data);
                const string patbegin = "<td><span>";
                var pos = 0;
                do
                {
                    pos = html.IndexOf(patbegin, pos);
                    if (pos < 0)
                        break;

                    var pose = html.IndexOf('<', pos + patbegin.Length);
                    if (pose < 0)
                        break;

                    var sid = html.Substring(pos + patbegin.Length, pose - pos - patbegin.Length);
                    if (!int.TryParse(sid, out int id))
                        return false;

                    if (id < 1 || id > 75)
                        return false;

                    pos = pose + 18;
                    pose = html.IndexOf('<', pos);
                    if (pose < 0)
                        break;

                    var sfreq = html.Substring(pos, pose - pos).Trim();
                    if (!int.TryParse(sfreq, out int freq))
                        return false;

                    if (freq < 1)
                        return false;

                    pos = pose;

                    var ball = new Ball()
                    {
                        Id = id,
                        Freq = freq,
                        Stat = 'c'
                    };

                    if (!balls.ContainsKey(id))
                    {
                        balls.Add(id, ball);
                    }
                    else
                    {
                        if (!megas.ContainsKey(id))
                            megas.Add(id, ball);
                        else
                            return false;
                    }
                }
                while (pos >= 0);
            }

            return true;
        }

        public static void PopulateRange(SortedList<int, Ball> balls)
        {
            var statballs = new List<Ball>(balls.Values);
            var total = 0;
            foreach (var ball in statballs)
                total += ball.Freq;

            var low = (int)(total * 0.25);
            var high = (int)(total * 0.75);

            statballs.Sort(ComparerByFreq);

            var sum = 0;
            for (var i = 0; i < statballs.Count; i++)
            {
                if (sum >= high)
                    balls[statballs[i].Id].Stat = 'p';

                sum += statballs[i].Freq;
                if (sum <= low)
                    balls[statballs[i].Id].Stat = 'r';
            }
        }

        private static int ComparerByFreq(Ball x, Ball y)
        {
            return x.Freq.CompareTo(y.Freq);
        }
    }
}
