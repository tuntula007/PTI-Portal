using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DeptCos
/// </summary>
public class DeptCourses
{

        private string academicLevel;
        private string assignedLecturer;
        private string courseCode;
        private int courseOfStudyID;
        private string courseOfStudy;
        private string courseTitle;
        private string courseType;
        private string createdBy;
        private string createdDate;
        private int creditLoad;
        private int departmentID;
        private int facultyID;
        private string semester;
        private string sessionName;
        private string staffNumber;
        private string uniqueItem;
        private string uniqueItem2;
        private string courseCodeName;
        private string course;
        private string departmentName;
        private string facultyName;
        private string modeOfStudy;
        private string courseGroup;
        private string programme;
        private int srn;
    
    public DeptCourses()
    {
        
         academicLevel ="";
         assignedLecturer="";
         courseCode="";
       courseOfStudyID =0;
         courseOfStudy="";
         courseTitle="";
         courseType="";
         createdBy="";
         createdDate="";
         creditLoad=0;
         departmentID=0;
        facultyID=0;
         semester="";
         sessionName="";
         staffNumber="";
         uniqueItem="";
         uniqueItem2="";
         courseCodeName="";
         course="";
         departmentName="";
         facultyName="";
         modeOfStudy="";
        courseGroup="";
         programme="";
        srn=0;
       
    }
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

        #region string CreatedDate
        public string CreatedDate
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

        #region int CreditLoad
        public int CreditLoad
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
        //uniqueItem
        #region string UniqueItem
        public string UniqueItem
        {
            get
            {
                return uniqueItem;
            }
            set
            {
                uniqueItem = value;
            }
        }
        #endregion
        #region string UniqueItem2
        public string UniqueItem2
        {
            get
            {
                return uniqueItem2;
            }
            set
            {
                uniqueItem2 = value;
            }
        }
        #endregion
        #region string CourseCodeName
        public string CourseCodeName
        {
            get
            {
                return courseCodeName;
            }
            set
            {
                courseCodeName = value;
            }
        }
        #endregion
        #region string Course
        public string Course
        {
            get
            {
                return course;
            }
            set
            {
                course = value;
            }
        }
        #endregion
        #region string DepartmentName
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }
            set
            {
                departmentName = value;
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
        //courseOfStudy
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
        //staffNumber
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


        //private string modeOfStudy;
        #region string modeOfStudy
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
        //private string courseGroup;
        #region string CourseGroup
        public string CourseGroup
        {
            get
            {
                return courseGroup;
            }
            set
            {
                courseGroup = value;
            }
        }
        #endregion
        //private string programme; 
        #region string programme
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
