using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ImportLegacySummary.DTO;
using Oracle.DataAccess.Client;

namespace ImportLegacySummary
{
    public class DbLegacy : IDisposable
    {
        private bool disposed = false;
        private IDbConnection connection;

        public IEnumerable<Summary> GetSummaries() 
        {
            return connection.Query<Summary, Diagnostic, Procedure, Medication, Hemotransfusion, Summary>(
                GetSql(),
                (Summary, Diagnostic, Procedure, Medication, Hemotransfusion) => {
                    Summary.Diagnostics.Add(Diagnostic);
                    Summary.Procedures.Add(Procedure);
                    Summary.Medications.Add(Medication);
                    Summary.Hemotransfusions.Add(Hemotransfusion);
                    return Summary;
                }, 
                splitOn: "DiagnosticId,ProcedureId, MedicationId,HemotransfusionId");
        }

        private string GetSql() 
        {
            return @"
            SELECT 
                S.IDINT058 as Id
                ,S.DESMDR as Observation
                ,S.DTAASSIMED as EntryDateTreatment
                ,S.IDDWD001 as HospitalId
                ,S.NRCPF as Cpf
                ,S.HISEXAFISADM as Admission
                ,S.ALGMEDQ as AllergyMed
                ,S.ALGMEDT as AllergyType

                ,D.IDINT098 as DiagnosticId
                ,DD.CDCID as Cid                
                ,D.TPDIAG as Type

                ,P.IDINT063 as ProcedureId
                ,P.DTPRC as DateProc
                ,TUS.CDTUSS AS TusCode
                
                ,M.IDINT067 as MedicationId
                ,'' as Presentation 
                ,'' PresentationType 
                ,'' as Dose
                ,M.DSEVAL as Dosage
                ,M.VIAMED as Way
                ,'' as Place
                ,M.FREQMED as Frequency
                ,'' as FrequencyCase
                ,M.DURMED as Duration 

                ,He.IDINT100 as HemotransfusionId
                ,HT.IDINT099 as HemotransfusionTypeId
            FROM 
                --Summary
                TBINT058 S

                --Diagnostic
                LEFT JOIN TBINT098 D ON S.IDINT058 = D.IDINT058
                LEFT JOIN TBDWD057 DD ON D.IDDWD057 = DD.IDDWD057 

                --Procedure
                LEFT JOIN TBINT063 P ON S.IDINT058 = P.IDINT058
                LEFT JOIN TBINT106 TUS ON P.IDDWD016 = TUS.IDINT106

                --Medication
                LEFT JOIN TBINT067 M ON S.IDINT058 = M.IDINT058
                
                --Hemotransfusion
                LEFT JOIN TBINT100 He ON S.IDINT058 = He.IDINT058
                LEFT JOIN TBINT099 HT ON He.IDINT100 = HT.IDINT099

                --Hemotransfusion Reactions
                
                
            WHERE 
                S.NRCPF IS NOT NULL
                AND S.NRCPF <> 0 
            ORDER BY NRCPF
            ";
        }

        public DbLegacy OpenConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LegacySummary"].ConnectionString;

            connection = new OracleConnection(connectionString);
            connection.Open();

            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
            }
            disposed = true;
        }
    }
}
