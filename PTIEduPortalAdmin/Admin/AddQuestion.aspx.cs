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

public partial class AddQuestion : System.Web.UI.Page
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
        _news.UploadedBy = HttpContext.Current.User.Identity.Name;
        _news.UploadedOn = DateTime.Now;



    }

    private string GetImagePath()
    {
        String path = string.Empty;



        try
        {
            if (fupImage.HasFile)
            {
                path = Server.MapPath("~/NewsImages/");

                path = path.Replace("PTIEduPortalAdmin", "PTIEduPortal");

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
            _news.ImagePath = GetImagePath();

            _newsMgr.SaveRecord(_news);

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
        txtCaption.Text = string.Empty;
        txtBody.Text = string.Empty;
        cbType.SelectedIndex = 0;
        ckActive.Checked = false;
        imgNewsImage.ImageUrl = string.Empty;

    }




}
