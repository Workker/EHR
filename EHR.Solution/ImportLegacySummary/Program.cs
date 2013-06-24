using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ImportLegacySummary
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var dbLegacy = new DbLegacy().OpenConnection())
            {
                ImportSummary(dbLegacy);
            }
            Console.WriteLine("SUCCESS Importação concluída");
            Console.ReadKey();
        }

        static void ImportSummary(DbLegacy dbLegacy) 
        {
            var summaries = dbLegacy.GetSummaries();
        }
    }
}
