﻿using System;
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
using System.Text;//.Data.SqlClient;
using System.Messaging;



public partial class Admin_DispatchedPin : System.Web.UI.Page
{
    string str = System.Configuration.ConfigurationManager.AppSettings["conn"];
    string str2 = System.Configuration.ConfigurationManager.AppSettings["conn2"];

    private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string PinInfoUpdate = ".\\private$\\" + ConfigurationManager.AppSettings["PinInfoUpdate"];

    private static string msg = "";

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static string PixName = "NONE";
    private static string EditedPixName = "";

    private static int GViewWidth = 0;

    private static string PresentDept = "";
    private static ArrayList AllFaculty = null;
    private static ArrayList AllDept = null;

    private static Hashtable FacultyID = null;
    private static Hashtable DeptID = null;
    private static Hashtable CentreCode = new Hashtable();

    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];
    private static CPermit cp = null;
    private static CWritetoqueue rq = null;
    private static string Group = "";
    private static string ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //private static string Group = "";
        //private static string ID = "";
        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            if (Group.ToLower().Trim() != null)
            {
                //msg = "You have no right to this page";
                //showmassage(msg);
                return;

            }
            else
            {
                
            }
        }

        if (Page.IsPostBack == false)
        {
            TabContainer1.ActiveTabIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();
            LoadApprovedReservedBooks();
            LoadCentres();
            LoadProg();
            LoadSession();

        }

        GridView1.Width = GViewWidth;
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                if (permitedGroup())
                {
                    LoadApprovedReservedBooks();
                    LoadCentres();
                    LoadProg();
                    LoadSession();
                }
                else
                {
                    msg = "You have no right to this page";
                    showmassage(msg);
                    return;
                }

            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                if (Group.ToLower().Trim() != "superadmin")//|| //
                {
                    BtnApprove.Visible = false;
                    BtnDisapprove.Visible = false;
                    msg = "You have no right to access this tab";
                    showmassage(msg);
                    return;
                }
                BtnApprove.Visible = true;
                BtnDisapprove.Visible = true;
                GridView3.DataSource = null;
                GridView3.DataBind();
                LoadApprovedReservedBooks2();
            }

            if (TabContainer1.ActiveTabIndex == 2)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();


                GridView2.DataSource = null;
                GridView2.DataBind();
                

                if (permitedGroup())
                {

                    Loadprintoption();
                    loadPrintables();
                }
                else
                {
                    msg = "You have no right to this page";
                    showmassage(msg);
                    return;
                }
            }



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);

        }
    }
    private bool permitedGroup()
    {
        bool succ = false;
        if (Group.ToLower().Trim() == "superadmin" || Group.ToLower().Trim() == "admin")
        {
            succ = true;
        }
        return succ;
    }
    private void LoadApprovedReservedBooks2()
    {
        try
        {
            string query = "select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty],[Price] as [AMOUNT],[Programme],[Center],[SessionName] as [Session] from [MasterPins]  where [DispatchedStatus] = 0 and AllocationStatus = 1 and ApproveStatus = 0  order by BatchNumber desc";

            exportheader = "[Batch Number],[Batch Qty]";
            Exportfilename = "UnApproved Batched Pins";
            GridCaption = "UnApproved Batched Pins";
            GViewWidth = 700;
            populategrdv2(query);
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }

    private void populategrdv2(string query)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);

            dat.Fill(ds);
            if (GViewWidth > 0)
            {
                GridView3.Width = GViewWidth;
            }

            GridView3.DataSource = ds;
            Session["ds3"] = ds;

            GridView3.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                formatGridview();
            }
            GridView3.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView3.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView3.CaptionAlign = TableCaptionAlign.Left;
            //ChequePanelGridv.Visible = true;
        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }
    }
    private void formatGridview2()
    {
        try
        {
            String datef = "";
            DateTime datty;
            double dd = 0.0;
            int j = GridView2.HeaderRow.Cells.Count;//.Columns.Count;
            int Amt = 0;
            int collectiondate = 0;
            int Transdate = 0;
            int Duedate = 0;

            for (int m = 0; m < j; m++)
            {
                if (GridView2.HeaderRow.Cells[m].Text.ToUpper() == "AMOUNT")
                {
                    Amt = m;
                }

            }

            Double Total = 0.0;
            string dat = "";
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                if (Amt != 0)
                {
                    dat = GridView2.Rows[i].Cells[Amt].Text;
                    if (dat != "")
                    {
                        dd = Double.Parse(GridView2.Rows[i].Cells[Amt].Text);
                        GridView2.Rows[i].Cells[Amt].Text = String.Format("{0:N}", dd);
                        Total = Total + dd;
                    }
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
    private void formatGridview()
    {
        try
        {
            String datef = "";
            DateTime datty;
            double dd = 0.0;
            int j = GridView3.HeaderRow.Cells.Count;//.Columns.Count;
            int Amt = 0;
            int collectiondate = 0;
            int Transdate = 0;
            int Duedate = 0;

            for (int m = 0; m < j; m++)
            {
                if (GridView3.HeaderRow.Cells[m].Text.ToUpper() == "AMOUNT")
                {
                    Amt = m;
                }

            }

            Double Total = 0.0;
            string dat = "";
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {
                if (Amt != 0)
                {
                    dat = GridView3.Rows[i].Cells[Amt].Text;
                    if (dat != "")
                    {
                        dd = Double.Parse(GridView3.Rows[i].Cells[Amt].Text);
                        GridView3.Rows[i].Cells[Amt].Text = String.Format("{0:N}", dd);
                        Total = Total + dd;
                    }
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

    private void Loadprintoption()
    {
        try
        {
            DDListPrintOption.Items.Clear();
            DDListPrintOption.Items.Add("Noun Application pin");
            //DDListPrintOption.Items.Add("Clearance pin");            

        }
        catch (Exception ex)
        {

        }
    }
    private void LoadSession()
    {
        try
        {

            DDListSession.Items.Clear();
            DataSet ds = new DataSet();

            string Qry = "SELECT distinct [SessionName] FROM [Session]";
            ds = SearchData2(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListSession.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                }
            }

        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
            //return;
        }
    }
    private void LoadProg()
    {
        try
        {
            DDListProg.Items.Clear();
            DataSet ds = new DataSet();

            string Qry = "SELECT distinct [Programme] FROM [Payment_Plan]";
            ds = SearchData(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListProg.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
                loadcost(DDListProg.Text.Trim());

            }
            else
            {
                TxtCost.Text = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }

    private void loadcost(string Programme)
    {
        try
        {
            TxtCost.Text = "";
            DataSet ds = new DataSet();
            double dd = 0;
            string Qry = "SELECT [Amount] FROM [Payment_Plan] where [Programme]='" + Programme + "'";
            ds = SearchData(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    dd = Double.Parse(ds.Tables[0].Rows[jj][0].ToString());
                    TxtCost.Text = String.Format("{0:N}", dd);

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

    private void LoadCentres()
    {
        try
        {
            CentreCode.Clear();
            DDListCentre.Items.Clear();
            DataSet ds = new DataSet();
            string qry = "SELECT distinct [CenterName],[CenterCode] FROM [StudyCenterNew] order by [CenterName] asc";
            
            ds = SearchData2(qry);
            int code = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListCentre.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                    if (!CentreCode.ContainsKey(ds.Tables[0].Rows[jj][0].ToString().ToUpper()))
                    {
                        code = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                        CentreCode.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper(), code);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
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
    private DataSet SearchData2(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str2);
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
    protected void DDListProg_Changed(object sender, EventArgs e)
    {
        loadcost(DDListProg.Text.Trim());
    }
    private void LoadApprovedReservedBooks()
    {
        try
        {
            string query = "";
            if (Group.ToLower().Trim() == "superadmin")//|| //
            {
                query = "select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty] from [MasterPins]  where [DispatchedStatus] = 0 and AllocationStatus = 0 and ApproveStatus = 0  order by BatchNumber desc";

            }
            else
            {
                query = "select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty] from [MasterPins]  where [GeneratedBy] = '" + ID + "' and [DispatchedStatus] = 0 and AllocationStatus = 0 and ApproveStatus = 0  order by BatchNumber desc";

            }


            exportheader = "[Batch Number],[Batch Qty]";
            Exportfilename = "Unallocated Batched Pins";
            GridCaption = "Unallocated Batched Pins";
            GViewWidth = 400;
            populategrdv(query);
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }

    private void populategrdv(string query)
    {
        try
        {
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
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    formatGridview();
            //}
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

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = GridView2.SelectedIndex;
        string batch = "";
        string printoption = "";
        string session = "";
        Session["BatchNo"] = "";
        Session["printoption"] = "";
        Session["Programme"] = "";
        Session["Price"] = "";
        Session["Centre"] = "";
        Session["Session"] = "";

        batch = GridView2.Rows[selectedIndex].Cells[3].Text;
        session = GridView2.Rows[selectedIndex].Cells[8].Text;
        printoption = DDListPrintOption.Text.Trim();

        try
        {
            if (batch != "" && DDListCentre.Text.Trim() != "" && TxtCost.Text.Trim() != "")
            {
                Session["BatchNo"] = batch;
                Session["printoption"] = printoption;
                Session["Session"] = session;
                Response.Redirect("ReportPins.aspx");
            }
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        double amt = 0;
        if (double.TryParse(TxtCost.Text.Trim(), out amt))
        {
        }
        else
        {
            msg = "Enter price for this programme";
            showmassage(msg);
            TxtCost.Focus();
            return;
        }


        int selectedIndex = GridView1.SelectedIndex;

        try
        {
            Pins pin = new Pins();
            if (!updateBkdatails(GridView1.Rows[selectedIndex].Cells[1].Text))
            {

                pin.Encrypt = "";
                pin.PinBatchNo = GridView1.Rows[selectedIndex].Cells[1].Text;
                pin.PinDigits = 0;
                pin.PinFormat = "";
                pin.PinQty = 0;
                pin.Uploader = ID;
                pin.Price = amt;
                pin.Centre = DDListCentre.Text.Trim();
                pin.Programme = DDListProg.Text.Trim();

                sendtoUploadloadQ(pin);

                msg = "Dispatching in progress...";
                //showmassage(msg);
                GridView1.Rows[selectedIndex].Cells[1].Enabled = false;
                //LoadApprovedReservedBooks();
                //return;
            }
            else
            {
                msg = "This batch have been dispatched";

                GridView1.Rows[selectedIndex].Cells[1].Enabled = false;
            }
            showmassage(msg);
            LoadApprovedReservedBooks();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }

    }
    protected void BtndeleteClick(object sender, EventArgs e)
    {

        try
        {
            ArrayList ApprvedOGNO = new ArrayList();

            foreach (GridViewRow gvr in GridView2.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
                if (chk.Checked)
                {
                    string id = gvr.Cells[3].Text;
                    ApprvedOGNO.Add(id);

                }
            }

            foreach (string id in ApprvedOGNO)
            {
                string query1 = "";// "Delete from Customer_Register where [CustomerID] = '" + id + "'";

                //PerformUpdate(query1);


                query1 = "UPDATE MasterPins  SET [Printable] = 0 where GeneratedBy = '" + ID + "' and [BatchNumber]= '" + id + "'";

                PerformUpdate(query1);



            }
            loadPrintables();
        }
        catch (Exception ex)
        {

        }
        //LoadClients();
    }

    private void loadPrintables()
    {
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();
            DataSet ds = new DataSet();
            string qry = "";

            //string query = "select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty],[Price] as [AMOUNT],[Programme],[Center] from [MasterPins]  where [DispatchedStatus] = 0 and AllocationStatus = 1 and ApproveStatus = 0  order by BatchNumber desc";

            if (Group.ToLower().Trim() == "superadmin")//|| //
            {
                qry = " select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty],[Price] as [AMOUNT],[Programme],[Center],[SessionName] as [Session]  from [MasterPins]  where [DispatchedStatus] = 1 and [Printable] = 1 order by BatchNumber desc";

            }
            else
            {
                qry = " select distinct [BatchNumber] as [Batch Number],[BatchQty] as [Batch Qty],[Price] as [AMOUNT],[Programme],[Center],[SessionName] as [Session]  from [MasterPins]  where [GeneratedBy] = '" + ID + "' and [DispatchedStatus] = 1 and [Printable] = 1 order by BatchNumber desc";

            }



            SqlDataAdapter da = new SqlDataAdapter(qry, cnn);

            da.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();


            Session["ds2"] = ds;
            da.Dispose();
            ds.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
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

    private void sendtoUploadloadQ(Pins datInfo)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(PinInfoUpdate))
            {
                mq = MessageQueue.Create(PinInfoUpdate);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(PinInfoUpdate);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Pins) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + datInfo.PinQty.ToString() + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            mq.Send(datInfo);
            mq.Dispose();
            mq.Close();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //log.Error(msg);
        }
    }
    private bool updateBkdatails(string Accno)
    {
        bool rtn = false;

        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            string qry = "SELECT *  FROM [MasterPins] where BatchNumber ='" + Accno + "' and DispatchedStatus = 1 ";
            cmd = new SqlCommand(qry, cnn);

            //cmd = new SqlCommand("SELECT [ISBN],[DateAquired],[BarCode],[BookCode],[ShelfCode],[Availability],[SetupBy],[Datesetup]  FROM [BookDetails] where ISBN ='" + isbn + "' and Availability = 1 ", cnn);

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //string bcode = dr.GetString(3);

                //cmd.Dispose();
                //dr.Dispose();

                //cmd = new SqlCommand("Update BookDetails set [Availability]  = 'Not Available'  where BookCode = '" + bcode + "'", cnn);
                //cmd.ExecuteNonQuery();
                rtn = true;

            }

            cmd.Dispose();
            dr.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            string exx = ex.Message;
        }

        return rtn;
    }
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    protected void GridView3_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet ds3 = null;
            if (Session["ds3"] != null)
            {
                ds3 = (DataSet)Session["ds3"];
                GridView3.DataSource = ds3;
                GridView3.Width = GViewWidth;
                GridView3.PageIndex = e.NewPageIndex;
                //GrdVPerntwk.PageIndex = e.NewPageIndex;
                GridView3.DataBind();
                GridView3.Caption = GridCaption + ":" + " " + "Total = " + " " + ds3.Tables[0].Rows.Count.ToString();
                GridView3.ToolTip = ds3.Tables[0].Rows.Count.ToString();


            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Library Automation", "");
        }
    }
    //GridView1_OnPageIndexChanging
    protected void GridView1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
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


            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Library Automation", "");
        }
    }
    private void LogError(string strMsg, string SourceApp, string SourceMethod)
    {
        cp = new CPermit();
        cp.Direction = strMsg;
        cp.SourceApplication = SourceApp;
        cp.MethodName = SourceMethod;
        cp.MsgType = "ERROR";
        rq = new CWritetoqueue();
        rq.strPath = auditque;
        //rq.Logonpermit.MsgType=;
        rq.Writeaudit(cp);
    }


    private void ExportData(string hd, DataSet ds, string batchno)
    {
        Response.Clear();
        Response.ContentType = "text/plain";
        Response.Charset = "";
        StringBuilder str = new StringBuilder();
        str.Append(hd);
        str.AppendLine();

        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            str.Append(i + 1);

            for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
            {

                if (j == 0)
                {
                    DateTime dt;
                    DateTime.TryParse(ds.Tables[0].Rows[i][j].ToString(), out dt);

                    str.Append("," + dt.ToString("yyyy-MM-dd HH:mm:ss"));

                }
                else
                {

                    str.Append("," + ds.Tables[0].Rows[i][j].ToString().Replace(",", ";"));
                }




            }


            str.AppendLine();
        }

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=Pins-" + batchno + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".csv");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.csv";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Response.Write(str.ToString());
        try
        {
            Response.End();
        }
        catch (Exception ex)
        {

            string exx = ex.Message;
        }
    }
    protected void BtnAllocate_Click(object sender, EventArgs e)
    {
        double amt = 0;
        if (double.TryParse(TxtCost.Text.Trim(), out amt))
        {
        }
        else
        {
            msg = "Enter price for this programme";
            showmassage(msg);
            TxtCost.Focus();
            return;
        }

        if (DDListCentre.Text.Trim() == "")
        {
            msg = "Select study centre";
            showmassage(msg);
            return;
        }
        if (DDListSession.Text.Trim() == "")
        {
            msg = "Select academic session";
            showmassage(msg);
            return;
        }
        //int selectedIndex = GridView1.SelectedIndex;

        try
        {
            ArrayList ApprvedOGNO = new ArrayList();

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN2");
                if (chk.Checked)
                {
                    string id = gvr.Cells[1].Text;
                    ApprvedOGNO.Add(id);

                }
            }
            int cnt = 0;
            foreach (string batchno in ApprvedOGNO)
            {
                string query1 = "";
                query1 = "UPDATE MasterPins  SET [Price] = " + amt + ", Programme ='" + DDListProg.Text.Trim() + "',Center='" + DDListCentre.Text.Trim() + "', AllocationStatus = 1, [SessionName]='" + DDListSession.Text.Trim() + "' where GeneratedBy = '" + ID + "' and [BatchNumber]= '" + batchno + "'";
                PerformUpdate(query1);
                cnt++;
            }

            msg = cnt.ToString() + " " + "Batches have been allocated to" + " " + DDListCentre.Text.Trim() + " " + "study centre for" + " " + DDListProg.Text.Trim();
            showmassage(msg);
            LoadApprovedReservedBooks();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }
    protected void BtnApprove_Click(object sender, EventArgs e)
    {


        int cnt = 0;
        try
        {
            ArrayList ApprvedOGNO = new ArrayList();
            if (CentreCode.Count == 0)
            {
                getcodes();
            }

            if (CentreCode.Count == 0)
            {
                msg = "No Centre codes loaded";
                showmassage(msg);
                return;
            }

            foreach (GridViewRow gvr in GridView3.Rows)
            {
                Pins pin = new Pins();
                string PinBatchNo = "";
                string Centre = "";
                string Programme = "";
                string session = "";
                double amt = 0;
                string price = "";
                long BatchQty = 0;
                int centreid = 0;

                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN3");
                if (chk.Checked)
                {
                    //"[BatchNumber],[BatchQty],[Price],[Programme],[Center] from [MasterPins]  where [DispatchedStatus] = 0 and AllocationStatus = 1 and ApproveStatus = 0  order by BatchNumber desc";

                    PinBatchNo = gvr.Cells[1].Text;
                    BatchQty = long.Parse(gvr.Cells[2].Text);
                    price = gvr.Cells[3].Text;
                    amt = double.Parse(price);
                    Programme = gvr.Cells[4].Text;
                    Centre = gvr.Cells[5].Text;
                    if(CentreCode.ContainsKey(Centre.ToUpper()))
                    {
                        centreid = int.Parse(CentreCode[Centre.ToUpper()].ToString());
                    }
                    session = gvr.Cells[6].Text;


                    pin.Encrypt = "";
                    pin.PinBatchNo = PinBatchNo;
                    pin.PinDigits = 0;
                    pin.PinFormat = "";
                    pin.PinQty = BatchQty;
                    pin.Uploader = ID;
                    pin.Price = amt;
                    pin.Centre = Centre;
                    pin.Programme = Programme;
                    pin.Session = session;
                    pin.PinType = "application pin";
                    pin.CentreID = centreid;

                    sendtoUploadloadQ(pin);
                    cnt++;
                    chk.Checked = false;
                    gvr.Enabled = false;
                    //gvr.Enabled = false;
                }
            }

            msg = "Dispatching of " + cnt.ToString() + " pin batches in progress...";

            showmassage(msg);
            //LoadApprovedReservedBooks();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }

    private void getcodes()
    {
       // Hashtable CentreCode = new Hashtable();
        try
        {

            CentreCode.Clear();
            DataSet ds = null;

            string Sql = "SELECT [CenterName],[CenterCode] FROM [StudyCenterNew] order by [CenterName] asc";
           // string Sql = "SELECT [CenterName],[CenterCode] FROM [StudyCenterNew]";
            ds = new DataSet();
            ds = SearchData2(Sql);
            int code = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    if (!CentreCode.ContainsKey(ds.Tables[0].Rows[jj][0].ToString().ToUpper()))
                    {
                        code = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                        CentreCode.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper(), code);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        //return CentreCode;
    }
    protected void BtnDisapprove_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        try
        {
            ArrayList ApprvedOGNO = new ArrayList();

            foreach (GridViewRow gvr in GridView3.Rows)
            {
                Pins pin = new Pins();
                string PinBatchNo = "";

                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN3");
                if (chk.Checked)
                {
                    //"[BatchNumber],[BatchQty],[Price],[Programme],[Center] from [MasterPins]  where [DispatchedStatus] = 0 and AllocationStatus = 1 and ApproveStatus = 0  order by BatchNumber desc";

                    PinBatchNo = gvr.Cells[1].Text;

                    string query1 = "";
                    query1 = "UPDATE MasterPins  SET AllocationStatus = 0 where [BatchNumber]= '" + PinBatchNo + "'";
                    PerformUpdate(query1);
                    cnt++;

                }


            }
            LoadApprovedReservedBooks2();
            msg = "Disapproval of " + cnt.ToString() + " pin batches in successful";

            showmassage(msg);
            //LoadApprovedReservedBooks();
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }
}
