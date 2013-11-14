using EHR.CoreShared.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class StateController : EhrController
    {
        private Types<State> _states;
        public Types<State> States
        {
            get { return _states ?? (_states = new Types<State>()); }
            set
            {
                _states = value;
            }
        }

        [ExceptionLogger]
        public override IList<State> GetAll()
        {
            return States.All();
        }
    }
}
