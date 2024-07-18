﻿using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace AutoRSN
{
    public class MailHandler
    {

        public void sendFileCreatedMail(byte[] filebytes, string usermail, string userpass, string fullname, string filename)
        {

            try
            {
                var message = new MimeMessage();
                // message.From.Add(new MailboxAddress("eRSN Notification", "murodin@celestica.com"));
                message.From.Add(new MailboxAddress("eRSN Notification", "cmy-spectrumpro-cls@celestica.com"));
                // message.To.Add(new MailboxAddress("Adhi", "murodin@celestica.com"));
                message.To.Add(new MailboxAddress(fullname, usermail));
                //message.To.Add(new MailboxAddress("Adhi", "murodin@celestica.com"));
                // message.To.Add(new MailboxAddress("Adhi", "ady133t@gmail.com"));
                message.Subject = "eRSN ZVDOPGI Generat ed";

                message.Body = new TextPart("plain")
                {
                    Text = @"ZVDOPGI generated by " + fullname
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    //client.Connect("smtp.gmail.com", 465, true);
                    client.Connect("smtp.celestica.com", 25, false);

                    // Note: only needed if the SMTP s erver requires authentication
                    //client.Authenticate("murodin@celestica.com", "Celestica8");

                    var builder = new BodyBuilder();
                    //IWorkbook file = CreateExcelFile(); //create excel file
                    //var exportData = new MemoryStream(); // must be in stream in memory
                    //file.Write(exportData);
                    builder.TextBody = @"ZVDOPGI generated by " + fullname + " (" + usermail + ")";

                    builder.Attachments.Add(filename, filebytes, new ContentType("application", "vnd.ms-excel")); // optional for attachment
                    message.Body = builder.ToMessageBody(); //optional for attachment
                    client.Send(message);
                    client.Disconnect(true);

                }

            }

            catch (Exception ex)
            {

                Debug.WriteLine("Error sending email =" + ex.Message);
            }

            //Console.WriteLine("Mail Send..!!");


        }

        //public void sendFileCreatedMailAdmin(byte[] filebytes, string usermail, string fullname, string filename, string sheetPass)
        //{
        //    try
        //    {

        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("eRSN Notification", "murodin@celestica.com"));
        //        message.To.Add(new MailboxAddress("Adhi", "murodin@celestica.com"));
        //        // message.To.Add(new MailboxAddress(fullname, usermail));
        //        //message.To.Add(new MailboxAddress("Adhi", "ady133t@gmail.com"));
        //        message.Subject = "mail subject";

        //        //message.Body = new TextPart("plain")
        //        //{
        //        //    Text = @"ZVDOPGI generated by " + fullname
        //        //};

        //        using (var client = new SmtpClient())
        //        {
        //            // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
        //            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        //            client.Connect("smtp.gmail.com", 465, true);

        //            // Note: only needed if the SMTP s erver requires authentication
        //            client.Authenticate("murodin@celestica.com", "Celestica8");

        //            var builder = new BodyBuilder();
        //            //IWorkbook file = CreateExcelFile(); //create excel file
        //            //var exportData = new MemoryStream(); // must be in stream in memory
        //            //file.Write(exportData);
        //            builder.TextBody = @"you content here";

        //            //builder.Attachments.Add(filename, filebytes, new ContentType("application", "vnd.ms-excel")); // optional for attachment
        //            message.Body = builder.ToMessageBody(); //optional for attachment
        //            client.Send(message);
        //            client.Disconnect(true);

        //            //  Console.WriteLine("Mail Send..!!");
        //        }

        //    }



        //    catch (Exception ex)
        //    {

        //        Debug.WriteLine("Error sending email =" + ex.Message);
        //    }



        //}


        //public void send_mail(byte[] filebytes, string usermail, string fullname, string filename, string sheetPass)
        //{
        //    try
        //    {

        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("eRSN Notification", "murodin@celestica.com"));
        //        message.To.Add(new MailboxAddress("target_name", "target_mail"));

        //        message.Subject = "mail subject";

       

        //        using (var client = new SmtpClient())
        //        {
        //            // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
        //            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        //            client.Connect("smtp.gmail.com", 465, true);

        //            // Note: only needed if the SMTP s erver requires authentication
        //            client.Authenticate("sender_mail", "sender_pass");

        //            var builder = new BodyBuilder();
        //            builder.TextBody = @"you content here";

                  
        //            message.Body = builder.ToMessageBody(); //optional for attachment
        //            client.Send(message);
        //            client.Disconnect(true);
        //        }

        //    }



        //    catch (Exception ex)
        //    {

        //        Debug.WriteLine("Error sending email =" + ex.Message);
        //    }



        //}



    }
}