

#region using statements

using System;
using System.Collections;
//using CybSoft.EduPortal.Data;

using System.Data;
using System.Data.SqlClient;
using CybSoft.EduPortal.Business;
using System.Collections.Generic;
using System.Text;
#endregion


namespace CybSoft.EduPortal.Business
{

    public class BursaryBusiness
    {

        public static DataTable getAccountSummary(string ProgrammeCat, string Faculty, string Dept, string programme, string FilterLevel, string Sess, string DtFromm, string DtToo)
        {

            string fields = "ft.name as CourseOfStudy, sum(fs.indigene) as AcademicDesc",
                tablesClause = "studentpayment sp, feesstructure fs, feetype ft, students s, courseofstudy cs, [session] ses ",
                groupByClause = "ft.name",
                orderByCaluse = " ft.name",
                whereClause = "",
                qry = "";
            whereClause = "sp.matricnumber=s.matricnumber and s.courseofstudyid=cs.courseofstudyid and cs.courseofstudyid=fs.courseofstudyid and fs.feetypeid=ft.id and fs.sessionid=ses.srn and s.academiclevel=fs.[level] and sp.[session]='" + Sess + "'";
            whereClause += (ProgrammeCat.ToLower() != "all") ? " and S.programme='" + ProgrammeCat + "'" : "";
            whereClause += (Faculty.ToLower() != "all") ? " and S.facultyid=CS.FacultyID and CS.FacultyName='" + Faculty + "'" : "";
            whereClause += (Dept.ToLower() != "all") ? " and S.departmentid=CS.departmentid and CS.departmentname='" + Dept + "'" : "";
            whereClause += (programme.ToLower() != "all") ? " and fS.courseofstudyid=" + programme + " and CS.courseofstudyid =" + programme : "";
            whereClause += (FilterLevel.ToLower() != "all") ? " and cast(fs.[level] as nvarchar(5))='" + FilterLevel + "'" : "";
            whereClause += (string.IsNullOrEmpty(DtFromm) == false && string.IsNullOrEmpty(DtToo) == false) ?
                "and cast(datepaid as datetime) between '" + DtFromm + " 00:00:00.000' and '" + DtToo + " 23:59:59.999'" : "";
            qry = "SELECT " + fields + " FROM " + tablesClause + " WHERE " + whereClause + " GROUP BY " + groupByClause + " ORDER BY " + orderByCaluse;

            DataSet ds = new DataSet("AccLedgerRpt");
            DataTable dtt = new DataTable();
            DataRetrieval db = new DataRetrieval();
            try
            {
                ds = db.SelectQuery(qry);
                dtt = ds.Tables[0];
            }
            catch
            {
                dtt = null;
            }
            return dtt;

        }
        public static DataTable getAccountLedger(string ProgrammeCat, string Faculty, string Dept, string programme, string FilterLevel, string Sess, string DtFromm, string DtToo)
        {

            string fields = "s.surname + ' ' + s.othernames +'['+s.matricnumber+'] - ' + s.academiclevel +'L' as CourseofStudy,ft.name as Department, fs.indigene as AcademicDesc",
                tablesClause = "studentpayment sp, feesstructure fs, feetype ft, students s, courseofstudy cs, [session] ses ",
                groupByClause = "s.surname, s.othernames,s.matricnumber, s.academiclevel,ft.name,fs.indigene",
                orderByCaluse = " s.academiclevel,courseofstudy, department, academicdesc",
                whereClause = "",
                qry = "";
            whereClause = "sp.matricnumber=s.matricnumber and s.courseofstudyid=cs.courseofstudyid and cs.courseofstudyid=fs.courseofstudyid and fs.feetypeid=ft.id and fs.sessionid=ses.srn and s.academiclevel=fs.[level] and sp.[session]='" + Sess + "'";
            whereClause += (ProgrammeCat.ToLower() != "all") ? " and S.programme='" + ProgrammeCat + "'" : "";
            whereClause += (Faculty.ToLower() != "all") ? " and S.facultyid=CS.FacultyID and CS.FacultyName='" + Faculty + "'" : "";
            whereClause += (Dept.ToLower() != "all") ? " and S.departmentid=CS.departmentid and CS.departmentname='" + Dept + "'" : "";
            whereClause += (programme.ToLower() != "all") ? " and fS.courseofstudyid=" + programme + " and CS.courseofstudyid =" + programme : "";
            whereClause += (FilterLevel.ToLower() != "all") ? " and cast(fs.[level] as nvarchar(5))='" + FilterLevel + "'" : "";
            whereClause += (string.IsNullOrEmpty(DtFromm) == false && string.IsNullOrEmpty(DtToo) == false) ?
                "and cast(datepaid as datetime) between '" + DtFromm + " 00:00:00.000' and '" + DtToo + " 23:59:59.999'" : "";
            qry = "SELECT " + fields + " FROM " + tablesClause + " WHERE " + whereClause + " GROUP BY " + groupByClause + " ORDER BY " + orderByCaluse;

            DataSet ds = new DataSet("AccLedgerRpt");
            DataTable dtt = new DataTable();
            DataRetrieval db = new DataRetrieval();
            try
            {
                ds = db.SelectQuery(qry);
                dtt = ds.Tables[0];
            }
            catch
            {
                dtt = null;
            }
            return dtt;

        }
        public static DataTable getSignUpDetail(string formNumber)
        {
            string qry = "SELECT Al.[RegNo] [Form Number] ,Al.[Surname] +' ' +Al.[OtherNames] [Name],Al.[Sex] ,Al.[DateOfBirth] [DOB],Al.[Address],";
            qry += " Al.[AcademicLevel] [Level] ,Al.[AdmittedSession] [Session],Al.[Programme],Al.[Faculty],Al.[Department],";
            qry += " Al.[CourseOfStudy] [Program Of Study] ,Al.[Duration],Al.[EntryMode] [Entry Mode],Al.[Email] ,Al.[Phone]";
            qry += ", Case Al.[IsAdmitted] when 1 then 'ADMITTED' else 'PENDING' end [Admission Status] ";
            qry += ", Case isnull(st.isnewstudent,1) when '1' then 'TRUE' else 'FALSE' end [Is New Student?] ";
            qry += ", Case rtrim(ltrim((Isnull(St.[MatricNumber],'')))) When '' THEN 'FALSE' ELSE 'TRUE' END [Offer Accepted],";
            qry += " fs.indigene [Fees Expected], cast (isnull(sp.pinvalue,'0.00') as varchar) [Fees Paid], upper(isnull(sf.pinserialnumber,'N/A')) [Order Serial]";
            qry += " FROM [AdmissionList] Al left join ";
            qry += " ([Students] st inner join studentpayment SP on St.matricnumber=sp.matricnumber and sp.session=st.admittedsession";
            qry += " inner join schoolfeespin sf on st.matricnumber=sf.usedby and st.admittedsession=sf.sessionname)";
            qry += " on Al.regno=st.regno left join fees Fs on al.courseofstudyid=fs.courseofstudyid and al.academiclevel=fs.level";
            qry += " WHERE Al.[RegNo]='" + formNumber + "' and fs.sessionid=(select srn from session where sessionname=al.admittedsession)";
            //qry += "";
            DataSet ds = new DataSet("SignUpDt");
            DataTable dtt = new DataTable();
            DataRetrieval db = new DataRetrieval();
            try
            {
                ds = db.SelectQuery(qry);
                dtt = ds.Tables[0];
            }
            catch
            {
                dtt = null;
            }
            return dtt;

        }

