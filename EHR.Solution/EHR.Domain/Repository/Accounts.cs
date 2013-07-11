using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
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

        [ExceptionLogger]
        public virtual Account GetBy(string email)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "Endereço de e-mail não informado.").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Expression.Eq("Email", email));

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
            criterion.Add(Expression.Eq("Email", email));
            criterion.Add(Expression.Eq("Password", password));
            criterion.Add(Expression.Eq("Approved", true));
            criterion.Add(Expression.Eq("Refused", false));

            var account = criterion.UniqueResult<Account>();

            Assertion.NotNull(account, "Falha na autenticação. Verifique o usuário e senha digitados.").Validate();

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
        public virtual IList<Account> GetAllNotApproved()
        {
            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Expression.Eq("Approved", false));
            criterion.Add(Expression.Eq("Refused", false));
            criterion.AddOrder(Order.Desc("Id"));


            var account = criterion.List<Account>();

            Assertion.NotNull(account, "Lista de contas nula.").Validate();

            return account;
        }

        [ExceptionLogger]
        public virtual void Approve(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário não informada.").Validate();

            account.Approved = true;
            base.Save(account);

            Assertion.IsTrue(account.Approved, "Conta de usuário não aprovada.").Validate();
        }

        [ExceptionLogger]
        public virtual void Refuse(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário não informada.").Validate();

            account.Refused = true;
            base.Save(account);

            Assertion.IsTrue(account.Refused, "Conta de usuário não reprovada.").Validate();
        }
    }
}
