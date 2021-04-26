// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredUInt : IEquatable<ObscuredUInt>, IFormattable
  {
    private static uint cryptoKey = 240513;
    
    private uint currentCryptoKey;
    
    private uint hiddenValue;
    
    private bool inited;

    private ObscuredUInt(uint PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredUInt.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(uint DCCMJMPNCDO) => ObscuredUInt.cryptoKey = DCCMJMPNCDO;

    public static uint Encrypt(uint PDGAOEAMDCL) => ObscuredUInt.Encrypt(PDGAOEAMDCL, 0U);

    public static uint Decrypt(uint PDGAOEAMDCL) => ObscuredUInt.Decrypt(PDGAOEAMDCL, 0U);

    public static uint Encrypt(uint PDGAOEAMDCL, uint HDAJOEOLHGG) => HDAJOEOLHGG == 0U ? PDGAOEAMDCL ^ ObscuredUInt.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public static uint Decrypt(uint PDGAOEAMDCL, uint HDAJOEOLHGG) => HDAJOEOLHGG == 0U ? PDGAOEAMDCL ^ ObscuredUInt.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredUInt.cryptoKey)
        return;
      this.hiddenValue = ObscuredUInt.Encrypt(this.InternalDecrypt(), ObscuredUInt.cryptoKey);
      this.currentCryptoKey = ObscuredUInt.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      uint PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (uint) 0;
      this.hiddenValue = ObscuredUInt.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public uint GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(uint LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public uint GetDecrypted() => this.InternalDecrypt();

    private uint InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUInt.cryptoKey;
        this.hiddenValue = ObscuredUInt.Encrypt(0U);
        this.inited = true;
      }
      return ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredUInt(uint PDGAOEAMDCL) => new ObscuredUInt(ObscuredUInt.Encrypt(PDGAOEAMDCL));

    public static implicit operator uint(ObscuredUInt PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static explicit operator ObscuredInt(ObscuredUInt PDGAOEAMDCL) => (ObscuredInt) (int) PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredUInt operator ++(ObscuredUInt PBAIIOCIFDP)
    {
      uint PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1U;
      PBAIIOCIFDP.hiddenValue = ObscuredUInt.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredUInt operator --(ObscuredUInt PBAIIOCIFDP)
    {
      uint PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1U;
      PBAIIOCIFDP.hiddenValue = ObscuredUInt.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredUInt EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredUInt EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUInt.Decrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
