using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public enum StatusPrescriptionEnum : short
    {
        EmAberto =1,
        Iniciado,
        Finalizado
    }

    public class PrescriptionForService
    {
        public PrescriptionForService()
        {
            Status = StatusPrescriptionEnum.EmAberto; 
        }

        public virtual int Id { get; set; }
        public virtual int ProfessionalId { get; set; }
        public virtual TypePrescription TypePrescription { get; set; }
        public virtual PrescriptionItem PrescriptionItem { get; set; }
        public virtual string Presentation { get; set; }
        public virtual PresentationTypeEnum PresentationType { get; set; }
        public virtual string Dose { get; set; }
        public virtual DosageEnum Dosage { get; set; }
        public virtual WayEnum Way { get; set; }
        public virtual string Place { get; set; }
        public virtual FrequencyEnum Frequency { get; set; }
        public virtual FrequencyCaseEnum FrequencyCase { get; set; }
        public virtual int Duration { get; set; }
        public virtual string Observation { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool ForService { get; set; }
        public virtual bool Revoked
        {
            get; set;
        }
        public virtual ProfessionalRegistration ProfessionalRegistration { get; set; }
        public virtual string ProfessionalName { get; set; }
        public virtual string PatientName { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual DateTime? OpenDate { get; set; }
        public virtual DateTime? CloseDate { get; set; }
        public virtual StatusPrescriptionEnum Status { get; set; }
        //TODO: Alterar para Apontar para acucount, tanto de medico quanto de Usuário

    }
}
