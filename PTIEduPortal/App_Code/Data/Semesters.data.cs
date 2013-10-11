

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class Semesters
    public partial class Semesters
    {

        #region Private Variables
        private string semester;
        private int srn;
        #endregion

        #region Methods

            #region Clone()
            public Semesters Clone()
            {
                // Create New Object
                Semesters NewSemesters = new Semesters();

                // Clone Each Property
                NewSemesters.semester = this.Semester;
                NewSemesters.srn = this.Srn;

                // Return Cloned Object
                return NewSemesters;

            }
            #endregion


        #endregion

        #region Properties

            #region string Semester
            public string Semester
            {
                get
                {
                    return semester;
                }
                set
                {
                    semester = value;
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
