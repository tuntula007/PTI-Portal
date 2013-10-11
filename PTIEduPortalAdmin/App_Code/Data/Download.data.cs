

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Download
    public partial class Download
    {

        #region Private Variables
        private string title;
        private int downloadId;
        private int typeId;
        private bool active; 
        private string uploadedBy;
        private DateTime uploadedOn;
        private string lastUpdateBy;
        private DateTime lastUpdateOn;
        private string docPath;

        #endregion

        #region Methods

        #region Clone()
        public Download   Clone()
        {
            // Create New Object
            var newDownload = new Download() 
                {
                    downloadId  = this.DownloadId,
                    title = this.Title,
                    typeId = this.TypeId,
                    uploadedBy = this.UploadedBy,
                    uploadedOn = this.UploadedOn,
                    lastUpdateBy = this.LastUpdateBy,
                    lastUpdateOn = this.LastUpdateOn,
                    active =this .Active ,
                    docPath  =this .DocPath 

                };

            // Clone Each Property

            // Return Cloned Object
            return newDownload  ;

        }
        #endregion

        #endregion

        #region Properties

        #region int DownloadId
        public int DownloadId
        {
            get
            {
                return downloadId  ;
            }
            set
            {
                downloadId  = value;
            }
        }
        #endregion


        #region int TypeId
        public int TypeId
        {
            get
            {
                return typeId ;
            }
            set
            {
                typeId = value;
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

        #region String DocPath
        public string DocPath
        {
            get
            {
                return docPath  ;
            }
            set
            {
                docPath = value;
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
