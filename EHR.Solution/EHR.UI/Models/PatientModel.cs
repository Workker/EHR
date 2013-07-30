using System;
using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class PatientModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Cpf { get; set; }
        public string Hospital { get; set; }
        public List<TreatmentModel> Treatments { get; set; }

        public int GetAge()
        {
            return DateTime.Today.Year - DateBirthday.Year;
        }
    }
}