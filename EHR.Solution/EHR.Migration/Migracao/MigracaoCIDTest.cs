using System.Collections.Generic;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Migration.Entities.Migracao;
//using EHR.Domain.Entities.Sumario;
using EHR.Migration.Mapping.MappingMigracao;
using EHR.Migration.Repository.Integracao;
using NUnit.Framework;

namespace EHR.Migration.Migracao
{
    [TestFixture]
    public class MigracaoCIDTest 
    {
        [Test]
        public void MigrarCid()
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
