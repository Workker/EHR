using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using EHR.Domain.Entities;
using ImportLegacySummary.Mapping;
using Legacy = ImportLegacySummary.DTO;

namespace ImportLegacySummary
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperConfiguration.Configure();

            using(var dbLegacy = new DbLegacy().OpenConnection())
            {
                ImportSummary(dbLegacy);
            }

            Console.WriteLine("SUCCESS Importação concluída");
            Console.ReadKey();
        }

        static void ImportSummary(DbLegacy dbLegacy) 
        {
            var legacySummaries = dbLegacy.GetSummaries();
            foreach (var legacySummary in legacySummaries)
            {
                var newSummary = BuildNewSummary(legacySummary);
                //TODO: save newSummary
            }
        }

        static Summary BuildNewSummary(Legacy.Summary legacySummary) 
        {
            Mapper.CreateMap<Legacy.Summary, Summary>();
            return Mapper.Map(legacySummary, new Summary());
        }
    }
}
