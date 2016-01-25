using EHR.Domain.Entities;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class PrescriptionForServiceRepository: BaseRepository
    {
        public virtual IList<PrescriptionForService> GetByPeriod( DateTime? initialPeriod, DateTime? closePeriod)
        {
            var criterio = Session.CreateCriteria<PrescriptionForService>();
            criterio.Add(Restrictions.Between("CreationDate", initialPeriod, closePeriod));

            var prescriptions = criterio.List<PrescriptionForService>();

            Assertion.NotNull(prescriptions, "Lista de prescrições está vazia.").Validate();

            return prescriptions;
        }
    }
}
