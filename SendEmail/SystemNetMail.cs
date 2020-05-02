using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Advance_Csharp
{
    
    class EmailByNetMail
    {
        public static void SendEmail()
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("2016231075@qq.com");//收件人地址
            //msg.CC.Add("hellworld@qq.com");//抄送人地址

            //The server response was: Mail from address must be same as authorization user.'
            //注意这里from消息和下面搞认证的消息必须一致
            msg.From = new MailAddress("1743432766@qq.com", "Schdimt");//发件人邮箱，名称

            msg.Subject = "This is a test email from QQ";//邮件标题
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8

            msg.Body = "this is body";//邮件内容
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.qq.com";//SMTP服务器地址
            client.Port = 587;//SMTP端口，QQ邮箱填写587
            
            client.EnableSsl = true;//启用SSL加密


            //这里搞了一波机密管理
            //CLI命令输入就完事了
            string jsonfile = @"C:\Users\DELL\AppData\Roaming\Microsoft\UserSecrets\ee6f7777-9738-4ddc-b287-7868412d3df1\secrets.json";
            StreamReader file = File.OpenText(jsonfile);
            JsonTextReader reader = new JsonTextReader(file);
                
            JObject o = (JObject)JToken.ReadFrom(reader);
            var value = o["password"].ToString();                
            client.Credentials = new NetworkCredential("1743432766@qq.com",value);//发件人邮箱账号，密码是qq邮箱里面申请smtp所得16位的英文序列

            client.Send(msg);//发送邮件
        }
        public static void Main()
        {
            SendEmail();
        }
    }
}

