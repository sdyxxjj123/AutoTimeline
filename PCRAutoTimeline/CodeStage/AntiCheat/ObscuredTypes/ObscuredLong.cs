// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredLong : IEquatable<ObscuredLong>, IFormattable
  {
    private static long cryptoKey = 444442;
    
    private long currentCryptoKey;
    
    private long hiddenValue;
    
    private bool inited;

    private ObscuredLong(long PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredLong.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long DCCMJMPNCDO) => ObscuredLong.cryptoKey = DCCMJMPNCDO;

    public static long Encrypt(long PDGAOEAMDCL) => ObscuredLong.Encrypt(PDGAOEAMDCL, 0L);

    public static long Decrypt(long PDGAOEAMDCL) => ObscuredLong.Decrypt(PDGAOEAMDCL, 0L);

    public static long Encrypt(long PDGAOEAMDCL, long HDAJOEOLHGG) => HDAJOEOLHGG == 0L ? PDGAOEAMDCL ^ ObscuredLong.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public static long Decrypt(long PDGAOEAMDCL, long HDAJOEOLHGG) => HDAJOEOLHGG == 0L ? PDGAOEAMDCL ^ ObscuredLong.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredLong.cryptoKey)
        return;
      this.hiddenValue = ObscuredLong.Encrypt(this.InternalDecrypt(), ObscuredLong.cryptoKey);
      this.currentCryptoKey = ObscuredLong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      long PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = (long) 0;
      }
      while (this.currentCryptoKey == 0L);
      this.hiddenValue = ObscuredLong.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public long GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(long LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public long GetDecrypted() => this.InternalDecrypt();

    private long InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredLong.cryptoKey;
        this.hiddenValue = ObscuredLong.Encrypt(0L);
        this.inited = true;
      }
      return ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredLong(long PDGAOEAMDCL) => new ObscuredLong(ObscuredLong.Encrypt(PDGAOEAMDCL));

    public static implicit operator long(ObscuredLong PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredLong operator ++(ObscuredLong PBAIIOCIFDP)
    {
      long PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1L;
      PBAIIOCIFDP.hiddenValue = ObscuredLong.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredLong operator --(ObscuredLong PBAIIOCIFDP)
    {
      long PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1L;
      PBAIIOCIFDP.hiddenValue = ObscuredLong.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredLong EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredLong EACOJCAGIDK) => this.currentCryptoKey == EACOJCAGIDK.currentCryptoKey ? this.hiddenValue == EACOJCAGIDK.hiddenValue : ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredLong.Decrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
