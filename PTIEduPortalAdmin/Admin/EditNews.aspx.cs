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

public partial class EditNews : System.Web.UI.Page
{


    NewsItem _news = new NewsItem();
    NewsItemBusiness _newsMgr = new NewsItemBusiness();

    protected void Page_Load(object sender, EventArgs e)
    {

        _news.Title = txtTitle.Text;
        _news.Body = txtBody.Text;
        _news.Active = ckActive.Checked;
        _news.Caption = txtCaption.Text;
       
        _news.NewsType = cbType.SelectedValue;
        _news.IsSingle = false;
        _news.Author = HttpContext.Current.User.Identity.Name;
        _news.AuthorPosition = string.Empty;
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
                adminPath = Server.MapPath("~/NewsImages/");

                path = adminPath.Replace("PTIEduPortalAdmin", "PTIEduPortal");

                var extension = System.IO.Path.GetExtension(fupImage.FileName);
                if (extension != null)
                {
                    String fileExtension =
                        extension.ToLower();

                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            path = path  + fupImage.FileName;

                            fupImage.PostedFile.SaveAs(path);
                            fupImage.PostedFile.SaveAs(adminPath + fupImage.FileName);

                            path = "~/NewsImages/" + fupImage.PostedFile.FileName;


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
            _news.ImagePath = imgNewsImage.ImageUrl;
            if(fupImage .HasFile )
            {
                _news.ImagePath = GetImagePath();
           }
           
           
            _news.NewsId = int.Parse(hdfNewsId .Value);
            
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
            _news.NewsId = int.Parse(hdfNewsId.Value);

            _newsMgr.DeleteRecord( _news.NewsId );

            EmptyControls();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    
    protected void grdNews_SelectedIndexChanged(object sender, EventArgs e)
    {
      var news =   _newsMgr.FindNewsItems(int.Parse(grdNews.SelectedValue.ToString()));
        hdfNewsId.Value = grdNews.SelectedValue.ToString() ;
        mvView.ActiveViewIndex = 1;
        LoadDetails(news);
    }

    private void LoadDetails(NewsItem news )
    {
        txtTitle.Text = news.Title;
        txtCaption.Text = news.Caption;
        txtBody.Text = news.Body;
        cbType.SelectedValue = news.NewsType;
        ckActive.Checked = news.Active;
        imgNewsImage.ImageUrl = news.ImagePath;

    }

    private void EmptyControls()
    {
        mvView.ActiveViewIndex = 0;
        txtTitle.Text = string .Empty ;
        txtCaption.Text = string.Empty;
        txtBody.Text = string.Empty;
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
