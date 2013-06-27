using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace EHR.Controller
{
    public class SpecialtyController : EHRController
    {
        private Types<Specialty> typesRepository = new Types<Specialty>();

        public override List<Specialty> GetSpecialty(string term)
        {
            var listSpe = typesRepository.All<Specialty>().Where(l => l.Description.Substring(0, term.Length).ToUpper() == term.ToUpper()).ToList();
            return listSpe;
        }

        public override Specialty GetById(short id)
        {
            return typesRepository.Get(id);
        }
    }
}
