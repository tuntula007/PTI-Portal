using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Messaging;
//01-4315198
/// <summary>
/// Summary description for CWritetoqueue
/// </summary>
public class CWritetoqueue
{
    public   CPermit Logonpermit;
    public  string strPath;    

    public   void Writeaudit(CPermit cp)
    {
        try
        {
            //MessageQueue mq = new MessageQueue(strPath);
            //mq.DefaultPropertiesToSend.Label = "Sch Portal Audit trail " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //mq.DefaultPropertiesToSend.Recoverable = true;
            //mq.DefaultPropertiesToSend.AttachSenderId = true;
            //mq.Send(cp);

            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(strPath))
            {
                mq = MessageQueue.Create(strPath);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(strPath);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(CPermit) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + cp.Userid;
            mq.Send(cp);
            mq.Dispose();
            mq.Close();
        }
        catch (Exception er)
        {
            
            string err = er.Message;
        }
    }


}
