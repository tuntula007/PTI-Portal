

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class DeptCourses
    public partial class DeptCourses
    {

        #region Private Variables
        private string academicLevel;
        private string assignedLecturer;
        private string courseCode;
        private int courseOfStudyID;
        private string courseTitle;
        private string courseType;
        private string createdBy;
        private DateTime createdDate;
        private double creditLoad;
        private string departmentID;
        private string facultyID;
        private string modeOfStudy;
        private string programme;
        private string semester;
        private string sessionName;
        private int srn;
        private string approvalStatus;
        private int isApproved;
        private string staffNumber;
        #endregion

        #region Methods

            #region Clone()
            public DeptCourses Clone()
            {
                // Create New Object
                DeptCourses NewDeptCourses = new DeptCourses();

                // Clone Each Property
                NewDeptCourses.academicLevel = this.AcademicLevel;
                NewDeptCourses.assignedLecturer = this.AssignedLecturer;
                NewDeptCourses.courseCode = this.CourseCode;
                NewDeptCourses.courseOfStudyID = this.CourseOfStudyID;
                NewDeptCourses.courseTitle = this.CourseTitle;
                NewDeptCourses.courseType = this.CourseType;
                NewDeptCourses.createdBy = this.CreatedBy;
                NewDeptCourses.createdDate = this.CreatedDate;
                NewDeptCourses.creditLoad = this.CreditLoad;
                NewDeptCourses.departmentID = this.DepartmentID;
                NewDeptCourses.facultyID = this.FacultyID;
                NewDeptCourses.modeOfStudy = this.ModeOfStudy;
                NewDeptCourses.programme = this.Programme;
                NewDeptCourses.semester = this.Semester;
                NewDeptCourses.sessionName = this.SessionName;
                NewDeptCourses.srn = this.Srn;
                NewDeptCourses.approvalStatus = this.ApprovalStatus;
                NewDeptCourses.isApproved = this.IsApproved;
                NewDeptCourses.staffNumber = this.StaffNumber;

                // Return Cloned Object
                return NewDeptCourses;

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

            #region string AssignedLecturer
            public string AssignedLecturer
            {
                get
                {
                    return assignedLecturer;
                }
                set
                {
                    assignedLecturer = value;
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

            #region double CreditLoad
            public double CreditLoad
            {
                get
                {
                    return creditLoad;
                }
                set
                {
                    creditLoad = value;
                }
            }
            #endregion

            #region string DepartmentID
            public string DepartmentID
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

            #region string FacultyID
            public string FacultyID
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

            #region string ApprovalStatus
            public string ApprovalStatus
            {
                get
                {
                    return approvalStatus;
                }
                set
                {
                    approvalStatus = value;
                }
            }
            #endregion

            #region int IsApproved
            public int IsApproved
            {
                get
                {
                    return isApproved;
                }
                set
                {
                    isApproved = value;
                }
            }
            #endregion

            #region string StaffNumber
            public string StaffNumber
            {
                get
                {
                    return staffNumber;
                }
                set
                {
                    staffNumber = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
