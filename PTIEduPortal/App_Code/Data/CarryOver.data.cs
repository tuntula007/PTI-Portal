

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CarryOver
    public partial class CarryOver
    {

        #region Private Variables
        private string academicLevel;
        private string courseCode;
        private string fullName;
        private string matricNo;
        private string semester;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public CarryOver Clone()
            {
                // Create New Object
                CarryOver NewCarryOver = new CarryOver();

                // Clone Each Property
                NewCarryOver.academicLevel = this.AcademicLevel;
                NewCarryOver.courseCode = this.CourseCode;
                NewCarryOver.fullName = this.FullName;
                NewCarryOver.matricNo = this.MatricNo;
                NewCarryOver.semester = this.Semester;
                NewCarryOver.srn = this.Srn;

                // Return Cloned Object
                return NewCarryOver;

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

            #region string CourseCode
            public string CourseCode
            {
                get
                {
                    return courseCode;
                }
                set
                {
                    courseCode = value;
                }
            }
            #endregion

            #region string FullName
            public string FullName
            {
                get
                {
                    return fullName;
                }
                set
                {
                    fullName = value;
                }
            }
            #endregion

            #region string MatricNo
            public string MatricNo
            {
                get
                {
                    return matricNo;
                }
                set
                {
                    matricNo = value;
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
            }
            #endregion

        #endregion

    }
    #endregion

}
