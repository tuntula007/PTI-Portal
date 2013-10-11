using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for CyberEncryptor
/// </summary>
public class CyberEncryptor
{
    public CyberEncryptor()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static byte[] encypt(string Item, string key)
    {
        //string finalItem = "";

        TripleDES des = CreateDES(key);
        ICryptoTransform ct = des.CreateEncryptor();
        byte[] input =Encoding.Unicode.GetBytes(Item);
        return ct.TransformFinalBlock(input, 0, input.Length);
        //return finalItem;
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
