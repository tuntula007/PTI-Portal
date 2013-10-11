

#region using statements

using System;
using System.Collections;

#endregion


//namespace CybSoft.EduPortal.Data
//{

    #region class Students
    public class Students
    {

        #region Private Variables
        private string academicLevel;
        private string admissionStatus;
        private string admittedLevel;
        private string admittedSession;
        private string country;
        private int courseOfStudyID;
        private string createdBy;
        private string createdDate;
        private string dateOfBirth;
        private int departmentID;
        private string duration;
        private string email;
        private int facultyID;
        private string homeAddress;
        private string honours;
        private string localGovernmentArea;
        private string maidenName;
        private string maritalStatus;
        private string matricNumber;
        private string modeOfStudy;
        private string entrymode;
        private string nationality;
        private string otherNames;
        private string passportFile;
        private string localpassportFile;
        private string phoneNumber;
        private string placeOfBirth;
        private string presentSession;
        private string programme;
        private string regNo;
        private string residentialAddress;
        private string roomNo;
        private string sex;
        private string sponsorAddress;
        private string sponsorName;
        private string sponsorPhone;
        private string state;
        private int studentId;
        private string surname;
        private string firstname;
        private string middlename;
        private string faculty;
        private string courseofstudy;
        private string department;
        private string title;
        private string repeating;
        private string isindigene;
        private string isnewstudent;
        private string isevening;
        private byte[] studentpassport;
        private int canchangePassport;
        private double matricserial;
        private string teachingsubject;
        private string postaladdress;
        private string parentguardianname;
        private string parentguardianaddress;
        private string parentguardianphone;
        private string center;
        private string admissiontype;
        private string operationtype;
        private string batch;
        private string remark;
        #endregion

        #region Methods

            #region Clone()
            public Students Clone()
            {
                // Create New Object
                Students NewStudents = new Students();

                // Clone Each Property
                NewStudents.academicLevel = this.AcademicLevel;
                NewStudents.admissionStatus = this.AdmissionStatus;
                NewStudents.admittedLevel = this.AdmittedLevel;
                NewStudents.admittedSession = this.AdmittedSession;
                NewStudents.country = this.Country;
                NewStudents.courseOfStudyID = this.CourseOfStudyID;
                NewStudents.createdBy = this.CreatedBy;
                NewStudents.createdDate = this.CreatedDate;
                NewStudents.dateOfBirth = this.DateOfBirth;
                NewStudents.departmentID = this.DepartmentID;
                NewStudents.duration = this.Duration;
                NewStudents.email = this.Email;
                NewStudents.facultyID = this.FacultyID;
                NewStudents.homeAddress = this.HomeAddress;
                NewStudents.honours = this.Honours;
                NewStudents.localGovernmentArea = this.LocalGovernmentArea;
                NewStudents.maidenName = this.MaidenName;
                NewStudents.maritalStatus = this.MaritalStatus;
                NewStudents.matricNumber = this.MatricNumber;
                NewStudents.modeOfStudy = this.ModeOfStudy;
                NewStudents.entrymode = this.EntryMode;
                NewStudents.nationality = this.Nationality;
                NewStudents.otherNames = this.OtherNames;
                NewStudents.passportFile = this.PassportFile;
                NewStudents.localpassportFile = this.LocalPassportFile;
                NewStudents.phoneNumber = this.PhoneNumber;
                NewStudents.placeOfBirth = this.PlaceOfBirth;
                NewStudents.presentSession = this.PresentSession;
                NewStudents.programme = this.Programme;
                NewStudents.regNo = this.RegNo;
                NewStudents.residentialAddress = this.ResidentialAddress;
                NewStudents.roomNo = this.RoomNo;
                NewStudents.sex = this.Sex;
                NewStudents.sponsorAddress = this.SponsorAddress;
                NewStudents.sponsorName = this.SponsorName;
                NewStudents.sponsorPhone = this.SponsorPhone;
                NewStudents.state = this.State;
                NewStudents.studentId = this.StudentId;
                NewStudents.surname = this.Surname;
                NewStudents.firstname = this.Firstname;
                NewStudents.middlename = this.Middlename;
                NewStudents.faculty = this.Faculty;
                NewStudents.department = this.Department;
                NewStudents.courseofstudy = this.CourseOfStudy;
                NewStudents.title = this.Title;
                NewStudents.repeating = this.Repeating;
                NewStudents.isindigene = this.IsIndigene;
                NewStudents.isnewstudent = this.Isnewstudent;
                NewStudents.isevening = this.Isevening;
                NewStudents.studentpassport = this.StudentPassport;
                NewStudents.matricserial = this.MatricSerial;
                NewStudents.canchangePassport = this.CanChangePassport;
                NewStudents.teachingsubject = this.TeachingSubject;
                NewStudents.postaladdress  = this.PostalAddress;
                NewStudents.parentguardianname  = this.ParentGuardianName;
                NewStudents.parentguardianaddress  = this.ParentGuardianAddress;
                NewStudents.parentguardianphone  = this.ParentGuardianPhone;
                NewStudents.center  = this.Center;
                NewStudents.batch  = this.Batch;
                NewStudents.remark  = this.Remark;
                NewStudents.operationtype  = this.OperationType;
                NewStudents.admissiontype = this.AdmissionType;
                // Return Cloned Object
                return NewStudents;

            }
            #endregion


        #endregion

        #region Properties
            public string isNewStudent
            {
                get
                {
                    return isnewstudent;
                }
                set
                {
                    isnewstudent = value;
                }
            }
            public string isEvening
            {
                get
                {
                    return isevening;
                }
                set
                {
                    isevening = value;
                }
            }

            #region string IsIndigene
            public string IsIndigene
            {
                get
                {
                    return isindigene;
                }
                set
                {
                    isindigene = value;
                }
            }
            #endregion

            #region string Isnewstudent
            public string Isnewstudent
            {
                get
                {
                    return isnewstudent;
                }
                set
                {
                    isnewstudent = value;
                }
            }
            #endregion

            #region string Isevening
            public string Isevening
            {
                get
                {
                    return isEvening;
                }
                set
                {
                    isEvening = value;
                }
            }
            #endregion

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

            public string Faculty
            {
                get { return faculty; }
                set { faculty = value; }
            }

            public string Department
            {
                get { return department; }
                set { department = value; }
            }

            public string CourseOfStudy
            {
                get { return courseofstudy; }
                set { courseofstudy = value; }
            }

            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            public string Repeating
            {
                get { return repeating; }
                set { repeating = value; }

            }

            #region string AdmissionStatus
            public string AdmissionStatus
            {
                get
                {
                    return admissionStatus;
                }
                set
                {
                    admissionStatus = value;
                }
            }
            #endregion

            #region string AdmittedLevel
            public string AdmittedLevel
            {
                get
                {
                    return admittedLevel;
                }
                set
                {
                    admittedLevel = value;
                }
            }
            #endregion

            #region string AdmittedSession
            public string AdmittedSession
            {
                get
                {
                    return admittedSession;
                }
                set
                {
                    admittedSession = value;
                }
            }
            #endregion

            #region string Country
            public string Country
            {
                get
                {
                    return country;
                }
                set
                {
                    country = value;
                }
            }
            #endregion

            #region int CourseOfStudyID
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
            public String CreatedDate
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

            #region string DateOfBirth
            public string DateOfBirth
            {
                get
                {
                    return dateOfBirth;
                }
                set
                {
                    dateOfBirth = value;
                }
            }
            #endregion

            #region int DepartmentID
            public int DepartmentID
            {
                get
                {
                    return departmentID;
                }
                set
                {
                    departmentID = value;
                }
            }
            #endregion

            #region string Duration
            public string Duration
            {
                get
                {
                    return duration;
                }
                set
                {
                    duration = value;
                }
            }
            #endregion

            #region string Email
            public string Email
            {
                get
                {
                    return email;
                }
                set
                {
                    email = value;
                }
            }
            #endregion

            #region int FacultyID
            public int FacultyID
            {
                get
                {
                    return facultyID;
                }
                set
                {
                    facultyID = value;
                }
            }
            #endregion

            #region string HomeAddress
            public string HomeAddress
            {
                get
                {
                    return homeAddress;
                }
                set
                {
                    homeAddress = value;
                }
            }
            #endregion

            #region string Honours
            public string Honours
            {
                get
                {
                    return honours;
                }
                set
                {
                    honours = value;
                }
            }
            #endregion

            #region string LocalGovernmentArea
            public string LocalGovernmentArea
            {
                get
                {
                    return localGovernmentArea;
                }
                set
                {
                    localGovernmentArea = value;
                }
            }
            #endregion

            #region string MaidenName
            public string MaidenName
            {
                get
                {
                    return maidenName;
                }
                set
                {
                    maidenName = value;
                }
            }
            #endregion

            #region string MaritalStatus
            public string MaritalStatus
            {
                get
                {
                    return maritalStatus;
                }
                set
                {
                    maritalStatus = value;
                }
            }
            #endregion

            #region string MatriculationNumber
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

            #region string ModeOfStudy
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
            #endregion

            #region string EntryMode
            public string EntryMode
            {
                get
                {
                    return entrymode;
                }
                set
                {
                    entrymode = value;
                }
            }
            #endregion

            #region string Nationality
            public string Nationality
            {
                get
                {
                    return nationality;
                }
                set
                {
                    nationality = value;
                }
            }
            #endregion

            #region string OtherNames
            public string OtherNames
            {
                get
                {
                    return otherNames;
                }
                set
                {
                    otherNames = value;
                }
            }
            #endregion

            #region string PassportFile
            public string PassportFile
            {
                get
                {
                    return passportFile;
                }
                set
                {
                    passportFile = value;
                }
            }
            #endregion

            #region string LocalPassportFile
            public string LocalPassportFile
            {
                get
                {
                    return localpassportFile;
                }
                set
                {
                    localpassportFile = value;
                }
            }
            #endregion

            #region string PhoneNumber
            public string PhoneNumber
            {
                get
                {
                    return phoneNumber;
                }
                set
                {
                    phoneNumber = value;
                }
            }
            #endregion

            #region string PlaceOfBirth
            public string PlaceOfBirth
            {
                get
                {
                    return placeOfBirth;
                }
                set
                {
                    placeOfBirth = value;
                }
            }
            #endregion

            #region string PresentSession
            public string PresentSession
            {
                get
                {
                    return presentSession;
                }
                set
                {
                    presentSession = value;
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

            #region string RegNo
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
            #endregion

            #region string ResidentialAddress
            public string ResidentialAddress
            {
                get
                {
                    return residentialAddress;
                }
                set
                {
                    residentialAddress = value;
                }
            }
            #endregion

            #region string RoomNo
            public string RoomNo
            {
                get
                {
                    return roomNo;
                }
                set
                {
                    roomNo = value;
                }
            }
            #endregion

            #region string Sex
            public string Sex
            {
                get
                {
                    return sex;
                }
                set
                {
                    sex = value;
                }
            }
            #endregion

            #region string SponsorAddress
            public string SponsorAddress
            {
                get
                {
                    return sponsorAddress;
                }
                set
                {
                    sponsorAddress = value;
                }
            }
            #endregion

            #region string SponsorName
            public string SponsorName
            {
                get
                {
                    return sponsorName;
                }
                set
                {
                    sponsorName = value;
                }
            }
            #endregion

            #region string SponsorPhone
            public string SponsorPhone
            {
                get
                {
                    return sponsorPhone;
                }
                set
                {
                    sponsorPhone = value;
                }
            }
            #endregion

            #region string State
            public string State
            {
                get
                {
                    return state;
                }
                set
                {
                    state = value;
                }
            }
            #endregion

            #region int StudentId
            public int StudentId
            {
                get
                {
                    return studentId;
                }
                set
                {
                    studentId = value;
                }
            }
            #endregion

            #region int CanChangePassport
            public int CanChangePassport
            {
                get
                {
                    return canchangePassport;
                }
                set
                {
                    canchangePassport = value;
                }
            }
            #endregion

            #region string Surname
            public string Surname
            {
                get
                {
                    return surname;
                }
                set
                {
                    surname = value;
                }
            }
            #endregion

            #region string Firstname
            public string Firstname
            {
                get
                {
                    return firstname;
                }
                set
                {
                    firstname = value;
                }
            }
            #endregion

            #region string Middlename
            public string Middlename
            {
                get
                {
                    return middlename;
                }
                set
                {
                    middlename = value;
                }
            }
            #endregion

            public double MatricSerial
            {
                get
                {
                    return matricserial;
                }
                set
                {
                    matricserial = value;
                }
            }

            #region byte StudentPassport
            public byte[] StudentPassport
            {
                get
                {
                    return studentpassport;
                }
                set
                {
                    studentpassport = value;
                }
            }
            #endregion

            #region string TeachingSubject
            public string TeachingSubject
            {
                get
                {
                    return teachingsubject;
                }
                set
                {
                    teachingsubject = value;
                }
            }
            #endregion

            #region string PostalAddress
            public string PostalAddress
            {
                get
                {
                    return postaladdress ;
                }
                set
                {
                    postaladdress  = value;
                }
            }
            #endregion

            #region string ParentGuardianName
            public string ParentGuardianName
            {
                get
                {
                    return parentguardianname ;
                }
                set
                {
                    parentguardianname  = value;
                }
            }
            #endregion

            #region string ParentGuardianAddress
            public string ParentGuardianAddress
            {
                get
                {
                    return parentguardianaddress;
                }
                set
                {
                    parentguardianaddress = value;
                }
            }
            #endregion

            #region string ParentGuardianPhone
            public string ParentGuardianPhone
            {
                get
                {
                    return parentguardianphone;
                }
                set
                {
                    parentguardianphone = value;
                }
            }
            #endregion

            #region string Center
            public string Center
            {
                get
                {
                    return center;
                }
                set
                {
                    center = value;
                }
            }
            #endregion

            #region string Batch
            public string Batch
            {
                get
                {
                    return batch;
                }
                set
                {
                    batch = value;
                }
            }
            #endregion

            #region string Remark
            public string Remark
            {
                get
                {
                    return remark ;
                }
                set
                {
                    remark  = value;
                }
            }
            #endregion

            #region string AdmissionType
            public string AdmissionType
            {
                get
                {
                    return admissiontype ;
                }
                set
                {
                    admissiontype  = value;
                }
            }
            #endregion

            #region string OperationType
            public string OperationType
            {
                get
                {
                    return operationtype ;
                }
                set
                {
                    operationtype = value;
                }
            }
            #endregion
        #endregion

    }
    #endregion

//}
