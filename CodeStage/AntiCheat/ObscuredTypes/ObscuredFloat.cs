// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredFloat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Common;
using System;
using System.Runtime.InteropServices;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredFloat : IEquatable<ObscuredFloat>, IFormattable
  {
    private static int cryptoKey = 230887;
    
    private int currentCryptoKey;
    
    private ACTkByte4 hiddenValue;

    
    private int hiddenValueOld;
    
    private bool inited;

    private ObscuredFloat(ACTkByte4 PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.hiddenValueOld = 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int DCCMJMPNCDO) => ObscuredFloat.cryptoKey = DCCMJMPNCDO;

    public static int Encrypt(float PDGAOEAMDCL) => ObscuredFloat.Encrypt(PDGAOEAMDCL, ObscuredFloat.cryptoKey);

    public static int Encrypt(float PDGAOEAMDCL, int HDAJOEOLHGG)
    {
      ObscuredFloat.CLIIHCHMDEK cliihchmdek = new ObscuredFloat.CLIIHCHMDEK();
      cliihchmdek.f = PDGAOEAMDCL;
      cliihchmdek.i ^= HDAJOEOLHGG;
      return cliihchmdek.i;
    }

    private static ACTkByte4 InternalEncrypt(float PDGAOEAMDCL) => ObscuredFloat.InternalEncrypt(PDGAOEAMDCL, 0);

    private static ACTkByte4 InternalEncrypt(float PDGAOEAMDCL, int HDAJOEOLHGG)
    {
      int num = HDAJOEOLHGG;
      if (num == 0)
        num = ObscuredFloat.cryptoKey;
      ObscuredFloat.CLIIHCHMDEK cliihchmdek = new ObscuredFloat.CLIIHCHMDEK();
      cliihchmdek.f = PDGAOEAMDCL;
      cliihchmdek.i ^= num;
      return cliihchmdek.b4;
    }

    public static float Decrypt(int PDGAOEAMDCL) => ObscuredFloat.Decrypt(PDGAOEAMDCL, ObscuredFloat.cryptoKey);

    public static float Decrypt(int PDGAOEAMDCL, int HDAJOEOLHGG) => new ObscuredFloat.CLIIHCHMDEK()
    {
      i = (PDGAOEAMDCL ^ HDAJOEOLHGG)
    }.f;

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredFloat.cryptoKey)
        return;
      this.hiddenValue = ObscuredFloat.InternalEncrypt(this.InternalDecrypt(), ObscuredFloat.cryptoKey);
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      float PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = 0;
      }
      while (this.currentCryptoKey == 0);
      this.hiddenValue = ObscuredFloat.InternalEncrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredFloat.CLIIHCHMDEK()
      {
        b4 = this.hiddenValue
      }.i;
    }

    public void SetEncrypted(int LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = new ObscuredFloat.CLIIHCHMDEK()
      {
        i = LEBNEIHLING
      }.b4;
    }

    public float GetDecrypted() => this.InternalDecrypt();

    private float InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredFloat.cryptoKey;
        this.hiddenValue = ObscuredFloat.InternalEncrypt(0.0f);
        this.inited = true;
      }
      ObscuredFloat.CLIIHCHMDEK cliihchmdek = new ObscuredFloat.CLIIHCHMDEK();
      cliihchmdek.b4 = this.hiddenValue;
      cliihchmdek.i ^= this.currentCryptoKey;
      return cliihchmdek.f;
    }

    public static implicit operator ObscuredFloat(float PDGAOEAMDCL) => new ObscuredFloat(ObscuredFloat.InternalEncrypt(PDGAOEAMDCL));

    public static implicit operator float(ObscuredFloat PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredFloat operator ++(ObscuredFloat PBAIIOCIFDP)
    {
      float PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1f;
      PBAIIOCIFDP.hiddenValue = ObscuredFloat.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredFloat operator --(ObscuredFloat PBAIIOCIFDP)
    {
      float PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1f;
      PBAIIOCIFDP.hiddenValue = ObscuredFloat.InternalEncrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredFloat EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredFloat EACOJCAGIDK) => ((double) EACOJCAGIDK.InternalDecrypt()).Equals((double) this.InternalDecrypt());

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);

    [StructLayout(LayoutKind.Explicit)]
    private struct CLIIHCHMDEK
    {
      [FieldOffset(0)]
      public float f;
      [FieldOffset(0)]
      public int i;
      [FieldOffset(0)]
      public ACTkByte4 b4;
    }
  }
}
