using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class SpecialtyController : EHRController
    {
        private Types<Specialty> typesRepository = new Types<Specialty>();

        public override List<Specialty> GetSpecialty(string term)
        {
            //todo: do

            var listSpe = typesRepository.All<Specialty>().Where(l => l.Description.Substring(0, term.Length).ToUpper() == term.ToUpper()).ToList();

            Assertion.NotNull(listSpe, "Lista de especialidades nula.").Validate();

            return listSpe;
        }

        public override Specialty GetById(short id)
        {
            Assertion.GreaterThan((int)id, 0, "Especialidade não informada.").Validate();

            var specialty = typesRepository.Get(id);

            Assertion.NotNull(specialty, "Especialidade não encontrada.").Validate();

            return specialty;
        }
    }
}
