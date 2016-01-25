using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Controller
{
    public class PrescriptionsController : EhrController
    {
        private readonly PrescriptionForServiceRepository _prescriptionForServiceRepository;

        public PrescriptionsController()
        {
            _prescriptionForServiceRepository = _prescriptionForServiceRepository ?? (PrescriptionForServiceRepository)FactoryRepository.GetRepository(RepositoryEnum.Prescriptions);
        }

        public virtual IList<PrescriptionForService> getByPeriod(DateTime? initialPeriod, DateTime? closePeriod)
        {
            return _prescriptionForServiceRepository.GetByPeriod(initialPeriod, closePeriod);
        }

        public virtual IList<PrescriptionForService> All()
        {
            return _prescriptionForServiceRepository.All<PrescriptionForService>();
        }
    }
}
