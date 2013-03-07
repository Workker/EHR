using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Integracao
{
    public class PacienteIntegracao :IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string CodigoRegistroPaciente { get; set; }
        public virtual string CodigoProntuarioPaciente { get; set; }
        public virtual string DataEntrada { get; set; }
        
        
        
    //A1.cod_pac,       --- REGISTRO DO PACIENTE (ANTENDIMENTO)
    //A1.cod_prt,       --- PRONTUÁRIO DO PACIENTE 
    //A1.data_ent,      --- DATA DE ENTRADA
    //A1.data_alta,     --- DATA DA ALTA 
    //A1.hora_alta,     --- HORA DA ALTA 
    //A1.cod_pro,       --- CÓDIGO DO PROFISSIONAL (MÉDICO RESPONSÁVEL PELA INTERNAÇÃO)
    //A1.cod_esp,       --- CÓDIGO DA ESPECIALIDADE 
    //A1.leito,         --- LEITO 
    //B1.nome_pac,      --- NOME DO PACIENTE
    //B1.cpf,           --- CPF
    //B1.sexo,          --- SEXO
    //B1.nascimento,    --- DATA DE NASCIMENTO
    //B1.identidade,    --- NUMERO DA IDENTIDADE 
    //B1.data_abertu    --- DATA DA ABERTURA DO PRONTUÁRIO
    }
}
