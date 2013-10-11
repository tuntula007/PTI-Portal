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

public partial class EditDownload : System.Web.UI.Page
{


    Download  _news = new Download() ;
    DownloadBusiness _newsMgr = new DownloadBusiness() ;

    protected void Page_Load(object sender, EventArgs e)
    {

        cbType.DataBind();
        _news.Title = txtTitle.Text;
       _news.Active = ckActive.Checked;
        _news.TypeId = int.Parse(cbType.SelectedValue);
     
        _news.LastUpdateBy = HttpContext.Current.User.Identity.Name;
        _news.LastUpdateOn = DateTime.Now;



    }

    private string GetImagePath()
    {
        String path = string.Empty;
        string adminPath = string.Empty;


        try
        {
            if (fupImage.HasFile)
            {
                adminPath = Server.MapPath("~/Downloads/");

                path = adminPath.Replace("PTIEduPortalAdmin", "PTIEduPortal");

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
                            path = path  + fupImage.FileName;

                            fupImage.PostedFile.SaveAs(path);
                            fupImage.PostedFile.SaveAs(adminPath + fupImage.FileName);

                            path = "Downloads/" + fupImage.PostedFile.FileName;


                        }
                    }
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
            _news.DocPath = imgNewsImage.ImageUrl;
            if(fupImage .HasFile )
            {
                _news.DocPath  = GetImagePath();
           }
           
           
            _news.DownloadId  = int.Parse(hdfNewsId .Value);
            
            _newsMgr.UpdateRecord( _news);

            EmptyControls();
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _news.DownloadId = int.Parse(hdfNewsId.Value);

            _newsMgr.DeleteRecord( _news.DownloadId  );

            EmptyControls();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    
    protected void grdNews_SelectedIndexChanged(object sender, EventArgs e)
    {
      var news =   _newsMgr.FindDownloads(int.Parse(grdNews.SelectedValue.ToString()));
        hdfNewsId.Value = grdNews.SelectedValue.ToString() ;
        mvView.ActiveViewIndex = 1;
        LoadDetails(news);
    }

    private void LoadDetails(Download  download )
    {
        txtTitle.Text = download.Title;
        cbType.SelectedValue = download.TypeId.ToString();
        ckActive.Checked = download.Active;
        imgNewsImage.ImageUrl = download.DocPath ;

    }

    private void EmptyControls()
    {
        mvView.ActiveViewIndex = 0;
        txtTitle.Text = string .Empty ;
       cbType.SelectedIndex = 0;
        ckActive.Checked = false ;
        imgNewsImage.ImageUrl = string.Empty;
        grdNews.DataBind();

    }

    protected void linkBack_Click(object sender, EventArgs e)
    {
        EmptyControls();
    }
   
}
