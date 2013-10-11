using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Data;
using System.Data.SqlClient;
using System.Collections;
using CybSoft.EduPortal.Business;
/// <summary>
/// Summary description for FeedBackBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{

    public class FeedBackBusiness
    {
        public FeedBackBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static bool  SaveRecord(FeedBack fdbck)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@Category", fdbck.Category));
            db.AddParameter(new SqlParameter("@Comment", fdbck.Comment));
            db.AddParameter(new SqlParameter("@Email", fdbck.Email));
            db.AddParameter(new SqlParameter("@MatricNo", fdbck.MatricNo));
            db.AddParameter(new SqlParameter("@Name", fdbck.Name));
            db.AddParameter(new SqlParameter("@Phone", fdbck.Phone));
            db.AddParameter(new SqlParameter("@DateSubmitted", fdbck.DateSubmitted));
            
            int i = db.ExecuteNonQuery("FeedBack_SaveRecord");

            db.Dispose();
            if ( i > 0)  return true ;
             else return false ;
        }

        public static int SaveFeedBack(FeedBack fdbck)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@Category", fdbck.Category));
            db.AddParameter(new SqlParameter("@Comment", fdbck.Comment));
            db.AddParameter(new SqlParameter("@Email", fdbck.Email));
            db.AddParameter(new SqlParameter("@MatricNo", fdbck.MatricNo));
            db.AddParameter(new SqlParameter("@Name", fdbck.Name));
            db.AddParameter(new SqlParameter("@Phone", fdbck.Phone));
            db.AddParameter(new SqlParameter("@DateSubmitted", fdbck.DateSubmitted));

            int i = (int)db.ExecuteScalar("FeedBack_SaveRecord");

            db.Dispose();
            if (i > 0) return i;
            else return 0;
        }

    }
}