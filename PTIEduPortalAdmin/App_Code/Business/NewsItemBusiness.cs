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

    public class NewsItemBusiness : BaseBusiness
    {
        public NewsItemBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool SaveRecord(NewsItem news)
        {
            var db = new DBAccess();

            db.AddParameter(new SqlParameter("@Title", news.Title));
            db.AddParameter(new SqlParameter("@Caption", news.Caption));
            db.AddParameter(new SqlParameter("@Body", news.Body));
            db.AddParameter(new SqlParameter("@Type", news.NewsType));
            db.AddParameter(new SqlParameter("@ImagePath", news.ImagePath));
            db.AddParameter(new SqlParameter("@Active", news.Active));
            db.AddParameter(new SqlParameter("@IsSingle", news.IsSingle));
            db.AddParameter(new SqlParameter("@Author", news.Author));
            db.AddParameter(new SqlParameter("@AuthorPosition", news.AuthorPosition));
            db.AddParameter(new SqlParameter("@UploadedBy", news.UploadedBy));
            db.AddParameter(new SqlParameter("@UploadedOn", news.UploadedOn));
            //db.AddParameter(new SqlParameter("@LastUpdatedOn", news.LastUpdateOn ));
            //db.AddParameter(new SqlParameter("@LastUpdatedBy", news.LastUpdateBy ));

            int i = db.ExecuteNonQuery("Insert_NewsItem");

            db.Dispose();
            if (i > 0) return true;
            else return false;
        }

        public bool UpdateRecord(NewsItem news)
        {
            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@NewsId", news.NewsId));
            db.AddParameter(new SqlParameter("@Title", news.Title));
            db.AddParameter(new SqlParameter("@Caption", news.Caption));
            db.AddParameter(new SqlParameter("@Body", news.Body));
            db.AddParameter(new SqlParameter("@Type", news.NewsType));
            db.AddParameter(new SqlParameter("@ImagePath", news.ImagePath));
            db.AddParameter(new SqlParameter("@Active", news.Active));
            db.AddParameter(new SqlParameter("@IsSingle", news.IsSingle));
            db.AddParameter(new SqlParameter("@Author", news.Author));
            db.AddParameter(new SqlParameter("@AuthorPosition", news.AuthorPosition));
            db.AddParameter(new SqlParameter("@LastUpdatedOn", news.LastUpdateOn));
            db.AddParameter(new SqlParameter("@LastUpdatedBy", news.LastUpdateBy));

            int i = db.ExecuteNonQuery("Update_NewsItem");

            db.Dispose();
            if (i > 0) return true;
            return false;
        }

        public bool DeleteRecord(int newsId)
        {
            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@NewsId", newsId));


            int i = db.ExecuteNonQuery("Delete_NewsItem");

            db.Dispose();
            if (i > 0) return true;
            return false;
        }

        public List<NewsItem> GetNewsItems(bool active)
        {

            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@Active", active));

            var dr = (SqlDataReader)db.ExecuteReader("GetNewsItems");
            var result = new List<NewsItem>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var news = new NewsItem();
                    //
                    news.Title = dr.GetString(dr.GetOrdinal("Title"));
                    news.Caption = dr.GetString(dr.GetOrdinal("Caption"));
                    news.NewsId = int.Parse(dr["NewsId"].ToString());
                    news.NewsType = dr.GetString(dr.GetOrdinal("Type"));
                    news.ImagePath = dr.GetString(dr.GetOrdinal("ImagePath"));
                    news.IsSingle = dr.GetBoolean(dr.GetOrdinal("IsSingle"));
                    news.Active = dr.GetBoolean(dr.GetOrdinal("Active"));
                    news.Body = dr.GetString(dr.GetOrdinal("Body"));
                    news.Author = dr.GetString(dr.GetOrdinal("Author"));
                    news.AuthorPosition = dr.GetString(dr.GetOrdinal("AuthorPosition"));
                    news.UploadedBy = dr.GetString(dr.GetOrdinal("UploadedBy"));
                    news.UploadedOn = dr.GetDateTime(dr.GetOrdinal("UploadedOn"));
                    news.LastUpdateBy = dr.GetString(dr.GetOrdinal("LastUpdatedBy"));
                    news.LastUpdateOn = dr.GetDateTime(dr.GetOrdinal("LastUpdatedOn"));
                    //
                    result.Add(news);
                }

            }
            db.Dispose();
            return result;
        }

        public NewsItem FindNewsItems(int newsId)
        {

            var db = new DBAccess();
            db.AddParameter(new SqlParameter("@Id", newsId));

            var ds = db.ExecuteDataSet("GetNewsItemsById");
            var result = new NewsItem();

            if (ds.Tables[0].Rows.Count > 0)
            {

                //while (dr.Read())
                //{
                result = new NewsItem
                   {

                       Title = ds.Tables[0].Rows[0]["Title"].ToString(),
                       Caption = ds.Tables[0].Rows[0]["Caption"].ToString(),
                       NewsId = int.Parse(ds.Tables[0].Rows[0]["NewsId"].ToString()),
                       NewsType = ds.Tables[0].Rows[0]["Type"].ToString(),
                       ImagePath = ds.Tables[0].Rows[0]["ImagePath"].ToString(),
                       IsSingle = (bool)ds.Tables[0].Rows[0]["IsSingle"],
                       Active = (bool)ds.Tables[0].Rows[0]["Active"],
                       Body = ds.Tables[0].Rows[0]["Body"].ToString(),
                       Author = ds.Tables[0].Rows[0]["Author"].ToString(),
                       AuthorPosition = ds.Tables[0].Rows[0]["AuthorPosition"].ToString(),
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