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
using Environment = System.Environment;

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
                // Oracle Configuration

                //     Fluently.Configure()
                //.Database(OracleClientConfiguration.Oracle10.ConnectionString(c => c
                //    .FromAppSetting("connection"))
                //    .ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SummaryMap>()).Mappings(m => m.MergeMappings())
                //     .ExposeConfiguration(BuildSchema).BuildSessionFactory();

                // SQL Server Configuration

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

        public void insert_states()
        {
            var states = new List<State>
                             {
                                 new State {Id = 1,Description = "Acre", Acronym = "AC"},
                                 new State {Id = 2,Description = "Alagoas", Acronym = "AL"},
                                 new State {Id = 3,Description = "Amapá", Acronym = "AP"},
                                 new State {Id = 4,Description = "Amazonas", Acronym = "AM"},
                                 new State {Id = 5,Description = "Bahia", Acronym = "BA"},
                                 new State {Id = 6,Description = "Ceará", Acronym = "CE"},
                                 new State {Id = 7,Description = "Distrito Federal", Acronym = "DF"},
                                 new State {Id = 8,Description = "Espírito Santo", Acronym = "ES"},
                                 new State {Id = 9,Description = "Goiás", Acronym = "GO"},
                                 new State {Id = 10,Description = "Maranhão", Acronym = "MA"},
                                 new State {Id = 11,Description = "Mato Grosso", Acronym = "MT"},
                                 new State {Id = 12,Description = "Mato Grosso do Sul", Acronym = "MS"},
                                 new State {Id = 13,Description = "Minas Gerais", Acronym = "MG"},
                                 new State {Id = 14,Description = "Pará", Acronym = "PA"},
                                 new State {Id = 15,Description = "Paraíba", Acronym = "PB"},
                                 new State {Id = 16,Description = "Paraná", Acronym = "PR"},
                                 new State {Id = 17,Description = "Pernambuco", Acronym = "PE"},
                                 new State {Id = 18,Description = "Piauí", Acronym = "PI"},
                                 new State {Id = 19,Description = "Rio de Janeiro", Acronym = "RJ"},
                                 new State {Id = 20,Description = "Rio Grande do Norte", Acronym = "RN"},
                                 new State {Id = 21,Description = "Rio Grande do Sul", Acronym = "RS"},
                                 new State {Id = 22,Description = "Rondônia", Acronym = "RO"},
                                 new State {Id = 23,Description = "Roraima", Acronym = "RR"},
                                 new State {Id = 24,Description = "Santa Catarina", Acronym = "SC"},
                                 new State {Id = 25,Description = "São Paulo", Acronym = "SP"},
                                 new State {Id = 26,Description = "Sergipe", Acronym = "SE"},
                                 new State {Id = 27,Description = "Tocantins", Acronym = "TO"},
                             };

            var reactionTypes = new Types<State>();

            reactionTypes.SaveList(states);

        }

        public void insert_hospitals_SQL()
        {

            var state = new Types<State>();

            var hospitalList = new List<Hospital>
                                   {
                                       new Hospital{Name = "Assunção", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/assuncao.png", State = state.Get(25),Key = DbEnum.Assuncao},
                                       new Hospital{Name =  "Badim", Description = "Hospital", URLImage = "../../Images/Hospitals/badim.png", State = state.Get(19),Key = DbEnum.Badim},
                                       new Hospital{Name = "Bangu", Description = "Hospital", URLImage = "../../Images/Hospitals/bangu.png", State = state.Get(19),Key = DbEnum.Bangu},
                                       new Hospital{Name = "Barra D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/barrador.png", State = state.Get(19),Key = DbEnum.BarraDor},
                                       new Hospital{Name = "Brasil", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/brasil.png", State = state.Get(25),Key = DbEnum.Brasil},
                                       new Hospital{Name = "Copa D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/copador.png", State = state.Get(19),Key = DbEnum.CopaDor},
                                       new Hospital{Name = "Esperança", Description = "Hospital", URLImage = "../../Images/Hospitals/esperanca.png", State = state.Get(17),Key = DbEnum.Esperanca},
                                       new Hospital{Name = "Israelita Albert Sabim", Description = "Hospital", URLImage = "../../Images/Hospitals/israelitaalbertsabim.png", State = state.Get(19),Key = DbEnum.IsraelitaAlbertSabim},
                                       new Hospital{Name = "Joari", Description = "Hospital", URLImage = "../../Images/Hospitals/joari.png", State = state.Get(19),Key = DbEnum.Joari},
                                       new Hospital{Name = "Niterói D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/niteroidor.png", State = state.Get(19),Key = DbEnum.NiteroiDOr},
                                       new Hospital{Name = "Norte D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/nortedor.png", State = state.Get(19),Key = DbEnum.Norte},
                                       new Hospital{Name = "Prontolinda", Description = "Hospital", URLImage = "../../Images/Hospitals/prontolinda.png", State = state.Get(17),Key = DbEnum.Pronto},
                                       new Hospital{Name = "Quinta D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/quintador.png", State = state.Get(19),Key = DbEnum.QuintaDor},
                                       new Hospital{Name = "Rede D'Or São Luiz", Description = "Hospital", URLImage = "../../Images/Hospitals/saoluiz.png", State = state.Get(25),Key = DbEnum.RedeDOrSaoLuiz},
                                       new Hospital{Name = "Rio de Janeiro", Description = "Hospital", URLImage = "../../Images/Hospitals/riodejaneiro.png", State = state.Get(19),Key = DbEnum.RioDeJaneiro},
                                       new Hospital{Name = "Rios D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/riosdor.png", State = state.Get(19),Key = DbEnum.RiosDor},
                                       new Hospital{Name = "São Marcos", Description = "Hospital", URLImage = "../../Images/Hospitals/saomarcos.png", State = state.Get(17),Key = DbEnum.SaoMarcos}
                                   };
            var repository = new Hospitals();
            repository.Save(hospitalList);
        }


        public void insert_hospitals_Oracle()
        {

            var state = new Types<State>();

            var hospitalList = new List<Hospital>
                                   {
                                       new Hospital{Id = 1,Name = "Assunção", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/assuncao.png", State = state.Get(25)},
                                       new Hospital{Id = 2,Name = "Badim", Description = "Hospital", URLImage = "../../Images/Hospitals/badim.png", State = state.Get(19)},
                                       new Hospital{Id = 3,Name = "Bangu", Description = "Hospital", URLImage = "../../Images/Hospitals/bangu.png", State = state.Get(19)},
                                       new Hospital{Id = 4,Name = "Barra D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/barrador.png", State = state.Get(19)},
                                       new Hospital{Id = 5,Name = "Brasil", Description = "Hospital e Maternidade", URLImage = "../../Images/Hospitals/brasil.png", State = state.Get(25)},
                                       new Hospital{Id = 6,Name = "Copa D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/copador.png", State = state.Get(19)},
                                       new Hospital{Id = 7,Name = "Esperança", Description = "Hospital", URLImage = "../../Images/Hospitals/esperanca.png", State = state.Get(17)},
                                       new Hospital{Id = 8,Name = "Israelita Albert Sabim", Description = "Hospital", URLImage = "../../Images/Hospitals/israelitaalbertsabim.png", State = state.Get(19)},
                                       new Hospital{Id = 9,Name = "Joari", Description = "Hospital", URLImage = "../../Images/Hospitals/joari.png", State = state.Get(19)},
                                       new Hospital{Id = 10,Name = "Niterói D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/niteroidor.png", State = state.Get(19)},
                                       new Hospital{Id = 11,Name = "Norte D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/nortedor.png", State = state.Get(19)},
                                       new Hospital{Id = 12,Name = "Prontolinda", Description = "Hospital", URLImage = "../../Images/Hospitals/prontolinda.png", State = state.Get(17)},
                                       new Hospital{Id = 13,Name = "Quinta D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/quintador.png", State = state.Get(19)},
                                       new Hospital{Id = 14,Name = "Rede D'Or São Luiz", Description = "Hospital", URLImage = "../../Images/Hospitals/saoluiz.png", State = state.Get(25)},
                                       new Hospital{Id = 15,Name = "Rio de Janeiro", Description = "Hospital", URLImage = "../../Images/Hospitals/riodejaneiro.png", State = state.Get(19)},
                                       new Hospital{Id = 16,Name = "Rios D'Or", Description = "Hospital", URLImage = "../../Images/Hospitals/riosdor.png", State = state.Get(19)},
                                       new Hospital{Id = 17,Name = "São Marcos", Description = "Hospital", URLImage = "../../Images/Hospitals/saomarcos.png", State = state.Get(17)}
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
            var types = new List<ConditionAtDischarge>
                            {
                                new ConditionAtDischarge() {Id = 1, Description = "Curado" }, 
                                new ConditionAtDischarge() {Id = 2, Description = "Melhorado" },
                                new ConditionAtDischarge() {Id = 3, Description = "Desistência de tratamento" },
                            };

            var typesRepository = new Types<ConditionAtDischarge>();

            typesRepository.SaveList(types);
        }

        public void insert_hemotransfusion_types()
        {
            var types = new List<HemotransfusionType> 
            {
                new HemotransfusionType {Id=1, Description = "Criopreciptado" },
                new HemotransfusionType {Id=2, Description = "Concentrado de hemácias" },
                new HemotransfusionType {Id=3, Description = "Concentrado de neutrófilos" },
                new HemotransfusionType {Id=4, Description = "Concentrado de plaquetas" },
                new HemotransfusionType {Id=5, Description = "Plasma" }
            };

            var reactionTypes = new Types<HemotransfusionType>();

            reactionTypes.SaveList(types);
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

        public void insert_admin_account()
        {
            var hospitals = new Hospitals().All<Hospital>();

            foreach (var hospital in hospitals)
            {
                var account = new Account(true);

                account.AddProfessionalRegistration(new ProfessionalRegistration
                        {
                            Number = "123" + hospital.Id,
                            State = hospital.State,
                            Type = ProfessionalRegistrationTypeEnum.CRM
                        });
                account.ToEnterPassword("123");
                account.ToEnterFirstName(hospital.Name);
                account.ToEnterLastName("Admin");
                account.ToEnterGender(GenderEnum.Male);
                account.ToEnterEmail(RemoveSpecialCharacters(hospital.Name) + "@workker.com.br");
                account.ToEnterBirthday(new DateTime(1989, 7, 17));

                account.AddHospital(hospital);


                var accounts = new Accounts();

                accounts.Save(account);
            }
        }

        public void insert_Historical_Action_Types()
        {
            var list = new List<HistoricalActionType>
                           {
                               new HistoricalActionType{Id = 1,Description = "incluiu"},
                               new HistoricalActionType{Id = 2,Description = "alterou"},
                               new HistoricalActionType{Id = 3,Description = "excluiu"},
                               new HistoricalActionType{Id = 4,Description = "viu"},
                               new HistoricalActionType{Id = 5,Description = "fechou"},
                               new HistoricalActionType{Id = 6,Description = "reabriu"},
                               new HistoricalActionType{Id = 7,Description = "imprimiu"},
                           };
            var types = new Types<HistoricalActionType>();

            types.SaveList(list);
        }



        //public void insert_twenty_accounts()
        //{
        //    //var accountList = new List<Account>();
        //    //for (var i = 0; i <= 20; i++)
        //    //{
        //    //    var account = new Account()
        //    //    {
        //    //        Administrator = false,
        //    //        Approved = false,
        //    //        Refused = false,
        //    //    };

        //    //    account.ToEnterCRM("123");
        //    //    account.ToEnterFirstName("123");
        //    //    account.ToEnterLastName("Oliveira");
        //    //    account.ToEnterGender(GenderEnum.Male);
        //    //    account.ToEnterEmail(i + "@workker.com.br");
        //    //    account.ToEnterBirthday(new DateTime(1989, 7, 17));

        //    //    var hospitals = new Hospitals().All<Hospital>();

        //    //    foreach (var hospital in hospitals)
        //    //    {
        //    //        account.AddHospital(hospital);
        //    //    }

        //    //    accountList.Add(account);
        //    //}

        //    //var accounts = new Accounts();
        //    //accounts.SaveList(accountList);
        //}



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

        private string RemoveSpecialCharacters(string palavra)
        {
            string palavraSemAcento = null;
            string caracterComAcento = "áàãâäéèêëíìîïóòõôöúùûüçáàãâÄéèêëíìîïóòõÖôúùûÜç,. ?&:/!;ºª%‘’()\"”“";
            string caracterSemAcento = "aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUC--------------------";

            if (!String.IsNullOrEmpty(palavra))
            {
                for (int i = 0; i < palavra.Length; i++)
                {
                    if (caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1))) >= 0)
                    {
                        int car = caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1)));
                        palavraSemAcento += caracterSemAcento.Substring(car, 1);
                    }
                    else
                    {
                        palavraSemAcento += palavra.Substring(i, 1);
                    }
                }

                string[] cEspeciais = { "#39", "---", "--", "'", "#", "\r\n", "\n", "\r" };

                for (int q = 0; q < cEspeciais.Length; q++)
                {
                    palavraSemAcento = palavraSemAcento.Replace(cEspeciais[q], "-");
                }

                for (int x = (cEspeciais.Length - 1); x > -1; x--)
                {
                    palavraSemAcento = palavraSemAcento.Replace(cEspeciais[x], "-");
                }

                palavraSemAcento = palavraSemAcento.Replace("+", "-").Replace(Environment.NewLine, "").TrimStart('-').TrimEnd('-').Replace("<i>", "-").Replace("<-i>", "-").Replace("<br>", "").Replace("--", "-");
            }
            else
            {
                palavraSemAcento = "indefinido";
            }

            return palavraSemAcento.ToLower();
        }
    }
}