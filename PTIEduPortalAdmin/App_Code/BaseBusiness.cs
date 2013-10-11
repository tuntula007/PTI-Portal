using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for BaseBusiness
/// </summary>
public abstract  class BaseBusiness : BizObject 
{
    protected static bool  enableCache = bool.Parse ( System.Configuration.ConfigurationManager.AppSettings["EnableCaching"]);  //get from congig
    protected static int cacheDura = int.Parse ( System.Configuration.ConfigurationManager.AppSettings["CacheDuration"]); 
    protected static void CacheData(string key, object data)
    {
        if (enableCache && data != null)
        {
            BizObject.Cache.Insert(key, data, null,
                DateTime.Now.AddSeconds(cacheDura), TimeSpan.Zero); 
        }
    }
    public BaseBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
