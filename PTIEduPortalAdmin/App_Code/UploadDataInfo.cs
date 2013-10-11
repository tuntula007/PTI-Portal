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
//using System.Xml.Linq;

/// <summary>
/// Summary description for UploadDataInfo
/// </summary>
public class UploadDataInfo
{
    private string m_CurrentFile;
    private string m_Filename;
    private string m_EntryMode;
    private string m_Session;
    private string m_FileTpye;
    private string m_Purpose;
    private string m_Uploader;
    private string m_Faculty;
    private string m_Dept;
    private string m_CourseOfStudy;
    private string m_OperationType;
    private int m_DeptId;
    private int m_FacultyId;
    private int m_CourseOfStudyId;
    private string m_SchNicName;
    private string m_Programme;
    private string m_Honours;
    private string m_Duration;
    private string m_ModeOfStudy;
    private string m_AdmisssionType;
    private string m_StartLevel;
    private string m_Batch;
    private string m_Semester;


    public UploadDataInfo()
    {
        CurrentFile = "";
        Filename = "";
        EntryMode = "";
        Session = "";
        FileTpye = "";
        Purpose = "";
        Uploader = "";
        Faculty = "";
        Dept = "";
        OperationType = "";
        CourseOfStudy = "";
        SchNicName = "";
        Programme = "";
        Honours = "";
        Duration = "";
        ModeOfStudy = "";
        AdmisssionType = "";
        StartLevel = "";
        Batch = "";
        Semester = "";
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
    public string EntryMode
    {
        get { return m_EntryMode; }
        set { m_EntryMode = value; }
    }
    public string Session
    {
        get { return m_Session; }
        set { m_Session = value; }
    }
    public string FileTpye
    {
        get { return m_FileTpye; }
        set { m_FileTpye = value; }
    }
    public string Purpose
    {
        get { return m_Purpose; }
        set { m_Purpose = value; }
    }
    public string Uploader
    {
        get { return m_Uploader; }
        set { m_Uploader = value; }
    }
    public string Faculty
    {
        get { return m_Faculty; }
        set { m_Faculty = value; }
    }
    public string Dept
    {
        get { return m_Dept; }
        set { m_Dept = value; }
    }
    public string OperationType
    {
        get { return m_OperationType; }
        set { m_OperationType = value; }
    }
    //m_CourseOfStudy
    public string CourseOfStudy
    {
        get { return m_CourseOfStudy; }
        set { m_CourseOfStudy = value; }
    }

    //private int m_DeptId;
    //private int m_FacultyId;
    public int FacultyId
    {
        get { return m_FacultyId; }
        set { m_FacultyId = value; }
    }
    public int DeptId
    {
        get { return m_DeptId; }
        set { m_DeptId = value; }
    }
    //m_CourseOfStudyId

    public int CourseOfStudyId
    {
        get { return m_CourseOfStudyId; }
        set { m_CourseOfStudyId = value; }
    }
    //m_SchNicName
    public string SchNicName
    {
        get { return m_SchNicName; }
        set { m_SchNicName = value; }
    }
    //Programme = "";
    public string Programme
    {
        get { return m_Programme; }
        set { m_Programme = value; }
    }
    //Honours = "";
    public string Honours
    {
        get { return m_Honours; }
        set { m_Honours = value; }
    }
    //Duration = "";
    public string Duration
    {
        get { return m_Duration; }
        set { m_Duration = value; }
    }
    //ModeOfStudy
    public string ModeOfStudy
    {
        get { return m_ModeOfStudy; }
        set { m_ModeOfStudy = value; }
    }
    //m_AdmisssionType
    public string AdmisssionType
    {
        get { return m_AdmisssionType; }
        set { m_AdmisssionType = value; }
    }
    public string StartLevel
    {
        get { return m_StartLevel; }
        set { m_StartLevel = value; }
    }
    //m_Batch
    public string Batch
    {
        get { return m_Batch; }
        set { m_Batch = value; }
    }
    //m_Semester;
    public string Semester
    {
        get { return m_Semester; }
        set { m_Semester = value; }
    }
}
