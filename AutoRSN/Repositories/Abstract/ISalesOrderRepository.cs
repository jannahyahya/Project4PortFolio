using System.Collections.Generic;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface ISalesOrderRepository
    {
        IEnumerable<SalesOrder> GetSalesOrder(int ServerId, bool isActive = false);
        IEnumerable<SalesOrder> GetSalesOrderBySearch(int ServerId, string CCMG = null);
        SalesOrder IsExistSalesOrder(int ServerId, string BSTNK = null, string POSEX = null);
        SalesOrder GetSalesOrderByCustomerPONumber(int ServerId, string BSTNK = null, string POSEX = null);
        int UpdateSalesOrder(int ServerId, SalesOrder objSalesOrder);
        int InsertSalesOrder(int ServerId, SalesOrder objSalesOrder);

    }
}