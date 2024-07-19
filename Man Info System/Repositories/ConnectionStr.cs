using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoRSN.Repositories
{
    public class ConnectionStr
    {
        public const string OEE = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=oee;Password=o5oeetcp;";
        public const string HONEYWELL = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.43)(PORT=1521)))(CONNECT_DATA=(SID = odcse02)));User Id=honeyse;Password=o5honeytcp;";
        public const string THALES = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=thales;Password=o5thalestcp;";
        public const string COLLINS = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=hs;Password=o5hstcp;";
        public const string GOODRICH = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.195.80.42)(PORT=1521)))(CONNECT_DATA=(SID = odcse01)));User Id=goodrich;Password=o5goodrichtcp;";

        public const string CONNSTR_KEY = "CONNSTR";
    }
}