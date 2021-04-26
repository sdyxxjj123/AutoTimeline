using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using Neo.IronLua;

namespace PCRAutoTimeline
{
    class Program
    {
        private static long hwnd, addr, units, enemies, @base;
        private const int MOUSECOUNT = 5;

        private static NativeFunctions.POINT[] mousepos = new NativeFunctions.POINT[MOUSECOUNT];
        public static class LuaFunctions
        {
            public static void press(int id)
            {
                PressAt(mousepos[id - 1]);
            }

            public static void waitOneFrame()
            {
                var frame = getFrame();
                waitFrame(frame + 1);
            }

            public static void framePress(int id)
            {
                var point = mousepos[id - 1];
                NativeFunctions.SetCursorPos(point.X, point.Y);
                NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                waitOneFrame();
                NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                waitOneFrame();
            }

            public static long getUnitAddr(int unitid, int rarity, int promotion)
            {
                var b = BitConverter.GetBytes(unitid);
                var tuple = AobscanHelper.Aobscan(hwnd, b.Concat(b).ToArray(), addr =>
                {
                    var t = TryGetIntInt(hwnd, addr + 0x10);
                    if (t.Item1 == rarity && t.Item2 == promotion)
                    {
                        var tp = getTp(addr - 0x244);
                        Console.WriteLine($"unit found unitid = {unitid}, tp = {tp}");
                        if (tp == 0.0)
                            return true;
                    }
                    return false;
                }, @base - 0x20000000);
                if (tuple.Item1 != -1) return tuple.Item1 - 0x244;

                tuple = AobscanHelper.Aobscan(hwnd, b.Concat(b).ToArray(), addr =>
                {
                    var t = TryGetIntInt(hwnd, addr + 0x10);
                    if (t.Item1 == rarity && t.Item2 == promotion)
                    {
                        var tp = getTp(addr - 0x244);
                        Console.WriteLine($"unit found unitid = {unitid}, tp = {tp}");
                        if (tp == 0.0)
                            return true;
                    }
                    return false;
                });
                return tuple.Item1 != -1 ? tuple.Item1 - 0x244 : -1;
            }


            public static float getTp(long unitHandle)
            {
                NativeFunctions.ReadProcessMemory(hwnd, unitHandle + 0x4E0, out ObscuredFloat tp, 16, 0);
                return tp;
            }

            public static int getFrame()
            {
                return TryGetInfo(hwnd, addr).Item1;
            }

            public static float getTime()
            {
                return TryGetInfo(hwnd, addr).Item1;
            }

            public static void waitFrame(int frame)
            {
                WaitFor(inf => inf.Item1 >= frame);
            }

            public static void waitTime(float time)
            {
                WaitFor(inf => inf.Item2 <= time);
            }
        }

        private static readonly byte[] idcode =
        {
            0x3c, 0, 0, 0,
            0x89, 0x88, 0x88, 0x3C
        };

        private static void WaitFor(Func<(int, float), bool> check)
        {
            (int, float) frame;
            do
            {
                frame = TryGetInfo(hwnd, addr);
                Console.Write(
                    $"\rframeCount = {frame.Item1}, limitTime = {frame.Item2}                  ");
            } while (!check(frame));
        }

        private static (int, float) TryGetInfo(long hwnd, long addr)
        {
            var data = new byte[16];
            NativeFunctions.ReadProcessMemory(hwnd, addr - 0x44, data, 16, 0);
            return (BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 8));
        }

        private static (int, int) TryGetIntInt(long hwnd, long addr)
        {
            var data = new byte[16];
            NativeFunctions.ReadProcessMemory(hwnd, addr, data, 16, 0);
            return (BitConverter.ToInt32(data, 0), BitConverter.ToInt32(data, 4));
        }

        private static void PressAt(NativeFunctions.POINT point)
        {
            NativeFunctions.SetCursorPos(point.X, point.Y);
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        static void Main(string[] args)
        {
            using var lua = new Lua();
            var env = lua.CreateEnvironment();
            env.RegisterPackage("autopcr", typeof(LuaFunctions));
            
            var chunk = lua.CompileChunk(File.ReadAllText("timeline.lua"), "timeline.lua", new LuaCompileOptions());

            Console.WriteLine("----Mouse Calibration----");

            for (int i = 1; i <= MOUSECOUNT; ++i)
            {
                Console.Write($"Mouse for pos #{i}:");
                Console.ReadLine();
                NativeFunctions.GetCursorPos(out mousepos[i - 1]);
                Console.WriteLine(mousepos[i - 1]);
            }

            Console.Write("pid>");
            //var pid = int.Parse(Console.ReadLine());
            var pid = 7424;
            hwnd = NativeFunctions.OpenProcess(NativeFunctions.PROCESS_ALL_ACCESS, false, pid);

            var tuple =  AobscanHelper.Aobscan(hwnd, idcode, addr =>
            {
                var frame = TryGetInfo(hwnd, addr);
                if (frame.Item1 >= 0 && frame.Item1 < 1000 && frame.Item2 > 80 && frame.Item2 < 100)
                {
                    Console.WriteLine($"data found, frameCount = {frame.Item1}, limitTime = {frame.Item2}");
                    return true;
                }
                return false;
            });

            addr = tuple.Item1;
            @base = tuple.Item2;

            Console.WriteLine($"addr = {addr}, base = {@base}");

            Console.WriteLine();

            if (addr == -1)
            {
                Console.WriteLine("aobscan failed.");
                return;
            }

            chunk.Run(env);

        }
    }
}
