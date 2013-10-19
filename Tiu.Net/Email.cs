using System;
using System.Collections.Generic;

using System.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace Tiu.Net
{
    /// <summary>  
    /// 邮件类  
    /// </summary>  
    public class EMail
    {
        #region 【字段】

        /// <summary>
        /// 邮件信息对象
        /// </summary>
        private MailMessage _mailMessage;

        /// <summary>
        /// SMTP客户端对象
        /// </summary>
        private SmtpClient _smtpClient;

        /// <summary>
        /// 发件人密码
        /// </summary>
        private string password;

        #endregion


        #region 【构造方法】

        /// <summary>  
        /// 构造函数
        /// </summary>  
        /// <param name="to">收件人地址</param>  
        /// <param name="from">发件人地址</param>  
        /// <param name="body">邮件正文</param>  
        /// <param name="title">邮件的主题</param>  
        /// <param name="pwd">发件人密码</param>  
        public EMail(string to, string from, string body, string title, string pwd)
        {
            // 初始化相关属性
            _mailMessage = new MailMessage();
            _mailMessage.To.Add(to);
            _mailMessage.From = new System.Net.Mail.MailAddress(from);
            _mailMessage.Subject = title;
            _mailMessage.Body = body;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.Priority = System.Net.Mail.MailPriority.High;

            password = pwd;
        }
                
        #endregion


        #region 【方法】

        /// <summary>  
        /// 添加附件  
        /// <param name="paths">附件路径（多个，用","隔开）</param>
        /// </summary>  
        public void Attachments(string paths)
        {
            string[] path = paths.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化附件  
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);//获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的读取日期  
                _mailMessage.Attachments.Add(data);//添加到附件中  
            }
        }

        /// <summary>  
        /// 异步发送邮件  
        /// </summary>  
        /// <param name="completedMethod">发送邮件完成时的回调</param>  
        public void SendAsync(SendCompletedEventHandler completedMethod)
        {
            if (_mailMessage != null)
            {
                _smtpClient = new SmtpClient();
                _smtpClient.Credentials = new System.Net.NetworkCredential(_mailMessage.From.Address, password);//设置发件人身份的票据
                _smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                _smtpClient.Host = "smtp." + _mailMessage.From.Host;
                if (completedMethod != null)
                {
                    _smtpClient.SendCompleted += new SendCompletedEventHandler(completedMethod);//注册异步发送邮件完成时的回调
                }
                _smtpClient.SendAsync(_mailMessage, _mailMessage.Body);
            }
        }

        /// <summary>  
        /// 发送邮件  
        /// <param name="port">指定发送邮件端口</param>
        /// </summary>  
        public void Send(int port)
        {
            if (_mailMessage != null)
            {
                _smtpClient = new SmtpClient();
                _smtpClient.Credentials = new System.Net.NetworkCredential(_mailMessage.From.Address, password);//设置发件人身份的票据  
                _smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                _smtpClient.Host = "smtp." + _mailMessage.From.Host;
                _smtpClient.Port = port; //指定发送邮件端口 
                _smtpClient.Send(_mailMessage);
            }
        }

        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        public void Send()
        {
            if (_mailMessage != null)
            {
                _smtpClient = new SmtpClient();
                _smtpClient.Credentials = new System.Net.NetworkCredential(_mailMessage.From.Address, password);//设置发件人身份的票据  
                _smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                _smtpClient.Host = "smtp." + _mailMessage.From.Host;
                _smtpClient.Send(_mailMessage);
            }
        }

        #endregion
    }
}
