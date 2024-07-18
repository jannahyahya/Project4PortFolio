using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IEmailRepository
    {

        IEnumerable<Email> GetEmail(int ServerId, bool isActive = false);
        IEnumerable<Email> GetEmailBySearch(int ServerId, string CCMG = null);
        Email IsExistEmail(int ServerId, string CustomerCode = null, string EmailTitle = null);
        Email GetEmailByEmailId(int ServerId, int EmailId);
        int UpdateEmail(int ServerId, Email objEmail);
        int InsertEmail(int ServerId, Email objEmail);
    }
}