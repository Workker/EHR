using System.Collections.Generic;
using System.Linq;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Migration.Migracao;
using EHRIntegracao.Domain.Services.SaveLucene;
using NUnit.Framework;

namespace EHR.Migration.FillLucene
{
    [TestFixture]
    [Ignore]
    public class FillDefDbToLuceneTest
    {
        [Test]
        public void Fill()
        {
            var repository = new Defs(Conexao.CreateSessionFactory().OpenSession());
            var defs = repository.All<Def>().ToList();
            var defsDTO = Convert(defs);
            var service = new SaveDefInLuceneService();
            service.Save(defsDTO);
        }

        private List<DefDTO> Convert(List<Def> defs)
        {
            var defsDTO = new List<DefDTO>();

            foreach (var t in defs)
            {
                defsDTO.Add(new DefDTO { Description = t.Description, Id = t.Id });
            }

            return defsDTO;
        }
    }
}
