using EHR.CoreShared;
using EHR.Domain.Repository;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class HospitalController : EhrController
    {
        public override IList<Hospital> GetAllHospitals()
        {
            var hospitals = (Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals);

            var hospitalList = hospitals.GetAll();

            Assertion.GreaterThan(hospitalList.Count, 0, "Não foram encontrados hospitais cadastrados.").Validate();

            return hospitalList;
        }

    }
}
