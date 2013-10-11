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

public partial class CourseMat : System.Web.UI.Page
{
    private string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            HttpContext context = HttpContext.Current;
            if (context.Items.Count > 0)
            {
                string id = context.Request.QueryString[0].ToString();
                if (id != null)
                {
                    lblContentID.Text = "<b>ContentID#:</b> " + id;
                    CreateFileInfo(id);
                    //CreateDownload(id);
                    CreateDownLoads(id);
                }
                else
                {
                    Response.Write("This is an illegal operation");
                }
            }

            //CreateDownLoads();

        }
    }

    private void CreateDownLoads(string id)
    {
        try
        {
           // string id = "1111108152405651";
            System.IO.FileInfo file = null;
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            //SqlCommand cmd = new SqlCommand("select P.DocumentID, P.FilePath,P.FileSize, P.DownloadCount,F.thumbnailPath,P.Srn,P.ContentType,P.Uploaded from Content_Posts p,Content_FileExtentions F where P.DocumentID='" + id + "' and F.Extention=P.FileExtension", cnn);


            //HyperLink hl = new HyperLink();
            //hl.ResolveUrl("http://ardownload.adobe.com/pub/adobe/reader/win/9.x/9.2/enu/AdbeRdr920_en_US.exe");//

            SqlCommand cmd = new SqlCommand("select P.DocumentID, P.FilePath,P.FileSize, P.DownLoadCnt,P.Srn,P.ContentType,P.LastModifiedDate from CourseMaterial p where P.DocumentID='" + id + "'", cnn);

            SqlDataReader dr = cmd.ExecuteReader();
            string ext = "";
            if (dr.HasRows == true)
            {
                int nFile = 1;
                while (dr.Read() == true)
                {
                    string fName;
                    switch (nFile)
                    {
                        case 1:
                            BulletedList1.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel1.Visible = true;
                             file = new System.IO.FileInfo(fName);
                             ext = System.IO.Path.GetExtension(fName);
                             if (ext.ToLower() == ".pdf")
                             {
                                 BulletedList1.Items.Add("File Name: \t\t" + file.Name);
                                 BulletedList1.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                                 BulletedList1.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                                 //Image1.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));//pdf.png
                                 Image1.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                                 btnDownload1.ToolTip = dr.GetValue(4).ToString();
                                 BulletedList1.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                                 BulletedList1.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));
                             
                             }
                             else
                             {
                                 BulletedList1.Items.Add("File Name: \t\t" + file.Name);
                                 BulletedList1.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                                 BulletedList1.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                                 //Image1.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));//pdf.png
                                 Image1.ImageUrl = ResolveUrl("~/Images/Png/" + "wma.png");
                                 btnDownload1.ToolTip = dr.GetValue(4).ToString();
                                 BulletedList1.Items.Add("Content Type: \t\t" + "application/octet-stream");//audio/x-ms-wma//audio/mpeg3//audio/x-wav";

                                 BulletedList1.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));
                             
                             }

                            break;
                        case 2:
                            BulletedList2.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel2.Visible = true;
                            file = new System.IO.FileInfo(fName);

                            BulletedList2.Items.Add("File Name: \t\t" + file.Name);
                            BulletedList2.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList2.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image2.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                            btnDownload2.ToolTip = dr.GetValue(4).ToString();
                            BulletedList2.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                            BulletedList2.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));

                            break;

                        case 3:
                            BulletedList3.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel3.Visible = true;
                            file = new System.IO.FileInfo(fName);

                            BulletedList3.Items.Add("File Name: \t\t" + file.Name);
                            BulletedList3.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList3.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image3.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                            btnDownload3.ToolTip = dr.GetValue(4).ToString();
                            BulletedList3.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                            BulletedList3.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));

                            break;

                        case 4:
                            BulletedList4.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel4.Visible = true;
                            file = new System.IO.FileInfo(fName);

                            BulletedList4.Items.Add("File Name: \t\t" + file.Name);
                            BulletedList4.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList4.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image4.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                            btnDownload4.ToolTip = dr.GetValue(4).ToString();
                            BulletedList4.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                            BulletedList4.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));

                            break;


                        case 5:
                            BulletedList5.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel5.Visible = true;
                            file = new System.IO.FileInfo(fName);

                            BulletedList5.Items.Add("File Name: \t\t" + file.Name);
                            BulletedList5.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList5.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image5.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                            btnDownload5.ToolTip = dr.GetValue(4).ToString();
                            BulletedList5.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                            BulletedList5.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));

                            break;

                        case 6:
                            BulletedList6.Items.Clear();
                            fName = dr.GetString(1);//.Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel6.Visible = true;
                            file = new System.IO.FileInfo(fName);

                            BulletedList6.Items.Add("File Name: \t\t" + file.Name);
                            BulletedList6.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList6.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image6.ImageUrl = ResolveUrl("~/Images/Png/" + "pdf.png");
                            btnDownload6.ToolTip = dr.GetValue(4).ToString();
                            BulletedList6.Items.Add("Content Type: \t\t" + dr.GetValue(5));
                            BulletedList6.Items.Add("Uploaded: \t\t" + dr.GetDateTime(6).ToString("MMMM dd, yyyy"));

                            break;


                    }
                    nFile++;
                }
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            // logger.Error(ex.Message);
        }
    }

    private void CreateDownload(string id)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("select P.DocumentID, P.FilePath,P.FileSize, P.DownloadCount,F.thumbnailPath,P.Srn,P.ContentType,P.Uploaded from Content_Posts p,Content_FileExtentions F where P.DocumentID='" + id + "' and F.Extention=P.FileExtension", cnn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                int nFile = 1;
                while (dr.Read() == true)
                {
                    string[] fName;
                    switch (nFile)
                    {

                        case 1:
                            BulletedList1.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel1.Visible = true;
                            BulletedList1.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList1.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList1.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image1.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload1.ToolTip = dr.GetValue(5).ToString();
                            BulletedList1.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList1.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;
                        case 2:
                            BulletedList2.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel2.Visible = true;
                            BulletedList2.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList2.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList2.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image2.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload2.ToolTip = dr.GetValue(5).ToString();
                            BulletedList2.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList2.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;

                        case 3:
                            BulletedList3.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel3.Visible = true;
                            BulletedList3.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList3.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList3.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image3.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload3.ToolTip = dr.GetValue(5).ToString();
                            BulletedList3.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList3.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;

                        case 4:
                            BulletedList4.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel4.Visible = true;
                            BulletedList4.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList4.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList4.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image4.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload4.ToolTip = dr.GetValue(5).ToString();
                            BulletedList4.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList4.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;


                        case 5:
                            BulletedList5.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel5.Visible = true;
                            BulletedList5.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList5.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList5.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image5.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload5.ToolTip = dr.GetValue(5).ToString();
                            BulletedList5.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList5.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;

                        case 6:
                            BulletedList6.Items.Clear();
                            fName = dr.GetString(1).Split(new string[] { id }, StringSplitOptions.RemoveEmptyEntries);
                            Panel6.Visible = true;
                            BulletedList6.Items.Add("File Name: \t\t" + fName[1]);
                            BulletedList6.Items.Add("File Size: \t\t" + dr.GetValue(2).ToString() + " bytes");
                            BulletedList6.Items.Add("Download Count: \t\t" + dr.GetValue(3));
                            Image6.ImageUrl = ResolveUrl("~/Images/Png/" + dr.GetValue(4));
                            btnDownload6.ToolTip = dr.GetValue(5).ToString();
                            BulletedList6.Items.Add("Content Type: \t\t" + dr.GetValue(6));
                            BulletedList6.Items.Add("Uploaded: \t\t" + dr.GetDateTime(7).ToString("MMMM dd, yyyy"));

                            break;


                    }
                    nFile++;
                }
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            // logger.Error(ex.Message);
        }
    }

    private void CreateFileInfo(string id)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [DocumentID],[DocumentCategory],[DocumentName] ,[LastModifiedDate],[ContentDescription] FROM  [CourseMaterial] where [DocumentID]='" + id + "' and [PublishedStatus]=1", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                if (dr.Read() == true)
                {
                    lblCategory.Text = "<b>Course Code#:</b> " + dr.GetString(1);
                    lblName.Text = "<b>Name#:</b> " + dr.GetString(2);
                    lblDatePublished.Text = "<b>Published#:</b> " + dr.GetDateTime(3).ToString("MMMM dd,yyyy");
                    lblNote.Text = "<b>Description#:</b> " + dr.GetString(4);
                }
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            // logger.Error(ex.Message);
        }

    }



    private void loadFiles()
    {

    }

    protected void btnDownload1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string fileId = lb.ToolTip;
        string documentid = lblContentID.Text.Replace("<b>ContentID#:</b>", "").Trim();

        SqlConnection cnn = new SqlConnection(strConnectionString);
        cnn.Open();

        string Qry = "";
        Qry = "SELECT [FilePath],[FileSize],[ContentType] FROM  [CourseMaterial] where srn =" + int.Parse(fileId) + " and [DocumentID]='" + documentid + "'";
        
        SqlCommand cmd = new SqlCommand(Qry, cnn);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows != false)
        {
            if (dr.Read() == true)
            {
                string path = dr.GetString(0);

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                string contentlength = dr.GetValue(1).ToString();
                string contentype = dr.GetString(2);



                if (file.Exists) //Then  set appropriate headers
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", contentlength);
                Response.ContentType = contentype;
                Response.TransmitFile(file.FullName);
                //Response.End(); //if file does not exist
            }

        }
        dr.Dispose();
        cmd.Dispose();
        // Update the dounload counter
        cmd = new SqlCommand("Update [CourseMaterial] set [DownLoadCnt]=[DownLoadCnt] + 1 where srn =" + int.Parse(fileId), cnn);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        cnn.Dispose();
        cnn.Close();


        Response.End();
    }
}
