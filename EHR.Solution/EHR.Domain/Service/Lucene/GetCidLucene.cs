﻿using EHR.CoreShared;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetCidLucene
    {
        private GetCidFromLuceneService _getCidFromLuceneService;
        public virtual GetCidFromLuceneService GetCidFromLuceneService
        {
            get { return _getCidFromLuceneService ?? (_getCidFromLuceneService = new GetCidFromLuceneService()); }
            set
            {
                _getCidFromLuceneService = value;
            }
        }

        public List<CidDTO> GetCid(string code)
        {
            return GetCidFromLuceneService.GetCid(code);
        }
    }
}
