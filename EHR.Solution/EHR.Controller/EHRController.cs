﻿using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using EHR.Domain.DTOs;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public abstract class EhrController
    {

        private ItemPrescriptionRepository _itemPrescriptionRepository;




        private Summaries _summaries;
        public Summaries Summaries
        {
            get { return _summaries ?? (_summaries = new Summaries()); }
            set
            {
                _summaries = value;
            }
        }

        public virtual List<TUSS> GetTus(string term) { return null; }
        public virtual List<CID> GetCids(string term) { return null; }

        public virtual void SaveObservation(int summaryId, string observation) { }

        public virtual IList<Summary> GetLastTenSumaries(int id) { return null; }

        #region Patient

        public virtual IPatient GetBy(string cpf) { return null; }
        public virtual IList<IPatient> GetBy(IPatient dto) { return null; }
        public virtual IList<IPatient> GetBy(IPatient patient, List<string> hospital) { return null; }
        public virtual Summary GetSummaryBy(IPatient patient, string treatment, int idAccount) { return null; }
        public virtual IList<Allergy> GetAllergiesBy(string cpf) { return null; }
        public virtual IList<Medication> GetMedicationsOfUseAfterInternationBy(string cpf) { return null; }
        public virtual void Registering(IPatient patient) { }

        #endregion

        #region Summary

        public virtual void SaveMdr(int summaryId, string mdr) { }
        public virtual Summary GetBy(int id) { return null; }
        public virtual void AddToHistorical(int idSummary, int idAccount, HistoricalActionTypeEnum actionType, DateTime date, string description) { }
        public virtual void FinalizeSummary(int summaryid) { }
        public virtual void ReOpenSummary(int summaryId) { }
        public virtual DischargeSummaryReportDTO GetReportData(int summaryId) { return null; }
        public virtual IList<ReasonOfAdmission> GetReasonsOfAdmission() { return null; }
        public virtual void SaveReasonOfAdmission(int idSummary, IList<short> reasonsOfAdmission) { }

        #endregion

        #region Medication
        public virtual List<PrescriptionItem> GetPrescription(string term) { return null; }
        public virtual List<CuidadoMedico> GetCuidadosMedicos(string term) { return null; }
        public virtual List<DEF> GetDef(string term) { return null; }
        public virtual void SaveMedication(int idSummary, short medicationType, short def, string description, string presentation,
            short presentationType, string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration)
        { }

        [ExceptionLogger]
        public virtual void SavePrescriptionForService(int idSummary, TypePrescription typePrescriptionEnum, short itemPrescriptionID, string itemPrescriptionCode, PrescriptionItemType prescriptionItemType, string presentation,
           short presentationType, string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration
            , int PrescriptionHighMonth, int PrescriptionHighYear, int PrescriptionHighDay, int PrescriptionHighHour, int PrescriptionHighMinute,
            int quantity, string observation)
        {
            #region Preconditions
            Assertion.GreaterThan(idSummary, 0, "Prontuário inválido, favor contactar o suporte.").Validate();
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            if (typePrescriptionEnum == TypePrescription.Medicamentos)
            {
                Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
                Assertion.GreaterThan((int)presentationType, 0, "Tipo de apresentação não informado.").Validate();
                Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
                Assertion.GreaterThan((int)dosage, 0, "Dosagem não informada.").Validate();
                Assertion.GreaterThan((int)way, 0, "Via informada.").Validate();
                Assertion.GreaterThan((int)frequency, 0, "Frequencia não informada.").Validate();
            }

            #endregion

            var summary = GetBy(idSummary);

            PrescriptionItem prescriptionItem;
            Assertion.GreaterThan(itemPrescriptionCode, string.Empty, "Item de prescrição não informado.").Validate();
            _itemPrescriptionRepository = new ItemPrescriptionRepository();

            prescriptionItem = _itemPrescriptionRepository.GetById(itemPrescriptionCode, prescriptionItemType);

            summary.CreatePrescriptionForService(prescriptionItem, typePrescriptionEnum, presentation, presentationType, dose, dosage, way, place, frequency
                , frequencyCase, duration, PrescriptionHighMonth, PrescriptionHighYear, PrescriptionHighDay, PrescriptionHighHour, PrescriptionHighMinute
                , quantity, observation);

            Summaries.Save(summary);
        }
        public virtual void RemoveMedication(int idSummary, int id) { }
        public virtual void InactivePrescriptionForService(int idSummary, int id) { }
        #endregion

        #region Exam

        public virtual void SaveExam(int idSummary, short type, DateTime date, string description) { }
        public virtual void RemoveExam(int idSummary, int id) { }

        #endregion

        #region Account

        public virtual void Register(string firstName, string lastName, short gender, short professionalResgistrationType, string professionalResgistrationNumber, string email,
                                      string password, DateTime birthday, short hospitalId)
        { }
        public virtual Account Login(string email, string password) { return null; }
        public virtual bool VerifyIfExist(string email) { return false; }
        public virtual IList<Account> GetAllNotApproved(short hospitalId) { return null; }
        public virtual IList<Summary> GetLastSumariesRealizedby(int accountId) { return null; }
        public virtual void ApproveProfessionalRegistration(int accountId, int professionalRegistrationId) { }
        public virtual void RefuseAccount(int id, int ProfessionalId) { }
        public virtual void AlterPasswordOfAccount(int id, string password, string newPasswordConfirm) { }
        public virtual void AddprofessionalResgistration(int accountId, short professionalResgistrationType, string professionalResgistrationNumber, short stateId) { }

        #endregion

        #region Procedure

        public virtual void SaveProcedure(string day, string month, string year, string procedureCode, int idSummary, string description) { }
        public virtual void RemoveProcedure(int idSummary, int id) { }

        #endregion

        #region Allergy

        public virtual void SaveAllergy(string theWitch, IList<short> types, int idSummary) { }
        public virtual void RemoveAllergy(int idSummary, int id) { }

        #endregion

        #region Diagnostic

        public virtual void SaveDiagnostic(short diagnosticType, string description, string cid, int idSummary) { }
        public virtual void RemoveDiagnostic(int idSummary, int id) { }

        #endregion

        #region Hemotransfusion

        public virtual void SaveHemotransfusion(IList<short> typeReaction, short typeHemotrasfusion, int idSummary) { }
        public virtual void RemoveHemotransfusion(int idSummary, int id) { }

        #endregion

        #region HighData

        #region Specialty

        public virtual List<Specialty> GetSpecialty(string term) { return null; }
        public virtual Specialty GetById(short id) { return null; }

        #endregion

        public virtual void SaveDischargeData(int idSummary, IList<ComplementaryExam> complementaryExams, IList<int> complementaryExamDeleteds, IList<MedicalReview> medicalReviews, IList<int> medicalReviewDeleteds, short highType,
            short conditionOfThePatientAtDischargeId, short destinationOfThePatientAtDischarge,
           short orientationOfMultidisciplinaryTeamsMet, DateTime prescribedHigh,
            string personWhoDeliveredTheSummary, DateTime deliveredDate)
        { }

        #endregion


        #region State

        public virtual IList<State> GetAll() { return null; }

        #endregion

        #region

        public virtual IList<Hospital> GetAllHospitals() { return null; }

        #endregion

    }
}
