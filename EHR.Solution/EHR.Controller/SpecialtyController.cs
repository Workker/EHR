using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class SpecialtyController : EhrController
    {
        private readonly Types<Specialty> _typesRepository = new Types<Specialty>();

        [ExceptionLogger]
        public override List<Specialty> GetSpecialty(string term)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(term), "Termo não informado.").Validate();

            var listSpe =
                _typesRepository.All<Specialty>().Where(
                    l => l.Description.Substring(0, term.Length).ToUpper() == term.ToUpper()).ToList();

            Assertion.NotNull(listSpe, "Lista de especialidades nula.").Validate();

            return listSpe;
        }

        [ExceptionLogger]
        public override Specialty GetById(short id)
        {
            Assertion.GreaterThan((int)id, 0, "Especialidade não informada.").Validate();

            var specialty = _typesRepository.Get(id);

            Assertion.NotNull(specialty, "Especialidade não encontrada.").Validate();

            return specialty;
        }
    }
}
