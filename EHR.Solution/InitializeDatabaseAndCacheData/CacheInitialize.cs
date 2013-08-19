using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Infrastructure.Service.Cache;

namespace InitializeDatabaseAndCacheData
{
    public class CacheInitialize
    {
        public void insert_hospitals_in_cache()
        {
            var repository = new Hospitals();
            var list = repository.GetAll();

            CacheManagementService.SetIn(1, "Hospitals", list);
        }

        public void insert_allergy_type_in_cache()
        {
            var typesRepository = new Types<AllergyType>();
            var list = typesRepository.All();

            CacheManagementService.SetIn(1, "AllergyTypes", list);
        }

        public void insert_diagnostic_type_in_cache()
        {
            var typesRepository = new Types<DiagnosticType>();
            var list = typesRepository.All();

            CacheManagementService.SetIn(1, "DiagnosticTypes", list);
        }

        public void insert_conditions_Of_The_Patient_At_Discharge()
        {
            var typesRepository = new Types<ConditionOfThePatientAtDischarge>();
            var list = typesRepository.All();

            CacheManagementService.SetIn(1, "ConditionsOfThePatientAtDischarge", list);
        }
    }
}