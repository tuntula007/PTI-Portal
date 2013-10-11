

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Departments
    public partial class Departments
    {

        #region Private Variables
        private string departmentCode;
        private int departmentId;
        private string departmentName;
        private int facultyID;
        private string hod;
        #endregion

        #region Methods

            #region Clone()
            public Departments Clone()
            {
                // Create New Object
                Departments NewDepartments = new Departments();

                // Clone Each Property
                NewDepartments.departmentCode = this.DepartmentCode;
                NewDepartments.departmentId = this.DepartmentId;
                NewDepartments.departmentName = this.DepartmentName;
                NewDepartments.facultyID = this.FacultyID;
                NewDepartments.hod = this.Hod;

                // Return Cloned Object
                return NewDepartments;

            }
            #endregion


        #endregion

        #region Properties

            #region string DepartmentCode
            public string DepartmentCode
            {
                get
                {
                    return departmentCode;
                }
                set
                {
                    departmentCode = value;
                }
            }
            #endregion

            #region int DepartmentId
            public int DepartmentId
            {
                get
                {
                    return departmentId;
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

            #region string Hod
            public string Hod
            {
                get
                {
                    return hod;
                }
                set
                {
                    hod = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
