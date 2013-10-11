

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CourseTypes
    public partial class CourseTypes
    {

        #region Private Variables
        private string courseType;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public CourseTypes Clone()
            {
                // Create New Object
                CourseTypes NewCourseTypes = new CourseTypes();

                // Clone Each Property
                NewCourseTypes.courseType = this.CourseType;
                NewCourseTypes.srn = this.Srn;

                // Return Cloned Object
                return NewCourseTypes;

            }
            #endregion


        #endregion

        #region Properties

            #region string CourseType
            public string CourseType
            {
                get
                {
                    return courseType;
                }
                set
                {
                    courseType = value;
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
