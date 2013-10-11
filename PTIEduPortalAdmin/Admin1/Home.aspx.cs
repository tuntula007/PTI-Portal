using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public partial class Home : System.Web.UI.Page
{
    private static string userGroup = "";
    private static string ID = "";
    private static string msg = "";
    private static string query = "";
    
    private static string str = ConfigurationManager.AppSettings["conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated == true)
        {

            if (Cache[HttpContext.Current.User.Identity.Name] != null)
            {
                userGroup = (string)Cache[HttpContext.Current.User.Identity.Name];
                if (userGroup.ToLower() != null)
                {
                    if (!IsPostBack)
                    {
                        ID = HttpContext.Current.User.Identity.Name;
                       string ClentName = getname(ID);
                        LblVisitor.Text = "WELCOME"+" "+ ClentName.ToUpper();
                        LblVisitor.Visible = true;
                    }
                    else
                    {
                        LblVisitor.Text = "";
                        LblVisitor.Visible = false;
                    }
                    
                }
                else
                {
                    return;
                }                
            }
            
        }
    }
    private string getname(string ID)
    {
        string NM = "";
        try
        {
            DataSet ds = new DataSet();

            query = "SELECT [userid] FROM [TUsers] where [userid] = '" + ID + "'";

            ds = SearchData(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    NM = ds.Tables[0].Rows[jj][0].ToString();//.ToUpper();// +" " + ds.Tables[0].Rows[jj][1].ToString().ToUpper();

                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }

        return NM;
    }
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);
            dat.Fill(ds);

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception e)
        {
            string ex = e.Message;
        }
        return ds;
    }
}
