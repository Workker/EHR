using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Mapping;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using EHR.Domain.Util;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EHR.Test
{
    [TestFixture]
    [Ignore]
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
            insert_hospitals_in_database();
            insert_allergies_types();
            insert_diagnostic_types();
            insert_reactions_types();
            insert_hemotransfusion_types();
            insert_admin_account();
            insert_twenty_accounts();
            data_initialize_all_sumaries_for_patients();
            //var summaries = new Summaries();
            //var sumary = new Summary { Cpf = "02338013751" };
            //summaries.Save(sumary);

            //  sumary.CreateAllergy("Teste", new List<AllergyType>() { new AllergyType() {Description = AllergyTypeEnum.Angioedema.ToString() } });
            // sumary.CreateDiagnostic(new DiagnosticType() { Description = DiagnosticTypeEnum.Principal.ToString() }, new Cid() { Code = "0001", Description = "Teste" });
            //sumary.CreateProcedure(5, 5, 2013, new Tus() { Code = "001", Description = "Teste" });
        }

        public void insert_hospitals_in_database()
        {
            var hospitals = new Hospitals();
            var hospitalList = new List<Hospital>();

            foreach (var id in Enum.GetValues(typeof(DbEnum)).Cast<short>().ToList())
            {
                if (id != (short)DbEnum.sumario)
                {
                    var hospital = new Hospital()
                    {
                        Description = "Hospital",
                        Name = EnumUtil.GetDescriptionFromEnumValue(
                            (DbEnum)Enum.Parse(typeof(DbEnum), id.ToString())) + "  "
                    };

                    if (id == (short)DbEnum.QuintaDor)
                    {
                        hospital.URLImage = "../../Images/Hospitals/quintador.png";
                    }
                    else if (id == (short)DbEnum.Pronto)
                    {
                        hospital.URLImage = "../../Images/Hospitals/prontolinda.png";
                    }
                    else if (id == (short)DbEnum.Rios)
                    {
                        hospital.URLImage = "../../Images/Hospitals/riosdor.png";
                    }
                    else if (id == (short)DbEnum.Esperanca)
                    {
                        hospital.URLImage = "../../Images/Hospitals/esperanca.png";
                    }

                    hospitalList.Add(hospital);
                }
            }
            hospitals.Save(hospitalList);
        }

        public void insert_allergies_types()
        {
            var angioedema = new AllergyType() { Id = (short)AllergyTypeEnum.Angioedema, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Angioedema) };
            var urticaria = new AllergyType() { Id = (short)AllergyTypeEnum.Urticaria, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Urticaria) };
            var choqueAnafilatico = new AllergyType() { Id = (short)AllergyTypeEnum.ChoqueAnafilatico, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.ChoqueAnafilatico) };
            var broncoespasmo = new AllergyType() { Id = (short)AllergyTypeEnum.Broncoespasmo, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Broncoespasmo) };
            var larigoespasmo = new AllergyType() { Id = (short)AllergyTypeEnum.Laringoespasmo, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Laringoespasmo) };
            var outros = new AllergyType() { Id = (short)AllergyTypeEnum.Outros, Description = EnumUtil.GetDescriptionFromEnumValue(AllergyTypeEnum.Outros) };

            var types = new List<AllergyType> { angioedema, urticaria, choqueAnafilatico, broncoespasmo, larigoespasmo, outros };
            var typesRepository = new Types<AllergyType>();

            typesRepository.SaveList<AllergyType>(types);

        }

        public void insert_diagnostic_types()
        {
            var AssociadosEOuOutros = new DiagnosticType() { Id = (short)DiagnosticTypeEnum.AssociadosEOuOutros, Description = EnumUtil.GetDescriptionFromEnumValue(DiagnosticTypeEnum.AssociadosEOuOutros) };

            var Principal = new DiagnosticType() { Id = (short)DiagnosticTypeEnum.Principal, Description = EnumUtil.GetDescriptionFromEnumValue(DiagnosticTypeEnum.Principal) };

            var types = new List<DiagnosticType> { AssociadosEOuOutros, Principal };
            var typesRepository = new Types<DiagnosticType>();

            typesRepository.SaveList<DiagnosticType>(types);

        }

        public void insert_reactions_types()
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

            reactionTypes.SaveList<ReactionType>(types);
        }

        public void insert_admin_account()
        {
            var account = new Account()
                              {
                                  Administrator = true,
                                  Approved = true,
                                  Birthday = new DateTime(1989, 7, 17),
                                  CRM = "123",
                                  Email = "thiago@workker.com.br",
                                  FirstName = "Thiago",
                                  LastName = "Oliveira",
                                  Gender = GenderEnum.Male,
                                  Password = "123"
                              };

            account.EncryptPassword();

            account.Hospitals = new Hospitals().All<Hospital>();

            var accounts = new Accounts();
            accounts.Save(account);
        }

        public void insert_twenty_accounts()
        {
            List<Account> accountList = new List<Account>();
            for (int i = 0; i < 20; i++)
            {
                var account = new Account()
                {
                    Administrator = false,
                    Approved = false,
                    Refused = false,
                    Birthday = new DateTime(1989, 7, 17),
                    CRM = "123",
                    Email = i + "@workker.com.br",
                    FirstName = "Thiago",
                    LastName = "Oliveira",
                    Gender = GenderEnum.Male,
                    Password = "123"
                };

                account.EncryptPassword();

                account.Hospitals = new Hospitals().All<Hospital>();

                accountList.Add(account);
            }

            var accounts = new Accounts();
            accounts.SaveList(accountList);
        }

        //[Test]
        //public void insert_reason_of_admission()
        //{
        //    var listOfReasons = new List<ReasonOfAdmission>();

        //    foreach (var id in Enum.GetValues(typeof(ReasonOfAdmissionEnum)).Cast<short>().ToList())
        //    {
        //        var resonOfAdmission = new ReasonOfAdmission()
        //        {
        //            Id = id,
        //            Description = EnumUtil.GetDescriptionFromEnumValue(
        //                (ReasonOfAdmissionEnum)Enum.Parse(typeof(ReasonOfAdmissionEnum), id.ToString())) + "  "
        //        };

        //        listOfReasons.Add(resonOfAdmission);
        //    }

        //    var typesRepository = new Types<ReasonOfAdmission>();

        //    typesRepository.SaveList<ReasonOfAdmission>(listOfReasons);
        //}

        public void insert_hemotransfusion_types()
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
                Criopreciptado,
                Plasma
            };

            var reactionTypes = new Types<HemotransfusionType>();

            reactionTypes.SaveList<HemotransfusionType>(types);
        }

        [Test]
        public void data_initialize_all_sumaries_for_patients()
        {
            GetPatientByHospitalService service = new GetPatientByHospitalService();
            var patients = service.MockPatients("Ana");

            var doctor = new Accounts().GetBy(1);

            var sumariesList = new List<Summary>();
            foreach (var patient in patients)
            {
                if (patient.CPF != "02338013751")
                {
                    foreach (var treatment in patient.Treatments)
                    {
                        var summary = new Summary
                                          {
                                              Cpf = patient.CPF,
                                              Account = doctor,
                                              Date = new DateTime(2013, 6, 10),
                                              Hospital = treatment.Hospital,
                                              CodeMedicalRecord = treatment.Id,
                                              EntryDateTreatment = treatment.EntryDate
                                          };
                        sumariesList.Add(summary);    
                    }
                    
                }
            }

            var summaries = new Summaries();
            summaries.SaveList<Summary>(sumariesList);

            //  sumary.CreateAllergy("Teste", new List<AllergyType>() { new AllergyType() {Description = AllergyTypeEnum.Angioedema.ToString() } });
            // sumary.CreateDiagnostic(new DiagnosticType() { Description = DiagnosticTypeEnum.Principal.ToString() }, new Cid() { Code = "0001", Description = "Teste" });
            //sumary.CreateProcedure(5, 5, 2013, new Tus() { Code = "001", Description = "Teste" });
        }
    }
}
