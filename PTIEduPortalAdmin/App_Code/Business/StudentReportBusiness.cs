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
/// <summary>
/// Summary description for StudentReportBusiness
/// </summary>
public class StudentReportBusiness
{
	public StudentReportBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public static DataTable getStudentListStat(string Rscope, string Sess, string RtypeV)
    {
        string qry = "";
        DataSet ds = new DataSet("StudStat");
        DataTable dtt = new DataTable();
        DataRetrieval db = new DataRetrieval();
        switch (Rscope)
        {
            case "1":
                //Paid
                qry = "SELECT " + RtypeV + " as Faculty, Count(StudentID) as Srn from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and MatricNumber IN (SELECT MatricNumber FROM StudentPayment WHERE Session='" + Sess + "')  GROUP BY " + RtypeV + "  ORDER BY " + RtypeV;
                break;
            case "2":
                //UnPaid
                qry = "SELECT " + RtypeV + " as Faculty, Count(StudentID) as Srn from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and MatricNumber NOT IN (SELECT MatricNumber FROM StudentPayment WHERE Session='" + Sess + "')  GROUP BY " + RtypeV + "  ORDER BY " + RtypeV;
                break;
            case "3":
                //All Student
                qry = "SELECT " + RtypeV + " as Faculty, Count(StudentID) as Srn from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID GROUP BY " + RtypeV;
                break;
            case "4":
                //Admitted and Accepted
                qry = "SELECT " + RtypeV + " as Faculty, Count(S.StudentID) as Srn from Students S, AdmissionList Al, CourseOfStudy CS where CS.CourseOfStudyID =Al.CourseOfStudyID and CS.RegNo=S.RegNo and S.PresentSession='" + Sess + "' and Al.AdmittedSession='" + Sess + "' GROUP BY " + RtypeV;
                break;
            case "5":
                //Admitted and Not Accepted
                qry = "SELECT " + RtypeV + " as Faculty, Count(S.srn) as Srn from AdmissionList S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and S.RegNo NOT IN (Select RegNo FROM Students) and S.AdmittedSession='" + Sess + "' GROUP BY " + RtypeV + "  ORDER BY " + RtypeV;
                break;
            default:
                break;
        }
        try
        {
            ds = db.SelectQuery(qry);
            dtt = ds.Tables[0];
        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return dtt;
    }
    public static DataSet GetSessions()
    {

        DataRetrieval db = new DataRetrieval();
        DataTable dt = new DataTable();
        DataSet ds = null;
        try
        {
            ds = db.SelectQuery("select distinct PresentSession from Students where isnull(isnull(PresentSession,''),'0')<>'0' order by PresentSession desc");

        }
        catch (Exception exy)
        {

            string jj = exy.Message;
        }
        return ds;

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
        retValue = retValue.TrimEnd(' ').TrimEnd(',') + " ";
        return retValue;
    }
    public static DataTable getStudentDetail(string Rscope, string ProgrammeCat, string programme, string Faculty, string Dept, string Sess, string FilterLevel, string fields)
    {
        string appended = "";
        switch (Rscope)
        {
            case "1":
                //Paid
                appended = "SELECT " + fields + " from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and MatricNumber IN (SELECT MatricNumber FROM StudentPayment WHERE Session='" + Sess + "')";
                break;
            case "2":
                //UnPaid
                appended = "SELECT " + fields + " from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and MatricNumber NOT IN (SELECT MatricNumber FROM StudentPayment WHERE Session='" + Sess + "')";
                break;
            case "3":
                //All Student
                appended = "SELECT " + fields + " from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID ";
                break;
            case "4":
                //Admitted and Accepted
                appended = "SELECT " + fields + " from Students S, AdmissionList Al, CourseOfStudy CS where CS.CourseOfStudyID =Al.CourseOfStudyID and Al.RegNo=S.RegNo and S.PresentSession='" + Sess + "' and Al.AdmittedSession='" + Sess + "'";
                break;
            case "5":
                //Admitted and Not Accepted
                fields = fields.Replace("ISNULL(phonenumber,'')", "ISNULL(phone,'')");
                fields = fields.Replace("S.MatricNumber", "S.regno MatricNumber");
                appended = "SELECT " + fields + " from AdmissionList S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and S.RegNo NOT IN (Select RegNo FROM Students) and S.AdmittedSession='" + Sess + "'";
                break;
            case "6":
                //Paid and Registered
                appended = "SELECT " + fields + " from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and S.MatricNumber IN (Select MatricNumber FROM StudentPayment where Session='" + Sess + "') and S.MatricNumber in (Select Distinct MatricNumber From CourseRegistration where SessionName ='" + Sess + "') and S.PresentSession='" + Sess + "'";
                break;
            case "7":
                //Paid and Not Registered
                appended = "SELECT " + fields + " from Students S, CourseOfStudy CS where CS.CourseOfStudyID =S.CourseOfStudyID and S.MatricNumber IN (Select MatricNumber FROM StudentPayment where Session='" + Sess + "') and S.MatricNumber in (Select Distinct MatricNumber From CourseRegistration where SessionName ='" + Sess + "') and S.PresentSession='" + Sess + "'";
                break;
            default:
                break;
        }

        string whereClause = "", qry = "";
        //whereClause = "S.CourseofStudyid=CS.COURSEOFSTUDYID and S.MatricNumber in (Select Distinct MatricNumber From CourseRegistration where SessionName ='" + Sess + "') AND S.PresentSession = '" + @Sess + "'";
        whereClause = (ProgrammeCat.ToLower() != "all") ? " and S.programme='" + ProgrammeCat + "'" : "";
        whereClause += (Faculty.ToLower() != "all") ? " and S.facultyid=CS.FacultyID and CS.FacultyName='" + Faculty + "'" : "";
        whereClause += (Dept.ToLower() != "all" & Rscope != "5") ? " and S.departmentid=CS.departmentid and CS.departmentname='" + Dept + "'" : (Dept.ToLower() != "all" & Rscope == "5") ? " and S.department=CS.departmentname and CS.departmentname='" + Dept + "'" : "";
        whereClause += (programme.ToLower() != "all") ? " and S.courseofstudyid=" + programme + " and CS.courseofstudyid =" + programme : "";
        whereClause += (FilterLevel.ToLower() != "all") ? " and cast(s.academiclevel as nvarchar(5))='" + FilterLevel + "'" : "";
        qry = appended + whereClause + " ORDER BY S.programme, CS.FacultyName, CS.Departmentname, CS.courseofstudyname, S.academiclevel, S.surname";

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


}
