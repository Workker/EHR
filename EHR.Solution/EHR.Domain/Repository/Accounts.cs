using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Accounts : BaseRepository
    {
        public Accounts() { }
        public Accounts(ISession session) : base(session) { }

        [ExceptionLogger]
        public virtual Account GetBy(int id)
        {
            Assertion.GreaterThan(id, 0, "Identificador não informado").Validate();

            var account = base.Get<Account>(id);

            Assertion.NotNull(account, "Conta não encontrada.").Validate();

            return account;
        }

        public virtual Account GetAdministratorAccount(Hospital hospital)
        {
            Assertion.NotNull(hospital, "Conta não encontrada.").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Restrictions.Eq("Administrator", true));
            criterion.Add(Restrictions.Eq("Hospital", hospital));

            var account = criterion.UniqueResult<Account>();

            Assertion.NotNull(account, "Conta não encontrada.").Validate();

            return account;
        }

        [ExceptionLogger]
        public virtual Account GetBy(string email)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "Endereço de e-mail não informado.").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Restrictions.Eq("Email", email));

            var account = (Account)criterion.UniqueResult();

            Assertion.Null(account, "E-mail já cadastrado.");

            return account;
        }

        [ExceptionLogger]
        public virtual Account GetBy(string email, string password)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "Endereço de e-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Restrictions.Eq("Email", email));
            criterion.Add(Restrictions.Eq("Password", password));
            criterion.CreateAlias("ProfessionalRegistrations", "ProfessionalRegistration", JoinType.LeftOuterJoin);

            var account = criterion.UniqueResult<Account>();

            Assertion.NotNull(account, "Falha na autenticação. Verifique o usuário e senha digitados.").Validate();

            if (account.Administrator)
            {
                return account;
            }

            if (account.ProfessionalRegistrations.Count > 0)
            {
                foreach (var professionalRegistration in account.ProfessionalRegistrations)
                {
                    if (professionalRegistration.Approved && professionalRegistration.Refused == false)
                    {
                        return account;
                    }
                }
            }

            Assertion.IsTrue(false, "Falha na autenticação. Conta nao aprovada.").Validate();

            return account;
        }

        [ExceptionLogger]
        public virtual void Save(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário não informada.").Validate();

            base.Save(account);

            Assertion.GreaterThan(account.Id, 0, "A conta de usuário não foi salva.").Validate();
        }

        [ExceptionLogger]
        public virtual IList<Account> GetAllNotApproved(Hospital hospital)
        {
            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Restrictions.Eq("Hospital", hospital));
            criterion.Add(Restrictions.Eq("Administrator", false));
            criterion.CreateAlias("ProfessionalRegistrations", "ProfessionalRegistration", JoinType.InnerJoin).Add(
                Restrictions.Eq("ProfessionalRegistration.Approved", false)).Add(Restrictions.Eq("ProfessionalRegistration.Refused", false));
            criterion.AddOrder(Order.Desc("Id"));

            var accounts = criterion.List<Account>();

            Assertion.NotNull(accounts, "Lista de contas nula.").Validate();

            return accounts;
        }

        [ExceptionLogger]
        public virtual void ApproveProfessionalRegistration(int accountId, int professionalRegistrationId)
        {
            Assertion.GreaterThan(accountId, 0, "Conta inválida.").Validate();
            Assertion.GreaterThan(professionalRegistrationId, 0, "Registro Profissional inválido.").Validate();

            var account = base.Get<Account>(accountId);
            
            foreach (var professionalRegistration in account.ProfessionalRegistrations)
            {
                if (professionalRegistration.Id == professionalRegistrationId)
                {
                    professionalRegistration.Approved = true;
                }
            }

            base.Save(account);
        }

        [ExceptionLogger]
        public virtual void Refuse(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário não informada.").Validate();

            account.ToRefuse(true);
            base.Save(account);

            Assertion.IsTrue(account.Refused, "Conta de usuário não reprovada.").Validate();
        }

        private IList<Account> RemoveAppoveds(IList<Account> accounts)
        {
            foreach (var account in accounts)
            {
                foreach (var professionalRegistration in account.ProfessionalRegistrations)
                {
                    if (professionalRegistration.Approved)
                    {
                        account.ProfessionalRegistrations.Remove(professionalRegistration);
                    }
                }
            }
            return accounts;
        }
    }
}