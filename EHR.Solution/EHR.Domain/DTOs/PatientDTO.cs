using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services.DTO;

namespace EHR.Domain.DTOs
{
    public class PatientDTO : IPatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public string CPF { get; set; }
        public string Identity { get; set; }
        public DbEnum Hospital { get; set; }
    }
}
