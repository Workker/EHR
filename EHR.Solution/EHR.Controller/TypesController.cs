using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class TypesController : EhrController
    {
        [ExceptionLogger]
        public override IList<ReasonOfAdmission> GetReasonsOfAdmission()
        {
            var repository = new Types<ReasonOfAdmission>();
            var reasons = repository.All();

            Assertion.GreaterThan(reasons.Count, 0, "Nenhum motivo de admissao cadastrado.").Validate();

            return reasons;
        }
    }
}
