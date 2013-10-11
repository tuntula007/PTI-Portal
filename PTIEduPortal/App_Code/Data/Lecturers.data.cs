

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Lecturers
    public partial class Lecturers
    {

        #region Private Variables
        private string createdBy;
        private int departmentID;
        private string departmentName;
        private string designation;
        private string email;
        private string experience;
        private int facultyID;
        private string facultyName;
        private string password;
        private string phone;
        private string pixName;
        private string publication;
        private string qualification;
        private int srn;
        private string staffName;
        private string staffNumber;
        private string staffType;
        private string title;
        private string uniqueItem;
        #endregion

        #region Methods

            #region Clone()
            public Lecturers Clone()
            {
                // Create New Object
                Lecturers NewLecturers = new Lecturers();

                // Clone Each Property
                NewLecturers.createdBy = this.CreatedBy;
                NewLecturers.departmentID = this.DepartmentID;
                NewLecturers.departmentName = this.DepartmentName;
                NewLecturers.designation = this.Designation;
                NewLecturers.email = this.Email;
                NewLecturers.experience = this.Experience;
                NewLecturers.facultyID = this.FacultyID;
                NewLecturers.facultyName = this.FacultyName;
                NewLecturers.password = this.Password;
                NewLecturers.phone = this.Phone;
                NewLecturers.pixName = this.PixName;
                NewLecturers.publication = this.Publication;
                NewLecturers.qualification = this.Qualification;
                NewLecturers.srn = this.Srn;
                NewLecturers.staffName = this.StaffName;
                NewLecturers.staffNumber = this.StaffNumber;
                NewLecturers.staffType = this.StaffType;
                NewLecturers.title = this.Title;
                NewLecturers.uniqueItem = this.UniqueItem;

                // Return Cloned Object
                return NewLecturers;

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

            #region string Designation
            public string Designation
            {
                get
                {
                    return designation;
                }
                set
                {
                    designation = value;
                }
            }
            #endregion

            #region string Email
            public string Email
            {
                get
                {
                    return email;
                }
                set
                {
                    email = value;
                }
            }
            #endregion

            #region string Experience
            public string Experience
            {
                get
                {
                    return experience;
                }
                set
                {
                    experience = value;
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

            #region string Password
            public string Password
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
                }
            }
            #endregion

            #region string Phone
            public string Phone
            {
                get
                {
                    return phone;
                }
                set
                {
                    phone = value;
                }
            }
            #endregion

            #region string PixName
            public string PixName
            {
                get
                {
                    return pixName;
                }
                set
                {
                    pixName = value;
                }
            }
            #endregion

            #region string Publication
            public string Publication
            {
                get
                {
                    return publication;
                }
                set
                {
                    publication = value;
                }
            }
            #endregion

            #region string Qualification
            public string Qualification
            {
                get
                {
                    return qualification;
                }
                set
                {
                    qualification = value;
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

            #region string StaffName
            public string StaffName
            {
                get
                {
                    return staffName;
                }
                set
                {
                    staffName = value;
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

            #region string StaffType
            public string StaffType
            {
                get
                {
                    return staffType;
                }
                set
                {
                    staffType = value;
                }
            }
            #endregion

            #region string Title
            public string Title
            {
                get
                {
                    return title;
                }
                set
                {
                    title = value;
                }
            }
            #endregion

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

        #endregion

    }
    #endregion

}
