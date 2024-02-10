using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//*****************************************************
using MailKit.Net.Smtp;   // 請在 Nuget裡面，安裝「MailKit」
using MailKit;
using MimeKit;
//             https://docs.microsoft.com/zh-tw/dotnet/api/system.net.mail （微軟想廢了他   *驚！！*  ）
//             請改用 https://github.com/jstedfast/MailKit （範例在此）
//             https://codeforwin.org/2015/03/sending-emails-in-c.html  （文末有列出各種 Mail Server & Port）


namespace WindoswDesktopClassLibrary1
{
    //***** 別忘了設為 public，才能被引用！！
    public class MyMailDLL
    {


        public static void SendMail()
        {   // https://github.com/jstedfast/MailKit （範例在此）
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("寄信人 Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("收信人 Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "信件標題";
            message.Body = new TextPart("plain")
            {   // 純文字
                Text = @"信件內容"
            };

            using (var client = new SmtpClient())
            {   // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);
                //Gmail      smtp.gmail.com  587
                //Hotmail      smtp.live.com   465
                //Outlook      smtp.live.com   587
                //Office365      smtp.office365.com  587
                //Yahoo mail       smtp.mail.yahoo.com 465
                //Yahoo mail plus      plus.smtp.mail.yahoo.com    465
                //Verizon     outgoing.yahoo.verizon.net  587

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("寄信的帳號", "密碼");

                client.Send(message);   // 信件寄出！！
                client.Disconnect(true);
            }
        }




    }
}
