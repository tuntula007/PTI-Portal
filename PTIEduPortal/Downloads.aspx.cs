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

public partial class Downloads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void rptDownloadTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var item = e.Item;
        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            var childRepeater = (Repeater)e.Item.FindControl("rptDownloads");
            childRepeater.DataBind();
        }
    }
}
