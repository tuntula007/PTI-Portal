

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Faculty
    public partial class Faculty
    {

        #region Private Variables
        private string createdBy;
        private DateTime createdDate;
        private string deanOfFaculty;
        private int facultyID;
        private string facultyName;
        private string facultyPrefix;
        #endregion

        #region Methods

            #region Clone()
            public Faculty Clone()
            {
                // Create New Object
                Faculty NewFaculty = new Faculty();

                // Clone Each Property
                NewFaculty.createdBy = this.CreatedBy;
                NewFaculty.createdDate = this.CreatedDate;
                NewFaculty.deanOfFaculty = this.DeanOfFaculty;
                NewFaculty.facultyID = this.FacultyID;
                NewFaculty.facultyName = this.FacultyName;
                NewFaculty.facultyPrefix = this.FacultyPrefix;

                // Return Cloned Object
                return NewFaculty;

            }
            #endregion


        #endregion

        #region Properties

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

            #region string DeanOfFaculty
            public string DeanOfFaculty
            {
                get
                {
                    return deanOfFaculty;
                }
                set
                {
                    deanOfFaculty = value;
                }
            }
            #endregion

            #region int FacultyID
            public int FacultyID
            {
                get
                {
                    return facultyID;
                }
            }
            #endregion

            #region string FacultyName
            public string FacultyName
            {
                get
                {
                    return facultyName;
                }
                set
                {
                    facultyName = value;
                }
            }
            #endregion

            #region string FacultyPrefix
            public string FacultyPrefix
            {
                get
                {
                    return facultyPrefix;
                }
                set
                {
                    facultyPrefix = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
