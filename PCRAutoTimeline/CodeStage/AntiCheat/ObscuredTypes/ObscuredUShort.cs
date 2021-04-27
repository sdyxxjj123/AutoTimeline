// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredUShort : IEquatable<ObscuredUShort>, IFormattable
  {
    private static ushort cryptoKey = 224;
    private ushort currentCryptoKey;
    private ushort hiddenValue;
    private bool inited;

    private ObscuredUShort(ushort PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ushort DCCMJMPNCDO) => ObscuredUShort.cryptoKey = DCCMJMPNCDO;

    public static ushort EncryptDecrypt(ushort PDGAOEAMDCL) => ObscuredUShort.EncryptDecrypt(PDGAOEAMDCL, (ushort) 0);

    public static ushort EncryptDecrypt(ushort PDGAOEAMDCL, ushort HDAJOEOLHGG) => HDAJOEOLHGG == (ushort) 0 ? (ushort) ((uint) PDGAOEAMDCL ^ (uint) ObscuredUShort.cryptoKey) : (ushort) ((uint) PDGAOEAMDCL ^ (uint) HDAJOEOLHGG);

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredUShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredUShort.cryptoKey);
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ushort PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (ushort) 0;
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public ushort GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ushort LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public ushort GetDecrypted() => this.InternalDecrypt();

    private ushort InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUShort.cryptoKey;
        this.hiddenValue = ObscuredUShort.EncryptDecrypt((ushort) 0);
        this.inited = true;
      }
      return ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredUShort(ushort PDGAOEAMDCL) => new ObscuredUShort(ObscuredUShort.EncryptDecrypt(PDGAOEAMDCL));

    public static implicit operator ushort(ObscuredUShort PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredUShort operator ++(ObscuredUShort PBAIIOCIFDP)
    {
      ushort PDGAOEAMDCL = (ushort) ((uint) PBAIIOCIFDP.InternalDecrypt() + 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredUShort.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredUShort operator --(ObscuredUShort PBAIIOCIFDP)
    {
      ushort PDGAOEAMDCL = (ushort) ((uint) PBAIIOCIFDP.InternalDecrypt() - 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredUShort.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredUShort EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredUShort EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUShort.EncryptDecrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
