using System;
using System.Collections.Generic;
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

namespace CybSoft.EduPortal.Business
{

    public class DownloadBusiness : BaseBusiness
    {
        public DownloadBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool SaveRecord(Download download)
        {
            var db = new DBAccess();

            db.AddParameter(new SqlParameter("@Title", download.Title));
            db.AddParameter(new SqlParameter("@TypeId", download.TypeId));
            db.AddParameter(new SqlParameter("@DocPath", download.DocPath));
            db.AddParameter(new SqlParameter("@Active", download.Active));
            db.AddParameter(new SqlParameter("@UploadedBy", download.UploadedBy));
            db.AddParameter(new SqlParameter("@UploadedOn", download.UploadedOn));


            int i = db.ExecuteNonQuery("Insert_Download");

            db.Dispose();
            if (i > 0) return true;
            else return false;
        }

        public bool UpdateRecord(Download download)
        {
            var db = new DBAccess();

            db.AddParameter(new SqlParameter("@DownloadId", download.DownloadId ));
            db.AddParameter(new SqlParameter("@Title", download.Title));
            db.AddParameter(new SqlParameter("@TypeId", download.TypeId));
            db.AddParameter(new SqlParameter("@DocPath", download.DocPath));
            db.AddParameter(new SqlParameter("@Active", download.Active));
            db.AddParameter(new SqlParameter("@LastUpdatedBy", download.LastUpdateBy));
            db.AddParameter(new SqlParameter("@LastUpdatedOn", download.LastUpdateOn));


            int i = db.ExecuteNonQuery("Update_Download");

            db.Dispose();
            if (i > 0) return true;
            return false;
        }

        public bool DeleteRecord(int downloadId)
        {
            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@DownloadId", downloadId));


            int i = db.ExecuteNonQuery("Delete_Download");

            db.Dispose();
            if (i > 0) return true;
            return false;
        }

        public List<Download> GetDownloads(bool active)
        {

            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@Active", active));

            var dr = (SqlDataReader)db.ExecuteReader("GetDownloads");
            var result = new List<Download>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var downloads = new Download();
                    //
                    downloads.DownloadId = int.Parse(dr["DownloadId"].ToString());
                    downloads.TypeId = int.Parse(dr["TypeId"].ToString());
                    downloads.Title = dr.GetString(dr.GetOrdinal("Title"));
                    downloads.DocPath = dr.GetString(dr.GetOrdinal("DocPath"));
                    downloads.Active = dr.GetBoolean(dr.GetOrdinal("Active"));
                    downloads.UploadedBy = dr.GetString(dr.GetOrdinal("UploadedBy"));
                    downloads.UploadedOn = dr.GetDateTime(dr.GetOrdinal("UploadedOn"));
                    downloads.LastUpdateBy = dr.GetString(dr.GetOrdinal("LastUpdatedBy"));
                    downloads.LastUpdateOn = dr.GetDateTime(dr.GetOrdinal("LastUpdatedOn"));
                    //
                    result.Add(downloads);
                }

            }
            db.Dispose();
            return result;
        }

        public Download FindDownloads(int downloadId)
        {

            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@Id", downloadId));

            var ds = db.ExecuteDataSet("GetDownloadsById");
            var result = new Download();

            if (ds.Tables[0].Rows.Count > 0)
            {

                //while (dr.Read())
                //{
                result = new Download()
                   {
                       DownloadId = int.Parse(ds.Tables[0].Rows[0]["DownloadId"].ToString()),
                       Title = ds.Tables[0].Rows[0]["Title"].ToString(),
                       TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString()),
                       DocPath = ds.Tables[0].Rows[0]["DocPath"].ToString(),
                       Active = (bool)ds.Tables[0].Rows[0]["Active"],
                       UploadedBy = ds.Tables[0].Rows[0]["UploadedBy"].ToString(),
                       UploadedOn = (DateTime)ds.Tables[0].Rows[0]["UploadedOn"],
                       LastUpdateBy = (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastUpdatedBy"].ToString())) ? "" : ds.Tables[0].Rows[0]["LastUpdatedBy"].ToString(),
                       LastUpdateOn = (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastUpdatedOn"].ToString())) ? DateTime.Now : (DateTime)ds.Tables[0].Rows[0]["LastUpdatedOn"]
                   };

                //}

            }
            db.Dispose();
            return result;
        }




    }
}