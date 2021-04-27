// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredBool : IEquatable<ObscuredBool>
  {
    private static byte cryptoKey = 215;
    
    private byte currentCryptoKey;
    
    private int hiddenValue;
    
    private bool inited;

    private ObscuredBool(int PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredBool.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte DCCMJMPNCDO) => ObscuredBool.cryptoKey = DCCMJMPNCDO;

    public static int Encrypt(bool PDGAOEAMDCL) => ObscuredBool.Encrypt(PDGAOEAMDCL, (byte) 0);

    public static int Encrypt(bool PDGAOEAMDCL, byte HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == (byte) 0)
        HDAJOEOLHGG = ObscuredBool.cryptoKey;
      return (PDGAOEAMDCL ? 213 : 181) ^ (int) HDAJOEOLHGG;
    }

    public static bool Decrypt(int PDGAOEAMDCL) => ObscuredBool.Decrypt(PDGAOEAMDCL, (byte) 0);

    public static bool Decrypt(int PDGAOEAMDCL, byte HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == (byte) 0)
        HDAJOEOLHGG = ObscuredBool.cryptoKey;
      PDGAOEAMDCL ^= (int) HDAJOEOLHGG;
      return PDGAOEAMDCL != 181;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredBool.cryptoKey)
        return;
      this.hiddenValue = ObscuredBool.Encrypt(this.InternalDecrypt(), ObscuredBool.cryptoKey);
      this.currentCryptoKey = ObscuredBool.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      bool PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (byte) 0;
      this.hiddenValue = ObscuredBool.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(int LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public bool GetDecrypted() => this.InternalDecrypt();

    private bool InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredBool.cryptoKey;
        this.hiddenValue = ObscuredBool.Encrypt(false);
        this.inited = true;
      }
      return (this.hiddenValue ^ (int) this.currentCryptoKey) != 181;
    }

    public static implicit operator ObscuredBool(bool PDGAOEAMDCL) => new ObscuredBool(ObscuredBool.Encrypt(PDGAOEAMDCL));

    public static implicit operator bool(ObscuredBool PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredBool EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredBool EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? this.hiddenValue == EACOJCAGIDK.hiddenValue : ObscuredBool.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredBool.Decrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();
  }
}
