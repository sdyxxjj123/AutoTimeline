// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDouble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Common;
using System;
using System.Runtime.InteropServices;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredDouble : IEquatable<ObscuredDouble>, IFormattable
  {
    private static long cryptoKey = 210987;
    
    private long currentCryptoKey;
    
    private ACTkByte8 hiddenValue;

    private int hiddenValueOld;
    
    private bool inited;

    private ObscuredDouble(ACTkByte8 PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.hiddenValueOld = 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long DCCMJMPNCDO) => ObscuredDouble.cryptoKey = DCCMJMPNCDO;

    public static long Encrypt(double PDGAOEAMDCL) => ObscuredDouble.Encrypt(PDGAOEAMDCL, ObscuredDouble.cryptoKey);

    public static long Encrypt(double PDGAOEAMDCL, long HDAJOEOLHGG)
    {
      ObscuredDouble.LPAIILGOHKK lpaiilgohkk = new ObscuredDouble.LPAIILGOHKK();
      lpaiilgohkk.d = PDGAOEAMDCL;
      lpaiilgohkk.l ^= HDAJOEOLHGG;
      return lpaiilgohkk.l;
    }

    private static ACTkByte8 InternalEncrypt(double PDGAOEAMDCL) => ObscuredDouble.InternalEncrypt(PDGAOEAMDCL, 0L);

    private static ACTkByte8 InternalEncrypt(double PDGAOEAMDCL, long HDAJOEOLHGG)
    {
      long num = HDAJOEOLHGG;
      if (num == 0L)
        num = ObscuredDouble.cryptoKey;
      ObscuredDouble.LPAIILGOHKK lpaiilgohkk = new ObscuredDouble.LPAIILGOHKK();
      lpaiilgohkk.d = PDGAOEAMDCL;
      lpaiilgohkk.l ^= num;
      return lpaiilgohkk.b8;
    }

    public static double Decrypt(long PDGAOEAMDCL) => ObscuredDouble.Decrypt(PDGAOEAMDCL, ObscuredDouble.cryptoKey);

    public static double Decrypt(long PDGAOEAMDCL, long HDAJOEOLHGG) => new ObscuredDouble.LPAIILGOHKK()
    {
      l = (PDGAOEAMDCL ^ HDAJOEOLHGG)
    }.d;

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDouble.cryptoKey)
        return;
      this.hiddenValue = ObscuredDouble.InternalEncrypt(this.InternalDecrypt(), ObscuredDouble.cryptoKey);
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
    }
    public long GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredDouble.LPAIILGOHKK()
      {
        b8 = this.hiddenValue
      }.l;
    }

    public void SetEncrypted(long LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = new ObscuredDouble.LPAIILGOHKK()
      {
        l = LEBNEIHLING
      }.b8;
    }

    public double GetDecrypted() => this.InternalDecrypt();

    private double InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDouble.cryptoKey;
        this.hiddenValue = ObscuredDouble.InternalEncrypt(0.0);
        this.inited = true;
      }
      ObscuredDouble.LPAIILGOHKK lpaiilgohkk = new ObscuredDouble.LPAIILGOHKK();
      lpaiilgohkk.b8 = this.hiddenValue;
      lpaiilgohkk.l ^= this.currentCryptoKey;
      return lpaiilgohkk.d;
    }

    public static implicit operator ObscuredDouble(double PDGAOEAMDCL) => new ObscuredDouble(ObscuredDouble.InternalEncrypt(PDGAOEAMDCL));

    public static implicit operator double(ObscuredDouble PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredDouble operator ++(ObscuredDouble PBAIIOCIFDP)
    {
      double PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1.0;
      PBAIIOCIFDP.hiddenValue = ObscuredDouble.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredDouble operator --(ObscuredDouble PBAIIOCIFDP)
    {
      double PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1.0;
      PBAIIOCIFDP.hiddenValue = ObscuredDouble.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredDouble EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredDouble EACOJCAGIDK) => EACOJCAGIDK.InternalDecrypt().Equals(this.InternalDecrypt());

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    [StructLayout(LayoutKind.Explicit)]
    private struct LPAIILGOHKK
    {
      [FieldOffset(0)]
      public double d;
      [FieldOffset(0)]
      public long l;
      [FieldOffset(0)]
      public ACTkByte8 b8;
    }
  }
}
