using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for SchPin
/// </summary>
public class SchPin
{
    private string serialno = "";
    private string pinno = "";
    private string pinstatus = "";
    private string level="";
    private string prog = "";
    private double amount = 0;
    private string isindigene = "";
	public SchPin()
	{
	}

    public string SerialNo
    {
        get { return serialno; }
        set { serialno = value; }
    }

    public string PinNumber
    {
        get { return pinno; }
        set { pinno = value; }
    }

    public string PinStatus
    {
        get { return pinstatus; }
        set { pinstatus = value; }
    }

    public string AcademicLevel
    {
        get { return level; }
        set { level = value; }
    }

    public string Programme
    {
        get { return prog; }
        set { prog = value; }
    }

    public double Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public string isIndigene
    {
        get { return isindigene; }
        set { isindigene = value; }
    }
}
