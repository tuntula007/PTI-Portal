#region using statements

using System;
using System.Collections;

#endregion

namespace CybSoft.EduPortal.Data
{
 #region class Admission
public class Admission
{
        #region Private Variables
        private string regNo;
        private string surname;
        private string othernames;
        private string address;
        private string programme;
        private string modeofstudy;
        private string courseofstudy;
        private string faculty;
        private string academiclevel;
        private string sessionname;
        private string begindate;
        private string dateofbith;
        private string email;
        private bool verified;
        #endregion

        #region Methods

        #region Clone()
        public Admission Clone()
        {
            // Create New Object
            Admission NewAdm = new Admission();
            NewAdm.RegNo = this.RegNo;
            NewAdm.surname = this.Surname;
            NewAdm.othernames = this.OtherNames;
            NewAdm.address = this.Address;
            NewAdm.programme = this.Programme;
            NewAdm.modeofstudy = this.ModeOfStudy;
            NewAdm.courseofstudy = this.CourseOfStudy;
            NewAdm.faculty = this.Faculty;
            NewAdm.academiclevel = this.AcademicLevel;
            NewAdm.sessionname = this.SessionName;
            NewAdm.begindate = this.BeginDate;
            NewAdm.dateofbith = this.DateOfBirth;
            NewAdm.email = this.Email;
            NewAdm.verified = this.Verified;
            return NewAdm;
        }
        #endregion

        #endregion

        #region Properties

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

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        public string OtherNames
        {
            get
            {
                return othernames;
            }
            set
            {
                othernames = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

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

        public string ModeOfStudy
        {
            get
            {
                return modeofstudy;
            }
            set
            {
                modeofstudy = value;
            }
        }

        public string CourseOfStudy
        {
            get
            {
                return courseofstudy;
            }
            set
            {
                courseofstudy = value;
            }
        }

        public string Faculty
        {
            get
            {
                return faculty;
            }
            set
            {
                faculty = value;
            }
        }

        public string AcademicLevel
        {
            get
            {
                return academiclevel;
            }
            set
            {
                academiclevel = value;
            }
        }

        public string SessionName
        {
            get
            {
                return sessionname;
            }
            set
            {
                sessionname = value;
            }
        }

        public string BeginDate
        {
            get
            {
                return begindate;
            }
            set
            {
                begindate = value;
            }
        }

        public string DateOfBirth
        {
            get
            {
                return dateofbith;
            }
            set
            {
                dateofbith = value;
            }
        }
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
        public bool Verified
        {
            get
            {
                return verified;
            }
            set
            {
                verified = value;
            }
        }
        #endregion

    }
    #endregion
}
