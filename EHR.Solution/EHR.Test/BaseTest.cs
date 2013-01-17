﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Patient;
using EHR.Domain.Mapping;
using EHR.Domain.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace EHR.Test
{
    [TestFixture]
    public class BaseTest
    {
        [Test]
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
                    c => c.FromAppSetting("connection")).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<PatientMap>()).
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
            var admission = new Admission { Id = 1 };
            var reasons = new List<Reason> { Reason.Cirurgic };
            admission.ReasonOfAdmission = reasons;

            var allergy = new Allergy { Id = 1, HaveAllergies = true, TheWhich = "egg", Type = TypeOfAllergy.Angioedema };

            var diagnostic1 = new Diagnostic {Id = 1, Type = "test1", CidCode = "1", Cid = "test1"};
            var diagnostic2 = new Diagnostic { Id = 2, Type = "test2", CidCode = "2", Cid = "test2" };
            var diagnostic3 = new Diagnostic { Id = 3, Type = "test3", CidCode = "3", Cid = "test3" };

            var patient = new Patient();
            patient.AddAdmission(admission);
            patient.AddAllergy(allergy);
            patient.AddDiagnostic(diagnostic1);
            patient.AddDiagnostic(diagnostic2);
            patient.AddDiagnostic(diagnostic3);

            var patients = new Patients();
            patients.Save(patient);
        }
    }
}