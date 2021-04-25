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
        private static long Memmem(byte[] a, long alen, byte[] b, int blen)
        {
            long i, j;
            for (i = 0; i < alen - blen; ++i)
            {
                for (j = 0; j < blen; ++j)
                    if (a[i + j] != b[j])
                        break;
                if (j >= blen)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 失败返回-1
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="aob"></param>
        /// <returns></returns>
        public static long Aobscan(int handle, byte[] aob, Func<long, bool> matchValidator, int blockToStart = 0)
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
                long r = Memmem(va, mbi.RegionSize, aob, aob.Length);
                if (r >= 0)
                {
                    if (matchValidator(mbi.BaseAddress + r))
                        return mbi.BaseAddress + r;
                }
                i = mbi.BaseAddress + mbi.RegionSize;
            }
            return -1;
        }
    }
}
