﻿using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;


namespace EHR.Controller
{
    public class PatientController : EHRController
    {
        [ExceptionLogger]
        public override IPatientDTO GetBy(string cpf)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(cpf), "CPF não informado.").Validate();

            var service = new GetPatientByHospitalService();
            var patient = service.GetPatientBy(cpf);

            Assertion.NotNull(patient, "Paciente não encontrado.").Validate();

            return patient;
        }

        [ExceptionLogger]
        public override IList<IPatientDTO> GetBy(PatientDTO patientDTO)
        {
            Assertion.NotNull(patientDTO, "Paciente não informado.").Validate();

            var service = new GetPatientByHospitalService();
            var patients = service.GetPatientBy(patientDTO);

            Assertion.NotNull(patients, "Lista de pacientes está nula.").Validate();

            return patients;
        }

        [ExceptionLogger]
        public override IList<IPatientDTO> GetBy(PatientDTO patientDTO, List<string> hospitals)
        {
            Assertion.NotNull(patientDTO, "Paciente não informado.").Validate();
            Assertion.NotNull(hospitals, "Lista dos hospitais está nula.").Validate();

            var service = new GetPatientByHospitalService();
            var patients = service.AdvancedGetPatientBy(patientDTO, hospitals);

            Assertion.NotNull(patients, "Lista de pacientes está nula.").Validate();

            return patients;
        }

        [ExceptionLogger]
        public override Summary GetSummaryBy(IPatientDTO patient, string treatment, int accountId)
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
        private static Summary CreateMedicalRecord(IPatientDTO patient, int accountId, string treatment)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            Assertion.GreaterThan(accountId, 0, "Conta de usuário não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(treatment), "Tratamento não informado.").Validate();

            var account = ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).GetBy(accountId);
            var treatmentDTO = patient.Treatments.OrderByDescending(t => t.EntryDate).FirstOrDefault();

            var summary = new Summary()
                         {
                             Cpf = patient.CPF,
                             Date = DateTime.Now,
                             Treatment = patient.Treatments.OrderByDescending(t => t.EntryDate).FirstOrDefault(),
                             CodeMedicalRecord = string.IsNullOrEmpty(treatment) ? treatmentDTO.Id : treatment,
                             Account = account,
                             HighData = new HighData()
                         };

            Assertion.NotNull(summary, "Sumário de alta foi criado.");

            return summary;
        }

        [ExceptionLogger]
        private static Summary GetSummary(IPatientDTO patient, string treatment, Summaries summaries)
        {
            Assertion.NotNull(patient, "Paciente não informado.").Validate();
            //Assertion.IsFalse(string.IsNullOrEmpty(treatment), "Tratamento não informado.").Validate();
            Assertion.NotNull(summaries, "Repositório de sumários de alta não informado.").Validate();

            Summary summary;
            if (string.IsNullOrEmpty(treatment))
                summary = summaries.GetLastSummary(patient.CPF);
            else
                summary = summaries.GetSummaryByTreatment(patient.CPF, treatment);

            Assertion.NotNull(summary, "Sumário de Alta não encontrado.").Validate();

            return summary;
        }
    }
}