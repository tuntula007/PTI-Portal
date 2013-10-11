

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class DownloadType
    public partial class DownloadType
    {

        #region Private Variables
       
        private int typeId;
        private string  type; 
        
        #endregion

        #region Methods

        #region Clone()
        public DownloadType   Clone()
        {
            // Create New Object
            var newDownloadType = new DownloadType() 
                {
                    type = this.Type,
                    typeId = this.TypeId,
                    
                };

            // Clone Each Property

            // Return Cloned Object
            return newDownloadType;

        }
        #endregion

        #endregion

        #region Properties


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

        #region string Type
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type  = value;
            }
        }
        #endregion
       
       
        #endregion

    }
    #endregion

}
