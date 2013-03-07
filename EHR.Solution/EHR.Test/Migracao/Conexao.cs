using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Mapping;
using EHR.Domain.MappingMigracao;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace EHR.Test.Migracao
{
    public class Conexao
    {
        public static ISessionFactory CreateSessionFactoryOracle()
        {
            return
                 Fluently.Configure()
            .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"))
            .Database(OracleClientConfiguration.Oracle10.ConnectionString(c => c
                .FromAppSetting("connectionOracleSumario")
                )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<DefMigracaoMap>()).BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                    .FromAppSetting("connection")
                    )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<PatientMap>()).BuildSessionFactory();
        }
    }
}
