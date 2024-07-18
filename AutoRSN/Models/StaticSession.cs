using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace AutoRSN.Models
{
    public class StaticSession
    {
        public static List<string> sessionList = new List<string>();

        public void startTimer(string user)
        {
            Thread thread = new Thread(()=> timerThread(user));
            thread.Start();
        }

        public void timerThread(string user)
        {
            System.Diagnostics.Debug.WriteLine(HttpContext.Current.Session["UserId"]);
          while(HttpContext.Current.Session["UserId"] != null)
            {
                Thread.Sleep(1000);
            }
            sessionList.Remove(user);

        }
    }
}