        public static bool ReverseSignUp(string wrongFormNumber, string actualFormNumber, string usedPinSerial, string userName)
        {
            //delete registration
            //change payment details/pin usage
            //change signup details
            //change studentdetails
            bool retValue = false;
            string qry = "delete courseregistration where matricnumber=(select matricnumber from students where regno='" + wrongFormNumber + "');";
            qry += " Update StudentsignOn SET email=al.email , Phone=al.phone, Regno=al.regno from StudentsignOn s join admissionlist al on s.regno='" + wrongFormNumber + "' ";
            qry += " where al.regno='" + actualFormNumber + "' and s.MatricNumber=(select matricnumber from students where regno='" + wrongFormNumber + "');";
            qry += " Update Students set [RegNo]=al.regno,[Surname]=al.Surname,[Othernames]=al.OtherNames,[Sex]=al.Sex,[MaritalStatus]=al.[MaritalStatus],";
            qry += "[Email]=al.Email,[PhoneNumber]=al.Phone,[Nationality]=al.Nationality,[Country]=al.Nationality,[State]=al.[State],";
            qry += "[LocalGovernmentArea]=al.LocalGovernmentArea,[FacultyID]=al.FacultyID,[DepartmentID]=cs.DepartmentID,[CourseOfStudyID]=al.CourseOfStudyID,";
            qry += "[AcademicLevel]=al.AcademicLevel,[AdmissionStatus]='ADMITTED',[AdmittedLevel]=al.AdmittedLevel,[AdmittedSession]=al.AdmittedSession,";
            qry += "[CourseOfStudyName]=al.CourseOfStudy,[Duration]=al.Duration, [TeachingSubject]=isnull(al.TeachingSubject,''),[Programme]=al.Programme,";
            qry += "[EntryMode]=al.EntryMode,[ModeOfStudy]=al.ModeOfStudy,[Repeating]='0',[DateOfBirth]=isnull([al].[DateOfBirth] ,'')";
            qry += " from Students S join AdmissionList al on S.regno='" + wrongFormNumber + "' left join CourseOfStudy cs on al.CourseOfStudyID=cs.CourseOfStudyID";
            qry += " where al.RegNo ='" + actualFormNumber + "' and al.Department =cs.DepartmentName;";
            qry += " INSERT INTO TUsersLogs (StaffName, [Action]) values ('" + userName + "','SIGNUP REVERSAL: " + wrongFormNumber.ToUpper() + " SIGNUP DETAILS WAS REVERSE TO " + actualFormNumber.ToUpper() + " USING CONFIRMATION ORDER SERIAL:" + usedPinSerial + "');";
            DataRetrieval db = new DataRetrieval();
            try
            {
                db.PerformQuery(qry);
                retValue = true;
            }
            catch
            {
                retValue = false;
            }
            return retValue;
        }

