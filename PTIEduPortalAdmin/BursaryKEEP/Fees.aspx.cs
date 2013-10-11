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
using System.Text;

public partial class Admin_Fees : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];

    private static string msg = "";
    private static Hashtable Dura = null;
    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";

    private static int GViewWidth = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        BtnExport.Visible = false;
        BtnRefresh.Visible = false;
        //TabContainer1.TabIndex= 1; 
        if (!IsPostBack)
        {
            TabContainer1_ActiveTabChanged(TabContainer1, null);
            TabContainer1.ActiveTabIndex = 0;

        }
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                //LoadCategory();
                //LoadDuration();
                //populategrd();
            }


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll Automation", "");
        }
    }
    //private void populategrd()
    //{
    //    query = "SELECT Top(20)[ShareHolderCategory] as [Share Holder Category],[DurationPlan] as [Duration Plan],[NumberOfMonths] as [Months],[Amount],[Description] FROM [Payment_Plan] order by DateMod desc";
    //    exportheader = "[Share Holder Category],[Duration Plan],[Months],[Amount],[Description] ";

    //    Exportfilename = "Duration plans";
    //    GridCaption = "Duration plans";
    //    GViewWidth = 700;
    //    populategrdv(query);
    //}

    //protected void LnkBtnRefresh_Click(object sender, EventArgs e)
    //{
    //    //populategrdv(query);
    //    populategrd();
    //}
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (DDListCategory.Text.Trim() == "")
        {
            msg = "Specify customer catagory";
            showmassage(msg);
            return;
        }
        if (DDListDuration.Text.Trim() == "")
        {
            msg = "Specify duration plan";
            showmassage(msg);
            return;
        }

        int MTH = 0;
        if (TxtMonths.Text.Trim() == "")
        {
            msg = "Specify months for the plan";
            showmassage(msg);
            return;
        }
        else
        {
            if (int.TryParse(TxtMonths.Text.Trim(), out MTH))
            {
            }
            else
            {
                msg = "Invalid months for the plan";
                showmassage(msg);
                TxtMonths.Focus();
                return;
            }
        }

        double cost = 0;

        if (TxtCost.Text.Trim() == "")
        {
            msg = "Enter cost for the plan";
            showmassage(msg);
            TxtCost.Focus();
            return;
        }
        else
        {
            if (double.TryParse(TxtCost.Text.Trim(), out cost))
            {
            }
            else
            {
                msg = "Invalid cost for the plan";
                showmassage(msg);
                TxtCost.Focus();
                return;
            }
        }


        if (TxtDesc.Text.Trim() != "")
        {
            TxtDesc.Text = TxtDesc.Text.Trim().ToUpper().Replace("'", "''");
        }
        else
        {
            TxtDesc.Text = DDListCategory.Text.Trim();
        }


        string Qry = "";

        Qry = "SELECT * FROM [Payment_Plan] where [ShareHolderCategory] = '" + DDListCategory.Text.Trim() + "' and DurationPlan = '" + DDListDuration.Text.Trim() + "'";
        if (Existed(Qry))
        {
            msg = "Price existed before for the category";
            showmassage(msg);
            return;
        }
        else
        {
            Qry = "INSERT INTO [Payment_Plan]([ShareHolderCategory],[DurationPlan],[NumberOfMonths],[Amount],[Description],DateMod) VALUES ('" + DDListCategory.Text.Trim() + "','" + DDListDuration.Text.Trim() + "'," + MTH + "," + cost + ",'" + TxtDesc.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
            PerformInsert(Qry);
        }

        TxtDesc.Text = "";
        TxtCost.Text = "";


        populategrd();
        //populategrdv(query);
    }
    private void populategrd()
    {

        query = "SELECT [ShareHolderCategory] as [Share Holder Category],[DurationPlan] as [Duration Plan],[NumberOfMonths] as [Months],[Amount],[Description] FROM [Payment_Plan] order by DateMod desc ";
        exportheader = "[Share Holder Category],[Duration Plan],[Months],[Amount],[Description] ";

        Exportfilename = "Duration plans";
        GridCaption = "Duration plans";
        GViewWidth = 1000;
        populategrdv(query);
    }
    private void populategrdv(string query)
    {
        try
        {
            BtnExport.Visible = true;
            BtnRefresh.Visible = true;
            GridView1.Visible = true;

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);

            dat.Fill(ds);
            if (GViewWidth > 0)
            {
                GridView1.Width = GViewWidth;
            }

            GridView1.DataSource = ds;
            Session["ds2"] = ds;

            GridView1.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                formatGridview();
            }
            GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView1.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView1.CaptionAlign = TableCaptionAlign.Left;
            //ChequePanelGridv.Visible = true;
        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }

    }

    private void formatGridview()
    {

        try
        {
            String datef = "";
            DateTime datty;
            double dd = 0.0;
            int j = GridView1.HeaderRow.Cells.Count;//.Columns.Count;
            int Amt = 0;
            int collectiondate = 0;
            int Transdate = 0;
            int Duedate = 0;

            for (int m = 0; m < j; m++)
            {
                if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "AMOUNT")
                {
                    Amt = m;
                }
                //if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "COLLECTIONDATE")
                //{
                //    collectiondate = m;
                //}
                //if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "TRANSDATE")
                //{
                //    Transdate = m;
                //}
                //if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "DUEDATE")
                //{
                //    Duedate = m;
                //}
            }

            Double Total = 0.0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (Amt != 0)
                {
                    // GridView1.Rows[i].Cells[7].BackColor = System.Drawing.Color.Yellow;
                    dd = Double.Parse(GridView1.Rows[i].Cells[Amt].Text);
                    GridView1.Rows[i].Cells[Amt].Text = String.Format("{0:N}", dd);
                    Total = Total + dd;
                }
                ////if (collectiondate != 0)
                ////{
                ////    datty = Convert.ToDateTime(GridView1.Rows[i].Cells[collectiondate].Text);
                ////    datef = datty.ToString("yyyy-MM-dd");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                ////    GridView1.Rows[i].Cells[collectiondate].Text = datef;
                ////}
                ////if (Transdate != 0)
                ////{
                ////    datty = Convert.ToDateTime(GridView1.Rows[i].Cells[Transdate].Text);
                ////    datef = datty.ToString("yyyy-MM-dd HH:mm:ss");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                ////    GridView1.Rows[i].Cells[Transdate].Text = datef;
                ////}
                //////
                ////if (Duedate != 0)
                ////{
                ////    datty = Convert.ToDateTime(GridView1.Rows[i].Cells[Duedate].Text);
                ////    datef = datty.ToString("yyyy-MM-dd");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                ////    GridView1.Rows[i].Cells[Duedate].Text = datef;
                ////}

            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }
    }
    //protected void BtnClose_Click(object sender, EventArgs e)
    //{
    //    MultiView1.ActiveViewIndex = -1;
    //}
    //protected void LinkBtnEdit_Click(object sender, EventArgs e)
    //{
    //    MultiView1.ActiveViewIndex = 1;
    //}
    //protected void LinkBtnAddNew_Click(object sender, EventArgs e)
    //{
    //    //MultiView1.ActiveViewIndex = 0;
    //    //LoadCategory();
    //    //LoadDuration();

    //}

    private void LoadDuration()
    {
        try
        {
            DDListDuration.Items.Clear();
            DataSet ds = new DataSet();

            Dura = new Hashtable();
            string Plan = "";
            int M = 0;

            string Qry = "SELECT distinct [DurationPlan],[Months] FROM [DurationPlans]";
            ds = SearchData(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    Plan = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    M = int.Parse(ds.Tables[0].Rows[jj][1].ToString().ToUpper());
                    DDListDuration.Items.Add(Plan);
                    Dura.Add(Plan, M);
                }

                DisplayMonth(DDListDuration.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }

    private void DisplayMonth(string Plan)
    {
        try
        {
            TxtMonths.Text = "";
            if (Dura.Count > 0)
            {
                if (Dura.ContainsKey(Plan))
                {
                    TxtMonths.Text = Dura[Plan].ToString();
                }

                TxtCost.Text = "";
                TxtDesc.Text = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    private bool Existed(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(str);

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
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }

        return ret;
    }
    private void PerformInsert(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(str);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }

    private void PerformDelete(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(str);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void grdViewStatustory_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        GridView1.EditIndex = -1;
        try
        {
            populategrd();

        }
        catch (Exception ex)
        {

            //logger.Error(ex.Message);
        }
    }
    protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;


            populategrd();// DisplayGrid();
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = true;
            row.Cells[5].Enabled = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }

    protected void grdViewStatustory_OnRowUpdating(Object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView1.ToolTip = GridView1.Rows[e.RowIndex].Cells[1].Text;
            GridViewRow row = GridView1.Rows[e.RowIndex];
            GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();

            string Pcategory = "";
            string PDuration = "";
            double amt = 0;
            //Qry = "SELECT * FROM [Payment_Plan] where [ShareHolderCategory] = '" + DDListCategory.Text.Trim() + "' and DurationPlan = '" + DDListDuration.Text.Trim() + "'";

            string Ldays = GridView1.ToolTip.Trim();//.ToString();//.Text;
            if (double.TryParse(Ldays, out amt))
            {

            }
            else
            {
                msg = "enter a numeric value";
                showmassage(msg);
                populategrd();
                return;
            }

            Pcategory = row.Cells[1].Text.Trim();//.Controls[0])).Text.Trim();
            PDuration = row.Cells[2].Text.Trim();
            GridView1.EditIndex = -1;
            //update
            string qry = "UPDATE [Payment_Plan] SET Amount = " + amt + " where [ShareHolderCategory] = '" + Pcategory + "' and DurationPlan = '" + PDuration + "'";
            PerformUpdate(qry);
            populategrd();
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }
    private void PerformUpdate(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(str);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void grdViewStatustory_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count <= 0)
            {
                e.Cancel = true;
                return;
            }


            string Pcategory = "";
            string PDuration = "";

            Pcategory = GridView1.Rows[e.RowIndex].Cells[1].Text;
            PDuration = GridView1.Rows[e.RowIndex].Cells[2].Text;

            //string qry = "SELECT * FROM [LeaveTypes] where [LeaveType] ='" + TxtLeaveType.Text.Trim() + "'";

            if (Pcategory != "" && PDuration != "")
            {

                string qry = "Delete [Payment_Plan] where [ShareHolderCategory] = '" + Pcategory + "' and DurationPlan = '" + PDuration + "'";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                //state.InsertState(st);
            }


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }

    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    private void LoadCategory()
    {
        try
        {
            DDListCategory.Items.Clear();
            DataSet ds = new DataSet();

            string Qry = "SELECT [ShareHolderCategory]  FROM [Customer_ShareHolderCategory]";
            ds = SearchData(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListCategory.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }
    protected void BankGridv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet ds3 = null;
            if (Session["ds2"] != null)
            {
                ds3 = (DataSet)Session["ds2"];
                GridView1.DataSource = ds3;
                GridView1.Width = GViewWidth;
                GridView1.PageIndex = e.NewPageIndex;
                //GrdVPerntwk.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
                GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds3.Tables[0].Rows.Count.ToString();
                GridView1.ToolTip = ds3.Tables[0].Rows.Count.ToString();
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    //formatGridview();
                }

                GridView1.Visible = true;

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void DDListDuration_Changed(object sender, EventArgs e)
    {
        DisplayMonth(DDListDuration.Text.Trim());
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
    //protected void LinkBtnExit_Click(object sender, EventArgs e)
    //{
    //    //MultiView1.ActiveViewIndex = -1;
    //    BtnExport.Visible = false;
    //    BtnRefresh.Visible = false;
    //    GridView1.Visible = false;
    //    Server.Transfer("Clientwelcome.aspx?re=0");
    //}
    protected void BtnExport_Click(object sender, EventArgs e)
    {

        string header = exportheader;
        string filename = Exportfilename;
        ExportData(header, filename);
    }
    private void ExportData(string header, string filename)
    {
        StringBuilder stb = new StringBuilder();
        //cmd = new SqlDataAdapter("select MSISDN,Service_Name as CODE, Operator_ID as NETWORK, Log_Date as LOGDAY,Message_Body As [MESSAGE BODY], GUID from Mg_Transaction where Service_Name in " + shortcode + " and  Direction = 'IN' and Log_Date between '" + txtBeginDate.Text.Trim() + "' and '" + txtEndDate.Text.Trim() + "' order by Log_Date desc", cnn);
        //stb.Append("BANK NAME,BRANCH,ADDRESS,CONTACT PERSON,PHONE,MAIL");
        stb.Append(header);
        stb.AppendLine();
        DataSet ds = new DataSet();

        String msg;
        int j;
        int k;
        int i;
        int m;

        try
        {
            if (Session["ds2"] != null)
            {
                ds = (DataSet)Session["ds2"];
                j = ds.Tables[0].Columns.Count;
                //j = j - 1;
                k = ds.Tables[0].Rows.Count;

                for (i = 0; i < k; i++)
                {
                    for (m = 0; m < j; m++)
                    {
                        if (m == 0)
                        {
                            stb.Append(ds.Tables[0].Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                        }
                        else
                        {
                            stb.Append("," + ds.Tables[0].Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                        }

                    }
                    stb.AppendLine();
                }


                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.csv";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Response.Write(stb.ToString());
                Response.End();
            }
            else
            {
                msg = "Specify what you want to Export";
                showmassage(msg);
                //TxtMsisdn.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
            return;
        }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        populategrd();
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = -1;
    }
}
