using EHR.CoreShared;
using EHR.CoreShared.Interfaces;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;


namespace EHR.Controller
{
    public class PatientController : EhrController
    {
        [ExceptionLogger]
        public override IPatient GetBy(string cpf)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(cpf), "CPF não informado.").Validate();

            var service = new GetPatientByHospitalService();
            var patient = service.GetPatientBy(cpf);

            Assertion.NotNull(patient, "Paciente não encontrado.").Validate();

            return patient;
        }

        [ExceptionLogger]
        public override IList<Allergy> GetAllergiesBy(string cpf)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(cpf), "Paciente inválido.").Validate();

            var summaries = Summaries.GetAllSummaries(cpf);

            var allergies = new List<Allergy>();
            foreach (var summary in summaries)
            {
                foreach (var allergy in summary.Allergies)
                {
                    allergies.Add(allergy);
                }
            }

            Assertion.NotNull(allergies, "Lista de alergias nula.").Validate();

            return allergies;
        }

        [ExceptionLogger]
        public override IList<Medication> GetMedicationsOfUseAfterInternationBy(string cpf)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(cpf), "Paciente inválido.").Validate();

            var summaries = Summaries.GetAllSummaries(cpf).OrderByDescending(x => x.Date).ToList();

            var medications = new List<Medication>();

            if (summaries.Count > 1)
            {
                foreach (var medication in summaries[1].Medications)
                {
                    medications.Add(medication);
                }

            }

            Assertion.NotNull(medications, "Lista de medicamentos nula.").Validate();

            return medications;
        }

        [ExceptionLogger]
        public override IList<IPatient> GetBy(Patient patient)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();

            var service = new GetPatientByHospitalService();
            var patients = service.GetPatientBy(patient);

            Assertion.NotNull(patients, "Lista de pacientes está nula.").Validate();

            return patients;
        }

        [ExceptionLogger]
        public override IList<IPatient> GetBy(Patient patient, List<string> hospitals)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            Assertion.NotNull(hospitals, "Lista dos hospitais está nula.").Validate();

            var service = new GetPatientByHospitalService();
            var patients = service.AdvancedGetPatientBy(patient, hospitals);

            Assertion.NotNull(patients, "Lista de pacientes está nula.").Validate();

            return patients;
        }

        [ExceptionLogger]
        public override Summary GetSummaryBy(IPatient patient, string treatment, int accountId)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            //Assertion.IsFalse(string.IsNullOrEmpty(treatment), "Tratamento não informado.").Validate();
            Assertion.GreaterThan(accountId, 0, "Conta de usuário não informada.").Validate();

            var summary = GetSummary(patient, treatment, Summaries);

            if (summary != null)
                summary.Patient = patient;
            else
            {
                if (patient.Treatments != null && patient.Treatments.Count > 0)
                {
                    summary = CreateMedicalRecord(patient, accountId, treatment);
                    Summaries.Save(summary);
                }
            }

            Assertion.NotNull(summary, "Sumário de alta inválido.").Validate();

            return summary;
        }

        [ExceptionLogger]
        private static Summary CreateMedicalRecord(IPatient patient, int accountId, string treatment)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            Assertion.GreaterThan(accountId, 0, "Conta de usuário não informado.").Validate();
            //Assertion.IsFalse(string.IsNullOrEmpty(treatment), "Tratamento não informado.").Validate();

            var account = ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).GetBy(accountId);

            ITreatment treatmentDTO = string.IsNullOrEmpty(treatment) ? patient.Treatments.OrderByDescending(t => t.EntryDate).FirstOrDefault() : patient.Treatments.FirstOrDefault(t => t.Id == treatment);

            Assertion.NotNull(treatmentDTO, "Tratamento invalido.").Validate();

            var summary = new Summary
                              {
                                  Cpf = patient.CPF,
                                  Date = DateTime.Now,
                                  Treatment = treatmentDTO,
                                  CodeMedicalRecord = string.IsNullOrEmpty(treatment) ? treatmentDTO.Id : treatment,
                                  Account = account,
                                  HighData = new DischargeData(),
                                  Hospital = treatmentDTO.Hospital,
                                  TreatmentId = treatmentDTO.Id,
                                  EntryDateTreatment = treatmentDTO.EntryDate
                              };

            Assertion.NotNull(summary, "Sumário de alta foi criado.");

            return summary;
        }

        [ExceptionLogger]
        private static Summary GetSummary(IPatient patient, string treatment, Summaries summaries)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            //Assertion.IsFalse(string.IsNullOrEmpty(treatment), "Tratamento não informado.").Validate();
            Assertion.NotNull(summaries, "Repositório de sumários de alta não informado.").Validate();

            Summary summary = string.IsNullOrEmpty(treatment) ? summaries.GetLastSummary(patient.CPF) : summaries.GetSummaryByTreatment(patient.CPF, treatment);

            //Assertion.NotNull(summary, "Sumário de Alta não encontrado.").Validate();

            return summary;
        }
    }
}