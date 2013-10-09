using EHR.CoreShared;
using EHR.Domain.Repository;
using ImportTables.Repository;
using System.Collections.Generic;
using System.Data;

namespace ImportTables
{
    public class ImportCID
    {
        public void ImportFromExcelFile()
        {
            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom("B:\\Documents\\EHR\\EHR.Solution\\ImportTables", "CID.xls", "CID10");

            var list = new List<CID>();

            foreach (DataRow row in data.Rows)
            {
                var cid = new CID();

                foreach (DataColumn column in data.Columns)
                {
                    switch (column.Caption)
                    {
                        case "CODIGO":
                            cid.Code = (string)row[column];
                            break;
                        case "DESCRICAO":
                            cid.Description = (string)row[column];
                            break;
                        case "DESCRABREV":
                            cid.AbbreviatedDescription = (string)row[column];
                            break;
                    }
                }

                list.Add(cid);
            }

            var cids = new CIDRepository();

            cids.SaveList(list);
        }
    }
}
