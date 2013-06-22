namespace EHR.UI.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public short MedicationType { get; set; }
        public short Def { get; set; }
        public string Presentation { get; set; }
        public string PresentationType { get; set; }
        public string Dose { get; set; }
        public string Dosage { get; set; }
        public string Way { get; set; }
        public string Place { get; set; }
        public string Frequency { get; set; }
        public string FrequencyCase { get; set; }
        public int Duration { get; set; }
    }
}