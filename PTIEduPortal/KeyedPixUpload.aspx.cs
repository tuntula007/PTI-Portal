using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;

public partial class KeyedPixUpload : System.Web.UI.Page
{
    Students Stud = new Students();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Programmatically pick a random image from the ~/Images directory
            ProcessFolder("~/Picx");
        }
    }

    //Returns the virtual path to a randomly-selected image in the specified directory
    private string PickImageFromDirectory(string directoryPath)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(directoryPath));
        FileInfo[] fileList = dirInfo.GetFiles();
        int numberOfFiles = fileList.Length;

        //Pick a random image from the list
        //Random rnd = new Random();
        //int randomFileIndex = rnd.Next(numberOfFiles);
        for (int i = 0; i < numberOfFiles; i++)
        {
        }
        string imageFileName = fileList[0].Name;
        string fullImageFileName = Path.Combine(directoryPath, imageFileName);

        return fullImageFileName;
    }
    private void ProcessFolder(string sourceDir)
    {
        // Process the list of files found in the directory. 
        DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(sourceDir));
        FileInfo[] fileList = dirInfo.GetFiles("E0*.*");
        //string[] fileEntries = ;
        for (int i = 0; i < fileList.Length; i++)
        {
            //foreach (string fileName in fileEntries)
            // do something with fileName

            Response.Write("Treating " + fileList[i].Name);
            string matricNumber = fileList[i].Name.Replace(fileList[i].Extension, "");
            StudentsBusiness sb = new StudentsBusiness();
            Stud = sb.GetStudentsByMatNo(matricNumber);
            if (string.IsNullOrEmpty(Stud.RegNo) == false)
            {
                FileStream fileStream = new FileStream(fileList[i].FullName, FileMode.Open);
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    //Image.From
                    int length = (int)fileStream.Length;
                    byte[] image = reader.ReadBytes(length);
                    PictureManager.SaveImage(Stud.RegNo, image);
                }
                Response.Write("........Finished treating " + fileList[i].Name + "<br />");
            }
            else
            {
                Response.Write("........" + fileList[i].Name + " Not Found<br />");
            }
        }
    }
}
