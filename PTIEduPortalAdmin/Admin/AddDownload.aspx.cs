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
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;

public partial class AddDownload : System.Web.UI.Page
{


    Download _download = new Download();
    DownloadBusiness _downloadMgr = new DownloadBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {
        cbType.DataBind();
        _download.Title = txtTitle.Text;
        _download.Active = ckActive.Checked;
        _download.TypeId = int.Parse(cbType.SelectedValue);
        _download.UploadedBy = HttpContext.Current.User.Identity.Name;
        _download.UploadedOn = DateTime.Now;



    }

    private string GetImagePath()
    {
        String path = string.Empty;



        try
        {
            if (fupImage.HasFile)
            {
                path = Server.MapPath("~/Downloads/");

                path = path.Replace("PTIEduPortalAdmin", "PTIEduPortal");

                var extension = System.IO.Path.GetExtension(fupImage.FileName);
                if (extension != null)
                {
                    String fileExtension =
                        extension.ToLower();

                    String[] allowedExtensions = { ".pdf" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            path = path + fupImage.FileName;

                            fupImage.PostedFile.SaveAs(path);


                            path = "Downloads/" + fupImage.PostedFile.FileName;


                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid File Type. Only Pdf Files Allowed");
                }
            }



        }
        catch (Exception ex)
        {

        }


        return path;
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            _download .DocPath  = GetImagePath();

            _downloadMgr .SaveRecord(_download );

            EmptyControls();
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void EmptyControls()
    {
        txtTitle.Text = string.Empty;
       
        cbType.SelectedIndex = 0;
        ckActive.Checked = false;
        
    }




}
