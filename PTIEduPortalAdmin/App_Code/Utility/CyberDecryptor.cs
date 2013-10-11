using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for CyberDecryptor
/// </summary>
public class CyberDecryptor
{
	public CyberDecryptor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string Decryption(string item, string key)
    {

        byte[] b = Convert.FromBase64String(item);

        TripleDES des = CreateDES(key);
        ICryptoTransform ct = des.CreateDecryptor();
        byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
        return Encoding.Unicode.GetString(output);

    }  
    static TripleDES CreateDES(string key)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
        des.IV = new byte[des.BlockSize / 8];
        return des;
    }

}
