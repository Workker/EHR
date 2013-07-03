using System.Collections.Generic;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Migration.Entities.Migracao;
//using EHR.Domain.Entities.Sumario;
using EHR.Migration.Repository.Integracao;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace EHR.Migration.Migracao
{
    [TestFixture]
    public class MigracaoDEF
    {
        [Test]
        public void MigrarDEF()
        {
            var conexao = Conexao.CreateSessionFactoryOracle();
            DefsIntegracao defs = new DefsIntegracao(conexao.OpenSession());

            var listaDefIntegracao = defs.All<DefMigracao>();

            Defs repositorioDef = new Defs(Conexao.CreateSessionFactory().OpenSession());

            List<Def> listaDef = new List<Def>();

            foreach (var defIntegracao in listaDefIntegracao)
            {
                var def = new Def();
                def.Description = defIntegracao.Produto;

                listaDef.Add(def);
            }

            repositorioDef.SalvarLista(listaDef);

        }

    }
}
