using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace CybSoft.EduPortal.Data
{
    /// <summary>
    /// Summary description for FeedBack
    /// </summary>

    public class FeedBack
    {
        string _name = "";
        string _matricNo = "";
        string _phone = "";
        string _email = "";
        string _comment = "";
        string _department = "";
        string _course = "";
        string _level = "";
        string _category = "";
        DateTime _dateSubmitted = DateTime.MinValue;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string MatricNo
        {
            get { return _matricNo; }
            set { _matricNo = value; }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public DateTime DateSubmitted
        {
            get { return _dateSubmitted; }
            set { _dateSubmitted = value; }
        }
        public FeedBack()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}