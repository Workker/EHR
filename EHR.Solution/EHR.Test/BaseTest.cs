﻿using System.Collections.Generic;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Mapping;
using EHR.Domain.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System;
using EHR.Domain.Util;

namespace EHR.Test
{
    [TestFixture]
   // [Ignore]
    public class BaseTest
    {
        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Drop(true, true);

            new SchemaExport(config)
                .Create(true, true);
        }

        [Test]
        public void a_create_database_by_model()
        {
            try
            {
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                    c => c.FromAppSetting("connection")).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SummaryMap>()).
                    Mappings(m => m.MergeMappings())
                    .ExposeConfiguration(BuildSchema).BuildSessionFactory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Test]
        public void b_data_initialize()
        {
            var summaries = new Summaries();
            var sumary = new Summary { Cpf = "02338013751" };
            summaries.Save(sumary);

            //  sumary.CreateAllergy("Teste", new List<AllergyType>() { new AllergyType() {Description = AllergyTypeEnum.Angioedema.ToString() } });
            // sumary.CreateDiagnostic(new DiagnosticType() { Description = DiagnosticTypeEnum.Principal.ToString() }, new Cid() { Code = "0001", Description = "Teste" });
            //sumary.CreateProcedure(5, 5, 2013, new Tus() { Code = "001", Description = "Teste" });
        }

        [Test]
        public void create_allergies_types()
        {
            var angioedema = new AllergyType() { Id = (short)AllergyTypeEnum.Angioedema, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Angioedema) };
            var urticaria = new AllergyType() { Id = (short)AllergyTypeEnum.Urticaria, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Urticaria) };
            var choqueAnafilatico = new AllergyType() { Id = (short)AllergyTypeEnum.ChoqueAnafilatico, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.ChoqueAnafilatico) };
            var broncoespasmo = new AllergyType() { Id = (short)AllergyTypeEnum.Broncoespasmo, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Broncoespasmo) };
            var larigoespasmo = new AllergyType() { Id = (short)AllergyTypeEnum.Laringoespasmo, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Laringoespasmo) };
            var outros = new AllergyType() { Id = (short)AllergyTypeEnum.Outros, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Outros) };

            var types = new List<AllergyType> { angioedema, urticaria, choqueAnafilatico, broncoespasmo, larigoespasmo, outros };
            var typesRepository = new Types<AllergyType>();

            typesRepository.SalvarLista<AllergyType>(types);

        }

        [Test]
        public void create_reactions_types()
        {
            var alergicaLeveModeradaGrave = new ReactionType() { Id = (short)ReactionTypeEnum.AlergicaLeveModeradaGrave, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.AlergicaLeveModeradaGrave) };

            var AloimunizacaoEritrocitaria = new ReactionType() { Id = (short)ReactionTypeEnum.AloimunizacaoEritrocitaria, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.AloimunizacaoEritrocitaria) };

            var AloimunizacaoHla = new ReactionType() { Id = (short)ReactionTypeEnum.AloimunizacaoHla, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.AloimunizacaoHla) };

            var EnxertoXHospedeiro = new ReactionType() { Id = (short)ReactionTypeEnum.EnxertoXHospedeiro, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.EnxertoXHospedeiro) };

            var FebrilNaoHemolitica = new ReactionType() { Id = (short)ReactionTypeEnum.FebrilNaoHemolitica, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.FebrilNaoHemolitica) };

            var HemoliticaImune = new ReactionType() { Id = (short)ReactionTypeEnum.HemoliticaImune, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.HemoliticaImune) };

            var Imunomodulacao = new ReactionType() { Id = (short)ReactionTypeEnum.Imunomodulacao, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.Imunomodulacao) };

            var LesaoPulmonarRelacionadaATransfusao = new ReactionType() { Id = (short)ReactionTypeEnum.LesaoPulmonarRelacionadaATransfusao, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.LesaoPulmonarRelacionadaATransfusao) };

            var PurpuraPosTransfusional = new ReactionType() { Id = (short)ReactionTypeEnum.PurpuraPosTransfusional, Description = EnumUtil.GetDescriptionFromEnumValue(ReactionTypeEnum.PurpuraPosTransfusional) };



            var types = new List<ReactionType> 
            {
            alergicaLeveModeradaGrave,
            AloimunizacaoEritrocitaria,
            AloimunizacaoHla,
            EnxertoXHospedeiro,
            FebrilNaoHemolitica,
            HemoliticaImune,
            Imunomodulacao,
            LesaoPulmonarRelacionadaATransfusao,
            PurpuraPosTransfusional
            };

            var reactionTypes = new Types<ReactionType>();

            reactionTypes.SalvarLista<ReactionType>(types);
        }

        [Test]
        public void create_hemotransfusion_types()
        {
            var ConcentradoDeHemacias = new HemotransfusionType() { Id = (short)HemotransfusionTypeEnum.ConcentradoDeHemacias, Description = EnumUtil.GetDescriptionFromEnumValue(HemotransfusionTypeEnum.ConcentradoDeHemacias) };

            var ConcentradoDeNeutrofilos = new HemotransfusionType() { Id = (short)HemotransfusionTypeEnum.ConcentradoDeNeutrofilos, Description = EnumUtil.GetDescriptionFromEnumValue(HemotransfusionTypeEnum.ConcentradoDeNeutrofilos) };

            var ConcentradoDePlaquetas = new HemotransfusionType() { Id = (short)HemotransfusionTypeEnum.ConcentradoDePlaquetas, Description = EnumUtil.GetDescriptionFromEnumValue(HemotransfusionTypeEnum.ConcentradoDePlaquetas) };

            var Criopreciptado = new HemotransfusionType() { Id = (short)HemotransfusionTypeEnum.Criopreciptado, Description = EnumUtil.GetDescriptionFromEnumValue(HemotransfusionTypeEnum.Criopreciptado) };

            var Plasma = new HemotransfusionType() { Id = (short)HemotransfusionTypeEnum.Plasma, Description = EnumUtil.GetDescriptionFromEnumValue(HemotransfusionTypeEnum.Plasma) };


            var types = new List<HemotransfusionType> 
            {
                ConcentradoDeHemacias,
                ConcentradoDeNeutrofilos,
                ConcentradoDePlaquetas,
                Criopreciptado
            };

            var reactionTypes = new Types<HemotransfusionType>();

            reactionTypes.SalvarLista<HemotransfusionType>(types);
        }
    }
}
