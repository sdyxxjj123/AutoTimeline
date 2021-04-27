// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System;


namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public sealed class ObscuredString
  {
    private static string cryptoKey = "4441";
    
    private string currentCryptoKey;
    
    private byte[] hiddenValue;
    
    private bool inited;

    private ObscuredString()
    {
    }

    private ObscuredString(byte[] PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredString.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(string DCCMJMPNCDO) => ObscuredString.cryptoKey = DCCMJMPNCDO;

    public static string EncryptDecrypt(string PDGAOEAMDCL) => ObscuredString.EncryptDecrypt(PDGAOEAMDCL, "");

    public static string EncryptDecrypt(string PDGAOEAMDCL, string HDAJOEOLHGG)
    {
      if (string.IsNullOrEmpty(PDGAOEAMDCL))
        return "";
      if (string.IsNullOrEmpty(HDAJOEOLHGG))
        HDAJOEOLHGG = ObscuredString.cryptoKey;
      int length1 = HDAJOEOLHGG.Length;
      int length2 = PDGAOEAMDCL.Length;
      char[] chArray = new char[length2];
      for (int index = 0; index < length2; ++index)
        chArray[index] = (char) ((uint) PDGAOEAMDCL[index] ^ (uint) HDAJOEOLHGG[index % length1]);
      return new string(chArray);
    }

    public void ApplyNewCryptoKey()
    {
      if (!(this.currentCryptoKey != ObscuredString.cryptoKey))
        return;
      this.hiddenValue = ObscuredString.InternalEncrypt(this.InternalDecrypt());
      this.currentCryptoKey = ObscuredString.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      string PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = "";
      this.hiddenValue = ObscuredString.InternalEncrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public string GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return ObscuredString.GetString(this.hiddenValue);
    }

    public void SetEncrypted(string LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = ObscuredString.GetBytes(LEBNEIHLING);
    }

    public string GetDecrypted() => this.InternalDecrypt();

    private static byte[] InternalEncrypt(string PDGAOEAMDCL) => ObscuredString.InternalEncrypt(PDGAOEAMDCL, ObscuredString.cryptoKey);

    private static byte[] InternalEncrypt(string PDGAOEAMDCL, string HDAJOEOLHGG) => ObscuredString.GetBytes(ObscuredString.EncryptDecrypt(PDGAOEAMDCL, HDAJOEOLHGG));

    private string InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredString.cryptoKey;
        this.hiddenValue = ObscuredString.InternalEncrypt("");
        this.inited = true;
      }
      string HDAJOEOLHGG = this.currentCryptoKey;
      if (string.IsNullOrEmpty(HDAJOEOLHGG))
        HDAJOEOLHGG = ObscuredString.cryptoKey;
      return ObscuredString.EncryptDecrypt(ObscuredString.GetString(this.hiddenValue), HDAJOEOLHGG);
    }

    public int Length => this.hiddenValue.Length / 2;

    public static implicit operator ObscuredString(string PDGAOEAMDCL) => PDGAOEAMDCL == null ? (ObscuredString) null : new ObscuredString(ObscuredString.InternalEncrypt(PDGAOEAMDCL));

    public static implicit operator string(ObscuredString PDGAOEAMDCL) => PDGAOEAMDCL == (ObscuredString) null ? (string) null : PDGAOEAMDCL.InternalDecrypt();

    public override string ToString() => this.InternalDecrypt();

    public static bool operator ==(ObscuredString IPJGCOBNHLB, ObscuredString IMMPDMOKFGC)
    {
      if ((object) IPJGCOBNHLB == (object) IMMPDMOKFGC)
        return true;
      if ((object) IPJGCOBNHLB == null || (object) IMMPDMOKFGC == null)
        return false;
      return IPJGCOBNHLB.currentCryptoKey == IMMPDMOKFGC.currentCryptoKey ? ObscuredString.ArraysEquals(IPJGCOBNHLB.hiddenValue, IMMPDMOKFGC.hiddenValue) : string.Equals(IPJGCOBNHLB.InternalDecrypt(), IMMPDMOKFGC.InternalDecrypt());
    }

    public static bool operator !=(ObscuredString IPJGCOBNHLB, ObscuredString IMMPDMOKFGC) => !(IPJGCOBNHLB == IMMPDMOKFGC);

    public override bool Equals(object EACOJCAGIDK) => (object) (EACOJCAGIDK as ObscuredString) != null && this.Equals((ObscuredString) EACOJCAGIDK);

    public bool Equals(ObscuredString PDGAOEAMDCL)
    {
      if (PDGAOEAMDCL == (ObscuredString) null)
        return false;
      return this.currentCryptoKey == PDGAOEAMDCL.currentCryptoKey ? ObscuredString.ArraysEquals(this.hiddenValue, PDGAOEAMDCL.hiddenValue) : string.Equals(this.InternalDecrypt(), PDGAOEAMDCL.InternalDecrypt());
    }

    public bool Equals(ObscuredString PDGAOEAMDCL, StringComparison DAOPCOOFOAB) => !(PDGAOEAMDCL == (ObscuredString) null) && string.Equals(this.InternalDecrypt(), PDGAOEAMDCL.InternalDecrypt(), DAOPCOOFOAB);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    private static byte[] GetBytes(string JEKJHPPCDNC)
    {
      byte[] numArray = new byte[JEKJHPPCDNC.Length * 2];
      Buffer.BlockCopy((Array) JEKJHPPCDNC.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    private static string GetString(byte[] DOJDGDHBMIP)
    {
      char[] chArray = new char[DOJDGDHBMIP.Length / 2];
      Buffer.BlockCopy((Array) DOJDGDHBMIP, 0, (Array) chArray, 0, DOJDGDHBMIP.Length);
      return new string(chArray);
    }

    private static bool ArraysEquals(byte[] PNKAIPKJAMC, byte[] IAHILDEEMCC)
    {
      if (PNKAIPKJAMC == IAHILDEEMCC)
        return true;
      if (PNKAIPKJAMC == null || IAHILDEEMCC == null || PNKAIPKJAMC.Length != IAHILDEEMCC.Length)
        return false;
      for (int index = 0; index < PNKAIPKJAMC.Length; ++index)
      {
        if ((int) PNKAIPKJAMC[index] != (int) IAHILDEEMCC[index])
          return false;
      }
      return true;
    }
  }
}
