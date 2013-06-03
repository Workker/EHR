using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Accounts : BaseRepository
    {
        public Accounts() { }
        public Accounts(ISession session) : base(session) { }

        public virtual Account GetBy(int id)
        {
            Assertion.GreaterThan(id, 0, "Identificador não informado").Validate();
            var account = base.Get<Account>(id);
            Assertion.NotNull(account, "Conta não encontrada.").Validate();
            return account;
        }

        public virtual Account GetBy(string email)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "Endereço de e-mail não informado.").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Expression.Eq("Email", email));

            var account = (Account)criterion.UniqueResult();

            Assertion.Null(account, "E-mail já cadastrado.");
            return account;
        }

        public virtual Account GetBy(string email, string password)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "Endereço de e-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada").Validate();

            var criterion = Session.CreateCriteria<Account>();
            criterion.Add(Expression.Eq("Email", email));
            criterion.Add(Expression.Eq("Password", password));

            var account = criterion.UniqueResult<Account>();

            Assertion.NotNull(account, "Falha na autenticação. Verifique o usuário e senha digitados.").Validate();

            return account;
        }

        public virtual void Save(Account account)
        {
            base.Save(account);
        }
    }
}
