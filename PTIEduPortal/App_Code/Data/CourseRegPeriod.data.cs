

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CourseRegPeriod
    public partial class CourseRegPeriod
    {

        #region Private Variables
        private DateTime earlyRegDate;
        private DateTime lateRegDate;
        private string sessionName;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public CourseRegPeriod Clone()
            {
                // Create New Object
                CourseRegPeriod NewCourseRegPeriod = new CourseRegPeriod();

                // Clone Each Property
                NewCourseRegPeriod.earlyRegDate = this.EarlyRegDate;
                NewCourseRegPeriod.lateRegDate = this.LateRegDate;
                NewCourseRegPeriod.sessionName = this.SessionName;
                NewCourseRegPeriod.srn = this.Srn;

                // Return Cloned Object
                return NewCourseRegPeriod;

            }
            #endregion


        #endregion

        #region Properties

            #region DateTime EarlyRegDate
            public DateTime EarlyRegDate
            {
                get
                {
                    return earlyRegDate;
                }
                set
                {
                    earlyRegDate = value;
                }
            }
            #endregion

            #region DateTime LateRegDate
            public DateTime LateRegDate
            {
                get
                {
                    return lateRegDate;
                }
                set
                {
                    lateRegDate = value;
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
                set
                {
                    srn = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
