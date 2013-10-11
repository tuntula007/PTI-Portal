

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class GPMaster
    public partial class GPMaster
    {

        #region Private Variables
        private string academicLevel;
        private double cGPA;
        private string courseOfStudy;
        private double cTCP;
        private double cTNU;
        private string finalGrade;
        private double gPA;
        private string longRemark;
        private string modeOfStudy;
        private string overFlowRemark;
        private double pGPA;
        private string programme;
        private double pTCP;
        private double pTNU;
        private string regNo;
        private string reportRemark;
        private string semester;
        private string sessionName;
        private int srn;
        private string statusCode;
        private double tCP;
        private double tNU;
        #endregion

        #region Methods

            #region Clone()
            public GPMaster Clone()
            {
                // Create New Object
                GPMaster NewGPMaster = new GPMaster();

                // Clone Each Property
                NewGPMaster.academicLevel = this.AcademicLevel;
                NewGPMaster.cGPA = this.CGPA;
                NewGPMaster.courseOfStudy = this.CourseOfStudy;
                NewGPMaster.cTCP = this.CTCP;
                NewGPMaster.cTNU = this.CTNU;
                NewGPMaster.finalGrade = this.FinalGrade;
                NewGPMaster.gPA = this.GPA;
                NewGPMaster.longRemark = this.LongRemark;
                NewGPMaster.modeOfStudy = this.ModeOfStudy;
                NewGPMaster.overFlowRemark = this.OverFlowRemark;
                NewGPMaster.pGPA = this.PGPA;
                NewGPMaster.programme = this.Programme;
                NewGPMaster.pTCP = this.PTCP;
                NewGPMaster.pTNU = this.PTNU;
                NewGPMaster.regNo = this.RegNo;
                NewGPMaster.reportRemark = this.ReportRemark;
                NewGPMaster.semester = this.Semester;
                NewGPMaster.sessionName = this.SessionName;
                NewGPMaster.srn = this.Srn;
                NewGPMaster.statusCode = this.StatusCode;
                NewGPMaster.tCP = this.TCP;
                NewGPMaster.tNU = this.TNU;

                // Return Cloned Object
                return NewGPMaster;

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

            #region double CGPA
            public double CGPA
            {
                get
                {
                    return cGPA;
                }
                set
                {
                    cGPA = value;
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

            #region double CTCP
            public double CTCP
            {
                get
                {
                    return cTCP;
                }
                set
                {
                    cTCP = value;
                }
            }
            #endregion

            #region double CTNU
            public double CTNU
            {
                get
                {
                    return cTNU;
                }
                set
                {
                    cTNU = value;
                }
            }
            #endregion

            #region string FinalGrade
            public string FinalGrade
            {
                get
                {
                    return finalGrade;
                }
                set
                {
                    finalGrade = value;
                }
            }
            #endregion

            #region double GPA
            public double GPA
            {
                get
                {
                    return gPA;
                }
                set
                {
                    gPA = value;
                }
            }
            #endregion

            #region string LongRemark
            public string LongRemark
            {
                get
                {
                    return longRemark;
                }
                set
                {
                    longRemark = value;
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

            #region string OverFlowRemark
            public string OverFlowRemark
            {
                get
                {
                    return overFlowRemark;
                }
                set
                {
                    overFlowRemark = value;
                }
            }
            #endregion

            #region double PGPA
            public double PGPA
            {
                get
                {
                    return pGPA;
                }
                set
                {
                    pGPA = value;
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

            #region double PTCP
            public double PTCP
            {
                get
                {
                    return pTCP;
                }
                set
                {
                    pTCP = value;
                }
            }
            #endregion

            #region double PTNU
            public double PTNU
            {
                get
                {
                    return pTNU;
                }
                set
                {
                    pTNU = value;
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

            #region string ReportRemark
            public string ReportRemark
            {
                get
                {
                    return reportRemark;
                }
                set
                {
                    reportRemark = value;
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

            #region string StatusCode
            public string StatusCode
            {
                get
                {
                    return statusCode;
                }
                set
                {
                    statusCode = value;
                }
            }
            #endregion

            #region double TCP
            public double TCP
            {
                get
                {
                    return tCP;
                }
                set
                {
                    tCP = value;
                }
            }
            #endregion

            #region double TNU
            public double TNU
            {
                get
                {
                    return tNU;
                }
                set
                {
                    tNU = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
