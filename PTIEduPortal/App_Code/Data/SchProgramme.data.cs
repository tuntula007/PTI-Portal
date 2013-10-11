

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class SchProgramme
    public partial class SchProgramme
    {

        #region Private Variables
        private string description;
        private string programme;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public SchProgramme Clone()
            {
                // Create New Object
                SchProgramme NewSchProgramme = new SchProgramme();

                // Clone Each Property
                NewSchProgramme.description = this.Description;
                NewSchProgramme.programme = this.Programme;
                NewSchProgramme.srn = this.Srn;

                // Return Cloned Object
                return NewSchProgramme;

            }
            #endregion


        #endregion

        #region Properties

            #region string Description
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    description = value;
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
