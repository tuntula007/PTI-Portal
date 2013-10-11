

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Lga
    public partial class Lga
    {

        #region Private Variables
        private string lgaCode;
        private int lgaID;
        private string nAME;
        private int stateID;
        #endregion

        #region Methods

            #region Clone()
            public Lga Clone()
            {
                // Create New Object
                Lga NewLga = new Lga();

                // Clone Each Property
                NewLga.lgaCode = this.LgaCode;
                NewLga.lgaID = this.LgaID;
                NewLga.nAME = this.NAME;
                NewLga.stateID = this.StateID;

                // Return Cloned Object
                return NewLga;

            }
            #endregion


        #endregion

        #region Properties

            #region string LgaCode
            public string LgaCode
            {
                get
                {
                    return lgaCode;
                }
                set
                {
                    lgaCode = value;
                }
            }
            #endregion

            #region int LgaID
            public int LgaID
            {
                get
                {
                    return lgaID;
                }
                set
                {
                    lgaID = value;
                }
            }
            #endregion

            #region string NAME
            public string NAME
            {
                get
                {
                    return nAME;
                }
                set
                {
                    nAME = value;
                }
            }
            #endregion

            #region int StateID
            public int StateID
            {
                get
                {
                    return stateID;
                }
                set
                {
                    stateID = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
