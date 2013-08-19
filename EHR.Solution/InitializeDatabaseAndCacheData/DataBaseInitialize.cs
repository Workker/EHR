using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Mapping;
using EHR.Domain.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace InitializeDatabaseAndCacheData
{

    public class DataBaseInitialize
    {
        private void BuildSchema(Configuration config)
        {
            new SchemaExport(config)
                .Drop(true, true);

            new SchemaExport(config)
                .Create(true, true);
        }

        public void create_database_by_model()
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

        public void insert_hospitals()
        {
            var hospitalList = new List<Hospital>
                                   {
                                       new Hospital{Name = "Assunção", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/assuncao.png"},
                                       new Hospital{Name = "Badim", Description = "Hospital", URLImage = "../../Images/Hospitals/badim.png"},
                                       new Hospital{Name = "Bangu", Description = "Hospital", URLImage = "../../Images/Hospitals/bangu.png"},
                                       new Hospital{Name = "Barra D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/barrador.png"},
                                       new Hospital{Name = "Brasil", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/brasil.png"},
                                       new Hospital{Name = "Copa D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/copador.png"},
                                       new Hospital{Name = "Esperança", Description = "Hospital", URLImage = "../../Images/Hospitals/esperanca.png"},
                                       new Hospital{Name = "Israelita Albert Sabim", Description = "Hospital", URLImage = "../../Images/Hospitals/israelitaalbertsabim.png"},
                                       new Hospital{Name = "Joari", Description = "Hospital", URLImage = "../../Images/Hospitals/joari.png"},
                                       new Hospital{Name = "Niterói D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/niteroidor.png"},
                                       new Hospital{Name = "Norte D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/nortedor.png"},
                                       new Hospital{Name = "Prontolinda", Description = "Hospital", URLImage = "../../Images/Hospitals/prontolinda.png"},
                                       new Hospital{Name = "Quinta D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/quintador.png"},
                                       new Hospital{Name = "Rede D'Or São Luiz", Description = "Hospital", URLImage = "../../Images/Hospitals/saoluiz.png"},
                                       new Hospital{Name = "Rio de Janeiro", Description = "Hospital", URLImage = "../../Images/Hospitals/riodejaneiro.png"},
                                       new Hospital{Name = "Rios D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/riosdor.png"},
                                       new Hospital{Name = "São Marcos", Description = "Hospital", URLImage = "../../Images/Hospitals/saomarcos.png"}
                                   };
            var repository = new Hospitals();
            repository.Save(hospitalList);
        }

        public void insert_allergies_types()
        {
            var types = new List<AllergyType> 
                            { 
                                new AllergyType {Id = 1, Description = "Angioedema" },
                                new AllergyType {Id = 2, Description = "Urticária" }, 
                                new AllergyType {Id = 3, Description = "Choque Anafilático" }, 
                                new AllergyType {Id = 4, Description = "Broncoespasmo" }, 
                                new AllergyType {Id = 5, Description = "Laringoespasmo" }, 
                                new AllergyType {Id = 6, Description = "Outros" } 
                            };

            var typesRepository = new Types<AllergyType>();

            typesRepository.SaveList(types);
        }

        public void insert_diagnostic_types()
        {
            var types = new List<DiagnosticType>
                            {
                                new DiagnosticType() {Id = 1, Description = "Principal" }, 
                                new DiagnosticType() {Id = 2, Description = "Associados e/ou Outros" }
                            };

            var typesRepository = new Types<DiagnosticType>();

            typesRepository.SaveList(types);
        }

        public void insert_Conditions_Of_The_Patient_At_Discharge()
        {
            var types = new List<ConditionOfThePatientAtDischarge>
                            {
                                new ConditionOfThePatientAtDischarge() {Id = 1, Description = "Curado" }, 
                                new ConditionOfThePatientAtDischarge() {Id = 2, Description = "Melhorado" },
                                new ConditionOfThePatientAtDischarge() {Id = 3, Description = "Desistência de tratamento" },
                            };

            var typesRepository = new Types<ConditionOfThePatientAtDischarge>();

            typesRepository.SaveList(types);
        }

        public void insert_admin_account()
        {
            var account = new Account()
            {
                Administrator = true,
                Approved = true,
            };

            account.ToEnterCRM("123");
            account.ToEnterPassword("123");
            account.ToEnterFirstName("Thiago");
            account.ToEnterLastName("Oliveira");
            account.ToEnterGender(GenderEnum.Male);
            account.ToEnterEmail("thiago@workker.com.br");
            account.ToEnterBirthday(new DateTime(1989, 7, 17));

            var hospitals = new Hospitals().All<Hospital>();

            foreach (var hospital in hospitals)
            {
                account.AddHospital(hospital);
            }

            var accounts = new Accounts();

            accounts.Save(account);
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
                };

                account.ToEnterCRM("123");
                account.ToEnterFirstName("123");
                account.ToEnterLastName("Oliveira");
                account.ToEnterGender(GenderEnum.Male);
                account.ToEnterEmail(i + "@workker.com.br");
                account.ToEnterBirthday(new DateTime(1989, 7, 17));

                var hospitals = new Hospitals().All<Hospital>();

                foreach (var hospital in hospitals)
                {
                    account.AddHospital(hospital);
                }

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


        //public void b_data_initialize()
        //{
        //    insert_hospitals_in_database();
        //    insert_allergies_types();
        //    insert_diagnostic_types();
        //    insert_reactions_types();
        //    insert_hemotransfusion_types();
        //    insert_admin_account();
        //    //insert_twenty_accounts();
        //    //data_initialize_all_sumaries_for_patients();
        //    insert_specialties();
        //    //var summaries = new Summaries();
        //    //var sumary = new Summary { Cpf = "02338013751" };
        //    //summaries.Save(sumary);

        //    //  sumary.CreateAllergy("Teste", new List<AllergyType>() { new AllergyType() {Description = AllergyTypeEnum.Angioedema.ToString() } });
        //    // sumary.CreateDiagnostic(new DiagnosticType() { Description = DiagnosticTypeEnum.Principal.ToString() }, new Cid() { Code = "0001", Description = "Teste" });
        //    //sumary.CreateProcedure(5, 5, 2013, new Tus() { Code = "001", Description = "Teste" });
        //}
    }
}