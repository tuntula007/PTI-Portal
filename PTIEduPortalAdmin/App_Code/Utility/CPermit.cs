using System; 
using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Collections;


/// <summary>
/// Summary description for CPermit
/// </summary>
public class CPermit
{
    private ArrayList m_Access = new ArrayList();
    private string m_Userid;
    private string m_Usergroup;
    private string m_Logintime;
    private string m_Computer;
    private string m_Hostname;
    private string m_IPAddress;
    private string m_Direction;
    private string m_MsgType;
    private string m_SourceApplication;
    private string m_SourceClass;
    private string m_MethodName;
    private string m_Password;

    //SourceApplication
    //SourceClass
    //MethodName

    public ArrayList Access
    {
        get { return m_Access; }
        set { m_Access = value; }
    }

    public string Direction
    {
        get { return m_Direction; }
        set { m_Direction = value; }
    }

    public string Userid
    {
        get { return m_Userid; }
        set { m_Userid = value; }
    }
    public string Usergroup
    {
        get { return m_Usergroup; }
        set { m_Usergroup = value; }
    }
    public string Logintime
    {
        get { return m_Logintime; }
        set { m_Logintime = value; }
    }
    public string Computer
    {
        get { return m_Computer; }
        set { m_Computer = value; }
    }

    public string Hostname
    {
        get { return m_Hostname; }
        set { m_Hostname = value; }
    }
    public string IPAddress
    {
        get { return m_IPAddress; }
        set { m_IPAddress = value; }
    }
    //m_MsgType

    public string MsgType
    {
        get { return m_MsgType; }
        set { m_MsgType = value; }
    }
    //private string m_SourceApplication;
    public string SourceApplication
    {
        get { return m_SourceApplication; }
        set { m_SourceApplication = value; }
    }
    //private string m_SourceClass;
    public string SourceClass
    {
        get { return m_SourceClass; }
        set { m_SourceClass = value; }
    }
    //private string m_MethodName;
    public string MethodName
    {
        get { return m_MethodName; }
        set { m_MethodName = value; }
    }
    public string Password
    {
        get { return m_Password; }
        set { m_Password = value; }
    }

    public CPermit()
	{
		//
		// TODO: Add constructor logic here
		//
        MsgType = "INFO";
	}


}
