using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net;
using System.Web;


/// <summary>
/// Summary description for CSheetname
/// </summary>
public class ProgressBarFeedback
{
    //string IP = System.Configuration.ConfigurationManager.AppSettings["PayrollProcessingFeedbackIP"];
    public ProgressBarFeedback()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Getfeedback(string IP, string Ftype, string msg)
    {
        String responsefromserver =  String.Empty;

            try
            {
                //ProcessingFeedbackIP
                

                // Step create your parameters
                //string url = "http://41.138.183.103:3600/hh.htm?payroll=" + Ftype + "&msg=" + msg;
                string url = "http://"+IP.Trim() +"/hh.htm?pin=" + Ftype + "&msg=" + msg;
                //Step 2 - create a request
                HttpWebRequest httpwebrequest = (HttpWebRequest)WebRequest.Create(url);
                httpwebrequest.Timeout = 10000;
                httpwebrequest.Method = "POST";
                httpwebrequest.ContentType = "application/x-www-form-urlencode";

                // Step 3 create an encoded empty buffer
                ASCIIEncoding encoding = new ASCIIEncoding();
                string strmessage = "NIL";
                byte[] buffer = encoding.GetBytes(strmessage);

                // Step 4 set httpwebclient length to bufferlength
                httpwebrequest.ContentLength = buffer.Length;

                //Step 5 - Create a IO stream and write off the buffer
                Stream requeststream = httpwebrequest.GetRequestStream();
                requeststream.Write(buffer, 0, buffer.Length);
                requeststream.Close();



                //GETTING RESPONSE FROM THE SERVER"

                //Step 1
                HttpWebResponse httpwebresponse = null;
                httpwebresponse = (HttpWebResponse)httpwebrequest.GetResponse();

                // CREATE A STREAM TO RECEIVE IT
                Stream responsestream = httpwebresponse.GetResponseStream();
                StreamReader streamreader = new StreamReader(responsestream);

                char[] readbuffer = new char[1024];
                int count = streamreader.Read(readbuffer, 0, 1024);

                
                responsefromserver = new String(readbuffer, 0, count);
                while (count > 0)
                {
                    responsefromserver = new String(readbuffer, 0, count);
                    Console.WriteLine(responsefromserver);
                    count = streamreader.Read(readbuffer, 0, 1024);

                }

                //Console.WriteLine("RESPONSE= " + responsefromserver);
                streamreader.Close();
                responsestream.Close();
                httpwebresponse.Close();


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

            }
           

            return responsefromserver;
    }
}
