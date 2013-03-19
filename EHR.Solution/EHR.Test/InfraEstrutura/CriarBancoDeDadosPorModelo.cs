using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace EHR.Test.InfraEstrutura
{
    [TestFixture]
    [Ignore]
    public class CriarBancoDeDadosPorModelo
    {
        [Test]
        public void a_Criar_Banco_De_Dados_Por_Modelo()
        {
            try
            {
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
               .FromAppSetting("connection")
                ).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<AdmissionMap>()).Mappings(m => m.MergeMappings())
                .ExposeConfiguration(BuildSchema).BuildSessionFactory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            new SchemaExport(config)
                 .Drop(true, true);

            new SchemaExport(config)
                .Create(true, true);
        }

    }
}
