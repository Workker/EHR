using EHR.Domain.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Mapping
{
    public class PrescriptionForServiceMap : ClassMap<PrescriptionForService>
    {
        public PrescriptionForServiceMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.TypePrescription).CustomType<short>(); ;
            References(p => p.PrescriptionItem);
            Map(p => p.Presentation);
            Map(p => p.PresentationType).CustomType<short>();
            Map(p => p.Dose);
            Map(p => p.Dosage).CustomType<short>(); 
            Map(p => p.Way).CustomType<short>(); 
            Map(p => p.Place);
            Map(p => p.Frequency).CustomType<short>(); 
            Map(p => p.FrequencyCase).CustomType<short>(); 
            Map(p => p.Duration);
            Map(p => p.Observation);
            Map(p => p.Quantity);
            References(p => p.ProfessionalRegistration);
            Map(p => p.ForService);
            Map(p => p.Revoked);
            Map(p => p.ProfessionalName);
            Map(p => p.PatientName);
            Map(p => p.CloseDate);
            Map(p => p.OpenDate);
            Map(p => p.CreationDate);
            Map(p => p.Status).CustomType<short>();
            Map(p => p.ProfessionalId);
        }
    }
}
