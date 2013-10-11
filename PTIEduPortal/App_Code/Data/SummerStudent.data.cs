

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class SummerStudent
    public partial class SummerStudent
    {

        #region Private Variables
        private int srn;
        private string matricno;
        private string regno;
        private string sessionname;
        private string academiclevel;
        private int hasregistered;
        private int isactive;
        private int scholarship;
        #endregion

        #region Methods

        #region Clone()
        public SummerStudent Clone()
        {
            // Create New Object
            SummerStudent NewSummerPrices = new SummerStudent();

            // Clone Each Property
            NewSummerPrices.isactive = this.IsActive;
            NewSummerPrices.matricno = this.MatricNo;
            NewSummerPrices.regno = this.RegNo;
            NewSummerPrices.sessionname = this.SessionName;
            NewSummerPrices.hasregistered = this.HasRegistered;
            NewSummerPrices.academiclevel = this.AcademicLevel;
            NewSummerPrices.scholarship = this.Scholarship;
            NewSummerPrices.srn = this.Srn;

            // Return Cloned Object
            return NewSummerPrices;

        }
        #endregion


        #endregion

        #region Properties

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

        #region string AcademicLevel
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
        #endregion

        #region int HasRegistered
        public int HasRegistered
        {
            get
            {
                return hasregistered;
            }
            set
            {
                hasregistered = value;
            }
        }
        #endregion

        #region int IsActive
        public int IsActive
        {
            get
            {
                return isactive;
            }
            set
            {
                isactive = value;
            }
        }
        #endregion

        #region int Scholarship
        public int Scholarship
        {
            get
            {
                return scholarship;
            }
            set
            {
                scholarship = value;
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
    #endregion

}
