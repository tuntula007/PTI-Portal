

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Session
    public partial class Session
    {

        #region Private Variables
        private int activeStatus;
        private string createdBy;
        private DateTime createdDate;
        private string sessionName;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public Session Clone()
            {
                // Create New Object
                Session NewSession = new Session();

                // Clone Each Property
                NewSession.activeStatus = this.ActiveStatus;
                NewSession.createdBy = this.CreatedBy;
                NewSession.createdDate = this.CreatedDate;
                NewSession.sessionName = this.SessionName;
                NewSession.srn = this.Srn;

                // Return Cloned Object
                return NewSession;

            }
            #endregion


        #endregion

        #region Properties

            #region int ActiveStatus
            public int ActiveStatus
            {
                get
                {
                    return activeStatus;
                }
                set
                {
                    activeStatus = value;
                }
            }
            #endregion

            #region string CreatedBy
            public string CreatedBy
            {
                get
                {
                    return createdBy;
                }
                set
                {
                    createdBy = value;
                }
            }
            #endregion

            #region DateTime CreatedDate
            public DateTime CreatedDate
            {
                get
                {
                    return createdDate;
                }
                set
                {
                    createdDate = value;
                }
            }
            #endregion

            #region string SessionName
            public string SessionName
            {
                get
                {
                    return sessionName;
                }
                set
                {
                    sessionName = value;
                }
            }
            #endregion

            #region int Srn
            public int Srn
            {
                get
                {
                    return srn;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
