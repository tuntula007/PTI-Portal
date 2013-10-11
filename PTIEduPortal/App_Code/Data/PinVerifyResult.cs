

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class PinVerifyResult
    public partial class PinVerifyResult
    {

        #region Private Variables
        private bool verified;
        private string usedby;
        private string useddate;
        private string failurecomment;
        private string paymenttype;
        private string pinvalue;
        private string pinserial;
        private string pin;
        #endregion

        #region Methods

        #region Clone()
        public PinVerifyResult Clone()
        {
            // Create New Object
            PinVerifyResult NewPinVerifyResult = new PinVerifyResult();

            // Clone Each Property
            NewPinVerifyResult.verified = this.Verified;
            NewPinVerifyResult.usedby = this.UsedBy;
            NewPinVerifyResult.useddate = this.UsedDate;
            NewPinVerifyResult.failurecomment = this.FailureComment;
            NewPinVerifyResult.paymenttype = this.PaymentType;
            NewPinVerifyResult.pin = this.Pin;
            NewPinVerifyResult.pinserial = this.PinSerial;
            NewPinVerifyResult.pinvalue = this.PinValue;
            // Return Cloned Object
            return NewPinVerifyResult;

        }
        #endregion


        #endregion

        #region Properties

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

        #region string UsedBy
        public string UsedBy
        {
            get
            {
                return usedby;
            }
            set
            {
                usedby = value;
            }
        }
        #endregion

        #region string UsedDate
        public string UsedDate
        {
            get
            {
                return useddate;
            }
            set
            {
                useddate = value;
            }
        }
        #endregion
        #region string FailureComment
        public string FailureComment
        {
            get
            {
                return failurecomment;
            }
            set
            {
                failurecomment = value;
            }
        }
        #endregion
        #region string PaymentType
        public string PaymentType
        {
            get
            {
                return paymenttype;
            }
            set
            {
                paymenttype = value;
            }
        }
        #endregion
        #region string Pin
        public string Pin
        {
            get
            {
                return pin;
            }
            set
            {
                pin = value;
            }
        }
        #endregion
        #region string PinValue
        public string PinValue
        {
            get
            {
                return pinvalue;
            }
            set
            {
                pinvalue = value;
            }
        }
        #endregion
        #region string PinSerial
        public string PinSerial
        {
            get
            {
                return pinserial;
            }
            set
            {
                pinserial = value;
            }
        }
        #endregion
        #endregion

    }
    #endregion

}
