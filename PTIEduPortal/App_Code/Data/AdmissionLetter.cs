

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class AdmissionLetter
    public partial class AdmissionLetter
    {
        #region Private Variables
        private string  surname;
        private string othernames;
        private string fullnames;
        private string address;
        private string programme;
        private string courseofstudy;
        private string matricno;
        private string faculty;
        private string sessionname;
        private string begindate;
        private bool verified;
        private string level;
        private string duration;
        private string regno;
        private string honours;
        private string honoursfull;
        private string entrymode;
        private string payablefees;
        private string teachingsubject;
        #endregion

        #region Methods

        #region Clone()
        public AdmissionLetter Clone()
        {
            // Create New Object
            AdmissionLetter NewAdm = new AdmissionLetter();
            NewAdm.surname = this.Surname;
            NewAdm.othernames = this.OtherNames;
            NewAdm.address = this.Address;
            NewAdm.programme = this.Programme;
            NewAdm.courseofstudy = this.CourseOfStudy;
            NewAdm.faculty = this.Faculty;
            NewAdm.sessionname = this.SessionName;
            NewAdm.begindate = this.BeginDate;
            NewAdm.verified = this.Verified;
            NewAdm.matricno = this.MatricNo;
            NewAdm.fullnames = this.FullName;
            NewAdm.duration = this.Duration;
            NewAdm.honours = this.Honours;
            NewAdm.honoursfull = this.HonoursFull;
            NewAdm.level = this.Level;
            NewAdm.regno = this.RegNo;
            NewAdm.entrymode = this.EntryMode;
            NewAdm.payablefees = this.PayableFees;
            NewAdm.teachingsubject = this.TeachingSubject;
            return NewAdm;

        }
        #endregion

        #endregion

        #region Properties

        #region string Surname
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
        #endregion

        #region string OtherNames
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
        #endregion

        #region string FullName
        public string FullName
        {
            get
            {
                return fullnames;
            }
            set
            {
                fullnames = value;
            }
        }
        #endregion

        #region string MatricNo
        public string MatricNo
        {
            get
            {
                return matricno;
            }
            set
            {
                matricno = value;
            }
        }
        #endregion

        #region string Address
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

        #region string CourseOfStudy
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
        #endregion

        #region string Faculty
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
        #endregion

        #region string SessionName
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
        #endregion

        #region string BeginDate
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
        #endregion

        #region string Verified
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

        #region string Level
        public string Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
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

        #region string RegNo
        public string RegNo
        {
            get
            {
                return regno;
            }
            set
            {
                regno = value;
            }
        }
        #endregion

        #region string Honours
        public string Honours
        {
            get
            {
                return honours;
            }
            set
            {
                honours = value;
            }
        }
        #endregion

        #region string HonoursFull
        public string HonoursFull
        {
            get
            {
                return honoursfull;
            }
            set
            {
                honoursfull = value;
            }
        }
        #endregion

        #region string EntryMode
        public string EntryMode
        {
            get
            {
                return entrymode;
            }
            set
            {
                entrymode = value;
            }
        }
        #endregion

        #region string PayableFees
        public string PayableFees
        {
            get
            {
                return payablefees;
            }
            set
            {
                payablefees = value;
            }
        }
        #endregion

        #region string TeachingSubject
        public string TeachingSubject
        {
            get
            {
                return teachingsubject;
            }
            set
            {
                teachingsubject = value;
            }
        }
        #endregion

        #endregion

    }
    #endregion

}
