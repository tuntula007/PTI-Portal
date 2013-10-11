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
/// Summary description for CFileUploading
/// </summary>
public class CFileUploading
{
    private FileUpload m_FUpload;
    private string m_TicketID;
    private int m_ControlNumber;
    
    public CFileUploading()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public FileUpload FUpload
    {
        get { return m_FUpload; }
        set { m_FUpload = value; }


    }

    public string TicketID
    {
        get { return m_TicketID; }
        set { m_TicketID = value; }


    }

    public int ControlNumber
    {
        get { return m_ControlNumber; }
        set { m_ControlNumber = value; }


    }

}
