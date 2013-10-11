

#region using statements

using System;
using System.Collections;

#endregion


namespace CybSoft.EduPortal.Data
{

    #region class SubjectTable
    public partial class SubjectTable
    {

        #region Private Variables
        private int srn;
        private string subjectCode;
        private string subjectName;
        #endregion

        #region Methods

            #region Clone()
            public SubjectTable Clone()
            {
                // Create New Object
                SubjectTable NewSubjectTable = new SubjectTable();

                // Clone Each Property
                NewSubjectTable.srn = this.Srn;
                NewSubjectTable.subjectCode = this.SubjectCode;
                NewSubjectTable.subjectName = this.SubjectName;

                // Return Cloned Object
                return NewSubjectTable;

            }
            #endregion


        #endregion

        #region Properties

            #region int Srn
            public int Srn
            {
                get
                {
                    return srn;
                }
                set
                {
                    srn = value;
                }
            }
            #endregion

            #region string SubjectCode
            public string SubjectCode
            {
                get
                {
                    return subjectCode;
                }
                set
                {
                    subjectCode = value;
                }
            }
            #endregion

            #region string SubjectName
            public string SubjectName
            {
                get
                {
                    return subjectName;
                }
                set
                {
                    subjectName = value;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
