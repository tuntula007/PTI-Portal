using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



	public class DBAccess: BaseBusiness, IDisposable 
    {

		private IDbCommand cmd=new SqlCommand();
		private string strConnectionString="";
		private bool handleErrors=false;
        private string strLastError = "";
        SqlConnection conn = new SqlConnection();
        string key = "";
		public DBAccess()
		{


            string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
                //"server=;uid=;pwd=;database=saversplus;Max Pool Size=200;Connection Timeout=30";
            SqlConnection cnn=new SqlConnection();
			cnn.ConnectionString=strConnectionString;
			cmd.Connection=cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.ConnectionString = strConnectionString;
            
        }

        public DBAccess(string cachekey)
        {

            this.key = cachekey;
            string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
            //"server=;uid=;pwd=;database=saversplus;Max Pool Size=200;Connection Timeout=30";
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strConnectionString;
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            conn.ConnectionString = strConnectionString;

        }

        public DBAccess(int k1, int k2)
        {


            string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
            //"server=;uid=;pwd=;database=saversplus;Max Pool Size=200;Connection Timeout=30";
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strConnectionString;
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            conn.ConnectionString = strConnectionString;
           

        }

        /// <summary>
        /// to connect to portal data base
        /// </summary>
        /// <param name="k"></param>
        public DBAccess(int k)
        {


            string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["PortalConnString"];
            //"server=;uid=;pwd=;database=saversplus;Max Pool Size=200;Connection Timeout=30";
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = strConnectionString;
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            
            conn.ConnectionString = strConnectionString;

        }


		public IDataReader ExecuteReader()
		{
			IDataReader reader=null;
            try
            {
                this.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (handleErrors)
                    strLastError = ex.Message;
                else
                    throw;
            }
            
			return reader;
		}

		public IDataReader ExecuteReader(string commandtext)
		{
            IDataReader reader=null;
            
            if (BaseBusiness.enableCache && BizObject.Cache[key] != null)
            {
                reader = (SqlDataReader)BizObject.Cache[key];
            }
            else
            {
               
               try
		        {
			        cmd.CommandText=commandtext;
			        reader=this.ExecuteReader();
                    SqlDataReader sdr = (SqlDataReader)reader;
                    if (string.IsNullOrEmpty(key)==false)
                        BaseBusiness.CacheData(key, sdr);
		        }
		        catch(Exception ex)
		        {
			        if(handleErrors)
				        strLastError=ex.Message;
			        else
				        throw;
		        }  
               
            }

			
		
            

			return reader;
		}

		public object ExecuteScalar()
		{
			object obj=null;
			try
			{
				this.Open();
				obj= cmd.ExecuteScalar();
                this.Close();
			}
			catch(Exception ex)
			{
				if(handleErrors)
					strLastError=ex.Message;
				else
					throw;
			}
            

			return obj;
		}

		public object ExecuteScalar(string commandtext)
		{
			object obj=null;
            if (BaseBusiness.enableCache && BizObject.Cache[key] != null)
            {
                obj = (object)BizObject.Cache[key];
            }
            else
            {
                try
                {
                    cmd.CommandText = commandtext;
                    obj = this.ExecuteScalar();
                    if (string.IsNullOrEmpty(key) == false)
                        BaseBusiness.CacheData(key, obj);
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                        strLastError = ex.Message;
                    else
                        throw;
                }
            }

			return obj;
		}

		public int ExecuteNonQuery()
		{
			int i=-1;
			try
			{
			    this.Open();
				i=cmd.ExecuteNonQuery();
                this.Close();
			}
			catch(Exception ex)
			{
				if(handleErrors)
					strLastError=ex.Message;
				else
					throw;
			}
           

			return i;
		}


		public int ExecuteNonQuery(string commandtext)
		{
			int i=-1;
			try
			{
				cmd.CommandText=commandtext;

                cmd.CommandTimeout = 1800;
				i=this.ExecuteNonQuery();
                if (i >= 0)
                { 
                    if (string.IsNullOrEmpty(key)==false)
                        BizObject.PurgeCacheItems(key);
                }
			}
			catch(Exception ex)
			{
				if(handleErrors)
					strLastError=ex.Message;
				else
					throw;
			}
           
			return i;
		}


		public DataSet ExecuteDataSet()
		{
			SqlDataAdapter da=null;
			DataSet ds=null;
			try
			{
				da=new SqlDataAdapter();
				da.SelectCommand=(SqlCommand)cmd;
				ds=new DataSet();
				da.Fill(ds);
			}
			catch(Exception ex)
			{
				if(handleErrors)
					strLastError=ex.Message;
				else
					throw;
			}
            

			return ds;
		}


		public DataSet ExecuteDataSet(string commandtext)
		{
			DataSet ds=null;
            if (BaseBusiness.enableCache && BizObject.Cache[key] != null)
            {
                ds = (DataSet)BizObject.Cache[key];
            }
            else
            {

                try
                {
                    cmd.CommandText = commandtext;
                    ds = this.ExecuteDataSet();
                    if (string.IsNullOrEmpty(key) == false)
                        BaseBusiness.CacheData(key, ds);
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                        strLastError = ex.Message;
                    else
                        throw;
                }
            }

			return ds;
		}

		public string CommandText
		{
			get
			{
				return cmd.CommandText;
			}
			set
			{
				cmd.CommandText=value;
				cmd.Parameters.Clear();
			}
		}

		public IDataParameterCollection Parameters
		{
			get
			{
				return cmd.Parameters;
			}
		}

    	public void AddParameter(string paramname,object paramvalue)
		{
			SqlParameter param=new SqlParameter(paramname,paramvalue);
			cmd.Parameters.Add(param);
		}

		public void AddParameter(IDataParameter param)
		{
			cmd.Parameters.Add(param);
		}


		public string ConnectionString
		{
			get
			{
				return strConnectionString;
			}
			set
			{
				strConnectionString=value;
			}
		}

        private void Open()
        {
            cmd.Connection.Open();
        }

        private void Close()
        {
            cmd.Connection.Close();
        }

		public bool HandleExceptions
		{
			get
			{
				return handleErrors;
			}
			set
			{
				handleErrors=value;
			}
		}

		public string LastError
		{
			get
			{
				return strLastError;
			}
		}

        public void Dispose()
        {
            cmd.Dispose();
        }

        public   SqlCommand OpenCommand(String sql)
        {
            // DataClass cdata = new DataClass();
            // SqlConnection cn = cdata.getConn();
            conn.Open();
            
            SqlCommand comm = new SqlCommand(sql, conn);
            return comm;

        }

        public void setPara(SqlCommand cmd, String paraName, SqlDbType paraType, object paraValue)
        {

            cmd.Parameters.Add(paraName, paraType);
            cmd.Parameters[paraName].Value = paraValue;
        }

   

	}


