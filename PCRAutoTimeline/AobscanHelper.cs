using System;
using System.Runtime.InteropServices;

namespace PCRAutoTimeline
{
    public class AobscanHelper
    {
        /// <summary>
        /// 搜索Byte数组
        /// </summary>
        /// <param name="a">源数组</param>
        /// <param name="alen">长度</param>
        /// <param name="b">被搜索的数组</param>
        /// <param name="blen">被搜数组的长度</param>
        /// <returns>失败返回-1</returns>
        private static long Memmem(byte[] a, long alen, byte[] b, int blen, Func<long, bool> matchValidator)
        {
            long i, j, diff = alen - blen;
            for (i = 0; i < diff; i += 4) /* 4 bytes alignment */
            {
                j = 0;
                while (j < blen)
                {
                    if (a[i + j] != b[j])
                        goto next;
                    ++j;
                }
                if (matchValidator(i))
                    return i;
                next: ;
            }
            return -1;
        }

        /// <summary>
        /// 失败返回-1
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="aob"></param>
        /// <returns></returns>
        public static (long, long) Aobscan(long handle, byte[] aob, Func<long, bool> matchValidator, long blockToStart = 0)
        {
            long i = blockToStart;
            while (i < long.MaxValue)
            {
                int flag = NativeFunctions.VirtualQueryEx(handle, i, out NativeFunctions.MEMORY_BASIC_INFORMATION mbi, NativeFunctions.MEMORY_BASIC_INFORMATION_SIZE);
                if (flag != NativeFunctions.MEMORY_BASIC_INFORMATION_SIZE)
                    break;
                if (mbi.RegionSize <= 0)
                    break;
                if (mbi.State != (int)NativeFunctions.AllocationType.Commit)
                {
                    i = mbi.BaseAddress + mbi.RegionSize;
                    continue;
                }
                Console.Write($"\rscanning {mbi.BaseAddress:x}...");
                byte[] va = new byte[mbi.RegionSize];
                NativeFunctions.ReadProcessMemory(handle, mbi.BaseAddress, va, mbi.RegionSize, 0);
                long r = Memmem(va, mbi.RegionSize, aob, aob.Length, r => matchValidator(mbi.BaseAddress + r));
                //long r = KMP.IndexOf(va, aob);
                if (r >= 0)
                {
                    return (mbi.BaseAddress + r, i);
                }
                i = mbi.BaseAddress + mbi.RegionSize;
            }
            return (-1, -1);
        }
    }
}
