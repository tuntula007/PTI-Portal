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

/// <summary>
/// Summary description for Numeric
/// </summary>
public class Numeric
{
	public Numeric()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool IsNumeric(string p)
    {
        bool rtn = false;

        string[] msg1 = p.Trim().Split(new Char[] { ' ', ':', ';', '-', '.', '#', '@', '~', '_', '+', '%', '"', '>', '!', '<', '*', '^', '?', ',', '|', '(', ')' });


        string k = "";
        foreach (string numb in msg1)
        {
            foreach (char c in numb)
            {
                if (c <= 0x39 && c >= 0x30)
                {
                    k = k + c.ToString();

                }


            }
            if (k.Trim() == numb)
            {
                k = "";
                rtn = true;
            }
            else
            {
                break;
            }

        }

        return rtn;
    }
}
