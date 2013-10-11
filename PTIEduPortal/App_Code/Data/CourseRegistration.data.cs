

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class CourseRegistration
    public partial class CourseRegistration
    {

        #region Private Variables
        private string academicLevel;
        private double cA;
        private string category;
        private string courseCode;
        private string courseOfStudy;
        private int courseOfStudyID;
        private string courseType;
        private string courseUnit;
        private int departmentID;
        private double exam;
        private int facultyID;
        private string grade;
        private double gradePoint;
        private int isApproved;
        private bool isProcessed;
        private string marks;
        private string matricNumber;
        private string modeOfStudy;
        private double passMark;
        private string programme;
        private int programmeID;
        private string regNo;
        private string semester;
        private string sessionName;
        private int srn;
        private string staffNumber;
        #endregion

        #region Methods

            #region Clone()
            public CourseRegistration Clone()
            {
                // Create New Object
                CourseRegistration NewCourseRegistration = new CourseRegistration();

                // Clone Each Property
                NewCourseRegistration.academicLevel = this.AcademicLevel;
                NewCourseRegistration.cA = this.CA;
                NewCourseRegistration.category = this.Category;
                NewCourseRegistration.courseCode = this.CourseCode;
                NewCourseRegistration.courseOfStudy = this.CourseOfStudy;
                NewCourseRegistration.courseOfStudyID = this.CourseOfStudyID;
                NewCourseRegistration.courseType = this.CourseType;
                NewCourseRegistration.courseUnit = this.CourseUnit;
                NewCourseRegistration.departmentID = this.DepartmentID;
                NewCourseRegistration.exam = this.Exam;
                NewCourseRegistration.facultyID = this.FacultyID;
                NewCourseRegistration.grade = this.Grade;
                NewCourseRegistration.gradePoint = this.GradePoint;
                NewCourseRegistration.marks = this.Marks;
                NewCourseRegistration.matricNumber = this.MatricNumber;
                NewCourseRegistration.modeOfStudy = this.ModeOfStudy;
                NewCourseRegistration.passMark = this.PassMark;
                NewCourseRegistration.programme = this.Programme;
                NewCourseRegistration.programmeID = this.ProgrammeID;
                NewCourseRegistration.regNo = this.RegNo;
                NewCourseRegistration.semester = this.Semester;
                NewCourseRegistration.sessionName = this.SessionName;
                NewCourseRegistration.srn = this.Srn;
                NewCourseRegistration.staffNumber = this.StaffNumber;
                NewCourseRegistration.isApproved = this.IsApproved;
                // Return Cloned Object
                return NewCourseRegistration;

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

            #region double CA
            public double CA
            {
                get
                {
                    return cA;
                }
                set
                {
                    cA = value;
                }
            }
            #endregion

            #region string Category
            public string Category
            {
                get
                {
                    return category;
                }
                set
                {
                    category = value;
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

            #region string CourseOfStudy
            public string CourseOfStudy
            {
                get
                {
                    return courseOfStudy;
                }
                set
                {
                    courseOfStudy = value;
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

            #region string CourseUnit
            public string CourseUnit
            {
                get
                {
                    return courseUnit;
                }
                set
                {
                    courseUnit = value;
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

            #region double Exam
            public double Exam
            {
                get
                {
                    return exam;
                }
                set
                {
                    exam = value;
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

            #region string Grade
            public string Grade
            {
                get
                {
                    return grade;
                }
                set
                {
                    grade = value;
                }
            }
            #endregion

            #region double GradePoint
            public double GradePoint
            {
                get
                {
                    return gradePoint;
                }
                set
                {
                    gradePoint = value;
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

            #region bool IsProcessed
            public bool IsProcessed
            {
                get
                {
                    return isProcessed;
                }
                set
                {
                    isProcessed = value;
                }
            }
            #endregion

            #region string Marks
            public string Marks
            {
                get
                {
                    return marks;
                }
                set
                {
                    marks = value;
                }
            }
            #endregion

            #region string MatricNumber
            public string MatricNumber
            {
                get
                {
                    return matricNumber;
                }
                set
                {
                    matricNumber = value;
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

            #region double PassMark
            public double PassMark
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

            #region int ProgrammeID
            public int ProgrammeID
            {
                get
                {
                    return programmeID;
                }
                set
                {
                    programmeID = value;
                }
            }
            #endregion

            #region string RegNo
            public string RegNo
            {
                get
                {
                    return regNo;
                }
                set
                {
                    regNo = value;
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
