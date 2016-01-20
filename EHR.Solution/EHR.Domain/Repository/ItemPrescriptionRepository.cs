using EHR.Domain.Entities;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class ItemPrescriptionRepository :  BaseRepository
    {
        [ExceptionLogger]
        public virtual PrescriptionItem GetById(string code, PrescriptionItemType prescriptionItemType)
        {
           
            var criterio = Session.CreateCriteria<PrescriptionItem>();
            criterio.Add(Restrictions.Eq("Id",Convert.ToInt16( code)));
            // criterio.Add(Restrictions.Eq("code", code));
            // criterio.Add(Restrictions.Eq("PrescriptionItemType",(short)prescriptionItemType));

            var itemDePrescricao = criterio.UniqueResult<PrescriptionItem>();

            Assertion.NotNull(itemDePrescricao, "Item de prescrição inválido.").Validate();

            return itemDePrescricao;
        }
    }
}
