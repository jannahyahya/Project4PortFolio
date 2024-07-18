using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN
{
    [HubName("HubProxy")]
    public class MainHub:Hub
    {

        public void onConnected()
        {
            Clients.Client(Context.ConnectionId).sendHello("Welcome.." + Context.ConnectionId);

        }

    }
}