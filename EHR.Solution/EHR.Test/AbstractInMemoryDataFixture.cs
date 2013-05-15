using NHibernate;
using NUnit.Framework;

namespace EHR.Test
{
    public abstract class AbstractInMemoryDataFixture
    {
        private ISession session;

        [SetUp]
        public void BaseSetup()
        {
            session = InMemorySessionFactoryProvider.Instance.OpenSession();
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (session != null)
                session.Dispose();
        }

        protected ISession Session
        {
            get { return session; }
        }
    }
}
