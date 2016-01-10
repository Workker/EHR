using System.Configuration;
using EHR.CoreShared.Entities;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetDefLuceneService
    {
        private GetDefFromLuceneService _getDefFromLuceneService;
        public virtual GetDefFromLuceneService GetDefFromLuceneService
        {
            get { return _getDefFromLuceneService ?? (_getDefFromLuceneService = new GetDefFromLuceneService(ConfigurationManager.AppSettings["DEFIndexPath"])); }
            set
            {
                _getDefFromLuceneService = value;
            }
        }

        public List<DEF> GetDef(string code)
        {
            return GetDefFromLuceneService.GetDef(code);
        }
    }
}