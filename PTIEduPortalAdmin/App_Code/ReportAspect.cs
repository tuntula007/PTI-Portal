using System;
using System.Collections;

 
    public class ReportAspect
    {

        private string modeOfStudy;
        private string programme;
        private string regNo;
        private string semester;
        private string sessionName;
        private string courseofstudy;
        private int courseOfStudyID;
        private int programmeID;
        private string religion = "";

        private byte[] picture;

        public byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string formnumber = "";
        public string Formnumber
        {
            get { return formnumber; }
            set { formnumber = value; }
        }

        private string surname = "";
        public string SurName
        {
            get { return surname; }
            set { surname = value; }
        }

        private string sex = "";
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private string othernames = "";
        public string Othernames
        {
            get { return othernames; }
            set { othernames = value; }
        }


        private string choice1 = "";
        public string Choice1
        {
            get { return choice1; }
            set { choice1 = value; }
        }

        private string choice2 = "";
        public string Choice2
        {
            get { return choice2; }
            set { choice2 = value; }
        }

        private string choice3 = "";
        public string Choice3
        {
            get { return choice3; }
            set { choice3 = value; }
        }


        private string dateofbirth = "";
        public string Dateofbirth
        {
            get { return dateofbirth; }
            set { dateofbirth = value; }
        }

        private string candidateaddress = "";
        public string Candidateaddress
        {
            get { return candidateaddress; }
            set { candidateaddress = value; }
        }

        private string choiceofexamcenter = "";
        public string Choiceofexamcenter
        {
            get { return choiceofexamcenter; }
            set { choiceofexamcenter = value; }
        }

        private string subjectcombination = "";
        public string Subjectcombination
        {
            get { return subjectcombination; }
            set { subjectcombination = value; }
        }

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
        public string RegNo
        {
            get
            {
                return regNo;
            }
            set
            {
                regNo = value;
            }
        }
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
        public int CourseOfStudyID
        {
            get
            {
                return courseOfStudyID;
            }
            set
            {
                courseOfStudyID = value;
            }
        }
        public int ProgrammeID
        {
            get
            {
                return programmeID;
            }
            set
            {
                programmeID = value;
            }
        } 
         

    }


 