using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPKadryPlace.ViewModel
{
    public static class LibraryConfig
    {
        public static string _connectionString = string.Empty;

        static LibraryConfig()
        {
        }

        public static void SetConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
