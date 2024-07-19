using AutoRSN.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AutoRSN.Repositories
{
    class WebService
    {
        List<GTSModel> gtsList = null;
        public WebService()
        {

            gtsList = getgtsList();


        }

        public List<GTSModel> getgtsList()
        {
          //  request.UseDefaultCredentials = true;
            String URL = "http://10.195.80.15/GoogleData/webservice.asmx/GetGTS";
            WebClient wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
            wc.Encoding = Encoding.UTF8;
            var strJson = wc.DownloadString(URL);
            //System.Diagnostics.Debug.WriteLine(strJson);
            //var translated = JsonConvert.DeserializeObject(strJson);
            return JsonConvert.DeserializeObject<List<GTSModel>>(strJson);


        }

        public bool checkGTS(string matnr)
        {
            bool exist = false;
            foreach (GTSModel gtsModel in gtsList)
            {
                if (gtsModel.PN.Equals(matnr))
                {
                    if (gtsModel.Export)
                        return true;
                    else
                        return false;
                }

            }

            return exist;
        }
    }
}