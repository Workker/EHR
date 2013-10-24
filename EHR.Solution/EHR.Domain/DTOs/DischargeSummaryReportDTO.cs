using EHR.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EHR.Domain.DTOs
{
    public class DischargeSummaryReportDTO
    {
        //Patient
        public string Name { get; set; }
        public char Gender { get; set; }
        public int YearsOld { get; set; }
        public string Birthday { get; set; }
        public string MedicalRecord { get; set; }
        public string BedFloor { get; set; }
        //Summary
        public string HospitalName { get; set; }
        public string EntryDate { get; set; }
        public string AdmissionDate { get; set; }
        public string DischargeDate { get; set; }
        public int DaysHospitalized { get; set; }
        public string ReasonOfAdmission { get; set; }
        public IList<Allergy> Allergies { get; set; }
        public IList<Diagnostic> Diagnostics { get; set; }
        public IList<Medication> Medications { get; set; }
        public string Observation { get; set; }
        public string ConditionOfPatientAtDischarge { get; set; }
        public string DestinationOfPatientAtDischarge { get; set; }
        public IList<Procedure> Procedures { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Hemotransfusion> Hemotransfusions { get; set; }
        public string MDR { get; set; }
        public string MultidisciplinaryTeamsMet { get; set; }
        public IList<MedicalReview> MedicalReviews { get; set; }
        public IList<ComplementaryExam> ComplementaryExams { get; set; }
        public string NameOfPersonWhoDeliveredTheSummary { get; set; }
        public string DeliveredDate { get; set; }
        //Professional
        public string ProfissionalName { get; set; }
        public string ProfissionalRegistration { get; set; }
        //Data ?
    }
}