using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Migracao;
using EHR.Domain.Entities.Sumario;
using EHR.Domain.Mapping;
using EHR.Domain.MappingMigracao;
using EHR.Domain.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace EHR.Test.Migracao
{
    [TestFixture]
    [Ignore]
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
                def.Produto = defIntegracao.Produto;

                listaDef.Add(def);
            }

            repositorioDef.SalvarLista(listaDef);
            
        }
       
    }
}
