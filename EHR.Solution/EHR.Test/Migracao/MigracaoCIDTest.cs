using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using EHR.Domain.Entities.Migracao;
//using EHR.Domain.Entities.Sumario;
using EHR.Domain.MappingMigracao;
using EHR.Domain.Repository;
using EHR.Domain.Repository.Integracao;
using NUnit.Framework;

namespace EHR.Test.Migracao
{
    [TestFixture]
    
    public class MigracaoCIDTest 
    {
        [Test]
        [Ignore]
        public void MigrarDEF()
        {
            var conexao = Conexao.CreateSessionFactoryOracle();
            CidsIntegracao defs = new CidsIntegracao(conexao.OpenSession());

            var listaCidIntegracao = defs.All<CidMigracao>();

            Cids repositorioCid = new Cids(Conexao.CreateSessionFactory().OpenSession());

            List<Cid> listaCid = new List<Cid>();

            foreach (var cidIntegracao in listaCidIntegracao)
            {
                var cid = new Cid();
                cid.Code = cidIntegracao.CodigoCid;
                cid.Description = cidIntegracao.Descricao;

                listaCid.Add(cid);
            }

            repositorioCid.SalvarLista(listaCid);

        }
    }
}
