using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadDataInfosms
/// </summary>

    public class UploadDataInfosms
    {
        private string m_CurrentFile;
        private string m_Filename;
        private string m_FileTpye;
        private string m_Uploader;
        private string m_Group;
        private string m_SubGroup;
        private string m_SubGroupCategory;
        private string m_Sdate;
        private string m_Stime;
        private string m_Messagebody;
        private string m_Sourceaddress;
        private string m_MsgType;
        private string m_Template;
        private string m_MsisdnType;
        private string m_Msisdn;

        //private string m_Group;
        //private string m_SubGroup;
        //private string m_GroupCategory;
        private int m_Depht;


        public UploadDataInfosms()
        {
            CurrentFile = "";
            Filename = "";
            FileTpye = "";
            Uploader = "";
            Group = "";
            SubGroup = "";
            SubGroupCategory = "";
            Sdate = "";
            Stime = "";
            Messagebody = "";
            Sourceaddress = "";
            MsgType = "";
            Template = "";
            MsisdnType = "";
            Msisdn = "";
            //Group = "";
            //SubGroup = "";
            //GroupCategory= "";        
            Depht = -1;
        }

        public string CurrentFile
        {
            get { return m_CurrentFile; }
            set { m_CurrentFile = value; }
        }
        public string Filename
        {
            get { return m_Filename; }
            set { m_Filename = value; }
        }
        public string Group
        {
            get { return m_Group; }
            set { m_Group = value; }
        }
        public string SubGroup
        {
            get { return m_SubGroup; }
            set { m_SubGroup = value; }
        }
        public string FileTpye
        {
            get { return m_FileTpye; }
            set { m_FileTpye = value; }
        }
        public string SubGroupCategory
        {
            get { return m_SubGroupCategory; }
            set { m_SubGroupCategory = value; }
        }
        //
        public string Uploader
        {
            get { return m_Uploader; }
            set { m_Uploader = value; }
        }
        public string Sdate
        {
            get { return m_Sdate; }
            set { m_Sdate = value; }
        }
        //
        public string Stime
        {
            get { return m_Stime; }
            set { m_Stime = value; }
        }
        //
        public string Messagebody
        {
            get { return m_Messagebody; }
            set { m_Messagebody = value; }
        }
        //m_CourseOfStudy
        public string Sourceaddress
        {
            get { return m_Sourceaddress; }
            set { m_Sourceaddress = value; }
        }

        //private int m_DeptId;
        //private int m_FacultyId;
        public string MsgType
        {
            get { return m_MsgType; }
            set { m_MsgType = value; }
        }
        public string Template
        {
            get { return m_Template; }
            set { m_Template = value; }
        }
        //m_CourseOfStudyId

        public string MsisdnType
        {
            get { return m_MsisdnType; }
            set { m_MsisdnType = value; }
        }
        //Msisdn
        public string Msisdn
        {
            get { return m_Msisdn; }
            set { m_Msisdn = value; }
        }
        //

        //private string m_GroupCategory;
        //public string GroupCategory
        //{
        //    get { return m_GroupCategory; }
        //    set { m_GroupCategory = value; }
        //}
        //private int m_Depht;
        public int Depht
        {
            get { return m_Depht; }
            set { m_Depht = value; }
        }
    }

