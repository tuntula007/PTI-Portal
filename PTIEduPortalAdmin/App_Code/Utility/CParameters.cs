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
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Summary description for CParameters
/// </summary>
public class CParameters
{
    private StringBuilder parameters = new StringBuilder();
    public CParameters()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Add(string pName, object pValue)
    {
        parameters.Append(pName);
        parameters.Append(";");
        parameters.Append(pValue);
        parameters.Append(";");

    }
    public override string ToString()
    {
       string pvar = parameters.ToString();
       return pvar.Substring(0, pvar.Length - 1);
    }
}
