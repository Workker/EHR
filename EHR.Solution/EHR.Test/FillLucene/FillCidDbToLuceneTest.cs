using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Test.Migracao;
using EHRIntegracao.Domain.Services.SaveLucene;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EHR.Test.FillLucene
{
    [TestFixture]
    public class FillCidDbToLuceneTest
    {
        [Test]
        [Ignore]
        public void Fill()
        {
            var repository = new Cids(Conexao.CreateSessionFactory().OpenSession());
            var cids = repository.All<Cid>().ToList();
            var cidsDTO = Convert(cids);
            var service = new SaveCidInLuceneService();
            service.Save(cidsDTO);
        }

        private List<CidDTO> Convert(List<Cid> defs)
        {
            var cidsDTO = new List<CidDTO>();

            foreach (var t in defs)
            {
                cidsDTO.Add(new CidDTO {Code = t.Code, Description = t.Description, Id = t.Id });
            }

            return cidsDTO;
        }
    }
}
