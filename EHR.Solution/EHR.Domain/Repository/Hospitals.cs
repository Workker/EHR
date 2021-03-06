﻿using EHR.CoreShared.Entities;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Hospitals : BaseRepository
    {
        [ExceptionLogger]
        public Hospital GetBy(short id)
        {
            Assertion.GreaterThan((int)id, 0, "Nenhum hospital informado.").Validate();

            var hospitals = new Types<Hospital>();

            var hostpital = hospitals.Get(id);

            Assertion.NotNull(hostpital, "Nenhum hospital encontrado, que corresponde a lista informada.").Validate();

            return hostpital;
        }

        [ExceptionLogger]
        public IList<Hospital> GetBy(IList<short> list)
        {
            Assertion.NotNull(list, "Não foram informados hospitais.").Validate();
            Assertion.GreaterThan(list.Count, 0, "Nenhum hospital informado.").Validate();

            var criterio = Session.CreateCriteria<Hospital>();
            criterio.Add(Restrictions.In("Id", list.ToList()));

            var hospitalList = criterio.List<Hospital>();

            Assertion.NotNull(hospitalList, "Nem um hospital encontrado, que corresponde a lista informada.").Validate();
            Assertion.Equals(hospitalList.Count, list.Count, "Quantidade de hospitais informados não batem com a quantidade retornada.").Validate();

            return hospitalList;
        }

        [ExceptionLogger]
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
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        [ExceptionLogger]
        public IList<Hospital> GetAll()
        {
            var hospitalList = base.All<Hospital>();

            Assertion.GreaterThan(hospitalList.Count, 0, "Não foram encontados hospitais cadastrados.").Validate();

            return hospitalList;
        }

        public List<Hospital> GetAllCached()
        {
            return Session.CreateCriteria(typeof(Hospital))
                    .SetCacheable(true)
                    .SetCacheRegion("Hospitals")
                    .SetCacheMode(CacheMode.Normal)
                    .AddOrder(Order.Asc("Id"))
                    .List<Hospital>().ToList();
        }
    }
}
