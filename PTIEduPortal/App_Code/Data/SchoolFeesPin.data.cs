

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class SchoolFeesPin
    public partial class SchoolFeesPin
    {

        #region Private Variables
        private string bankBranch;
        private string bankName;
        private DateTime dateUploaded;
        private string issuingMode;
        private int pinID;
        private string pinNumber;
        private string programme;
        private string modeofstudy;
        private string paymenttype;
        private string faculty;
        private string pinStatus;
        private string sendTo;
        private string serialNo;
        private string sessionName;
        private string tellerNo;
        private string uploadBy;
        private string usedBy;
        private DateTime usedDate;
        #endregion

        #region Methods

            #region Clone()
            public SchoolFeesPin Clone()
            {
                // Create New Object
                SchoolFeesPin NewSchoolFeesPin = new SchoolFeesPin();

                // Clone Each Property
                NewSchoolFeesPin.bankBranch = this.BankBranch;
                NewSchoolFeesPin.bankName = this.BankName;
                NewSchoolFeesPin.dateUploaded = this.DateUploaded;
                NewSchoolFeesPin.issuingMode = this.IssuingMode;
                NewSchoolFeesPin.pinID = this.PinID;
                NewSchoolFeesPin.pinNumber = this.PinNumber;
                NewSchoolFeesPin.pinStatus = this.PinStatus;
                NewSchoolFeesPin.sendTo = this.SendTo;
                NewSchoolFeesPin.faculty = this.Faculty;
                NewSchoolFeesPin.paymenttype = this.PaymentType;
                NewSchoolFeesPin.modeofstudy = this.ModeOfStudy;
                NewSchoolFeesPin.faculty = this.Faculty;
                NewSchoolFeesPin.serialNo = this.SerialNo;
                NewSchoolFeesPin.sessionName = this.SessionName;
                NewSchoolFeesPin.tellerNo = this.TellerNo;
                NewSchoolFeesPin.uploadBy = this.UploadBy;
                NewSchoolFeesPin.usedBy = this.UsedBy;
                NewSchoolFeesPin.usedDate = this.UsedDate;

                // Return Cloned Object
                return NewSchoolFeesPin;

            }
            #endregion


        #endregion

        #region Properties

            #region string BankBranch
            public string BankBranch
            {
                get
                {
                    return bankBranch;
                }
                set
                {
                    bankBranch = value;
                }
            }
            #endregion

            #region string BankName
            public string BankName
            {
                get
                {
                    return bankName;
                }
                set
                {
                    bankName = value;
                }
            }
            #endregion

            #region DateTime DateUploaded
            public DateTime DateUploaded
            {
                get
                {
                    return dateUploaded;
                }
                set
                {
                    dateUploaded = value;
                }
            }
            #endregion

            #region string IssuingMode
            public string IssuingMode
            {
                get
                {
                    return issuingMode;
                }
                set
                {
                    issuingMode = value;
                }
            }
            #endregion

            #region int PinID
            public int PinID
            {
                get
                {
                    return pinID;
                }
                set
                {
                    pinID = value;
                }
            }
            #endregion

            #region string PinNumber
            public string PinNumber
            {
                get
                {
                    return pinNumber;
                }
                set
                {
                    pinNumber = value;
                }
            }
            #endregion

            #region string PinStatus
            public string PinStatus
            {
                get
                {
                    return pinStatus;
                }
                set
                {
                    pinStatus = value;
                }
            }
            #endregion

            #region string SendTo
            public string SendTo
            {
                get
                {
                    return sendTo;
                }
                set
                {
                    sendTo = value;
                }
            }
            #endregion

            #region string SerialNo
            public string SerialNo
            {
                get
                {
                    return serialNo;
                }
                set
                {
                    serialNo = value;
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
                    return modeofstudy;
                }
                set
                {
                    modeofstudy = value;
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

            #region string TellerNo
            public string TellerNo
            {
                get
                {
                    return tellerNo;
                }
                set
                {
                    tellerNo = value;
                }
            }
            #endregion

            #region string UploadBy
            public string UploadBy
            {
                get
                {
                    return uploadBy;
                }
                set
                {
                    uploadBy = value;
                }
            }
            #endregion

            #region string UsedBy
            public string UsedBy
            {
                get
                {
                    return usedBy;
                }
                set
                {
                    usedBy = value;
                }
            }
            #endregion

            #region DateTime UsedDate
            public DateTime UsedDate
            {
                get
                {
                    return usedDate;
                }
                set
                {
                    usedDate = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
