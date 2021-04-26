// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredInt : IEquatable<ObscuredInt>, IFormattable
  {
    private static int cryptoKey = 444444;
    
    private int currentCryptoKey;
    
    private int hiddenValue;
    
    private bool inited;

    private ObscuredInt(int PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredInt.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int DCCMJMPNCDO) => ObscuredInt.cryptoKey = DCCMJMPNCDO;

    public static int Encrypt(int PDGAOEAMDCL) => ObscuredInt.Encrypt(PDGAOEAMDCL, 0);

    public static int Encrypt(int PDGAOEAMDCL, int HDAJOEOLHGG) => HDAJOEOLHGG == 0 ? PDGAOEAMDCL ^ ObscuredInt.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public static int Decrypt(int PDGAOEAMDCL) => ObscuredInt.Decrypt(PDGAOEAMDCL, 0);

    public static int Decrypt(int PDGAOEAMDCL, int HDAJOEOLHGG) => HDAJOEOLHGG == 0 ? PDGAOEAMDCL ^ ObscuredInt.cryptoKey : PDGAOEAMDCL ^ HDAJOEOLHGG;

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredInt.cryptoKey)
        return;
      this.hiddenValue = ObscuredInt.Encrypt(this.InternalDecrypt(), ObscuredInt.cryptoKey);
      this.currentCryptoKey = ObscuredInt.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      this.hiddenValue = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = 0;
      }
      while (this.currentCryptoKey == 0);
      this.hiddenValue = ObscuredInt.Encrypt(this.hiddenValue, this.currentCryptoKey);
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

    public int GetDecrypted() => this.InternalDecrypt();

    private int InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredInt.cryptoKey;
        this.hiddenValue = ObscuredInt.Encrypt(0);
        this.inited = true;
      }
      return ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public int CompareTo(ObscuredInt IBPDFBHKABH) => this.GetDecrypted() - IBPDFBHKABH.GetDecrypted();

    public static implicit operator ObscuredInt(int PDGAOEAMDCL) => new ObscuredInt(ObscuredInt.Encrypt(PDGAOEAMDCL));

    public static implicit operator int(ObscuredInt PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static implicit operator ObscuredFloat(ObscuredInt PDGAOEAMDCL) => (ObscuredFloat) (float) PDGAOEAMDCL.InternalDecrypt();

    public static implicit operator ObscuredDouble(ObscuredInt PDGAOEAMDCL) => (ObscuredDouble) (double) PDGAOEAMDCL.InternalDecrypt();

    public static explicit operator ObscuredUInt(ObscuredInt PDGAOEAMDCL) => (ObscuredUInt) (uint) PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredInt operator ++(ObscuredInt PBAIIOCIFDP)
    {
      int PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() + 1;
      PBAIIOCIFDP.hiddenValue = ObscuredInt.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredInt operator --(ObscuredInt PBAIIOCIFDP)
    {
      int PDGAOEAMDCL = PBAIIOCIFDP.InternalDecrypt() - 1;
      PBAIIOCIFDP.hiddenValue = ObscuredInt.Encrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredInt EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredInt EACOJCAGIDK) => this.currentCryptoKey == EACOJCAGIDK.currentCryptoKey ? this.hiddenValue == EACOJCAGIDK.hiddenValue : ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredInt.Decrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
