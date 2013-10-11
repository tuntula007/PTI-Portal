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
using System.Collections.Specialized;

public partial class loginaccess : System.Web.UI.Page
{
    private string strConnectionString = ConfigurationManager.AppSettings["conn"];//.ConnectionString; 
    private static string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LabRelogin.Visible = false;
        //if(IsPostBack)
        //{
        //    LabRelogin.Text = "Please, re-login to proceed";
        //    LabRelogin.Visible = true;
        //}
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (ValidateUser(Login1.UserName.Trim(), Login1.Password.Trim()) == true)
        {
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName.Trim(), false);            
        }
    }

    
    private bool Existed(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

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
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "", "");
            showmassage(msg);
        }

        return ret;
    }
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
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
    private bool ValidateUser(string UserName, string strPassword)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [passw],[usergroup],[FacultyName],[DepartmentName] from [TUsers] where [userid] ='" + UserName.Trim() + "' and [UserStatus]=1", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                if (dr.Read() == true)
                {
                    if (string.Compare(strPassword, dr.GetString(0), false) == 0)
                    {
                        // clean up first
                        if (Cache[UserName] != null)
                        {
                            Cache.Remove(UserName.Trim());
                        }
                        // then put it again
                        LabRelogin.Visible = false;
                        Cache.Add(UserName.Trim(), dr.GetString(1).Trim(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(2, 0, 0), System.Web.Caching.CacheItemPriority.High, null);
                        
                        ret = true;
                    }

                }
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }

        return ret;
    }
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
}
