﻿using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using ImportTables.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace ImportTables
{
   public  class ImportDEF
    {
        public void ImportFromExcelFile(string path)
        {
            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom(path, "DEF.xls", "Sheet1");

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

        public void ImportPrescriptionItemFromExcelFile(string path)
        {
            var excelRepository = new ExcelRepository();

            var data = excelRepository.GetAllDataFrom(path, "DEF.xls", "Sheet1");

            var list = new List<PrescriptionItem>();

            foreach (DataRow row in data.Rows)
            {
                var def = new PrescriptionItem();

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
                def.code = Guid.NewGuid().ToString();
                def.PrescriptionItemType = PrescriptionItemType.Medicamentos;
                list.Add(def);
            }

            var defs = new DEFRepository();

            defs.SaveList(list);
        }
    }
}
