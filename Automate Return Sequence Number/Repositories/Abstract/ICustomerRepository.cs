using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomer(int ServerId, bool isActive = false);
        IEnumerable<Customer> GetCustomerBySearch(int ServerId, string CCMG = null);
        Customer IsExistCustomer(int ServerId, string CustomerCode = null, string MaterialGroup = null);
        Customer GetCustomerByCustomerPONumber(int ServerId, string CustomerCode = null, string MaterialGroup = null);
        int UpdateCustomer(int ServerId, Customer objCustomer);
        int InsertCustomer(int ServerId, Customer objCustomer);
    }
}