        public static bool RollBackSignUp(string wrongFormNumber, string actualFormNumber, string usedPinSerial, string userName)
        {
            //delete reg
            //delete payment
            //blocked pin
            //delete student details
            //delete signup details
            bool retValue = false;
            string qry = "delete courseregistration where matricnumber=(select matricnumber from students where regno='" + wrongFormNumber + "');";
            qry += " delete studentpayment where matricnumber=(select matricnumber from students where regno='" + wrongFormNumber + "');";
            qry += " update schoolfeespin set pinstatus=1,paymenttype='ROLL BACK MATRIC:'+st.matricnumber,usedby='zenith(ROLLBACK)' from schoolfeespin s join students st on s.usedby=st.matricnumber where st.regno='" + wrongFormNumber + "';";
            qry += " delete studentsignon where regno='" + wrongFormNumber + "';";
            qry += " delete students where regno='" + wrongFormNumber + "';";
            qry += " INSERT INTO TUsersLogs (StaffName, Action) values ('" + userName + "','SIGNUP ROLLBACK: " + wrongFormNumber.ToUpper() + " SIGNUP DETAILS WAS ROLLBACK TO " + actualFormNumber.ToUpper() + " USING CONFIRMATION ORDER SERIAL:" + usedPinSerial + "')";

            DataRetrieval db = new DataRetrieval();
            try
            {
                db.PerformQuery(qry);
                retValue = true;
            }
            catch
            {
                retValue = false;
            }
            return retValue;
        }

        public static string getActiveSession()
        {
            string current = "";
            try
            {
                current = new DataRetrieval().SelectQuery("select sessionname from session where activestatus=1").Tables[0].Rows[0][0].ToString();
            }
            catch
            {
                current = "";
            }
            return current;
        }

    }



}
