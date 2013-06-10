﻿using System.Linq;
using EHR.Domain.Entities;
using NHibernate.Criterion;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Hospitals : BaseRepository
    {
        public IList<Hospital> GetBy(IList<short> list)
        {
            Assertion.NotNull(list, "Não foram informados hospitais.").Validate();
            Assertion.GreaterThan(list.Count, 0, "").Validate();
            var criterio = Session.CreateCriteria<Hospital>();
            criterio.Add(Expression.In("Id", list.ToList()));
            var hospitalList = criterio.List<Hospital>();
            Assertion.NotNull(hospitalList, "Nem um hospital encontrado, que corresponde a lista informada.").Validate();
            Assertion.Equals(hospitalList.Count, list.Count, "Quantidade de hospitais informados não batem com a quantidade retornada.").Validate();
            return hospitalList;
        }

        public virtual void Save(IList<Hospital> hospitals)
        {
            Assertion.GreaterThan(hospitals.Count, 0, "Lista de hospitais vazia.").Validate();
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var hospital in hospitals)
                {
                    Session.SaveOrUpdate(hospital);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}