﻿using System;

public class PostSecAppEntryQualification
{
    /// <summary>
    /// Default Contructor
    /// <summary>
    public PostSecAppEntryQualification()
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

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    private string _UserName;
    public string NameOfSchool
    {
        get { return _NameOfSchool; }
        set { _NameOfSchool = value; }
    }
    private string _NameOfSchool;
    public string ExamName
    {
        get { return _ExamName; }
        set { _ExamName = value; }
    }
    private string _ExamName;

    public string ExamRegNo
    {
        get { return _ExamRegNo; }
        set { _ExamRegNo = value; }
    }
    private string _ExamRegNo;

    public string ExamDate
    {
        get { return _ExamDate; }
        set { _ExamDate = value; }
    }
    private string _ExamDate;

    public string Sitting
    {
        get { return _Sitting; }
        set { _Sitting = value; }
    }
    private string _Sitting;

    public string SubjectName1
    {
        get { return _SubjectName1; }
        set { _SubjectName1 = value; }
    }
    private string _SubjectName1;

    public string SubjectGrade1
    {
        get { return _SubjectGrade1; }
        set { _SubjectGrade1 = value; }
    }
    private string _SubjectGrade1;

    public string SubjectName2
    {
        get { return _SubjectName2; }
        set { _SubjectName2 = value; }
    }
    private string _SubjectName2;

    public string SubjectGrade2
    {
        get { return _SubjectGrade2; }
        set { _SubjectGrade2 = value; }
    }
    private string _SubjectGrade2;

    public string SubjectName3
    {
        get { return _SubjectName3; }
        set { _SubjectName3 = value; }
    }
    private string _SubjectName3;

    public string SubjectGrade3
    {
        get { return _SubjectGrade3; }
        set { _SubjectGrade3 = value; }
    }
    private string _SubjectGrade3;

    public string SubjectName4
    {
        get { return _SubjectName4; }
        set { _SubjectName4 = value; }
    }
    private string _SubjectName4;

    public string SubjectGrade4
    {
        get { return _SubjectGrade4; }
        set { _SubjectGrade4 = value; }
    }
    private string _SubjectGrade4;

    public string SubjectName5
    {
        get { return _SubjectName5; }
        set { _SubjectName5 = value; }
    }
    private string _SubjectName5;

    public string SubjectGrade5
    {
        get { return _SubjectGrade5; }
        set { _SubjectGrade5 = value; }
    }
    private string _SubjectGrade5;

    public string SubjectName6
    {
        get { return _SubjectName6; }
        set { _SubjectName6 = value; }
    }
    private string _SubjectName6;

    public string SubjectGrade6
    {
        get { return _SubjectGrade6; }
        set { _SubjectGrade6 = value; }
    }
    private string _SubjectGrade6;

    public string SubjectName7
    {
        get { return _SubjectName7; }
        set { _SubjectName7 = value; }
    }
    private string _SubjectName7;

    public string SubjectGrade7
    {
        get { return _SubjectGrade7; }
        set { _SubjectGrade7 = value; }
    }
    private string _SubjectGrade7;

    public string ModeOfStudy
    {
        get { return _ModeOfStudy; }
        set { _ModeOfStudy = value; }
    }
    private string _ModeOfStudy;

    public PostSecAppEntryQualification(

        int Srn,
        string RegNo,
        string UserName,
        string ExamName,
        string ExamRegNo,
        string ExamDate,
        string Sitting,
        string SubjectName1,
        string SubjectGrade1,
        string SubjectName2,
        string SubjectGrade2,
        string SubjectName3,
        string SubjectGrade3,
        string SubjectName4,
        string SubjectGrade4,
        string SubjectName5,
        string SubjectGrade5,
        string SubjectName6,
        string SubjectGrade6,
        string SubjectName7,
        string SubjectGrade7,
        string ModeOfStudy)
    {


        this._Srn = Srn;
        this._RegNo = RegNo;
        this._UserName = UserName;
        this._ExamName = ExamName;
        this._ExamRegNo = ExamRegNo;
        this._ExamDate = ExamDate;
        this._Sitting = Sitting;
        this._SubjectName1 = SubjectName1;
        this._SubjectGrade1 = SubjectGrade1;
        this._SubjectName2 = SubjectName2;
        this._SubjectGrade2 = SubjectGrade2;
        this._SubjectName3 = SubjectName3;
        this._SubjectGrade3 = SubjectGrade3;
        this._SubjectName4 = SubjectName4;
        this._SubjectGrade4 = SubjectGrade4;
        this._SubjectName5 = SubjectName5;
        this._SubjectGrade5 = SubjectGrade5;
        this._SubjectName6 = SubjectName6;
        this._SubjectGrade6 = SubjectGrade6;
        this._SubjectName7 = SubjectName7;
        this._SubjectGrade7 = SubjectGrade7;
        this._ModeOfStudy = ModeOfStudy;
    }
}