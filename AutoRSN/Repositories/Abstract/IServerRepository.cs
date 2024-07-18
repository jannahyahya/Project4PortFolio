using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IServerRepository
    {
        IEnumerable<Server> GetServer(bool IsActive=false);
        IEnumerable<Server> GetServerBySearch(string server=null);
        Server IsExistServer(string server = null);
        Server GetServerByServerId(int ServerId);
        int UpdateServerByServerId(Server objServer);
        int InsertServer(Server objServer);

    }
}