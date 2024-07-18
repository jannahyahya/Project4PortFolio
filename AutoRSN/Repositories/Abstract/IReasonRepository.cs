using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IReasonRepository
    {
        IEnumerable<Reason> GetReason(int ServerId,bool isActive=false);
        IEnumerable<Reason> GetReasonBySearch(int ServerId,string Reason = null);
        Reason IsExistReason(int ServerId, string reason = null);
        Reason GetReasonByReasonId(int ServerId,int ReasonId);
        int UpdateReasonByReasonId(int ServerId,Reason objServer);
        int InsertReason(int ServerId, Reason objServer);

    }
}