using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace PCRAutoTimeline
{
    class SkillPoint
    {
        public int pos, frame;
        public float time;
    }

    class Timeline
    {
        public SkillPoint[] skills;
        public int frameOffset;
        public float timeOffset;

        public static Timeline ParseFromText(string text)
        {
            var splits = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var off = splits[0].Split(' ');
            return new()
            {
                frameOffset = int.Parse(off[0]),
                timeOffset = float.Parse(off[1]),
                skills = splits.Skip(1).Select(s =>
                    {
                        var p = s.Split(' ');
                        return new SkillPoint
                        {
                            pos = int.Parse(p[1]),
                            frame = p[0][0] == 'f' ? int.Parse(p[0][1..]) : -1,
                            time = p[0][0] == 't' ? float.Parse(p[0][1..]) : -1,
                        };
                    }).ToArray()
            };
        }
    }

    class Program
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        private static readonly byte[] idcode =
        {
            0x3c, 0, 0, 0,
            0x89, 0x88, 0x88, 0x3C
        };

        private static (int, float) TryGetInfo(int hwnd, long addr)
        {
            var data = new byte[16];
            NativeFunctions.ReadProcessMemory(hwnd, addr - 0x44, data, 16, 0);
            return (BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 8));
        }

        private static void PressAt(NativeFunctions.POINT point)
        {
            NativeFunctions.SetCursorPos(point.X, point.Y);
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        static void Main(string[] args)
        {
            var timeline = Timeline.ParseFromText(File.ReadAllText("timeline.txt"));
            var mousepos = new NativeFunctions.POINT[5];

            Console.WriteLine("----Mouse Calibration----");

            for (int i = 1; i < 6; ++i)
            {
                Console.Write($"Mouse for pos #{i}:");
                Console.ReadLine();
                NativeFunctions.GetCursorPos(out mousepos[i - 1]);
                Console.WriteLine(mousepos[i - 1]);
            }


            Console.Write("pid>");
            var pid = int.Parse(Console.ReadLine());

            var hwnd = NativeFunctions.OpenProcess(PROCESS_ALL_ACCESS, false, pid);

            var addr = AobscanHelper.Aobscan(hwnd, idcode, addr =>
            {
                var frame = TryGetInfo(hwnd, addr);
                if (frame.Item1 >= 0 && frame.Item1 < 1000 && frame.Item2 > 80 && frame.Item2 < 100)
                {
                    Console.WriteLine($"data found, frameCount = {frame.Item1}, limitTime = {frame.Item2}");
                    return true;
                }
                return false;
            });

            Console.WriteLine();

            var sw = new Stopwatch();
            sw.Start();
            var count = 0;
            var cur = 0;

            while (cur < timeline.skills.Length)
            {
                ++count;
                var frame = TryGetInfo(hwnd, addr);
                var curpt = timeline.skills[cur];
                if (curpt.frame - timeline.frameOffset <= frame.Item1 && curpt.frame >= 0 || curpt.time - timeline.timeOffset >= frame.Item2)
                {
                    PressAt(mousepos[curpt.pos - 1]);
                    Console.WriteLine();
                    Console.WriteLine($"firing skill@{curpt.pos} expecting @(f{curpt.frame}, t{curpt.time})");
                    Console.WriteLine();
                    ++cur;
                }
                Console.Write($"\rframeCount = {frame.Item1}, limitTime = {frame.Item2}, accuracy={sw.ElapsedTicks / 10.0 / count:f1}us                  ");
            }

            Console.WriteLine();
            Console.WriteLine("finished.");
            Console.ReadLine();
        }
    }
}
