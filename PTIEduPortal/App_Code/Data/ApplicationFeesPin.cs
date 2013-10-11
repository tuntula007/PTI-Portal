using System;

public class ApplicationFeesPin
{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public ApplicationFeesPin()
	{}


	public int SerialNo
	{ 
		get { return _SerialNo; }
		set { _SerialNo = value; }
	}
	private int _SerialNo;

	public string PinNumber
	{ 
		get { return _PinNumber; }
		set { _PinNumber = value; }
	}
	private string _PinNumber;

	public string PinSerialNumber
	{ 
		get { return _PinSerialNumber; }
		set { _PinSerialNumber = value; }
	}
	private string _PinSerialNumber;

	public decimal Amount
	{ 
		get { return _Amount; }
		set { _Amount = value; }
	}
	private decimal _Amount;

	public string PinStatus
	{ 
		get { return _PinStatus; }
		set { _PinStatus = value; }
	}
	private string _PinStatus;

	public string SessionName
	{ 
		get { return _SessionName; }
		set { _SessionName = value; }
	}
	private string _SessionName;

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

	public string UsedBy
	{ 
		get { return _UsedBy; }
		set { _UsedBy = value; }
	}
	private string _UsedBy;

	public string UsedDate
	{ 
		get { return _UsedDate; }
		set { _UsedDate = value; }
	}
	private string _UsedDate;

	public ApplicationFeesPin(

		int SerialNo, 
		string PinNumber, 
		string PinSerialNumber, 
		decimal Amount, 
		string PinStatus, 
		string SessionName, 
		string Programme, 
		string ModeOfStudy, 
		string UsedBy, 
		string UsedDate)
	{

	
		this._SerialNo = SerialNo; 
		this._PinNumber = PinNumber; 
		this._PinSerialNumber = PinSerialNumber; 
		this._Amount = Amount; 
		this._PinStatus = PinStatus; 
		this._SessionName = SessionName; 
		this._Programme = Programme; 
		this._ModeOfStudy = ModeOfStudy; 
		this._UsedBy = UsedBy; 
		this._UsedDate = UsedDate; 
	}
}