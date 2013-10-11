using System;

public class ApplicantPostQualification
{
    /// <summary>
    /// Default Contructor
    /// <summary>
    public ApplicantPostQualification()
    { }


    public int Srn
    {
        get { return _Srn; }
        set { _Srn = value; }
    }
    private int _Srn;

    public string RegNo
    {
        get { return _RegNo; }
        set { _RegNo = value; }
    }
    private string _RegNo;

    public string PostMatric
    {
        get { return _PostMatric; }
        set { _PostMatric = value; }
    }
    private string _PostMatric;

    public string CourseName
    {
        get { return _CourseName; }
        set { _CourseName = value; }
    }
    private string _CourseName = "";
    public string CourseCode
    {
        get { return _CourseCode; }
        set { _CourseCode = value; }
    }
    private string _CourseCode = "";
    public string CourseGrade
    {
        get { return _CourseGrade; }
        set { _CourseGrade = value; }
    }
    private string _CourseGrade;

    public int CourseGradePoint
    {
        get { return _CourseGradePoint; }
        set { _CourseGradePoint = value; }
    }
    private int _CourseGradePoint;

    public string QualyType
    {
        get { return _QualyType; }
        set { _QualyType = value; }
    }
    private string _QualyType = "";

    public string Institution
    {
        get { return _Institution; }
        set { _Institution = value; }
    }
    private string _Institution;

    public string ModeOfStudy
    {
        get { return _ModeOfStudy; }
        set { _ModeOfStudy = value; }
    }
    private string _ModeOfStudy;

    public string QualYear
    {
        get { return _QualYear; }
        set { _QualYear = value; }
    }
    private string _QualYear;

    public ApplicantPostQualification(

        int Srn,
        string RegNo,
        string PostMatric,
        string CourseName,
        string CourseGrade,
        string ExamDate,
        string QualyType,
        string QualYear,
        string Institution,
        int CourseGradePoint)
    {


        this._Srn = Srn;
        this._RegNo = RegNo;
        this._PostMatric = PostMatric;
        this._CourseName = CourseName;
        this._CourseGrade = CourseGrade;
        this._CourseGradePoint = CourseGradePoint;
        this._QualyType = QualyType;
        this._Institution = Institution;
        this._QualYear = QualYear;
    }
}
