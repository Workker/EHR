using EHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Repository;

namespace EHR.Controller
{
    public class ProcedureController : IEHRController
    {
       public  void RemoveProcedure(Summary summary,int id)
       {
           summary.RemoveProcedure(id);
           var summaries = new Summaries();
           summaries.Save(summary);
       }

       public CoreShared.IPatientDTO GetBy(string cpf)
       {
           throw new NotImplementedException();
       }

       public IList<CoreShared.IPatientDTO> GetBy(CoreShared.DbEnum hospital, CoreShared.PatientDTO dto)
       {
           throw new NotImplementedException();
       }

       public IList<CoreShared.IPatientDTO> GetBy(CoreShared.PatientDTO dto, List<string> hospital)
       {
           throw new NotImplementedException();
       }
    }
}
