using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Newtonsoft.Json;
using PCRCaculator;
using PCRCaculator.Guild;
using UnityEngine;

namespace CalcInject
{
    public static class Injector
    {
        private static Dictionary<int, int> ubtime = new Dictionary<int, int>();

        private static int CacheGetUbTime(int unit)
        {
            if (ubtime.TryGetValue(unit, out var val)) return val;

            var textAsset = File.ReadAllText($"PCRGuildCalculator_Data/StreamingAssets/Datas/unitPrefabDatas/UNIT_{unit}.json");
            var time = JsonConvert.DeserializeObject<UnitPrefabData>(textAsset).UnitActionControllerData.UnionBurstList.First().BlackOutTime;

            float dtime = 1 / 60f, counter = 0;
            var frame = 1;
            while ((counter += dtime) <= time) ++frame;

            ubtime[unit] = frame;
            return frame;
        }

        private static void SaveTimeline(GuildTimelineData timelineData, string path)
        {
            var src = new StringBuilder();

            foreach (var unit in timelineData.playerGroupData.playerData.playrCharacters)
            {
                src.AppendLine($"print(\"calibrate for {unit.GetUnitName()}\");");
                src.AppendLine($"autopcr.calibrate({unit.unitId});");
            }

            src.AppendLine("autopcr.setOffset(2, 0); --offset calibration");

            var offset = 0;
            foreach (var ub in timelineData.allUnitStateChangeDic
                .Where(tuple => timelineData.playerGroupData.playerData.playrCharacters.Any(c => c.unitId == tuple.Key))
                .SelectMany(pair => pair.Value.Where(data => data.changStateTo == Elements.UnitCtrl.ActionState.SKILL_1)
                    .Select(data => new Tuple<int, int>(pair.Key, data.currentFrameCount)))
                .OrderBy(tuple => tuple.Item2))
            {
                src.AppendLine($"autopcr.waitFrame({offset + ub.Item2}); autopcr.press({ub.Item1});");
                offset += CacheGetUbTime(ub.Item1);
            }

            File.WriteAllText(path, src.ToString());
        }

        public static void OutputGuildTimeLine(GuildTimelineData timelineData, string defaultName)
        {
            try
            {
                OpenFileName openFileName = new OpenFileName();
                openFileName.structSize = Marshal.SizeOf(openFileName);
                openFileName.filter = "Lua scripts(*.lua)\0*.lua\0";
                openFileName.file = new string(new char[256]);
                openFileName.maxFile = openFileName.file.Length;
                openFileName.fileTitle = new string(new char[64]);
                openFileName.maxFileTitle = openFileName.fileTitle.Length;
                openFileName.initialDir = Application.dataPath;
                openFileName.title = "选择保存Lua脚本路径";
                openFileName.defExt = "lua";
                openFileName.file = defaultName;
                openFileName.flags = 530952;
                var flag = DllTest.GetSaveFileName(openFileName);
                var text = openFileName.file.Replace("\\", "/");
                if (flag)
                {
                    SaveTimeline(timelineData, text);
                }
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowConfigMessage(e.ToString(), null);
            }
        }

        private static Harmony inst = new Harmony("injector");
        public static void Main()
        {
            inst.Patch(typeof(ExcelHelper.ExcelHelper)
                .GetMethod("OutputGuildTimeLine"), postfix: new HarmonyMethod(typeof(Injector), "OutputGuildTimeLine"));
        }
    }
}
