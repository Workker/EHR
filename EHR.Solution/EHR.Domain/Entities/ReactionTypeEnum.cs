using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public enum ReactionTypeEnum
    {
        [Description("Aloimunização Eritrocitária")]
        AloimunizacaoEritrocitaria = 0,
        [Description("Aloimunização HLA")]
        AloimunizacaoHla = 1,
        [Description("Imunomodulação")]
        Imunomodulacao = 2,
        [Description("Lesão pulmonar relacionada a transfusão")]
        LesaoPulmonarRelacionadaATransfusao = 3,
        [Description("Púrpura pós transfusional")]
        PurpuraPosTransfusional = 4,
        [Description("Alérgica: leve; moderada; grave")]
        AlergicaLeveModeradaGrave = 5,
        [Description("Enxerto x Hospedeiro")]
        EnxertoXHospedeiro = 6,
        [Description("Febril não hemolítica")]
        FebrilNaoHemolitica = 7,
        [Description("Hemolítica Imune")]
        HemoliticaImune = 8,
    }
}
