using System;
using System.Data;
using System.Configuration;


/// <summary>
/// Summary description for UploadDataInfo
/// </summary>
/// 
public class Pins
{
    private long m_PinQty;
    private string m_PinBatchNo;
    private string m_Session;
    private int m_PinDigits;
    private int m_BatchQty;
    private string m_PinFormat;
    private string m_PinType;
    private string m_Uploader;
    private string m_Encrypt;
    private string m_Centre;
    private int m_CentreID;
    private string m_Programme;
    private double m_Price;

    public Pins()
    {
        PinQty = 0;
        PinBatchNo = "";
        PinDigits = 0;
        PinFormat = "";
        Uploader = "";
        Encrypt = "";
        Centre = "";
        Programme = "";
        Price = 0;
        BatchQty = 0;
        PinType = "";
        Session = "";
        CentreID = 0;
    }

    public long PinQty
    {
        get { return m_PinQty; }
        set { m_PinQty = value; }
    }
    public int PinDigits
    {
        get { return m_PinDigits; }
        set { m_PinDigits = value; }
    }
    //private string m_PinBatchNo;
    public string PinBatchNo
    {
        get { return m_PinBatchNo; }
        set { m_PinBatchNo = value; }
    }
    //private string m_PinFormat;
    public string PinFormat
    {
        get { return m_PinFormat; }
        set { m_PinFormat = value; }
    }
    //m_PinType
    public string PinType
    {
        get { return m_PinType; }
        set { m_PinType = value; }
    }
    //m_Session
    public string Session
    {
        get { return m_Session; }
        set { m_Session = value; }
    }
    public string Encrypt
    {
        get { return m_Encrypt; }
        set { m_Encrypt = value; }
    }

    public string Uploader
    {
        get { return m_Uploader; }
        set { m_Uploader = value; }
    }
    //private string m_Centre;
    public string Centre
    {
        get { return m_Centre; }
        set { m_Centre = value; }
    }
    //m_CentreID
    public int CentreID
    {
        get { return m_CentreID; }
        set { m_CentreID = value; }
    }
    //private string m_Programme;
    public string Programme
    {
        get { return m_Programme; }
        set { m_Programme = value; }
    }
    //private double m_Price;
    public double Price
    {
        get { return m_Price; }
        set { m_Price = value; }
    }
    //
    public int BatchQty
    {
        get { return m_BatchQty; }
        set { m_BatchQty = value; }
    }
}

