using System;

public class ApplicantSignOn
{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public ApplicantSignOn()
	{}


    private string _emailVerificationCode = "";
    public string EmailVerificationCode
    {
        get { return _emailVerificationCode; }
        set { _emailVerificationCode = value; }
    }
    private int _verifyFlag = 0;
    public int VerifyFlag
    {
        get { return _verifyFlag; }
        set { _verifyFlag = value; }
    }
    private bool _verified;
    public bool Verified
    {
        get { return _verified; }
        set { _verified = value; }
    }
    private string _FormNumber;
    public string FormNumber
    {
        get { return _FormNumber; }
        set { _FormNumber = value; }
    }

    private string _IsFastTrack;
    public string IsFastTrack
    {
        get { return _IsFastTrack; }
        set { _IsFastTrack = value; }
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

	public string ModeOfStudy
	{ 
		get { return _ModeOfStudy; }
		set { _ModeOfStudy = value; }
	}
	private string _ModeOfStudy;

	public string Programme
	{ 
		get { return _Programme; }
		set { _Programme = value; }
	}
	private string _Programme;

	public string UserName
	{ 
		get { return _UserName; }
		set { _UserName = value; }
	}
	private string _UserName;

	public string Password
	{ 
		get { return _Password; }
		set { _Password = value; }
	}
	private string _Password;

	public string Email
	{ 
		get { return _Email; }
		set { _Email = value; }
	}
	private string _Email;

	public string Phone
	{ 
		get { return _Phone; }
		set { _Phone = value; }
	}
	private string _Phone;

	public string ApplicationPIN
	{ 
		get { return _ApplicationPIN; }
		set { _ApplicationPIN = value; }
	}
	private string _ApplicationPIN;

	public int CanLogin
	{ 
		get { return _CanLogin; }
		set { _CanLogin = value; }
	}
	private int _CanLogin;

	public System.DateTime DateSignOn
	{ 
		get { return _DateSignOn; }
		set { _DateSignOn = value; }
	}
	private System.DateTime _DateSignOn;

	public System.DateTime LastLoginDate
	{ 
		get { return _LastLoginDate; }
		set { _LastLoginDate = value; }
	}
    
	private System.DateTime _LastLoginDate;

    private string m_ApplicationSession;
    /// <summary>
    /// To capture the current application session
    /// </summary>
    public string ApplicationSession
    {
        get { return m_ApplicationSession; }
        set { m_ApplicationSession = value; }


    }

    /// <summary>
    /// Srn: For numbering/indexing
    /// </summary>
	public int Srn
	{ 
		get { return _Srn; }
		set { _Srn = value; }
	}
	private int _Srn;

	public ApplicantSignOn(

		string Surname, 
		string OtherNames, 
		string ModeOfStudy,
        string IsFastTrack,
		string Programme, 
		string UserName, 
		string Password, 
		string Email, 
		string Phone, 
		string ApplicationPIN, 
		int CanLogin, 
		System.DateTime DateSignOn, 
		System.DateTime LastLoginDate, 
		int Srn)
	{

	
		this._Surname = Surname; 
		this._OtherNames = OtherNames; 
		this._ModeOfStudy = ModeOfStudy; 
		this._Programme = Programme; 
		this._UserName = UserName; 
		this._Password = Password;
        this._IsFastTrack = IsFastTrack;
		this._Email = Email; 
		this._Phone = Phone; 
		this._ApplicationPIN = ApplicationPIN; 
		this._CanLogin = CanLogin; 
		this._DateSignOn = DateSignOn; 
		this._LastLoginDate = LastLoginDate; 
		this._Srn = Srn; 
	}
}