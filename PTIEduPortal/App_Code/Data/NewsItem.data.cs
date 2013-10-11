

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class NewsItem
    public partial class NewsItem
    {

        #region Private Variables
        private string title;
        private int newsId;
        private string caption;
        private string body;
        private string type;
        private bool isSingle;
        private bool active;
        private string author;
        private string authorPosition;
        private string uploadedBy;
        private DateTime uploadedOn;
        private string lastUpdateBy;
        private DateTime lastUpdateOn;
        private string imagePath;

        #endregion

        #region Methods

        #region Clone()
        public NewsItem  Clone()
        {
            // Create New Object
            var newNewsItem = new NewsItem
                {
                    newsId = this.NewsId,
                    title = this.Title,
                    caption = this.Caption,
                    body = this.Body,
                    author = this.Author,
                    authorPosition = this.AuthorPosition,
                    uploadedBy = this.UploadedBy,
                    uploadedOn = this.UploadedOn,
                    lastUpdateBy = this.LastUpdateBy,
                    lastUpdateOn = this.LastUpdateOn,
                    isSingle = this.IsSingle,
                    type = this.NewsType,
                    active =this .Active ,
                    imagePath =this .ImagePath

                };

            // Clone Each Property

            // Return Cloned Object
            return newNewsItem ;

        }
        #endregion


        #endregion

        #region Properties

        #region int NewsId
        public int NewsId
        {
            get
            {
                return newsId ;
            }
            set
            {
                newsId = value;
            }
        }
        #endregion

        #region string Title
        public string  Title
        {
            get
            {
                return title;
            }
            set
            {
                title  = value;
            }
        }
        #endregion

        #region string Body
        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }
        #endregion

        #region string Caption
        public string Caption
        {
            get
            {
                return caption ;
            }
            set
            {
                caption = value;
            }
        }
        #endregion

        #region string Author
        public string Author
        {
            get
            {
                return author ;
            }
            set
            {
                author  = value;
            }
        }
        #endregion

        #region string AuthorPosition
        public string AuthorPosition
        {
            get
            {
                return authorPosition ;
            }
            set
            {
                authorPosition  = value;
            }
        }
        #endregion

        #region string UploadedBy
        public string UploadedBy
        {
            get
            {
                return uploadedBy ;
            }
            set
            {
                uploadedBy  = value;
            }
        }
        #endregion

        #region string LastUpdateBy
        public string LastUpdateBy
        {
            get
            {
                return lastUpdateBy ;
            }
            set
            {
                lastUpdateBy  = value;
            }
        }
        #endregion

        #region Datetime UploadedOn
        public DateTime  UploadedOn
        {
            get
            {
                return uploadedOn ;
            }
            set
            {
                uploadedOn  = value;
            }
        }
        #endregion

        #region Datetime LastUpdateOn
        public DateTime LastUpdateOn
        {
            get
            {
                return lastUpdateOn ;
            }
            set
            {
                lastUpdateOn  = value;
            }
        }
        #endregion

        #region String NewsType
        public string  NewsType
        {
            get
            {
                return type ;
            }
            set
            {
                type  = value;
            }
        }
        #endregion

        #region String ImagePath
        public string ImagePath
        {
            get
            {
                return imagePath ;
            }
            set
            {
                imagePath  = value;
            }
        }
        #endregion

        #region bool IsSingle
        public bool  IsSingle
        {
            get
            {
                return isSingle ;
            }
            set
            {
                isSingle  = value;
            }
        }
        #endregion

        #region bool Active
        public bool Active
        {
            get
            {
                return active ;
            }
            set
            {
                active = value;
            }
        }
        #endregion

        #endregion

    }
    #endregion

}
