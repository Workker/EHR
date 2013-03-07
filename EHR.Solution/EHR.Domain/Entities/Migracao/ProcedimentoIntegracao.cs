﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Migracao
{
    public class ProcedimentoMigracao : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string CodigoProcedimento { get; set; }

        public virtual string Procedimento { get; set; }

        public virtual string Grupo { get; set; }

        public virtual string SubGrupo { get; set; }
    }
}
