using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Messaging;
//using Cyberspace.ServiceBrocker;


public partial class ResetPassword : System.Web.UI.Page
{
   
    private string str = ConfigurationManager.AppSettings["conn"];
    private static string msg = "";

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
      
   
    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        var lbl = new Label
                      {
                          Text =
                              "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg +
                              "'" + ")</script>"
                      };

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
      
    protected void btnApply_Click(object sender, EventArgs e)
    {
        SqlConnection  conn = null;
        try
        {
            if ( !string.IsNullOrEmpty( txtUserid.Text.Trim()) )
            {
                conn  = new SqlConnection(str);
                var query = "UPDATE StudentSignOn SET Password=MatricNumber WHERE MatricNumber= '" + txtUserid.Text + "'";
                var comm = new SqlCommand(query, conn);

                conn.Open();
                var count = comm.ExecuteNonQuery();

                MessageBox(count > 0
                               ? "Password Reset Successfull"
                               : "Password Reset Failed. Please Input Valid Matric Number");

                txtUserid.Focus();
                txtUserid.Text = string.Empty;
            }
            else
            {
                MessageBox("Matric Number Is Required");
            }
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }
        finally
        {
            if(conn !=null)
            {
                conn.Close();
            }
            
        }

    }
     
   
}
