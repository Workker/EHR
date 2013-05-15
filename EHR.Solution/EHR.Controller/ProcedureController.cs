using EHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Repository;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class ProcedureController : EHRController
    {
        private TusRepository tusRepository;
        public TusRepository TusRepository
        {
            get { return tusRepository ?? (tusRepository = new TusRepository()); }
            set
            {
                tusRepository = value;
            }
        }

        public virtual void RemoveProcedure(Summary summary, int id)
        {
            summary.RemoveProcedure(id);
            Summaries.Save(summary);
        }


        public virtual List<Tus> GetTus()
        {
            return TusRepository.All<Tus>().ToList();
        }


        public virtual void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, Summary summary)
        {
            Assertion.GreaterThan(int.Parse(dob_month), 0, "Mês inválido").Validate();
            Assertion.GreaterThan(int.Parse(dob_day), 0, "Dia inválido").Validate();
            Assertion.GreaterThan(int.Parse(dob_year), 0, "Ano inválido").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(procedureCode), "Codigo do procedimento inválido").Validate();
            Assertion.NotNull(summary, "Não existe nenhum sumário selecionado para inserir o procedimento.");

            var tus = TusRepository.GetByCode(procedureCode);

            summary.CreateProcedure(int.Parse(dob_month), int.Parse(dob_day), int.Parse(dob_year), tus);

            Summaries.Save(summary);
        }
    }
}
