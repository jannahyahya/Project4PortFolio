using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoRSN.Models;

namespace AutoRSN.Repositories.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUser(int ServerId, bool isActive = false);
        IEnumerable<User> GetUserBySearch(int ServerId, string CCMG = null);
        User IsExistUser(int ServerId, string UserCode = null, string MaterialGroup = null);
        User GetUserByUserPONumber(int ServerId, string UserCode = null, string MaterialGroup = null);
        int UpdateUser(int ServerId, User objUser);
        int InsertUser(int ServerId, User objUser);
    }
}