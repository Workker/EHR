using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    [Serializable]
    public class Patient : IAggregateRoot<int>, IPatientDTO
    {
        #region Properties

        string IPatientDTO.Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string CPF { get; set; }
        public string Identity { get; set; }
        public DbEnum Hospital { get; set; }
        List<RecordDTO> IPatientDTO.Records { get; set; }
        public List<ITreatmentDTO> Treatments { get; set; }
        public List<string> Records { get; set; }
        public virtual int Id { get; set; }


        #endregion


        public string GetCPF()
        {
            throw new NotImplementedException();
        }

        public void AddRecord(RecordDTO record)
        {
            throw new NotImplementedException();
        }

        public void AddRecord(string record)
        {
            throw new NotImplementedException();
        }

        public void AddTreatments(IList<ITreatmentDTO> treatments)
        {
            throw new NotImplementedException();
        }

    }
}
