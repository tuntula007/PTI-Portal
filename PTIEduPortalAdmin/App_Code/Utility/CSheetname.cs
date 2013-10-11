using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Data.Odbc;//.OleDb;


/// <summary>
/// Summary description for CSheetname
/// </summary>
public class CSheetname
{
    public CSheetname()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string name(string fname)
    {

        String Currsheet = "";// "Sheet1";
        //Get sheet name

        //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fname + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";

        //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fname + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
        string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fname + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

        OleDbConnection objConn = null;

        DataTable dt = new DataTable();
        objConn = new OleDbConnection(strConn);
        objConn.Open();

        // Get the data table containg the schema guid.
        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //dt = objConn.GetSchema();


        if (dt == null)
        {
            return null;
        }

        String[] excelSheets = new String[dt.Rows.Count];

        int i = 0;

        //// Add the sheet name to the string array.
        foreach (DataRow row in dt.Rows)
        {
            excelSheets[i] = row["TABLE_NAME"].ToString();
            i++;
        }

        if (excelSheets.Length > 0)
        {
            Currsheet = excelSheets[0].ToString();//.Replace("$","");
        }

        objConn.Dispose();
        objConn.Close();
        return Currsheet;
    }
}
