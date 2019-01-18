using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Auditor
{
    public static class MailUtils
    {
        //TODO change mail server settings
        private const string mailServerIp = "*****";

        private const int mailServerPort = 25;
        private const string mailSender = "AppNotifier@DoNotReply.com";

        public static void SendEmail(string subject, string body, List<string> recipients) => SendEmail(subject, body, recipients, new List<Attachment>());

        public static void SendEmail(string subject, string body, List<string> recipients, List<Attachment> attachments)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(mailServerIp, mailServerPort))
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(mailSender);
                    foreach (string mail in recipients)
                    {
                        if (EmailAddressValid(mail))
                        {
                            message.To.Add(mail);
                        }
                    }
                    if (message.To.Count == 0)
                    {
                        throw new Exception("Email sending error - not a single recipient mail address is valid!");
                    }
                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = body;
                    foreach (var attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Email sending error - not a single recipient mail address is valid!")
                {
                    string errorMessage = ex.ToString();
                    AppUtils.LogError(errorMessage);
                }
                else
                {
                    throw;
                }
            }
        }

        public static bool EmailAddressValid(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) return false;
            string mailPattern = "\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z";
            bool isEmail = Regex.IsMatch(emailAddress, mailPattern, RegexOptions.IgnoreCase);
            return isEmail;
        }
    }
}