

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class SummerPrices
    public partial class SummerPrices
    {

        #region Private Variables
        private int srn;
        private string faculty;
        private int prices;
        private string programme;
        private string modeOfStudy;
        private int lowerQty;
        private int upperQty;
        private string uploader;
        private string createdDate;
        #endregion

        #region Methods

        #region Clone()
        public SummerPrices Clone()
        {
            // Create New Object
            SummerPrices NewSummerPrices = new SummerPrices();

            // Clone Each Property
            NewSummerPrices.createdDate = this.CreateDate;
            NewSummerPrices.faculty = this.Faculty;
            NewSummerPrices.lowerQty = this.LowerQty;
            NewSummerPrices.modeOfStudy = this.ModeOfStudy;
            NewSummerPrices.prices = this.Prices;
            NewSummerPrices.programme = this.Programme;
            NewSummerPrices.uploader = this.Uploader;
            NewSummerPrices.upperQty = this.UpperQty;
            NewSummerPrices.srn = this.Srn;

            // Return Cloned Object
            return NewSummerPrices;

        }
        #endregion


        #endregion

        #region Properties

        #region string CreateDate
        public string CreateDate
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

        #region int Prices
        public int Prices
        {
            get
            {
                return prices;
            }
            set
            {
                prices = value;
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

        #region int LowerQty
        public int LowerQty
        {
            get
            {
                return lowerQty;
            }
            set
            {
                lowerQty = value;
            }
        }
        #endregion

        #region int UpperQty
        public int UpperQty
        {
            get
            {
                return upperQty;
            }
            set
            {
                upperQty = value;
            }
        }
        #endregion

        #region string Uploader
        public string Uploader
        {
            get
            {
                return uploader;
            }
            set
            {
                uploader = value;
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
