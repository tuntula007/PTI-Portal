using System;

public class Applicants
{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public Applicants()
	{}


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
	public int ApplicantId
	{ 
		get { return _ApplicantId; }
		set { _ApplicantId = value; }
	}
	private int _ApplicantId;

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
	private string _AcademicLevel ="";

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
	private string _AdmittedSession ="";

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

	public string PresentSession
	{ 
		get { return _PresentSession; }
		set { _PresentSession = value; }
	}
	private string _PresentSession ="";

	public string AdmittedLevel
	{ 
		get { return _AdmittedLevel; }
		set { _AdmittedLevel = value; }
	}
	private string _AdmittedLevel ="";

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
	private string _IsIndigene ="0";

	public string UserName
	{ 
		get { return _UserName; }
		set { _UserName = value; }
	}
	private string _UserName;

    private string _Center;
    public string Center
    {
        get {return _Center; }
        set { _Center = value; }
    }
	public Applicants(

		int ApplicantId, 
		string Surname, 
		string OtherNames, 
		string MaidenName, 
		string Sex, 
		string DateOfBirth, 
		string MaritalStatus, 
		string Nationality, 
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
		string PresentSession, 
		string AdmittedLevel, 
		string Title, 
		string PresentEmployment, 
		string PositionHeld, 
		string PreviousEmployment, 
		string PresentHighestQualification, 
		string SchoolAttended, 
		int FirstFacultyID, 
		int SecondFacultyID, 
		int FirstDepartmentID, 
		int SecondDepartmentID, 
		string PreviousCourseAttended, 
		string PreviousGrade, 
		string PreviousRegNo, 
		string Repeating, 
		string IsIndigene, 
        string Center,
		string UserName)
	{

	
		this._ApplicantId = ApplicantId; 
		this._Surname = Surname; 
		this._OtherNames = OtherNames; 
		this._MaidenName = MaidenName; 
		this._Sex = Sex; 
		this._DateOfBirth = DateOfBirth; 
		this._MaritalStatus = MaritalStatus; 
		this._Nationality = Nationality; 
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
		this._FirstDepartmentID = FirstDepartmentID; 
		this._SecondDepartmentID = SecondDepartmentID; 
		this._PreviousCourseAttended = PreviousCourseAttended; 
		this._PreviousGrade = PreviousGrade; 
		this._PreviousRegNo = PreviousRegNo; 
		this._Repeating = Repeating; 
		this._IsIndigene = IsIndigene;
        this._Center = Center;
		this._UserName = UserName; 
	}
}