// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredSByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredSByte : IEquatable<ObscuredSByte>, IFormattable
  {
    private static sbyte cryptoKey = 112;
    private sbyte currentCryptoKey;
    private sbyte hiddenValue;
    private bool inited;

    private ObscuredSByte(sbyte PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredSByte.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(sbyte DCCMJMPNCDO) => ObscuredSByte.cryptoKey = DCCMJMPNCDO;

    public static sbyte EncryptDecrypt(sbyte PDGAOEAMDCL) => ObscuredSByte.EncryptDecrypt(PDGAOEAMDCL, (sbyte) 0);

    public static sbyte EncryptDecrypt(sbyte PDGAOEAMDCL, sbyte HDAJOEOLHGG) => HDAJOEOLHGG == (sbyte) 0 ? (sbyte) ((int) PDGAOEAMDCL ^ (int) ObscuredSByte.cryptoKey) : (sbyte) ((int) PDGAOEAMDCL ^ (int) HDAJOEOLHGG);

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredSByte.cryptoKey)
        return;
      this.hiddenValue = ObscuredSByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredSByte.cryptoKey);
      this.currentCryptoKey = ObscuredSByte.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      sbyte PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = (sbyte) 0;
      }
      while (this.currentCryptoKey == (sbyte) 0);
      this.hiddenValue = ObscuredSByte.EncryptDecrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public sbyte GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(sbyte LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public sbyte GetDecrypted() => this.InternalDecrypt();

    private sbyte InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredSByte.cryptoKey;
        this.hiddenValue = ObscuredSByte.EncryptDecrypt((sbyte) 0);
        this.inited = true;
      }
      return ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredSByte(sbyte PDGAOEAMDCL) => new ObscuredSByte(ObscuredSByte.EncryptDecrypt(PDGAOEAMDCL));

    public static implicit operator sbyte(ObscuredSByte PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredSByte operator ++(ObscuredSByte PBAIIOCIFDP)
    {
      sbyte PDGAOEAMDCL = (sbyte) ((int) PBAIIOCIFDP.InternalDecrypt() + 1);
      PBAIIOCIFDP.hiddenValue = ObscuredSByte.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredSByte operator --(ObscuredSByte PBAIIOCIFDP)
    {
      sbyte PDGAOEAMDCL = (sbyte) ((int) PBAIIOCIFDP.InternalDecrypt() - 1);
      PBAIIOCIFDP.hiddenValue = ObscuredSByte.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredSByte EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredSByte EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredSByte.EncryptDecrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
