using ImportTables;
using NUnit.Framework;

namespace InitializeDatabaseAndCacheData
{
    [TestFixture]
    public class Initialize
    {
        [Test]
        [Ignore]
        public void LoadData()
        {
            var dataBase = new DataBaseInitialize();

            dataBase.create_database_by_model();
            dataBase.insert_states();
            dataBase.insert_hospitals_SQL();
            //dataBase.insert_hospitals_Oracle();
            dataBase.insert_allergies_types();
            dataBase.insert_diagnostic_types();
            dataBase.insert_Conditions_Of_The_Patient_At_Discharge();
            dataBase.insert_hemotransfusion_types();
            dataBase.insert_specialties();
            dataBase.insert_reactions_types();
            dataBase.insert_Historical_Action_Types();
            dataBase.insert_admin_account();

            var importCid = new ImportCID();
            importCid.ImportFromExcelFile();

            var importDEF = new ImportDEF();
            importDEF.ImportFromExcelFile();

            var importTus = new ImportTUSS();
            importTus.ImportFromExcelFile();

            var importSpecialty = new ImportSpecialty();
            importSpecialty.ImportFromExcelFile();

            var index = new IndexUpdate();
            index.update_cid_index();
            index.update_def_index();
            index.update_tuss_index();

            //var cache = new CacheInitialize();
            //cache.insert_hospitals_in_cache();
            //cache.insert_allergy_type_in_cache();
            //cache.insert_diagnostic_type_in_cache();
            //cache.insert_conditions_Of_The_Patient_At_Discharge();
        }
    }
}
