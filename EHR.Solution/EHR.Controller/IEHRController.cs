using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;

namespace EHR.Controller
{
    public interface IEHRController
    {
        IPatientDTO GetBy(string cpf);
        IList<IPatientDTO> GetBy(DbEnum hospital, PatientDTO dto);
        IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital);

    }
}
