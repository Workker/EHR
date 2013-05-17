using EHR.Domain.Entities;
using EHR.Domain.Entities.Migracao;
//using EHR.Domain.Entities.Sumario;
using EHR.Domain.Repository;
using NUnit.Framework;
using System.Collections.Generic;

namespace EHR.Test.Migracao
{
    [TestFixture]
    public class MigracaoTUSSTest
    {
        [Test]
        [Ignore]
        public void MigrarProcedimentos()
        {
            var conexao = Conexao.CreateSessionFactoryOracle();
            ProcedimentosIntegracao defs = new ProcedimentosIntegracao(conexao.OpenSession());

            var listaDefIntegracao = defs.All<ProcedimentoMigracao>();

            TusRepository repositorioProcedimento = new TusRepository(Conexao.CreateSessionFactory().OpenSession());

            List<Tus> listaProcedimento = new List<Tus>();

            foreach (var procedimentoIntegracao in listaDefIntegracao)
            {
                var procedimento = new Tus();
                procedimento.Code = procedimentoIntegracao.CodigoProcedimento;
                procedimento.Description = procedimentoIntegracao.Procedimento;
                listaProcedimento.Add(procedimento);
            }

            repositorioProcedimento.Save(listaProcedimento);

        }

    }
}
