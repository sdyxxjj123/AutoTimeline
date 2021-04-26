using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRAutoTimeline
{
    class KMP
    {
        public static long IndexOf<T>(T[] s, T[] t) where T : IEquatable<T>
        {
            long[] next = InitNext(t);
            return IndexWithNext(s, next, t, t.Length);
        }

        public static long IndexWithNext<T>(T[] s, long[] next, T[] t, long tl) where T : IEquatable<T>
        {
            long time = 0;
            long i, j;
            var sl = s.Length;
            for (i = 0; i < sl;)
            {
                for (j = 0; j < tl;)
                {
                    time++;
                    if (s[i].Equals(t[j]))
                    {
                        i++;
                        j++;
                    }
                    else
                    {
                        i = j > 0 ? i - next[j - 1] : i + 1;
                        break;
                    }
                }
                if (j == tl)
                    return i - tl;
            }
            return -1;
        }
        public static long[] InitNext<T>(T[] t) where T : IEquatable<T>
        {
            long[] ret = new long[t.Length];
            for (long i = 0; i < t.Length; i++)
                ret[i] = 0;
            for (long i = 1; i < t.Length; i++)
            {
                long temp = 0;
                for (long j = 0; j < t.Length; j++)
                {
                    while (i < t.Length && j < t.Length && t[i].Equals(t[j]))
                    {
                        ret[i] = ++temp;
                        i++;
                        j++;
                    }
                    break;

                }
            }
            return ret;


        }
    }
}
