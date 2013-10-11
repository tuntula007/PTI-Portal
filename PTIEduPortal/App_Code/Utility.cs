using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
    private static string strConn = ConfigurationManager.AppSettings["ConnString"];

    public Utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void MessageBox(string strMsg, Page myPage)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + "); </script>";

        //add the label to the page to display the alert
        myPage.Controls.Add(lbl);

    }
    public void MessageBox(string strMsg, string redirectUrl, Page myPage)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + "); window.location.href=" + "'" + redirectUrl + "';  </script>";

        //add the label to the page to display the alert
        myPage.Controls.Add(lbl);

    }
    public void ConfirmBox(string strMsg, Page myPage)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.confirm(" + "'" + strMsg + "'" + ");</script>";

        //add the label to the page to display the alert
        myPage.Controls.Add(lbl);

    }
    public void PerformQuery(string Qry)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();

            SqlCommand cmd = null;
            //SqlDataReader dr = null;
            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
    }
    public DataSet SelectQuery(string Qry)
    {
        DataSet dt = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();

            SqlCommand cmd = null;
            //SqlDataReader dr = null;
            SqlDataAdapter dat = new SqlDataAdapter(Qry, cnn);
            dat.Fill(dt);
            dat.Dispose();
            //cmd = new SqlCommand();
            //dt= cmd.ExecuteReader();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
        return dt;
    }

    public string DoubleString(string value)
    {
        //String.Format("{0:0.00}", 123.4567);
        int prsInt; string input = "";
        input = (int.TryParse(value, out prsInt)) ? String.Format("{0,0:N2}", Int32.Parse(value) / 1.0) : value;
        return input;
    }
    public string DoubleStrings(string value)
    {
        int tryInt;
        string retValue = (int.TryParse(value, out tryInt)) ? int.Parse(value).ToString("F2") : value;
        return retValue;
    }
 
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name=”T”>Custome Class </typeparam>
    /// <param name=”lst”>List Of The Custome Class</param>
    /// <returns> Return the class datatbl </returns>
    public static DataTable ConvertTo<T>(IList<T> lst)
    {
        //create DataTable Structure
        DataTable tbl = CreateTable<T>();
        Type entType = typeof(T);

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
        //get the list item and add into the list
        foreach (T item in lst)
        {
            DataRow row = tbl.NewRow();
            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item);
            }
            tbl.Rows.Add(row);
        }

        return tbl;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name=”T”>Custome Class</typeparam>
    /// <returns></returns>
    public static DataTable CreateTable<T>()
    {
        //T –> ClassName
        Type entType = typeof(T);
        //set the datatable name as class name
        DataTable tbl = new DataTable(entType.Name);
        //get the property list
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
        foreach (PropertyDescriptor prop in properties)
        {
            //add property as column
            tbl.Columns.Add(prop.Name, prop.PropertyType);
        }
        return tbl;
    }

    public void LoadListByText(DropDownList cmb, string searchText)
    {
        searchText = (string.IsNullOrEmpty(searchText) == true) ? "" : searchText;
        ListItem li;
        li = cmb.Items.FindByText(searchText);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    public void LoadListByText(RadioButtonList cmb, string searchText)
    {
        searchText = (string.IsNullOrEmpty(searchText) == true) ? "" : searchText;
        ListItem li;
        li = cmb.Items.FindByText(searchText);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    public void LoadListByValue(DropDownList cmb, string searchValue)
    {
        searchValue = (string.IsNullOrEmpty(searchValue) == true) ? "" : searchValue;
        ListItem li;
        li = cmb.Items.FindByValue(searchValue);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    public void LoadListByValue(RadioButtonList cmb, string searchValue)
    {
        searchValue = (string.IsNullOrEmpty(searchValue) == true) ? "" : searchValue;
        ListItem li;
        li = cmb.Items.FindByValue(searchValue);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }
}

public static class StringExtension
{
    public static String InsertDecimal(this String @this, Int32 precision)
    {
        int res;
        if (!int.TryParse(@this, out res)) return @this;
        String padded = @this.PadLeft(precision, '0');
        return padded.Insert(padded.Length - precision, ".");
    }

    // string extension method ToUpperFirstLetter
    public static string ToUpperFirstLetter(this string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;
        // convert to char array of the string
        char[] letters = source.ToCharArray();
        // upper case the first char
        letters[0] = char.ToUpper(letters[0]);
        // return the array made of the new char array
        return new string(letters);
    }

    public static string ToUppercaseWords(this string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }

    public static string Right(this string value, int length)
    {
        return value.Substring(value.Length - length);
    }

    public static string Format234(this string phone)
    {
        phone = phone.Trim();
        string No234 = "";
        No234 = (phone.StartsWith("234")) ? phone : ("234" + ((phone.StartsWith("0")) ? phone.Remove(0, 1) : phone));

        return No234;
    }
}

