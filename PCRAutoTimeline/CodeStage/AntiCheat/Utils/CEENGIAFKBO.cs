// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Utils.CEENGIAFKBO
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

namespace CodeStage.AntiCheat.Utils
{
  internal class CEENGIAFKBO
  {
    private const uint PRIME32_1 = 2654435761;
    private const uint PRIME32_2 = 2246822519;
    private const uint PRIME32_3 = 3266489917;
    private const uint PRIME32_4 = 668265263;
    private const uint PRIME32_5 = 374761393;

    public static uint CalculateHash(byte[] DFFKEFLPKGO, int CIDIMJFKENL, uint FFIIABDADEN)
    {
      int index1 = 0;
      uint num1;
      if (CIDIMJFKENL >= 16)
      {
        int num2 = CIDIMJFKENL - 16;
        uint num3 = (uint) ((int) FFIIABDADEN - 1640531535 - 2048144777);
        uint num4 = FFIIABDADEN + 2246822519U;
        uint num5 = FFIIABDADEN;
        uint num6 = FFIIABDADEN - 2654435761U;
        do
        {
          byte[] numArray1 = DFFKEFLPKGO;
          int index2 = index1;
          int num7 = index2 + 1;
          int num8 = (int) numArray1[index2];
          byte[] numArray2 = DFFKEFLPKGO;
          int index3 = num7;
          int num9 = index3 + 1;
          int num10 = (int) numArray2[index3] << 8;
          int num11 = num8 | num10;
          byte[] numArray3 = DFFKEFLPKGO;
          int index4 = num9;
          int num12 = index4 + 1;
          int num13 = (int) numArray3[index4] << 16;
          int num14 = num11 | num13;
          byte[] numArray4 = DFFKEFLPKGO;
          int index5 = num12;
          int num15 = index5 + 1;
          int num16 = (int) numArray4[index5] << 24;
          uint num17 = (uint) (num14 | num16);
          uint num18 = num3 + num17 * 2246822519U;
          num3 = (num18 << 13 | num18 >> 19) * 2654435761U;
          byte[] numArray5 = DFFKEFLPKGO;
          int index6 = num15;
          int num19 = index6 + 1;
          int num20 = (int) numArray5[index6];
          byte[] numArray6 = DFFKEFLPKGO;
          int index7 = num19;
          int num21 = index7 + 1;
          int num22 = (int) numArray6[index7] << 8;
          int num23 = num20 | num22;
          byte[] numArray7 = DFFKEFLPKGO;
          int index8 = num21;
          int num24 = index8 + 1;
          int num25 = (int) numArray7[index8] << 16;
          int num26 = num23 | num25;
          byte[] numArray8 = DFFKEFLPKGO;
          int index9 = num24;
          int num27 = index9 + 1;
          int num28 = (int) numArray8[index9] << 24;
          uint num29 = (uint) (num26 | num28);
          uint num30 = num4 + num29 * 2246822519U;
          num4 = (num30 << 13 | num30 >> 19) * 2654435761U;
          byte[] numArray9 = DFFKEFLPKGO;
          int index10 = num27;
          int num31 = index10 + 1;
          int num32 = (int) numArray9[index10];
          byte[] numArray10 = DFFKEFLPKGO;
          int index11 = num31;
          int num33 = index11 + 1;
          int num34 = (int) numArray10[index11] << 8;
          int num35 = num32 | num34;
          byte[] numArray11 = DFFKEFLPKGO;
          int index12 = num33;
          int num36 = index12 + 1;
          int num37 = (int) numArray11[index12] << 16;
          int num38 = num35 | num37;
          byte[] numArray12 = DFFKEFLPKGO;
          int index13 = num36;
          int num39 = index13 + 1;
          int num40 = (int) numArray12[index13] << 24;
          uint num41 = (uint) (num38 | num40);
          uint num42 = num5 + num41 * 2246822519U;
          num5 = (num42 << 13 | num42 >> 19) * 2654435761U;
          byte[] numArray13 = DFFKEFLPKGO;
          int index14 = num39;
          int num43 = index14 + 1;
          int num44 = (int) numArray13[index14];
          byte[] numArray14 = DFFKEFLPKGO;
          int index15 = num43;
          int num45 = index15 + 1;
          int num46 = (int) numArray14[index15] << 8;
          int num47 = num44 | num46;
          byte[] numArray15 = DFFKEFLPKGO;
          int index16 = num45;
          int num48 = index16 + 1;
          int num49 = (int) numArray15[index16] << 16;
          int num50 = num47 | num49;
          byte[] numArray16 = DFFKEFLPKGO;
          int index17 = num48;
          index1 = index17 + 1;
          int num51 = (int) numArray16[index17] << 24;
          uint num52 = (uint) (num50 | num51);
          uint num53 = num6 + num52 * 2246822519U;
          num6 = (num53 << 13 | num53 >> 19) * 2654435761U;
        }
        while (index1 <= num2);
        num1 = (uint) (((int) num3 << 1 | (int) (num3 >> 31)) + ((int) num4 << 7 | (int) (num4 >> 25)) + ((int) num5 << 12 | (int) (num5 >> 20)) + ((int) num6 << 18 | (int) (num6 >> 14)));
      }
      else
        num1 = FFIIABDADEN + 374761393U;
      uint num54 = num1 + (uint) CIDIMJFKENL;
      while (index1 <= CIDIMJFKENL - 4)
      {
        int num2 = (int) num54;
        byte[] numArray1 = DFFKEFLPKGO;
        int index2 = index1;
        int num3 = index2 + 1;
        int num4 = (int) numArray1[index2];
        byte[] numArray2 = DFFKEFLPKGO;
        int index3 = num3;
        int num5 = index3 + 1;
        int num6 = (int) numArray2[index3] << 8;
        int num7 = num4 | num6;
        byte[] numArray3 = DFFKEFLPKGO;
        int index4 = num5;
        int num8 = index4 + 1;
        int num9 = (int) numArray3[index4] << 16;
        int num10 = num7 | num9;
        byte[] numArray4 = DFFKEFLPKGO;
        int index5 = num8;
        index1 = index5 + 1;
        int num11 = (int) numArray4[index5] << 24;
        int num12 = (num10 | num11) * -1028477379;
        uint num13 = (uint) (num2 + num12);
        num54 = (uint) (((int) num13 << 17 | (int) (num13 >> 15)) * 668265263);
      }
      for (; index1 < CIDIMJFKENL; ++index1)
      {
        uint num2 = num54 + (uint) DFFKEFLPKGO[index1] * 374761393U;
        num54 = (uint) (((int) num2 << 11 | (int) (num2 >> 21)) * -1640531535);
      }
      uint num55 = (num54 ^ num54 >> 15) * 2246822519U;
      uint num56 = (num55 ^ num55 >> 13) * 3266489917U;
      return num56 ^ num56 >> 16;
    }
  }
}
