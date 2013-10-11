

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class OlevelGrade
    public partial class OlevelGrade
    {

        #region Private Variables
        private string gradeCode;
        private string gradeName;
        #endregion

        #region Methods

            #region Clone()
            public OlevelGrade Clone()
            {
                // Create New Object
                OlevelGrade NewOlevelGrade = new OlevelGrade();

                // Clone Each Property
                NewOlevelGrade.gradeCode = this.GradeCode;
                NewOlevelGrade.gradeName = this.GradeName;

                // Return Cloned Object
                return NewOlevelGrade;

            }
            #endregion


        #endregion

        #region Properties

            #region string GradeCode
            public string GradeCode
            {
                get
                {
                    return gradeCode;
                }
                set
                {
                    gradeCode = value;
                }
            }
            #endregion

            #region string GradeName
            public string GradeName
            {
                get
                {
                    return gradeName;
                }
                set
                {
                    gradeName = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
