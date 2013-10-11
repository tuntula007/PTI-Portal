

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Courses
    public partial class Courses
    {

        #region Private Variables
        private string academicLevel;
        private string courseCode;
        private string courseTitle;
        private string createdBy;
        private DateTime createdDate;
        private int departmentID;
        private int facultyID;
        private int passMark;
        private string semester;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public Courses Clone()
            {
                // Create New Object
                Courses NewCourses = new Courses();

                // Clone Each Property
                NewCourses.academicLevel = this.AcademicLevel;
                NewCourses.courseCode = this.CourseCode;
                NewCourses.courseTitle = this.CourseTitle;
                NewCourses.createdBy = this.CreatedBy;
                NewCourses.createdDate = this.CreatedDate;
                NewCourses.departmentID = this.DepartmentID;
                NewCourses.facultyID = this.FacultyID;
                NewCourses.passMark = this.PassMark;
                NewCourses.semester = this.Semester;
                NewCourses.srn = this.Srn;

                // Return Cloned Object
                return NewCourses;

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

            #region string CourseTitle
            public string CourseTitle
            {
                get
                {
                    return courseTitle;
                }
                set
                {
                    courseTitle = value;
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

            #region int DepartmentID
            public int DepartmentID
            {
                get
                {
                    return departmentID;
                }
                set
                {
                    departmentID = value;
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
                set
                {
                    facultyID = value;
                }
            }
            #endregion

            #region int PassMark
            public int PassMark
            {
                get
                {
                    return passMark;
                }
                set
                {
                    passMark = value;
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
