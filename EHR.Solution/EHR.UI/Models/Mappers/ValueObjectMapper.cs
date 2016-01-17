using AutoMapper;
using EHR.CoreShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models.Mappers
{
    public static class ValueObjectMapper
    {
        public static List<ValueObjectModel> MapValueObjectsModelsFrom(IList<ValueObject> valuesObjects)
        {
            var defModels = new List<ValueObjectModel>();
            foreach (var valueObject in valuesObjects)
            {
                var valueObjectModel = MapValueObjectModelFrom(valueObject);
                valueObjectModel.Code = valueObject.Id;
                defModels.Add(valueObjectModel);
            }
            return defModels;
        }

        public static ValueObjectModel MapValueObjectModelFrom(ValueObject def)
        {
            Mapper.CreateMap<ValueObject, ValueObjectModel>();
            var defModel = Mapper.Map<ValueObject, ValueObjectModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }
    }
}