using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum ReactionTypeEnum : short
    {
        [Description("Aloimunização Eritrocitária")]
        AloimunizacaoEritrocitaria = 1,
        [Description("Aloimunização HLA")]
        AloimunizacaoHla = 2,
        [Description("Imunomodulação")]
        Imunomodulacao =3,
        [Description("Lesão pulmonar relacionada a transfusão")]
        LesaoPulmonarRelacionadaATransfusao = 4,
        [Description("Púrpura pós transfusional")]
        PurpuraPosTransfusional = 5,
        [Description("Alérgica: leve; moderada; grave")]
        AlergicaLeveModeradaGrave = 6,
        [Description("Enxerto x Hospedeiro")]
        EnxertoXHospedeiro = 7,
        [Description("Febril não hemolítica")]
        FebrilNaoHemolitica = 8,
        [Description("Hemolítica Imune")]
        HemoliticaImune = 9,
    }
}
