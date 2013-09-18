using EHR.Domain.Entities;
using EHR.Domain.Repository;
using ImportTables.Repository;
using System.Collections.Generic;
using System.Data;

namespace ImportTables
{
    public class ImportSpecialty
    {
        public void ImportFromExcelFile()
        {

            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom("E:\\Projects\\EHR\\EHR.Solution\\ImportTables", "ESPECIALIDADES.xls", "Plan1");

            var list = new List<Specialty>();

            foreach (DataRow row in data.Rows)
            {
                var specialty = new Specialty();

                foreach (DataColumn column in data.Columns)
                {
                    switch (column.Caption)
                    {
                        case "Código do Termo":
                            specialty.CodeTerm = row[column].ToString();
                            break;
                        case "Termo":
                            specialty.Description = row[column].ToString();
                            break;
                    }
                }

                list.Add(specialty);
            }

            var specialties = new Specialties();

            specialties.SaveList(list);
        }
    }
}
