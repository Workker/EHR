using EHR.CoreShared;
using EHR.Domain.Repository;
using ImportTables.Repository;
using System.Collections.Generic;
using System.Data;

namespace ImportTables
{
   public  class ImportDEF
    {
        public void ImportFromExcelFile()
        {
            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom("E:\\Projects\\EHR\\EHR.Solution\\ImportTables", "DEF.xls", "Sheet1");

            var list = new List<DEF>();

            foreach (DataRow row in data.Rows)
            {
                var def = new DEF();

                foreach (DataColumn column in data.Columns)
                {
                    switch (column.Caption)
                    {
                        case "PRINCÍPIO ATIVO":
                            def.ActivePrinciple = (string)row[column];
                            break;
                        case "PRODUTO":
                            def.Description = (string)row[column];
                            break;
                    }
                }

                list.Add(def);
            }

            var defs = new DEFRepository();

            defs.SaveList(list);
        }
    }
}
