using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

using System.IO;
/// <summary>
/// Summary description for ReportBusiness
/// </summary>
public class CourseRegReportBusiness
{
	public CourseRegReportBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable ByCourseAlone(string cCode, string aSession, string Department, string Faculty)
    {
        string qry = "select distinct s.MatricNumber, s.RegNo, s.surname, s.othernames, s.sex,s.academiclevel, cs.courseofstudyname as CourseOfStudy";
        qry += " from students s inner join courseregistration r on  s.matricnumber= r.matricnumber and r.SessionName = '" + aSession + "' and r.Department = '" + Department + "' and s.CourseOfStudyID = " + cCode;
        qry += " join  courseofstudy cs on s.courseofstudyid = cs.courseofstudyid order by s.matricnumber";
        DataRetrieval db = new DataRetrieval();

        DataSet ds = new DataSet("ByCourse");
        DataTable AdmRow = new DataTable();
        try
        {
            ds = db.SelectQuery(qry);
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseAloneNot(string cCode, string aSession, string Department, string Faculty)
    {
        string qry = "select distinct s.MatricNumber, s.RegNo, s.surname, s.othernames, s.sex,s.academiclevel, cs.courseofstudyname as CourseOfStudy";
        qry += " from students s inner join courseregistration r on  s.matricnumber= r.matricnumber and r.SessionName = '" + aSession + "' and r.Department = '" + Faculty + "' and s.CourseOfStudyID = " + cCode;
        qry += " join  courseofstudy cs on s.courseofstudyid = cs.courseofstudyid order by s.matricnumber";
        DataRetrieval db = new DataRetrieval();

        DataSet ds = new DataSet("ByCourse");
        DataTable AdmRow = new DataTable();
        try
        {
            ds = db.SelectQuery(qry);
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        return AdmRow;

    }

    

    public static DataSet GetSessionsRegistered()
    {
        DataRetrieval db = new DataRetrieval();
        DataTable dt = new DataTable();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct SessionName from CourseRegistration where isnull(isnull(SessionName,''),'0')<>'0' order by SessionName desc");
            
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }
    public static DataSet GetDepartmentsRegistered()
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct D.DepartmentName from CourseRegistration S Join Departments D on S.DepartmentID=D.DepartmentId where isnull(D.DepartmentName,'')<>'' order by D.DepartmentName");
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }
    public static DataSet GetDepartmentsRegistered(string ByCategory)
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct D.DepartmentName from CourseRegistration S Join Departments D on S.DepartmentID=D.DepartmentId where isnull(D.DepartmentName,'')<>'' and S.Programme='" + ByCategory + "' order by D.DepartmentName");
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }
    public static DataSet GetDepartmentsRegistered(string ByCategory, string ByFaculty)
    {
        string qry = "select distinct D.DepartmentName from CourseRegistration S, Departments D where D.departmentid=S.DepartmentId and S.FacultyID=D.FacultyID and D.FacultyName='" + ByFaculty + "'";
        qry += (ByCategory.ToLower() != "all category") ? " and S.Programme='" + ByCategory + "'" : "";
        qry += " order by D.DepartmentName";
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegistered()
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct courseofstudyid as ProgrammeCode,CourseOfStudy ProgrammeName from CourseRegistration where isnull(CourseOfStudy,'')<>'' order by ProgrammeName ");
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegisteredFromCourseReg()
    {
        string qry="select distinct c.courseofstudyid as ProgrammeCode,cs.courseofstudyname as ProgrammeName ";
        qry += "from CourseRegistration C,CourseOfStudy Cs where c.CourseOfStudyID=cs.CourseOfStudyID order by ProgrammeName";
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegisteredFromCourseRegByDepartment(string ByDepartment)
    {
        DataRetrieval db = new DataRetrieval();
        string qry = "select distinct c.courseofstudyid as ProgrammeCode,cs.courseofstudyname as ProgrammeName";
        qry += " from CourseRegistration C,CourseOfStudy Cs,Departments d where c.CourseOfStudyID=cs.CourseOfStudyID and d.departmentname='" + ByDepartment + "' order by ProgrammeName ";
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegistered(string ByCategory)
    {
        string qry = "select distinct courseofstudyid as ProgrammeCode,CourseofStudy as ProgrammeName from CourseRegistration where isnull(Courseofstudy,'')<>'' and Programme='" + ByCategory + "'";
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }
    
    public static DataSet GetRegisteredCourseCodeByCourse(string ByProgramOfStudy)
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct AcademicLevel CourseLevel,CourseCode from CourseRegistration where CourseOfStudyID =" + ByProgramOfStudy + " order by AcademicLevel,coursecode");
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegistered(string ByCategory, string ByFaculty)
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        string qry = "select distinct C.courseofstudyid as ProgrammeCode,C.CourseofStudyName ProgrammeName from CourseRegistration  S join CourseOfStudy C on S.CourseOfStudyID=c.CourseOfStudyID where isnull(S.CourseOfStudyID,0)<>0 and S.FacultyID = C.FacultyID and C.FacultyName ='" + ByFaculty + "'";
        qry += (ByCategory != "All Category") ? (" and S.Programme='" + ByCategory +"'") : "";
        qry += " order by ProgrammeName";
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetProgrammeRegistered(string ByCategory, string ByFaculty, string ByDepartment)
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        string qry = "select distinct C.courseofstudyid as ProgrammeCode,C.CourseofStudyName ProgrammeName from CourseRegistration S join CourseOfStudy C on S.CourseOfStudyID=c.CourseOfStudyID where isnull(S.CourseOfStudyID,0)<>0 and S.FacultyID = C.FacultyID and C.FacultyName ='" + ByFaculty + "' and S.DepartmentID =C.DepartmentId and C.DepartmentName ='" + ByDepartment + "'";
        qry += (ByCategory != "All Category") ? (" and S.Programme='" + ByCategory + "'") : "";
        qry += " order by ProgrammeName";
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetFacultyRegistered()
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct F.FacultyName from CourseRegistration CR join Faculty F on CR.FacultyID =F.FacultyID where isnull(F.FacultyName,'')<>'' order by F.FacultyName");
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataSet GetFacultyRegistered(string ByCategory)
    {
        DataRetrieval db = new DataRetrieval();
        DataSet ds = null;
        string qry = "select distinct F.FacultyName from CourseRegistration CR join Faculty F on CR.FacultyID =F.FacultyID where isnull(F.FacultyName,'')<>''";
        qry += (ByCategory != "All Category") ? (" and CR.Programme='" + ByCategory + "'") : "";
        qry += " order by F.FacultyName";
        try
        {
            ds = db.SelectQuery(qry);
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;
    }

    public static DataTable getCourseRegStat(string Rscope, string Sess, string RtypeV, string filters)
    {
        string filterparam = (filters.ToLower() != "all") ? " AND CS.FacultyName='" + filters + "'" : "";
        string whereClause = " from Students S, CourseOfStudy CS where S.CourseOfStudyId=CS.CourseOfStudyId AND S.MatricNumber in (Select Distinct MatricNumber From CourseRegistration where SessionName ='" + Sess + "') AND ";
        string qry = "";
        string sterik = "S.StudentID, CS.CourseOfStudyName As CourseOfStudy, CS.FacultyName as Faculty, CS.DepartmentName,S.Programme ";
        switch (Rscope)
        {
            case "1":
                qry = (RtypeV.ToLower() != "overall" & RtypeV != "*") ?
                    "SELECT " + RtypeV + " as MatricNumber, Count(" + RtypeV + ") as StudentId" + whereClause + "PresentSession = '" + Sess + "' " + filterparam + " GROUP BY " + RtypeV + " ORDER BY " + RtypeV :
                    (RtypeV.ToLower() != "overall" & RtypeV == "*") ? "select " + sterik + whereClause + "PresentSession ='" + Sess + "'" + filterparam :
                    "select 'All Student' as MatricNumber, COUNT(studentid) as  StudentId" + whereClause + "PresentSession='" + Sess + "'" + filterparam;
                break;
            case "2":
                qry = (RtypeV.ToLower() != "overall" & RtypeV != "*") ?
                    "SELECT " + RtypeV + " as MatricNumber, Count(" + RtypeV + ") as StudentId" + whereClause + "isnull(isNewStudent,'0')='1' and PresentSession = '" + Sess + "' " + filterparam + " GROUP BY " + RtypeV + " ORDER BY " + RtypeV :
                    (RtypeV.ToLower() != "overall" & RtypeV == "*") ? "select " + sterik + whereClause + "isnull(isNewStudent,'0')='1' and PresentSession ='" + Sess + "'" + filterparam :
                    "select 'All Student' as MatricNumber, COUNT(studentid) as  StudentId" + whereClause + "isnull(isNewStudent,'0')='1' and PresentSession='" + Sess + "'" + filterparam;
                break;
            case "3":
                qry = (RtypeV.ToLower() != "overall" & RtypeV != "*") ?
                    "SELECT " + RtypeV + " as MatricNumber, Count(" + RtypeV + ") as StudentId" + whereClause + "isnull(isNewStudent,'0')='0' and PresentSession = '" + Sess + "' " + filterparam + " GROUP BY " + RtypeV + " ORDER BY " + RtypeV :
                    (RtypeV.ToLower() != "overall" & RtypeV == "*") ? "select " + sterik + whereClause + "isnull(isNewStudent,'0')='0' and PresentSession ='" + Sess + "'" + filterparam :
                    "select 'All Student' as MatricNumber, COUNT(studentid) as  StudentId" + whereClause + "isnull(isNewStudent,'0')='0' and PresentSession='" + Sess + "'" + filterparam;
                break;
            default:
                break;
        }
        DataSet ds = new DataSet("CourseRegForm");
        DataTable dtt = new DataTable();
        DataRetrieval db = new DataRetrieval();
        //db.Parameters.Add(new SqlParameter("@Rtype", Rscope));
        //db.Parameters.Add(new SqlParameter("@Sess", Sess));
        //db.Parameters.Add(new SqlParameter("@GroupParam", RtypeV));
        //db.Parameters.Add(new SqlParameter("@Filter", filters));
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

    public static DataTable getCourseRegDetail(string ProgrammeCat, string programme, string Faculty, string Dept, string Sess, string FilterLevel, string fields)
    {
        string whereClause = "", qry = "";
        whereClause = " S.CourseofStudyid=CS.COURSEOFSTUDYID and S.MatricNumber in (Select Distinct MatricNumber From CourseRegistration where SessionName ='" + Sess + "') AND S.PresentSession = '" + Sess + "'";
        whereClause += (ProgrammeCat.ToLower() != "all") ? " and S.programme='" + ProgrammeCat + "'" : "";
        whereClause += (Faculty.ToLower() != "all") ? " and S.facultyid=CS.FacultyID and CS.FacultyName='" + Faculty + "'" : "";
        whereClause += (Dept.ToLower() != "all") ? " and S.departmentid=CS.departmentid and CS.departmentname='" + Dept + "'" : "";
        whereClause += (programme.ToLower() != "all") ? " and S.courseofstudyid=" + programme + " and CS.courseofstudyid =" + programme : "";
        whereClause += (FilterLevel.ToLower() != "all") ? " and cast(s.academiclevel as nvarchar(5))='" + FilterLevel + "'" : "";
        qry = "SELECT " + fields + " FROM Students S, CourseOfStudy CS WHERE " + whereClause + " ORDER BY S.programme, CS.FacultyName, CS.Departmentname, S.courseofstudyname, S.academiclevel, S.surname";

        DataSet ds = new DataSet("AppForm");
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

    public static DataTable getPinUsageDetail(string ProgrammeCat, string programme, string Faculty, string Dept, string Sess, string FilterLevel, string fields)
    {
        string whereClause = "", qry = "";
        whereClause = "P.usedby=S.Matricnumber and p.Pinstatus=1 and S.CourseofStudyid=CS.COURSEOFSTUDYID and P.SessionName='" + Sess + "' AND S.PresentSession = '" + Sess + "'";
        whereClause += (ProgrammeCat.ToLower() != "all") ? " and S.programme='" + ProgrammeCat + "'" : "";
        whereClause += (Faculty.ToLower() != "all") ? " and S.facultyid=CS.FacultyID and CS.FacultyName='" + Faculty + "'" : "";
        whereClause += (Dept.ToLower() != "all") ? " and S.departmentid=CS.departmentid and CS.departmentname='" + Dept + "'" : "";
        whereClause += (programme.ToLower() != "all") ? " and S.courseofstudyid=" + programme + " and CS.courseofstudyid =" + programme : "";
        whereClause += (FilterLevel.ToLower() != "all") ? " and cast(s.academiclevel as nvarchar(5))='" + FilterLevel + "'" : "";
        qry = "SELECT " + fields + " FROM SchoolFeesPin P, Students S, CourseOfStudy CS WHERE " + whereClause + " ORDER BY S.programme, CS.FacultyName, CS.Departmentname, S.courseofstudyname, S.academiclevel, S.surname";

        DataSet ds = new DataSet("AppForm");
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

    public static DataTable getCourseCodeRegDetail(string ProgrammeCat, string CourseCode, string programme, string Faculty, string Dept, string Sess, string FilterLevel, string fields)
    {
        string whereClause = "", qry = "";
        whereClause = "S.CourseofstudyID=CS.Courseofstudyid and S.MatricNumber =C.MatricNumber AND C.SessionName ='" + Sess + "' and S.PresentSession = '" + Sess + "' and C.CourseCode='" + CourseCode + "' ";
        whereClause += (Faculty.ToLower() != "all") ? "and CS.FacultyName='" + Faculty + "'" : "";
        whereClause += (Dept.ToLower() != "all") ? " and CS.DepartmentName='" + Dept + "'" : "";
        whereClause += (programme.ToLower() != "all") ? " and S.CourseOfStudyName='" + programme + "'" : "";
        qry = "SELECT " + fields + " FROM Students S, CourseRegistration C, CourseofStudy CS WHERE " + whereClause + " ORDER BY S.programme, CS.facultyName, CS.departmentname, S.courseofstudyname, S.academiclevel, S.surname";
        DataSet ds = new DataSet("AppForm");
        DataTable dtt = new DataTable();
        DataRetrieval db = new DataRetrieval();
        //db.Parameters.Add(new SqlParameter("@Level", FilterLevel));
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
    public static DataTable getCourseCodeRegDetail(string ExamCenter, string TeachingCenter, string ProgrammeCat, string CourseCode, string programme, string Faculty, string Dept, string Sess, string FilterLevel, string fields)
    {
        string whereClause = "", qry = "";
        whereClause = "S.CourseofstudyID=CS.Courseofstudyid and S.MatricNumber =C.MatricNumber AND C.SessionName ='"
            + Sess + "' and S.PresentSession = '" + Sess + "' and C.CourseCode='" + CourseCode + "' AND S.TeachingCenter='"
            + TeachingCenter + "' AND S.ExaminationCenter='" + ExamCenter + "' ";
        whereClause += (Faculty.ToLower() != "all") ? " and CS.FacultyName='" + Faculty + "'" : "";
        whereClause += (Dept.ToLower() != "all") ? " and CS.DepartmentName='" + Dept + "'" : "";
        whereClause += (programme.ToLower() != "all") ? " and S.CourseOfStudyName='" + programme + "'" : "";
        qry = "SELECT " + fields + " FROM Students S, CourseRegistration C, CourseofStudy CS WHERE " + whereClause + " ORDER BY S.programme, CS.facultyName, CS.departmentname, S.courseofstudyname, S.academiclevel, S.surname";
        DataSet ds = new DataSet("AppForm");
        DataTable dtt = new DataTable();
        DataRetrieval db = new DataRetrieval();
        //db.Parameters.Add(new SqlParameter("@Level", FilterLevel));
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

    public static string DisplayedColumns(CheckBoxList Fields, ref string[] otherFieldHeaders)
    {
        string retValue = "S.MatricNumber, S.Surname + ' ' + S.OtherNames as Surname";
        //string[] colHeader = null; 
        string[] columnHeader = new string[5];
        int countt = 0;
        for (int i = 2; i < Fields.Items.Count; i++)
        {
            if (Fields.Items[i].Selected == true)
            {
                //This will split the display columns to the available fields on the report sheet
                if (countt == 0)
                {
                    retValue = retValue + ", " + Fields.Items[i].Value + " as programme, ";
                    columnHeader[0] = Fields.Items[i].Text;
                }
                if (countt == 1)
                {
                    retValue = retValue + Fields.Items[i].Value + " as courseofstudy, ";
                    columnHeader[1] = Fields.Items[i].Text;
                }
                if (countt == 2)
                {
                    retValue = retValue + Fields.Items[i].Value + " as department, ";
                    columnHeader[2] = Fields.Items[i].Text;
                }
                if (countt == 3)
                {
                    retValue = retValue + Fields.Items[i].Value + " as Faculty, ";
                    columnHeader[3] = Fields.Items[i].Text;
                }
                if (countt == 4)
                {
                    retValue = retValue + Fields.Items[i].Value + " as academiclevel, ";
                    columnHeader[4] = Fields.Items[i].Text;
                }
                countt = countt + 1;
            }
        }
        otherFieldHeaders = columnHeader;
        retValue = retValue.TrimEnd(' ').TrimEnd(',') +" ";
        return retValue;
    }
    //public static DataTable ByAllCourses(string aSession, string Faculty)
    //{
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByAllCourses");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    db.AddParameter(new SqlParameter("@Faculty", Faculty));
    //    ds = db.ExecuteDataSet("RegistrationReport_ByAllCourses");

    //    return AdmRow;

    //}

    //public static DataTable ByClass(int cStudyId, string aLevel, string aSession)
    //{
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByClass");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId  ));
    //    db.AddParameter(new SqlParameter("@AcademicLevel",aLevel  ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByClass");

    //    return AdmRow;

    //}
    //public static DataTable ByStudentClass(int cStudyId, string aLevel, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByClass");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId));
    //    db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByStudentClass");

    //    return AdmRow;

    //}
    //public static DataTable ByStudent( string mNumber, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByStudent");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

        
    //    db.AddParameter(new SqlParameter("@MatricNumber", mNumber ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByStudent");

    //    return AdmRow;

    //}
    
    //public static DataTable YetToRegister( int cStudyId , string aLevel, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByStudent");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId ));
    //    db.AddParameter(new SqlParameter("@AcademicLevel", aLevel  ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_YetToRegister");

    //    return AdmRow;

    //}
   
}
