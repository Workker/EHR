namespace EHR.UI.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public short Type { get; set; }
        public DefModel Def { get; set; }
        public string Presentation { get; set; }
        public short PresentationType { get; set; }
        public string Dose { get; set; }
        public short Dosage { get; set; }
        public short Way { get; set; }
        public string Place { get; set; }
        public short Frequency { get; set; }
        public short FrequencyCase { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
    }

    
    public class PrescriptionForServiceModel
    {
        public int Id { get; set; }
        public short Type { get; set; }
        public PrescriptionDateModel PrescriptionDateModel { get; set; }
        public ItemPrescriptionModel TypePrescription { get; set; }
        public string Presentation { get; set; }
        public short PresentationType { get; set; }
        public string Dose { get; set; }
        public short Dosage { get; set; }
        public short Way { get; set; }
        public string Place { get; set; }
        public short Frequency { get; set; }
        public short FrequencyCase { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
    }
}