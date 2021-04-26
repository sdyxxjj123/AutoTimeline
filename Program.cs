using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net.Sockets;
using CodeStage.AntiCheat.ObscuredTypes;
using Neo.IronLua;

namespace PCRAutoTimeline
{
    class Program
    {
        public static long hwnd, addr;


        private static readonly byte[] idcode =
        {
            0x3c, 0, 0, 0,
            0x89, 0x88, 0x88, 0x3C
        };

        public static (int, float) TryGetInfo(long hwnd, long addr)
        {
            var data = new byte[16];
            NativeFunctions.ReadProcessMemory(hwnd, addr - 0x44, data, 16, 0);
            return (BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 8));
        }

        static void Main(string[] args)
        {
            //Minitouch.connect("localhost", 1111);
            //Minitouch.setPos(1, 100, 100);
            //Minitouch.press(1);

            using var lua = new Lua();
            var env = lua.CreateEnvironment();
            env.RegisterPackage("autopcr", typeof(AutoPcr));
            env.RegisterPackage("minitouch", typeof(Minitouch));

            var chunk = lua.CompileChunk(File.ReadAllText("timeline.lua"), "timeline.lua", new LuaCompileOptions());

            Console.Write("pid>");
            //var pid = int.Parse(Console.ReadLine());
            var pid = 11892;
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

            Console.WriteLine($"addr = {addr:x}");

            Console.WriteLine();

            if (addr == -1)
            {
                Console.WriteLine("aobscan failed.");
                return;
            }

            chunk.Run(env);

            Console.WriteLine("script finished.");
            Minitouch.exit();

            Console.ReadLine();

        }
    }
}
