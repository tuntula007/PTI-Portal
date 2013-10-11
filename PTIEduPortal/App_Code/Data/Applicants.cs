using System;

public class Applicants
{
    /// <summary>
    /// Default Contructor
    /// <summary>
    public Applicants()
    { }

    private string _myPostProgramme;
    public string MyPostProgramme
    {
        get { return _myPostProgramme; }
        set { _myPostProgramme = value; }
    }


    private string _myPostMatric;
    public string MyPostMatric
    {
        get { return _myPostMatric; }
        set { _myPostMatric = value; }
    }


    private string _myCourseGrade;
    public string MyCourseGrade
    {
        get { return _myCourseGrade; }
        set { _myCourseGrade = value; }
    }

    private string _myCourseName;
    public string MyCourseName
    {
        get { return _myCourseName; }
        set { _myCourseName = value; }
    }

    private string _myInstitution;
    public string MyInstitution
    {
        get { return _myInstitution; }
        set { _myInstitution = value; }
    }

    private string _myQualYear;
    public string MyQualYear
    {
        get { return _myQualYear; }
        set { _myQualYear = value; }
    }
    private string _myOtherSchsInfo;
    public string MyOtherSchsInfo
    {
        get { return _myOtherSchsInfo; }
        set { _myOtherSchsInfo = value; }
    }



    private string _formNumber;
    public string FormNumber
    {
        get { return _formNumber; }
        set { _formNumber = value; }
    }
    private string _FirstDepartment;
    public string FirstDepartment
    {
        get { return _FirstDepartment; }
        set { _FirstDepartment = value; }
    }
    private string _SecondDepartment;
    public string SecondDepartment
    {
        get { return _SecondDepartment; }
        set { _SecondDepartment = value; }
    }

    private string _ThirdDepartment;
    public string ThirdDepartment
    {
        get { return _ThirdDepartment; }
        set { _ThirdDepartment = value; }
    }

    private string _OtherNational;
    public string OtherNational
    {
        get { return _OtherNational; }
        set { _OtherNational = value; }
    }
    public int ApplicantId
    {
        get { return _ApplicantId; }
        set { _ApplicantId = value; }
    }
    private int _ApplicantId;

    private string _CourseofStudy1;
    public string CourseofStudy1
    {
        get { return _CourseofStudy1; }
        set { _CourseofStudy1 = value; }
    }

    private string _CourseofStudy2;
    public string CourseofStudy2
    {
        get { return _CourseofStudy2; }
        set { _CourseofStudy2 = value; }
    }


    private string _CourseofStudy3;
    public string CourseofStudy3
    {
        get { return _CourseofStudy3; }
        set { _CourseofStudy3 = value; }
    }


    public string Surname
    {
        get { return _Surname; }
        set { _Surname = value; }
    }
    private string _Surname;

    public string OtherNames
    {
        get { return _OtherNames; }
        set { _OtherNames = value; }
    }
    private string _OtherNames;

    public string MaidenName
    {
        get { return _MaidenName; }
        set { _MaidenName = value; }
    }
    private string _MaidenName;

    public string Sex
    {
        get { return _Sex; }
        set { _Sex = value; }
    }
    private string _Sex;

    public string DateOfBirth
    {
        get { return _DateOfBirth; }
        set { _DateOfBirth = value; }
    }
    private string _DateOfBirth;

    public string MaritalStatus
    {
        get { return _MaritalStatus; }
        set { _MaritalStatus = value; }
    }
    private string _MaritalStatus;

    public string Nationality
    {
        get { return _Nationality; }
        set { _Nationality = value; }
    }
    private string _Nationality;

    public string Religion
    {
        get { return _Religion; }
        set { _Religion = value; }
    }
    private string _Religion;

    public string State
    {
        get { return _State; }
        set { _State = value; }
    }
    private string _State;

    public string LocalGovernmentArea
    {
        get { return _LocalGovernmentArea; }
        set { _LocalGovernmentArea = value; }
    }
    private string _LocalGovernmentArea;

    public int DepartmentID
    {
        get { return _DepartmentID; }
        set { _DepartmentID = value; }
    }
    private int _DepartmentID;

    public string PlaceOfBirth
    {
        get { return _PlaceOfBirth; }
        set { _PlaceOfBirth = value; }
    }
    private string _PlaceOfBirth;

