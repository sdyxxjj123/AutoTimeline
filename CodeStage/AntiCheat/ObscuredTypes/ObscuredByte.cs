// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredByte : IEquatable<ObscuredByte>, IFormattable
  {
    private static byte cryptoKey = 244;
    private byte currentCryptoKey;
    private byte hiddenValue;
    private bool inited;

    private ObscuredByte(byte PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredByte.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte DCCMJMPNCDO) => ObscuredByte.cryptoKey = DCCMJMPNCDO;

    public static byte EncryptDecrypt(byte PDGAOEAMDCL) => ObscuredByte.EncryptDecrypt(PDGAOEAMDCL, (byte) 0);

    public static byte EncryptDecrypt(byte PDGAOEAMDCL, byte HDAJOEOLHGG) => HDAJOEOLHGG == (byte) 0 ? (byte) ((uint) PDGAOEAMDCL ^ (uint) ObscuredByte.cryptoKey) : (byte) ((uint) PDGAOEAMDCL ^ (uint) HDAJOEOLHGG);

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredByte.cryptoKey)
        return;
      this.hiddenValue = ObscuredByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredByte.cryptoKey);
      this.currentCryptoKey = ObscuredByte.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      byte PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (byte) 0;
      this.hiddenValue = ObscuredByte.EncryptDecrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public byte GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(byte LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public byte GetDecrypted() => this.InternalDecrypt();

    private byte InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredByte.cryptoKey;
        this.hiddenValue = ObscuredByte.EncryptDecrypt((byte) 0);
        this.inited = true;
      }
      return ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredByte(byte PDGAOEAMDCL) => new ObscuredByte(ObscuredByte.EncryptDecrypt(PDGAOEAMDCL));

    public static implicit operator byte(ObscuredByte PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredByte operator ++(ObscuredByte PBAIIOCIFDP)
    {
      byte PDGAOEAMDCL = (byte) ((uint) PBAIIOCIFDP.InternalDecrypt() + 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredByte.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredByte operator --(ObscuredByte PBAIIOCIFDP)
    {
      byte PDGAOEAMDCL = (byte) ((uint) PBAIIOCIFDP.InternalDecrypt() - 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredByte.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredByte EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredByte EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredByte.EncryptDecrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
