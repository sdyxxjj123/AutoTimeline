// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDecimal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Common;
using System;
using System.Runtime.InteropServices;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredDecimal : IEquatable<ObscuredDecimal>, IFormattable
  {
    private static long cryptoKey = 209208;
    private long currentCryptoKey;

    private int hiddenValueOld;
    private ACTkByte16 hiddenValue;
    private bool inited;

    private ObscuredDecimal(ACTkByte16 PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.hiddenValueOld = 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long DCCMJMPNCDO) => ObscuredDecimal.cryptoKey = DCCMJMPNCDO;

    public static Decimal Encrypt(Decimal PDGAOEAMDCL) => ObscuredDecimal.Encrypt(PDGAOEAMDCL, ObscuredDecimal.cryptoKey);

    public static Decimal Encrypt(Decimal PDGAOEAMDCL, long HDAJOEOLHGG)
    {
      ObscuredDecimal.HAIHIJOPFAC haihijopfac = new ObscuredDecimal.HAIHIJOPFAC();
      haihijopfac.d = PDGAOEAMDCL;
      haihijopfac.l1 ^= HDAJOEOLHGG;
      haihijopfac.l2 ^= HDAJOEOLHGG;
      return haihijopfac.d;
    }

    private static ACTkByte16 InternalEncrypt(Decimal PDGAOEAMDCL) => ObscuredDecimal.InternalEncrypt(PDGAOEAMDCL, 0L);

    private static ACTkByte16 InternalEncrypt(Decimal PDGAOEAMDCL, long HDAJOEOLHGG)
    {
      long num = HDAJOEOLHGG;
      if (num == 0L)
        num = ObscuredDecimal.cryptoKey;
      ObscuredDecimal.HAIHIJOPFAC haihijopfac = new ObscuredDecimal.HAIHIJOPFAC();
      haihijopfac.d = PDGAOEAMDCL;
      haihijopfac.l1 ^= num;
      haihijopfac.l2 ^= num;
      return haihijopfac.b16;
    }

    public static Decimal Decrypt(Decimal PDGAOEAMDCL) => ObscuredDecimal.Decrypt(PDGAOEAMDCL, ObscuredDecimal.cryptoKey);

    public static Decimal Decrypt(Decimal PDGAOEAMDCL, long HDAJOEOLHGG)
    {
      ObscuredDecimal.HAIHIJOPFAC haihijopfac = new ObscuredDecimal.HAIHIJOPFAC();
      haihijopfac.d = PDGAOEAMDCL;
      haihijopfac.l1 ^= HDAJOEOLHGG;
      haihijopfac.l2 ^= HDAJOEOLHGG;
      return haihijopfac.d;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDecimal.cryptoKey)
        return;
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(this.InternalDecrypt(), ObscuredDecimal.cryptoKey);
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Decimal PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = (long) 0;
      }
      while (this.currentCryptoKey == 0L);
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public Decimal GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredDecimal.HAIHIJOPFAC()
      {
        b16 = this.hiddenValue
      }.d;
    }

    public void SetEncrypted(Decimal LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = new ObscuredDecimal.HAIHIJOPFAC()
      {
        d = LEBNEIHLING
      }.b16;
    }

    public Decimal GetDecrypted() => this.InternalDecrypt();

    private Decimal InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDecimal.cryptoKey;
        this.hiddenValue = ObscuredDecimal.InternalEncrypt(0M);
        this.inited = true;
      }
      ObscuredDecimal.HAIHIJOPFAC haihijopfac = new ObscuredDecimal.HAIHIJOPFAC();
      haihijopfac.b16 = this.hiddenValue;
      haihijopfac.l1 ^= this.currentCryptoKey;
      haihijopfac.l2 ^= this.currentCryptoKey;
      return haihijopfac.d;
    }

    public static implicit operator ObscuredDecimal(Decimal PDGAOEAMDCL) => new ObscuredDecimal(ObscuredDecimal.InternalEncrypt(PDGAOEAMDCL));

    public static implicit operator Decimal(ObscuredDecimal PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static explicit operator ObscuredDecimal(ObscuredFloat ABCBPPLJGML) => (ObscuredDecimal) (Decimal) (float) ABCBPPLJGML;

    public static ObscuredDecimal operator ++(ObscuredDecimal PBAIIOCIFDP)
    {
      Decimal PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1M;
      PBAIIOCIFDP.hiddenValue = ObscuredDecimal.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredDecimal operator --(ObscuredDecimal PBAIIOCIFDP)
    {
      Decimal PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1M;
      PBAIIOCIFDP.hiddenValue = ObscuredDecimal.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredDecimal EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredDecimal EACOJCAGIDK) => EACOJCAGIDK.InternalDecrypt().Equals(this.InternalDecrypt());

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    [StructLayout(LayoutKind.Explicit)]
    private struct HAIHIJOPFAC
    {
      [FieldOffset(0)]
      public Decimal d;
      [FieldOffset(0)]
      public long l1;
      [FieldOffset(8)]
      public long l2;
      [FieldOffset(0)]
      public ACTkByte16 b16;
    }
  }
}
