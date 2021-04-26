using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRAutoTimeline
{
    public static class AutoPcr
    {
        private static readonly Dictionary<int, NativeFunctions.POINT> mousepos = new();

        public static void calibrate(int i)
        {
            Console.Write($"Mouse for pos #{i}:");
            Console.ReadLine();
            NativeFunctions.GetCursorPos(out var pos);
            if (!mousepos.ContainsKey(i)) mousepos.Add(i, pos);
            else mousepos[i] = pos;
            Console.WriteLine(pos);
        }

        public static void press(int id)
        {
            PressAt(mousepos[id]);
        }

        private static int frameoff = 0; 
        private static float timeoff = 0;

        public static void setOffset(int frame, float time)
        {
            frameoff = frame;
            timeoff = time;
        }

        public static void waitOneFrame()
        {
            var frame = getFrame();
            _waitFrame(frame + 1);
        }

        public static void framePress(int id)
        {
            var point = mousepos[id];
            NativeFunctions.SetCursorPos(point.X, point.Y);
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            waitOneFrame();
            NativeFunctions.mouse_event(NativeFunctions.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static bool UnitEvaluator(int unitid, int rarity, int promotion, long addr)
        {
            var t = TryGetIntInt(Program.hwnd, addr + 0x10);
            if (t.Item1 == rarity && t.Item2 == promotion)
            {
                var tp = getTp(addr - 0x244);
                var hp = getHp(addr - 0x244);
                var maxHp = getMaxHp(addr - 0x244);
                if (tp == 0.0 && maxHp == hp)
                {
                    Console.WriteLine($"unit found @{addr:x} unitid = {unitid}, tp = {tp}, hp = {hp}/{maxHp}");
                    return true;
                }
            }
            return false;
        }

        public static long getUnitAddr(int unitid, int rarity, int promotion)
        {
            var b = BitConverter.GetBytes(unitid);

            var tuple = AobscanHelper.Aobscan(Program.hwnd, b.Concat(b).ToArray(),
                addr => UnitEvaluator(unitid, rarity, promotion, addr));
            return tuple.Item1 != -1 ? tuple.Item1 - 0x244 : -1;
        }


        public static float getTp(long unitHandle)
        {
            NativeFunctions.ReadProcessMemory(Program.hwnd, unitHandle + 0x4E0, out ObscuredFloat tp, 16, 0);
            return tp;
        }

        public static long getHp(long unitHandle)
        {
            NativeFunctions.ReadProcessMemory(Program.hwnd, unitHandle + 0x390, out ObscuredLong hp, 20, 0);
            return hp;
        }

        public static long getMaxHp(long unitHandle)
        {
            NativeFunctions.ReadProcessMemory(Program.hwnd, unitHandle + 0x3A4, out ObscuredLong hp, 20, 0);
            return hp;
        }

        public static int getFrame()
        {
            return Program.TryGetInfo(Program.hwnd, Program.addr).Item1;
        }

        public static float getTime()
        {
            return Program.TryGetInfo(Program.hwnd, Program.addr).Item2;
        }

        private static void _waitFrame(int frame)
        {
            WaitFor(inf => inf.Item1 >= frame);
        }

        public static void waitFrame(int frame)
        {
            _waitFrame(frame - frameoff);
        }

        public static void waitTime(float time)
        {
            WaitFor(inf => inf.Item2 <= time - timeoff);
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
        private static void WaitFor(Func<(int, float), bool> check)
        {
            (int, float) frame;
            var last = -1;
            do
            {
                frame = Program.TryGetInfo(Program.hwnd, Program.addr);
                if (frame.Item1 != last)
                {
                    Console.Write(
                        $"\rframeCount = {frame.Item1}, limitTime = {frame.Item2}                  ");
                    last = frame.Item1;
                }
            } while (!check(frame));
            Console.WriteLine();
        }


    }
}
