

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class StudentPayment
    public partial class StudentPayment
    {

        #region Private Variables
        private string datePaid;
        private string matricNumber;
        private string paymentType;
        private string pin;
        private string pinSerialNo;
        private double pinValue;
        private string session;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public StudentPayment Clone()
            {
                // Create New Object
                StudentPayment NewStudentPayment = new StudentPayment();

                // Clone Each Property
                NewStudentPayment.datePaid = this.DatePaid;
                NewStudentPayment.matricNumber = this.MatricNumber;
                NewStudentPayment.paymentType = this.PaymentType;
                NewStudentPayment.pin = this.Pin;
                NewStudentPayment.pinSerialNo = this.PinSerialNo;
                NewStudentPayment.pinValue = this.PinValue;
                NewStudentPayment.session = this.Session;
                NewStudentPayment.srn = this.Srn;

                // Return Cloned Object
                return NewStudentPayment;

            }
            #endregion


        #endregion

        #region Properties

            #region string DatePaid
            public string DatePaid
            {
                get
                {
                    return datePaid;
                }
                set
                {
                    datePaid = value;
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

            #region string PaymentType
            public string PaymentType
            {
                get
                {
                    return paymentType;
                }
                set
                {
                    paymentType = value;
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

            #region string PinSerialNo
            public string PinSerialNo
            {
                get
                {
                    return pinSerialNo;
                }
                set
                {
                    pinSerialNo = value;
                }
            }
            #endregion

            #region double PinValue
            public double PinValue
            {
                get
                {
                    return pinValue;
                }
                set
                {
                    pinValue = value;
                }
            }
            #endregion

            #region string Session
            public string Session
            {
                get
                {
                    return session;
                }
                set
                {
                    session = value;
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
