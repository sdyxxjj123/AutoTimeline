// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredULong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredULong : IEquatable<ObscuredULong>, IFormattable
  {
    private static ulong cryptoKey = 444443;
    
    private ulong currentCryptoKey;
    
    private ulong hiddenValue;
    
    private bool inited;

    private ObscuredULong(ulong PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredULong.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ulong DCCMJMPNCDO) => ObscuredULong.cryptoKey = DCCMJMPNCDO;

    public static ulong Encrypt(ulong PDGAOEAMDCL) => ObscuredULong.Encrypt(PDGAOEAMDCL, 0UL);

    public static ulong Decrypt(ulong PDGAOEAMDCL) => ObscuredULong.Decrypt(PDGAOEAMDCL, 0UL);

    public static ulong Encrypt(ulong PDGAOEAMDCL, ulong HDAJOEOLHGG) => HDAJOEOLHGG == 0UL ? PDGAOEAMDCL ^ ObscuredULong.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public static ulong Decrypt(ulong PDGAOEAMDCL, ulong HDAJOEOLHGG) => HDAJOEOLHGG == 0UL ? PDGAOEAMDCL ^ ObscuredULong.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public void ApplyNewCryptoKey()
    {
      if ((long) this.currentCryptoKey == (long) ObscuredULong.cryptoKey)
        return;
      this.hiddenValue = ObscuredULong.Encrypt(this.InternalDecrypt(), ObscuredULong.cryptoKey);
      this.currentCryptoKey = ObscuredULong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ulong PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (ulong) 0;
      this.hiddenValue = ObscuredULong.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public ulong GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ulong LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public ulong GetDecrypted() => this.InternalDecrypt();

    private ulong InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredULong.cryptoKey;
        this.hiddenValue = ObscuredULong.Encrypt(0UL);
        this.inited = true;
      }
      return ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredULong(ulong PDGAOEAMDCL) => new ObscuredULong(ObscuredULong.Encrypt(PDGAOEAMDCL));

    public static implicit operator ulong(ObscuredULong PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredULong operator ++(ObscuredULong PBAIIOCIFDP)
    {
      ulong PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1UL;
      PBAIIOCIFDP.hiddenValue = ObscuredULong.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredULong operator --(ObscuredULong PBAIIOCIFDP)
    {
      ulong PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1UL;
      PBAIIOCIFDP.hiddenValue = ObscuredULong.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredULong EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredULong EACOJCAGIDK) => (long) this.currentCryptoKey == (long) EACOJCAGIDK.currentCryptoKey ? (long) this.hiddenValue == (long) EACOJCAGIDK.hiddenValue : (long) ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey) == (long) ObscuredULong.Decrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
