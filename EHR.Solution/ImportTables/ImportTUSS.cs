using EHR.CoreShared;
using EHR.Domain.Repository;
using ImportTables.Repository;
using System.Collections.Generic;
using System.Data;

namespace ImportTables
{
    public class ImportTUSS
    {
        public void ImportFromExcelFile()
        {
            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom("E:\\Projects\\EHR\\EHR.Solution\\ImportTables", "TUSS.xls", "TUSS");

            var list = new List<TUS>();

            foreach (DataRow row in data.Rows)
            {
                var tus = new TUS();

                foreach (DataColumn column in data.Columns)
                {
                    switch (column.Caption)
                    {
                        case "Código do Termo":
                            tus.Code = row[column].ToString();
                            break;
                        case "Termo":
                            tus.Description = row[column].ToString();
                            break;
                    }
                }

                list.Add(tus);
            }

            var tusRepository = new TUSSRepository();

            tusRepository.SaveList(list);
        }
    }
}
