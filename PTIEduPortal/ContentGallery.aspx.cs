using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;

public partial class Contents_AdminContentGallery : System.Web.UI.Page
{

    private static string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    Students students;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            So = (StudentSignOn)Session["StudentSignOn"];

            if (Request.QueryString["matricnumber"] != null)
            {
                MatricNumber = Request.QueryString["matricnumber"].ToString();

            }
            else if (Session["MatricNumber"] != null)
            {
                MatricNumber = Session["MatricNumber"].ToString();
                string AcademicSession = getSession();
                LoadContent(MatricNumber, AcademicSession);
            }
            else
            {

                //Session.Contents.Clear();
                Response.Redirect("StudentControlCenter.aspx");
                return;
            }


        }
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
    private string getSession()
    {
        string Asession = "";
        DataSet ds = new DataSet();
        string qry = "SELECT [SessionName] FROM [Session] where [ActiveStatus]=1";

        ds = SearchData(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
            {
                Asession = ds.Tables[0].Rows[jj][0].ToString();
            }
        }

        return Asession;
    }

    private void LoadContent(string MatNo, string Asession)
    {
        try
        {

            ArrayList CosCodes = new ArrayList();
            CosCodes = getcodes(MatNo, Asession);


            DataTable tb = new DataTable();
            int cnt = 1;

            DataRow drow = null;
            //p.DocumentID as ID,p.DocumentCategory as [Course Code],
            //p.DocumentName as Name,
            //p.PostedDate as Posted,p.ExpirationDate as Expiring,
            //p.LastModifiedDate as Updated,p.Quantity as Files,p.ContentDescription as Note

            tb.Columns.Add("ID", typeof(string));
            tb.Columns.Add("Course Code", typeof(string));
            tb.Columns.Add("Course Title", typeof(string));
            //tb.Columns.Add("Posted", typeof(string));
            //tb.Columns.Add("Expiring", typeof(string));
            //tb.Columns.Add("Updated", typeof(string));
            //tb.Columns.Add("Files", typeof(string));
            tb.Columns.Add("Course Description", typeof(string));


            foreach (string codes in CosCodes)
            {
                string ID = "";
                string CourseCode = "";
                string Name = "";
                string Posted = "";
                string Expiring = "";
                string Updated = "";
                string Files = "";
                string Note = "";
                

                DataSet ds = new DataSet();
                string qry = "Select p.DocumentID as ID,p.DocumentCategory as [Course Code],p.DocumentName as Name,p.PostedDate as Posted,p.ExpirationDate as Expiring,p.LastModifiedDate as Updated,p.Quantity as Files,p.ContentDescription as Note from [CourseMaterial] p where p.PublishedStatus =1 and p.DocumentCategory ='" + codes + "'";

                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        //courses.Add(ds.Tables[0].Rows[jj][0].ToString());

                        drow = tb.NewRow();
                        ID = ds.Tables[0].Rows[jj][0].ToString();
                        CourseCode = ds.Tables[0].Rows[jj][1].ToString();
                        Name = ds.Tables[0].Rows[jj][2].ToString();            
                        
                        Note = ds.Tables[0].Rows[jj][7].ToString();
                        drow["ID"] = ID;
                        drow["Course Code"] = CourseCode;
                        drow["Course Title"] = Name;
                        
                        drow["Course Description"] = Note;

                        tb.Rows.Add(drow);

                    }
                }


            }


            DataSet ds2 = new DataSet();
            ds2.Tables.Add(tb);
            GridView1.DataSource = ds2;
            GridView1.DataBind();


            ////for (int i = 0; i < GridView1.Rows.Count; i++)
            ////{

            ////    GridView1.Rows[i].Cells[7].Text = DateTime.Parse(GridView1.Rows[i].Cells[7].Text).ToString("yyyy-MM-dd");
            ////    GridView1.Rows[i].Cells[8].Text = DateTime.Parse(GridView1.Rows[i].Cells[8].Text).ToString("yyyy-MM-dd");

            ////    if (GridView1.Rows[i].Cells[6].Text == "1")
            ////    {
            ////        if (DateTime.Parse(GridView1.Rows[i].Cells[8].Text) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            ////        {
            ////            GridView1.Rows[i].Cells[8].ForeColor = System.Drawing.Color.Red;
            ////        }
            ////    }


            ////    //cb.BorderColor = System.Drawing.Color.Red;
            ////    //cb.ID = "_" + GridView1.Rows[i].Cells[2].Text;
            ////    if (GridView1.Rows[i].Cells[10].Text == "1")
            ////    {
            ////        //cb.Checked = true;
            ////        GridView1.Rows[i].Cells[10].Text = "True";


            ////    }
            ////    else
            ////    {
            ////        //cb.Checked = false;
            ////        GridView1.Rows[i].Cells[10].Text = "False";
            ////        GridView1.Rows[i].Cells[10].ForeColor = System.Drawing.Color.Red;
            ////    }
               
            ////}

        }
        catch (Exception ex)
        {
            //logger.Error(ex.Message);

        }
    }

    private ArrayList getcodes(string MatNo, string Asession)
    {
        ArrayList courses = new ArrayList();

        try
        {
            DataSet ds = new DataSet();
            string qry = "select distinct c.[CourseCode] from [CourseRegistration] c  where c.[IsApproved]=1 and c.[SessionName]='" + Asession + "' and c.MatricNumber='" + MatNo + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    courses.Add(ds.Tables[0].Rows[jj][0].ToString().Trim());
                }
            }
        }
        catch (Exception ex)
        {

        }
        return courses;
    }
    protected void cb_CheckedChanged(object sender, EventArgs e)
    {

        SqlConnection cnn = new SqlConnection(strConnectionString);
        cnn.Open();

    }
    protected void GridView1_OnSelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
    {
        GridView gv = (GridView)sender;
        //string selected = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text.ToLower();
        //string subject =  GridView1.Rows[e.NewSelectedIndex].Cells[2].Text.ToLower();
        string selected = gv.Rows[e.NewSelectedIndex].Cells[1].Text.ToLower();
        //string subject = gv.Rows[e.NewSelectedIndex].Cells[2].Text.ToLower();
        Response.Redirect("~/CourseMat.aspx?id=" + selected + "&guid=" + Guid.NewGuid().ToString());


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbb = (CheckBox)GridView1.Rows[i].Cells[9].FindControl("_" + GridView1.Rows[i].Cells[2].Text);
        }


        //CheckBox cb = new CheckBox();
        ////cb.AutoPostBack = true;
        //cb.CheckedChanged += new EventHandler(cb_CheckedChanged);

        //cb.BorderColor = System.Drawing.Color.Red;
        //cb.ID = "_" + GridView1.Rows[i].Cells[2].Text;
        ////if (GridView1.Rows[i].Cells[9].Text == "1")
        ////{
        //    cb.Checked = true;

        //}
        //else
        //{
        //    cb.Checked = false;
        //}
        //GridView1.Rows[i].Cells[9].Controls.Add(cb);

    }
}
