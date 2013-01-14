using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Patient
{
    public class Allergy : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual bool HaveAllergies { get; set; }
        public virtual string TheWhich { get; set; }
        public virtual TypeOfAllergy Type { get; set; }
    }

    public enum TypeOfAllergy
    {
        Angioedema = 1,
        Urticaria = 2,
        ChoqueAnafilatico = 3,
        Broncoespasmo = 4,
        Laringoespasmo = 5,
        Outros = 6
    }
}
