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
    public class FillTusDbToLuceneTest
    {
        [Test]
        [Ignore]
        public void Fill()
        {
            var repository = new TusRepository(Conexao.CreateSessionFactory().OpenSession());
            var tus = repository.All<Tus>().ToList();
            var TusDTO = Convert(tus);
            SaveTusInLuceneService service = new SaveTusInLuceneService();
            service.Save(TusDTO);
        }

        private List<TusDTO> Convert(List<Tus> tus)
        {
            var tusDTO = new List<TusDTO>();

            foreach (var t in tus)
            {
                tusDTO.Add(new TusDTO { Code = t.Code, Description = t.Description, Id = t.Id });
            }

            return tusDTO;
        }
    }
}
