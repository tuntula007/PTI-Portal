using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
/// <summary>
/// Summary description for PictureManager
/// </summary>
public class PictureManager
{
	public PictureManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static  int SaveImage(string key,byte[] image)
   {
   
        
        int rowsAffected=0;
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter ("@PicKey",key));
        db.AddParameter(new SqlParameter ("@Photo",image) );

        rowsAffected = db.ExecuteNonQuery("PictureFile_Insert");

    //using (SqlConnection connection = new SqlConnection(“...“))
    //{
    //    using (SqlCommand command = connection.CreateCommand())
    //    {
    //        command.CommandText = "INSERT INTO Photos
    //                                  (Photo) VALUES (@Photo)";
    //        command.Parameters.AddWithValue("@Photo", image);

    //        connection.Open();
    //        rowsAffected = command.ExecuteNonQuery();
    //    }
    //}
        db.Dispose ();
        return rowsAffected;
   }
public static Image RetrieveImage(string key)
{
         Image image = null;

        DBAccess db = new DBAccess ();
        db.AddParameter(new SqlParameter ("@PicKey",key));

        byte[] imageData = (byte[])db.ExecuteScalar("PictureFile_getPhoto");
     
             MemoryStream memStream = new MemoryStream(imageData);
            image = Image.FromStream(memStream);
            db.Dispose();

    return image;
}
}
//using (SqlConnection connection = new SqlConnection("..."))
//{
//    using (SqlCommand command = connection.CreateCommand())
//    {
//        command.CommandText = "SELECT Photo FROM Photos
//                                  WHERE PhotoId = @PhotoId";
//        command.Parameters.AddWithValue("@PhotoId", photoId);

//        connection.Open();
//        byte[] imageData = (byte[])command.ExecuteScalar();

//        MemoryStream memStream = new MemoryStream(imageData);
//        image = Image.FromStream(memStream);
//    }
//}