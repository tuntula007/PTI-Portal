

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CreditLoad
    public partial class CreditLoad
    {

        #region Private Variables
        private string academicLevel;
        private int courseOfStudyID;
        private int maxCore;
        private int maxCreditLoad;
        private int maxElective;
        private int minCore;
        private int minCreditLoad;
        private int minElective;
        private string semester;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public CreditLoad Clone()
            {
                // Create New Object
                CreditLoad NewCreditLoad = new CreditLoad();

                // Clone Each Property
                NewCreditLoad.academicLevel = this.AcademicLevel;
                NewCreditLoad.courseOfStudyID = this.CourseOfStudyID;
                NewCreditLoad.maxCore = this.MaxCore;
                NewCreditLoad.maxCreditLoad = this.MaxCreditLoad;
                NewCreditLoad.maxElective = this.MaxElective;
                NewCreditLoad.minCore = this.MinCore;
                NewCreditLoad.minCreditLoad = this.MinCreditLoad;
                NewCreditLoad.minElective = this.MinElective;
                NewCreditLoad.semester = this.Semester;
                NewCreditLoad.srn = this.Srn;

                // Return Cloned Object
                return NewCreditLoad;

            }
            #endregion


        #endregion

        #region Properties

            #region string AcademicLevel
            public string AcademicLevel
            {
                get
                {
                    return academicLevel;
                }
                set
                {
                    academicLevel = value;
                }
            }
            #endregion

            #region int CourseOfStudyID
            public int CourseOfStudyID
            {
                get
                {
                    return courseOfStudyID;
                }
                set
                {
                    courseOfStudyID = value;
                }
            }
            #endregion

            #region int MaxCore
            public int MaxCore
            {
                get
                {
                    return maxCore;
                }
                set
                {
                    maxCore = value;
                }
            }
            #endregion

            #region int MaxCreditLoad
            public int MaxCreditLoad
            {
                get
                {
                    return maxCreditLoad;
                }
                set
                {
                    maxCreditLoad = value;
                }
            }
            #endregion

            #region int MaxElective
            public int MaxElective
            {
                get
                {
                    return maxElective;
                }
                set
                {
                    maxElective = value;
                }
            }
            #endregion

            #region int MinCore
            public int MinCore
            {
                get
                {
                    return minCore;
                }
                set
                {
                    minCore = value;
                }
            }
            #endregion

            #region int MinCreditLoad
            public int MinCreditLoad
            {
                get
                {
                    return minCreditLoad;
                }
                set
                {
                    minCreditLoad = value;
                }
            }
            #endregion

            #region int MinElective
            public int MinElective
            {
                get
                {
                    return minElective;
                }
                set
                {
                    minElective = value;
                }
            }
            #endregion

            #region string Semester
            public string Semester
            {
                get
                {
                    return semester;
                }
                set
                {
                    semester = value;
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