    public string HomeAddress
    {
        get { return _HomeAddress; }
        set { _HomeAddress = value; }
    }
    private string _HomeAddress;

    public string PostalAddress
    {
        get { return _PostalAddress; }
        set { _PostalAddress = value; }
    }
    private string _PostalAddress;

    public string Country
    {
        get { return _Country; }
        set { _Country = value; }
    }
    private string _Country;

    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }
    private string _Email;

    public string PhoneNumber
    {
        get { return _PhoneNumber; }
        set { _PhoneNumber = value; }
    }
    private string _PhoneNumber;

    public string SponsorName
    {
        get { return _SponsorName; }
        set { _SponsorName = value; }
    }
    private string _SponsorName;

    public string SponsorAddress
    {
        get { return _SponsorAddress; }
        set { _SponsorAddress = value; }
    }
    private string _SponsorAddress;

    public string SponsorPhone
    {
        get { return _SponsorPhone; }
        set { _SponsorPhone = value; }
    }
    private string _SponsorPhone;

    public string SponsorEmail
    {
        get { return _SponsorEmail; }
        set { _SponsorEmail = value; }
    }
    private string _SponsorEmail;

    public string SponsorRelationship
    {
        get { return _SponsorRelationship; }
        set { _SponsorRelationship = value; }
    }
    private string _SponsorRelationship;

    public string Sponsor
    {
        get { return _Sponsor ; }
        set { _Sponsor  = value; }
    }
    private string _Sponsor ;




    public string AdmissionStatus
    {
        get { return _AdmissionStatus; }
        set { _AdmissionStatus = value; }
    }
    private string _AdmissionStatus;

    public string AcademicLevel
    {
        get { return _AcademicLevel; }
        set { _AcademicLevel = value; }
    }
    private string _AcademicLevel = "";

    public string RegNo
    {
        get { return _RegNo; }
        set { _RegNo = value; }
    }
    private string _RegNo;

    public System.DateTime CreatedDate
    {
        get { return _CreatedDate; }
        set { _CreatedDate = value; }
    }
    private System.DateTime _CreatedDate;

    public string CreatedBy
    {
        get { return _CreatedBy; }
        set { _CreatedBy = value; }
    }
    private string _CreatedBy;

    public string PassportFile
    {
        get { return _PassportFile; }
        set { _PassportFile = value; }
    }
    private string _PassportFile;

    public string LocalPassportFile
    {
        get { return _LocalPassportFile; }
        set { _LocalPassportFile = value; }
    }
    private string _LocalPassportFile;

    public string AdmittedSession
    {
        get { return _AdmittedSession; }
        set { _AdmittedSession = value; }
    }
    private string _AdmittedSession = "";

    public string Programme
    {
        get { return _Programme; }
        set { _Programme = value; }
    }
    private string _Programme;

    public string ModeOfStudy
    {
        get { return _ModeOfStudy; }
        set { _ModeOfStudy = value; }
    }
    private string _ModeOfStudy;

    public string EntryMode
    {
        get { return _EntryMode; }
        set { _EntryMode = value; }
    }
    private string _EntryMode;

    public string PresentSession
    {
        get { return _PresentSession; }
        set { _PresentSession = value; }
    }
    private string _PresentSession = "";

    public string AdmittedLevel
    {
        get { return _AdmittedLevel; }
        set { _AdmittedLevel = value; }
    }
    private string _AdmittedLevel = "";

    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }
    private string _Title;

    public string PresentEmployment
    {
        get { return _PresentEmployment; }
        set { _PresentEmployment = value; }
    }
    private string _PresentEmployment;

    public string PositionHeld
    {
        get { return _PositionHeld; }
        set { _PositionHeld = value; }
    }
    private string _PositionHeld;

    public string PreviousEmployment
    {
        get { return _PreviousEmployment; }
        set { _PreviousEmployment = value; }
    }
    private string _PreviousEmployment;

    public string PresentHighestQualification
    {
        get { return _PresentHighestQualification; }
        set { _PresentHighestQualification = value; }
    }
    private string _PresentHighestQualification;

    public string SchoolAttended
    {
        get { return _SchoolAttended; }
        set { _SchoolAttended = value; }
    }
    private string _SchoolAttended;

    public int FirstFacultyID
    {
        get { return _FirstFacultyID; }
        set { _FirstFacultyID = value; }
    }
    private int _FirstFacultyID;

    public int SecondFacultyID
    {
        get { return _SecondFacultyID; }
        set { _SecondFacultyID = value; }
    }
    private int _SecondFacultyID;

    public int ThirdFacultyID
    {
        get { return _ThirdFacultyID; }
        set { _ThirdFacultyID = value; }
    }
    private int _ThirdFacultyID;


    public int FirstDepartmentID
    {
        get { return _FirstDepartmentID; }
        set { _FirstDepartmentID = value; }
    }
    private int _FirstDepartmentID;

    public int SecondDepartmentID
    {
        get { return _SecondDepartmentID; }
        set { _SecondDepartmentID = value; }
    }
    private int _SecondDepartmentID;


    public int ThirdDepartmentID
    {
        get { return _ThirdDepartmentID; }
        set { _ThirdDepartmentID = value; }
    }
    private int _ThirdDepartmentID;

    public string PreviousCourseAttended
    {
        get { return _PreviousCourseAttended; }
        set { _PreviousCourseAttended = value; }
    }
    private string _PreviousCourseAttended;

    public string PreviousGrade
    {
        get { return _PreviousGrade; }
        set { _PreviousGrade = value; }
    }
    private string _PreviousGrade;

    public string PreviousAttendedFrom
    {
        get { return _PreviousAttendedFrom; }
        set { _PreviousAttendedFrom = value; }
    }
    private string _PreviousAttendedFrom;

    public string PreviousAttendedTo
    {
        get { return _PreviousAttendedTo; }
        set { _PreviousAttendedTo = value; }
    }
    private string _PreviousAttendedTo;


    public string PreviousRegNo
    {
        get { return _PreviousRegNo; }
        set { _PreviousRegNo = value; }
    }
    private string _PreviousRegNo;

    public string Repeating
    {
        get { return _Repeating; }
        set { _Repeating = value; }
    }
    private string _Repeating;

    public string IsIndigene
    {
        get { return _IsIndigene; }
        set { _IsIndigene = value; }
    }
    private string _IsIndigene = "0";

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    private string _UserName;

    private string _Center;
    public string Center
    {
        get { return _Center; }
        set { _Center = value; }
    }

    private string _UTMERegNumber;
    public string UTMERegNumber
    {
        get { return _UTMERegNumber; }
        set { _UTMERegNumber = value; }
    }

    private int _UTMEScore;
    public int UTMEScore
    {
        get { return _UTMEScore; }
        set { _UTMEScore = value; }
    }

    private string _UTMEFirstChoice;
    public string UTMEFirstChoice
    {
        get { return _UTMEFirstChoice; }
        set { _UTMEFirstChoice = value; }
    }

    private string _UTMESecondChoice;
    public string UTMESecondChoice
    {
        get { return _UTMESecondChoice; }
        set { _UTMESecondChoice = value; }
    }

    private string _UTMEFirstChoiceCourse;
    public string UTMEFirstChoiceCourse
    {
        get { return _UTMEFirstChoiceCourse; }
        set { _UTMEFirstChoiceCourse = value; }
    }

    private string _UTMESecondChoiceCourse;
    public string UTMESecondChoiceCourse
    {
        get { return _UTMESecondChoiceCourse; }
        set { _UTMESecondChoiceCourse = value; }
    }

    private int _PrintStatus;
    public int printStatus
    {
        get { return _PrintStatus; }
        set { _PrintStatus = value; }
    }

    private int _PersonalInfoStatus;
    public int personalInfoStatus
    {
        get { return _PersonalInfoStatus; }
        set { _PersonalInfoStatus = value; }
    }

    private int _ChoiceStatus;
    public int choiceStatus
    {
        get { return _ChoiceStatus; }
        set { _ChoiceStatus = value; }
    }

    private int _EducationStatus;
    public int educationStatus
    {
        get { return _EducationStatus; }
        set { _EducationStatus = value; }
    }

    private int _PostEducationStatus;
    public int posteducationStatus
    {
        get { return _PostEducationStatus; }
        set { _PostEducationStatus = value; }
    }

    private int _SubmitStatus;
    public int SubmitStatus
    {
        get { return _SubmitStatus; }
        set { _SubmitStatus = value; }
    }

    private int _AdmissionLetterPrintedStatus;
    public int AdmissionLetterPrintedStatus
    {
        get { return _AdmissionLetterPrintedStatus; }
        set { _AdmissionLetterPrintedStatus = value; }
    }

    private string _teachingsubject;
    public string TeachingSubject
    {
        get { return _teachingsubject; }
        set { _teachingsubject = value; }
    }

    private string _teachingsubjectone;
    public string TeachingSubjectOne
    {
        get { return _teachingsubjectone; }
        set { _teachingsubjectone = value; }
    }

    private string _teachingsubjecttwo;
    public string TeachingSubjectTwo
    {
        get { return _teachingsubjecttwo; }
        set { _teachingsubjecttwo = value; }
    }

    private string _bloodgroup;
    public string BloodGroup
    {
        get { return _bloodgroup; }
        set { _bloodgroup = value; }
    }

    private string _disability;
    public string Disability
    {
        get { return _disability; }
        set { _disability = value; }
    }

    private string _teachingcenter;
    public string TeachingCenter
    {
        get { return _teachingcenter; }
        set { _teachingcenter = value; }
    }

    private string _examinationcenter;
    public string ExaminationCenter
    {
        get { return _examinationcenter; }
        set { _examinationcenter = value; }
    }

    public string Referral
    {
        get { return _Referral; }
        set { _Referral = value; }
    }
    private string _Referral;

    #region byte ApplicantPassport
    public byte[] ApplicantPassport
    {
        get
        {
            return _applicantpassport;
        }
        set
        {
            _applicantpassport = value;
        }
    }
    #endregion
    private byte[] _applicantpassport;


    /////////////////////////////// introducing Entrance exan fields

    public string EntranceExamsubj1
    {
        get { return _entranceExamsubj1; }
        set { _entranceExamsubj1 = value; }
    }
    string _entranceExamsubj1;


    public Applicants(
        string EntranceExamsub1,
        string MyPostProgramme,
        string MyPostMatric,
        string MyCourseGrade,
        string MyCourseName,
        string MyInstitution,
        string MyQualYear,
        string MyOtherSchsInfo,

        int ApplicantId,
        string Surname,
        string OtherNames,
        string MaidenName,
        string Sex,
        string DateOfBirth,
        string MaritalStatus,
        string Nationality,
        string Religion,
        string State,
        string LocalGovernmentArea,
        int DepartmentID,
        string PlaceOfBirth,
        string HomeAddress,
        string PostalAddress,
        string Country,
        string Email,
        string PhoneNumber,
        string SponsorName,
        string SponsorAddress,
        string SponsorPhone,
        string SponsorEmail,
        string SponsorRelationship,
        string Sponsor ,
        string AdmissionStatus,
        string AcademicLevel,
        string RegNo,
        System.DateTime CreatedDate,
        string CreatedBy,
        string PassportFile,
        string LocalPassportFile,
        string AdmittedSession,
        string Programme,
        string ModeOfStudy,
        string EntryMode,
        string PresentSession,
        string AdmittedLevel,
        string Title,
        string PresentEmployment,
        string PositionHeld,
        string PreviousEmployment,
        string PresentHighestQualification,
        string SchoolAttended,
        string OtherNational,
        int FirstFacultyID,
        int SecondFacultyID,
        int ThirdFacultyID,
        int FirstDepartmentID,
        int SecondDepartmentID,
         int ThirdDepartmentID,
        string CourseofStudy1,
        string CourseofStudy2,
        string CourseofStudy3,
        string PreviousCourseAttended,
        string PreviousGrade,
        string PreviousAttendedFrom,
        string PreviousAttendedTo,
        string PreviousRegNo,
        string Repeating,
        string IsIndigene,
        string Center,
        string UserName,
        string UTMERegNo,
        int UTMEScore,
        string UTMEFirstChoice,
        string UTMEFirstChoiceCourse,
        string UTMESecondChoice,
        string UTMESecondChoiceCourse,
        int printStatus,
        int personalInfoStatus,
        int choiceStatus,
        int educationStatus,
        int posteducationStatus,
        int SubmitStatus,
        int AdmissionLetterPrintedStatus,
        string TeachingSubject,
        string TeachingSubjectOne,
        string TeachingSubjectTwo,
        string TeachingCenter,
        string ExaminationCenter,
        string BloodGroup,
        byte[] ApplicantPassport,
        string Disability,
        string Referral,
        string stEntranceExamsubj1
        )
    {


        this._myPostProgramme = MyPostProgramme;
        this._myPostMatric = MyPostMatric;
        this._myCourseGrade = MyCourseGrade;
        this._myCourseName = MyCourseName;
        this._myInstitution = MyInstitution;
        this._myQualYear = MyQualYear;
        this._myOtherSchsInfo =MyOtherSchsInfo;

        this._ApplicantId = ApplicantId;
        this._Surname = Surname;
        this._OtherNames = OtherNames;
        this._MaidenName = MaidenName;
        this._Sex = Sex;
        this._DateOfBirth = DateOfBirth;
        this._MaritalStatus = MaritalStatus;
        this._Nationality = Nationality;
        this._OtherNational = OtherNational;
        this._Religion = Religion;
        this._State = State;
        this._LocalGovernmentArea = LocalGovernmentArea;
        this._DepartmentID = DepartmentID;
        this._PlaceOfBirth = PlaceOfBirth;
        this._HomeAddress = HomeAddress;
        this._PostalAddress = PostalAddress;
        this._Country = Country;
        this._Email = Email;
        this._PhoneNumber = PhoneNumber;
        this._SponsorName = SponsorName;
        this._SponsorAddress = SponsorAddress;
        this._SponsorPhone = SponsorPhone;
        this._SponsorEmail = SponsorEmail;
        this._SponsorRelationship = SponsorRelationship;
        this._Sponsor = Sponsor;
        this._AdmissionStatus = AdmissionStatus;
        this._AcademicLevel = AcademicLevel;
        this._RegNo = RegNo;
        this._CreatedDate = CreatedDate;
        this._CreatedBy = CreatedBy;
        this._PassportFile = PassportFile;
        this._LocalPassportFile = LocalPassportFile;
        this._AdmittedSession = AdmittedSession;
        this._Programme = Programme;
        this._ModeOfStudy = ModeOfStudy;
        this._EntryMode = EntryMode;
        this._PresentSession = PresentSession;
        this._AdmittedLevel = AdmittedLevel;
        this._Title = Title;
        this._PresentEmployment = PresentEmployment;
        this._PositionHeld = PositionHeld;
        this._PreviousEmployment = PreviousEmployment;
        this._PresentHighestQualification = PresentHighestQualification;
        this._SchoolAttended = SchoolAttended;
        this._FirstFacultyID = FirstFacultyID;
        this._SecondFacultyID = SecondFacultyID;
        this._ThirdFacultyID = ThirdFacultyID;
        this._FirstDepartmentID = FirstDepartmentID;
        this._SecondDepartmentID = SecondDepartmentID;
        this._ThirdDepartmentID = ThirdDepartmentID;
        this._CourseofStudy1 = CourseofStudy1;
        this._CourseofStudy2 = CourseofStudy2;
        this._CourseofStudy3 = CourseofStudy3;
        this._PreviousCourseAttended = PreviousCourseAttended;
        this._PreviousGrade = PreviousGrade;
        this._PreviousRegNo = PreviousRegNo;
        this._PreviousAttendedFrom = PreviousAttendedFrom;
        this._PreviousAttendedTo = PreviousAttendedTo;
        this._Repeating = Repeating;
        this._IsIndigene = IsIndigene;
        this._Center = Center;
        this._UserName = UserName;
        this._UTMEFirstChoice = UTMEFirstChoice;
        this._UTMEFirstChoiceCourse = UTMEFirstChoiceCourse;
        this._UTMESecondChoice = UTMESecondChoice;
        this._UTMESecondChoiceCourse = UTMESecondChoiceCourse;
        this._UTMERegNumber = UTMERegNumber;
        this._UTMEScore = UTMEScore;
        this._PrintStatus = printStatus;
        this._PersonalInfoStatus = personalInfoStatus;
        this._ChoiceStatus = choiceStatus;
        this._EducationStatus = educationStatus;
        this.posteducationStatus = posteducationStatus;
        this._SubmitStatus = SubmitStatus;
        this._AdmissionLetterPrintedStatus = AdmissionLetterPrintedStatus;
        this._teachingsubject = TeachingSubject;
        this._teachingsubjectone = TeachingSubject;
        this._teachingsubjecttwo = TeachingSubjectTwo;
        this._teachingcenter = TeachingCenter;
        this._examinationcenter = ExaminationCenter;
        this._bloodgroup = BloodGroup;
        this._disability = Disability;
        this._Referral = Referral;
        this._applicantpassport = ApplicantPassport;
        this._entranceExamsubj1 = EntranceExamsubj1;
    }
}