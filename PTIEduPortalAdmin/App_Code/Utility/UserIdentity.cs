using System;
using System.Configuration;
using System.Collections;

/// <summary>
/// Summary description for UserIdentity
/// </summary>
public class UserIdentity
{
    
    private string m_Userid;
    
    private string m_Logintime;
    private string m_Computer;
    private string m_Hostname;
    private string m_IPAddress;
    private string m_FormNo;
    private string m_RegNo;
    private string m_MatricNo;
    private string m_PassportFile;
    private string m_CourseOfStudy;
    private bool m_SchoolFees = false ;
    //SourceApplication
    //SourceClass
    //MethodName

    public string CourseOfStudy
    {
        get { return m_CourseOfStudy; }
        set { m_CourseOfStudy = value; }
    }
    public string FormNo
    {
        get { return m_FormNo; }
        set { m_FormNo = value; }
    }
    public string RegNo
    {
        get { return m_RegNo; }
        set { m_RegNo = value; }
    }
    public string MatricNo
    {
        get { return m_MatricNo; }
        set { m_MatricNo = value; }
    }

    public string Userid
    {
        get { return m_Userid; }
        set { m_Userid = value; }
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
    public string PassportFile
    {
        get { return m_PassportFile; }
        set { m_PassportFile = value; }
    }
    public bool SchoolFees
    {
        get { return m_SchoolFees; }
        set { m_SchoolFees = value; }
    }
    public  UserIdentity()
	{
		//
		// TODO: Add constructor logic here
		//
    
	}


}

