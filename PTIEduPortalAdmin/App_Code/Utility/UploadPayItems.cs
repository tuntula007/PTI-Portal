using System;
using System.Data;
using System.Configuration;


/// <summary>
/// Summary description for UploadDataInfo
/// </summary>
/// 
public class UploadPayItems
{
    private string m_CurrentFile;
    private string m_Filename;
    private string m_Payitem;
    private string m_Payitemtype;
    private string m_Taxation;
    private string m_PayitemtCode;
    private string m_Uploader;
    private string m_FileTpye;
    



    public UploadPayItems()
    {
        CurrentFile = "";
        Filename = "";
        Payitem = "";
        PayitemtCode = "";
        Payitemtype = "";
        Taxation = "";
        Uploader = "";
        FileTpye = "";

    }

    public string CurrentFile
    {
        get { return m_CurrentFile; }
        set { m_CurrentFile = value; }
    }
    public string Filename
    {
        get { return m_Filename; }
        set { m_Filename = value; }
    }

    public string Payitem
    {
        get { return m_Payitem; }
        set { m_Payitem = value; }
    }

    public string Payitemtype
    {
        get { return m_Payitemtype; }
        set { m_Payitemtype = value; }
    }

    public string Taxation
    {
        get { return m_Taxation; }
        set { m_Taxation = value; }
    }

    public string PayitemtCode
    {
        get { return m_PayitemtCode; }
        set { m_PayitemtCode = value; }
    }
    public string Uploader
    {
        get { return m_Uploader; }
        set { m_Uploader = value; }
    }
    public string FileTpye
    {
        get { return m_FileTpye; }
        set { m_FileTpye = value; }
    }

}

