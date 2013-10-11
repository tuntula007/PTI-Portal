

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CourseOfStudy
    public partial class CourseOfStudy
    {

        #region Private Variables
        private int courseOfStudyID;
        private string courseOfStudyName;
        private int departmentID;
        private string duration;
        private int facultyID;
        private double minCGPA;
        private int minCrdtEarned;
        private int minCrdtRegistered;
        private string modeOfStudy;
        private string programme;
        #endregion

        #region Methods

            #region Clone()
            public CourseOfStudy Clone()
            {
                // Create New Object
                CourseOfStudy NewCourseOfStudy = new CourseOfStudy();

                // Clone Each Property
                NewCourseOfStudy.courseOfStudyID = this.CourseOfStudyID;
                NewCourseOfStudy.courseOfStudyName = this.CourseOfStudyName;
                NewCourseOfStudy.departmentID = this.DepartmentID;
                NewCourseOfStudy.duration = this.Duration;
                NewCourseOfStudy.facultyID = this.FacultyID;
                NewCourseOfStudy.minCGPA = this.MinCGPA;
                NewCourseOfStudy.minCrdtEarned = this.MinCrdtEarned;
                NewCourseOfStudy.minCrdtRegistered = this.MinCrdtRegistered;
                NewCourseOfStudy.modeOfStudy = this.ModeOfStudy;
                NewCourseOfStudy.programme = this.Programme;

                // Return Cloned Object
                return NewCourseOfStudy;

            }
            #endregion


        #endregion

        #region Properties

            #region int CourseOfStudyID
            public int CourseOfStudyID
            {
                get
                {
                    return courseOfStudyID;
                }
            }
            #endregion

            #region string CourseOfStudyName
            public string CourseOfStudyName
            {
                get
                {
                    return courseOfStudyName;
                }
                set
                {
                    courseOfStudyName = value;
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

            #region string Duration
            public string Duration
            {
                get
                {
                    return duration;
                }
                set
                {
                    duration = value;
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

            #region double MinCGPA
            public double MinCGPA
            {
                get
                {
                    return minCGPA;
                }
                set
                {
                    minCGPA = value;
                }
            }
            #endregion

            #region int MinCrdtEarned
            public int MinCrdtEarned
            {
                get
                {
                    return minCrdtEarned;
                }
                set
                {
                    minCrdtEarned = value;
                }
            }
            #endregion

            #region int MinCrdtRegistered
            public int MinCrdtRegistered
            {
                get
                {
                    return minCrdtRegistered;
                }
                set
                {
                    minCrdtRegistered = value;
                }
            }
            #endregion

            #region string ModeOfStudy
            public string ModeOfStudy
            {
                get
                {
                    return modeOfStudy;
                }
                set
                {
                    modeOfStudy = value;
                }
            }
            #endregion

            #region string Programme
            public string Programme
            {
                get
                {
                    return programme;
                }
                set
                {
                    programme = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
