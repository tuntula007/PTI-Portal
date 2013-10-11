

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class StudentSignOn
    public partial class StudentSignOn
    {

        #region Private Variables
        private string email;
        private string matricNumber;
        private string password;
        private string phone;
        private string schoolfeepin;
        private string studiofeepin;
        private bool verified;
        private string formNumber;
        private int status;
        private int srn;
        #endregion

        #region Methods

        #region Clone()
        public StudentSignOn Clone()
        {
            // Create New Object
            StudentSignOn NewStudentSignOn = new StudentSignOn();

            // Clone Each Property
            NewStudentSignOn.email = this.Email;
            NewStudentSignOn.matricNumber = this.MatricNumber;
            NewStudentSignOn.formNumber = this.FormNumber;
            NewStudentSignOn.password = this.Password;
            NewStudentSignOn.phone = this.Phone;
            NewStudentSignOn.schoolfeepin = this.SchoolFeePin;
            NewStudentSignOn.studiofeepin = this.StudioFeePin;
            NewStudentSignOn.verified = this.Verified;
            NewStudentSignOn.status = this.Status;
            NewStudentSignOn.srn = this.Srn;

            // Return Cloned Object
            return NewStudentSignOn;

        }
        #endregion


        #endregion

        #region Properties

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

        #region string FormNumber
        public string FormNumber
        {
            get
            {
                return formNumber;
            }
            set
            {
                formNumber = value;
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

        #region string SchoolFeePin
        public string SchoolFeePin
        {
            get
            {
                return schoolfeepin;
            }
            set
            {
                schoolfeepin = value;
            }
        }

        #endregion

        #region string StudioFeePin
        public string StudioFeePin
        {
            get
            {
                return studiofeepin;
            }
            set
            {
                studiofeepin = value;
            }
        }
        #endregion

        #region bool Verified
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

        #region int Srn
        public int Srn
        {
            get
            {
                return srn;
            }
        }
        #endregion

        #region int Status
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }

        }
        #endregion

        #endregion

    }
    #endregion

}
