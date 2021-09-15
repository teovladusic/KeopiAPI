using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiDataAccess.Models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public static string DatabaseName = "KeopiDatabase";
    }
}
