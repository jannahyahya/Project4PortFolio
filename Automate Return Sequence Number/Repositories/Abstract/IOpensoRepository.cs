using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IOpensoRepository
    {
        IEnumerable<Openso> GetOpenso();
        IEnumerable<Openso> GetOpenso(int ServerId, bool isActive = false);
        IEnumerable<Openso> GetOpensoBySearch(int ServerId, string CCMG = null, string CCMG1 = null, string CCMG2 = null, string CCMG3 = null, string CCMG4 = null, string CCMG5 = null);
        Openso IsExistOpenso(int ServerId, string MATNR = null, string RSNNO = null);
        Openso GetOpensoByCustomerPONumber(int ServerId, string MATNR = null, string RSNNO = null);
        int UpdateOpenso(int ServerId, Openso objOpenso);
        int InsertOpenso(int ServerId, Openso objOpenso);
    }
}