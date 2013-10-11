using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Caching ;
using System.Security.Principal ;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Summary description for BizObject
/// </summary>
public  abstract  class BizObject
{
    
    protected const int MAXROWS = 10;
    protected static Cache Cache
    {
       // get { return HttpRuntime.Cache;} // HttpContext.r.Current.Cache; }
        get { return HttpContext.Current.Cache; }
    }
    protected static IPrincipal CurrentUser
    {
        get { return HttpContext.Current.User; }
    }
    protected static string CurrentUserName
    {
        get 
        {
            string userName = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                userName = HttpContext.Current.User.Identity.Name;
            return userName;
        }
    }
    protected static int GetPageIndex(int startRowIndex, int maximunRows)
    {
        if (maximunRows <= 0)
            return 0;
        else
            return (int)Math.Floor((double)startRowIndex / (double)maximunRows);
    }
    protected static string EncodeText(string content)
    {
        content = HttpUtility.HtmlEncode(content);
        content = content.Replace(" ", "&nbsp;&nbsp;").Replace("\n", "<br>");
        return content;
    }
    protected static string ConvertNullToEmptyString(string input)
    {
        return (input == null )?"": input;
    }
    protected static void PurgeCacheItems(string prefix)
    {
        prefix = prefix.ToLower();
        List<string> itemsToRemove = new List<string>();
        IDictionaryEnumerator enumerator = BizObject.Cache.GetEnumerator ();
        while ( enumerator .MoveNext ())
        {
            if (enumerator .Key.ToString ().ToLower ().StartsWith (prefix ))
                itemsToRemove .Add (enumerator .Key .ToString ());

        }
        foreach (string itemToRemove in itemsToRemove )
            BizObject .Cache .Remove (itemToRemove );
    }

    public BizObject()
	{
		//
		// TODO: Add constructor logic here
		//
       
	}
}
