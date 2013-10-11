

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Levels
    public partial class Levels
    {

        #region Private Variables
        private string academicLevel;
        private string createdBy;
        private DateTime createdDate;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public Levels Clone()
            {
                // Create New Object
                Levels NewLevels = new Levels();

                // Clone Each Property
                NewLevels.academicLevel = this.AcademicLevel;
                NewLevels.createdBy = this.CreatedBy;
                NewLevels.createdDate = this.CreatedDate;
                NewLevels.srn = this.Srn;

                // Return Cloned Object
                return NewLevels;

            }
            #endregion


        #endregion

        #region Properties

            #region string AcademicLevel
            public string AcademicLevel
            {
                get
                {
                    return academicLevel;
                }
                set
                {
                    academicLevel = value;
                }
            }
            #endregion

            #region string CreatedBy
            public string CreatedBy
            {
                get
                {
                    return createdBy;
                }
                set
                {
                    createdBy = value;
                }
            }
            #endregion

            #region DateTime CreatedDate
            public DateTime CreatedDate
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
