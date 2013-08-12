﻿using System.Globalization;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Mapping;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
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
            //insert_twenty_accounts();
            //data_initialize_all_sumaries_for_patients();
            insert_specialties();
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
                            (DbEnum)Enum.Parse(typeof(DbEnum), id.ToString(CultureInfo.InvariantCulture))) + "  "
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
            var angioedema = new AllergyType() { Id = 1, Description = "Angioedema" };
            var urticaria = new AllergyType() { Id = 2, Description = "Urticária" };
            var choqueAnafilatico = new AllergyType() { Id = 3, Description = "Choque Anafilático" };
            var broncoespasmo = new AllergyType() { Id = 4, Description = "Broncoespasmo" };
            var larigoespasmo = new AllergyType() { Id = 5, Description = "Laringoespasmo" };
            var outros = new AllergyType() { Id = 6, Description = "Outros" };

            var types = new List<AllergyType> { angioedema, urticaria, choqueAnafilatico, broncoespasmo, larigoespasmo, outros };
            var typesRepository = new Types<AllergyType>();

            typesRepository.SaveList(types);
        }

        public void insert_specialties()
        {
            var specialties = new List<Specialty> { 
            new Specialty { Description = "Acupuntura" },
            new Specialty { Description = "Alergia e Imunologia" },
            new Specialty { Description = "Anestesiologia" },
            new Specialty { Description = "Angiologia" },
            new Specialty { Description = "Cancerologia (oncologia)" },
            new Specialty { Description = "Cardiologia" },
            new Specialty { Description = "Cirurgia Cardiovascular" },
            new Specialty { Description = "Cirurgia da Mão" },
            new Specialty { Description = "Cirurgia neurológica" },
            new Specialty { Description = "Cirurgia do Aparelho Digestório" },
            new Specialty { Description = "Cirurgia Geral" },
            new Specialty { Description = "Cirurgia Pediátrica" },
            new Specialty { Description = "Cirurgia Plástica" },
            new Specialty { Description = "Cirurgia Torácica" },
            new Specialty { Description = "Cirurgia Vascular" },
            new Specialty { Description = "Clínica Médica (Medicina interna)" },
            new Specialty { Description = "Coloproctologia" },
            new Specialty { Description = "Dermatologia" },
            new Specialty { Description = "Endocrinologia e Metabologia" },
            new Specialty { Description = "Endoscopia" },
            new Specialty { Description = "Gastroenterologia" },
            new Specialty { Description = "Genética médica" },
            new Specialty { Description = "Geriatria" },
            new Specialty { Description = "Ginecologia e Obstetrícia" },
            new Specialty { Description = "Hematologia e Hemoterapia" },
            new Specialty { Description = "Homeopatia" },
            new Specialty { Description = "Infectologia" },
            new Specialty { Description = "Mastologia" },
            new Specialty { Description = "Medicina de Família e Comunidade" },
            new Specialty { Description = "Medicina do Trabalho" },
            new Specialty { Description = "Medicina do Tráfego" },
            new Specialty { Description = "Medicina Esportiva" },
            new Specialty { Description = "Medicina Física e Reabilitação" },
            new Specialty { Description = "Medicina Intensiva" },
            new Specialty { Description = "Medicina Legal e Perícia Médica (ou medicina forense)" },
            new Specialty { Description = "Medicina Nuclear" },
            new Specialty { Description = "Medicina Preventiva e Social" },
            new Specialty { Description = "Nefrologia" },
            new Specialty { Description = "Neurocirurgia" },
            new Specialty { Description = "Neurologia" },
            new Specialty { Description = "Nutrologia" },
            new Specialty { Description = "Oftalmologia" },
            new Specialty { Description = "Ortopedia e Traumatologia" },
            new Specialty { Description = "Otorrinolaringologia" },
            new Specialty { Description = "Patologia" },
            new Specialty { Description = "Patologia Clínica/Medicina laboratorial" },
            new Specialty { Description = "Pediatria" },
            new Specialty { Description = "Pneumologia" },
            new Specialty { Description = "Psiquiatria" },
            new Specialty { Description = "Radiologia e Diagnóstico por Imagem" },
            new Specialty { Description = "Radioterapia" },
            new Specialty { Description = "Reumatologia" },
            new Specialty { Description = "Urologia" }
            };
            var typesRepository = new Types<Specialty>();
            typesRepository.SaveList<Specialty>(specialties);

        }

        public void insert_diagnostic_types()
        {
            var associadosEOuOutros = new DiagnosticType() { Id = 1, Description = "Principal" };

            var principal = new DiagnosticType() { Id = 2, Description = "Associados e/ou Outros" };

            var types = new List<DiagnosticType> { associadosEOuOutros, principal };
            var typesRepository = new Types<DiagnosticType>();

            typesRepository.SaveList(types);
        }

        public void insert_reactions_types()
        {
            var alergicaLeveModeradaGrave = new ReactionType() { Id = 1, Description = "Aloimunização Eritrocitária" };

            var aloimunizacaoEritrocitaria = new ReactionType() { Id = 2, Description = "Aloimunização HLA" };

            var aloimunizacaoHla = new ReactionType() { Id = 3, Description = "Imunomodulação" };

            var enxertoXHospedeiro = new ReactionType() { Id = 4, Description = "Lesão pulmonar relacionada a transfusão" };

            var febrilNaoHemolitica = new ReactionType() { Id = 5, Description = "Púrpura pós transfusional" };

            var hemoliticaImune = new ReactionType() { Id = 6, Description = "Alérgica: leve; moderada; grave" };

            var imunomodulacao = new ReactionType() { Id = 7, Description = "Enxerto x Hospedeiro" };

            var lesaoPulmonarRelacionadaATransfusao = new ReactionType() { Id = 8, Description = "Febril não hemolítica" };

            var purpuraPosTransfusional = new ReactionType() { Id = 9, Description = "Hemolítica Imune" };

            var types = new List<ReactionType> 
            {
            alergicaLeveModeradaGrave,
            aloimunizacaoEritrocitaria,
            aloimunizacaoHla,
            enxertoXHospedeiro,
            febrilNaoHemolitica,
            hemoliticaImune,
            imunomodulacao,
            lesaoPulmonarRelacionadaATransfusao,
            purpuraPosTransfusional
            };

            var reactionTypes = new Types<ReactionType>();

            reactionTypes.SaveList(types);
        }

        public void insert_admin_account()
        {
            var account = new Account()
                              {
                                  Administrator = true,
                                  Approved = true,
                                  Birthday = new DateTime(1989, 7, 17),
                                  Crm = "123",
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
            var accountList = new List<Account>();
            for (var i = 0; i <= 20; i++)
            {
                var account = new Account()
                {
                    Administrator = false,
                    Approved = false,
                    Refused = false,
                    Birthday = new DateTime(1989, 7, 17),
                    Crm = "123",
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

        public void insert_hemotransfusion_types()
        {
            var concentradoDeHemacias = new HemotransfusionType() { Id = 1, Description = "Criopreciptado" };

            var concentradoDeNeutrofilos = new HemotransfusionType() { Id = 2, Description = "Concentrado de hemácias" };

            var concentradoDePlaquetas = new HemotransfusionType() { Id = 3, Description = "Concentrado de neutrófilos" };

            var criopreciptado = new HemotransfusionType() { Id = 4, Description = "Concentrado de plaquetas" };

            var plasma = new HemotransfusionType() { Id = 5, Description = "Plasma" };


            var types = new List<HemotransfusionType> 
            {
                concentradoDeHemacias,
                concentradoDeNeutrofilos,
                concentradoDePlaquetas,
                criopreciptado,
                plasma
            };

            var reactionTypes = new Types<HemotransfusionType>();

            reactionTypes.SaveList(types);
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



        //[Test]
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
                                              EntryDateTreatment = treatment.EntryDate,
                                              HighData = new HighData()
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
