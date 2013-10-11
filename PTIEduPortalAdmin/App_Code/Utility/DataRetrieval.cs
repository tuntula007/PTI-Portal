using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DataRetrieval
/// </summary>
public class DataRetrieval
{
    private static string strConn = ConfigurationManager.AppSettings["conn"];
	public DataRetrieval()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void PerformQuery(string Qry)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();

            SqlCommand cmd = null;
            //SqlDataReader dr = null;
            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
    }
    public DataSet SelectQuery(string Qry)
    {
        DataSet dt = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();

            SqlCommand cmd = null;
            //SqlDataReader dr = null;
            SqlDataAdapter dat = new SqlDataAdapter(Qry, cnn);
            dat.Fill(dt);
            dat.Dispose();
            //cmd = new SqlCommand();
            //dt= cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
        return dt;
    }
    public bool Exists(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;

            cmd = new SqlCommand(qry, cnn);

            dr = cmd.ExecuteReader();//
            if (dr.HasRows)
            {
                ret = true;
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            ret = false;
        }

        return ret;
    }
}
