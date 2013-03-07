using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Migracao;
using EHR.Domain.Entities.Sumario;
using EHR.Domain.MappingMigracao;
using EHR.Domain.Repository;
using NUnit.Framework;

namespace EHR.Test.Migracao
{
    [TestFixture]
    public class MigracaoTUSSTest
    {
        [Test]
        public void MigrarProcedimentos()
        {
            var conexao = Conexao.CreateSessionFactoryOracle();
            ProcedimentosIntegracao defs = new ProcedimentosIntegracao(conexao.OpenSession());

            var listaDefIntegracao = defs.All<ProcedimentoMigracao>();

            Procedimentos repositorioProcedimento = new Procedimentos(Conexao.CreateSessionFactory().OpenSession());

            List<Procedimento> listaProcedimento = new List<Procedimento>();

            foreach (var procedimentoIntegracao in listaDefIntegracao)
            {
                var procedimento = new Procedimento();
                procedimento.CodigoProcedimento = procedimentoIntegracao.CodigoProcedimento;
                procedimento.Grupo = procedimentoIntegracao.Grupo;
                procedimento.NomeProcedimento = procedimentoIntegracao.Procedimento;
                procedimento.SubGrupo = procedimentoIntegracao.SubGrupo;

                listaProcedimento.Add(procedimento);
            }

            repositorioProcedimento.SalvarLista(listaProcedimento);

        }

    }
}
