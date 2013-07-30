using NHibernate;
using NUnit.Framework;

namespace EHR.Test
{
    public abstract class AbstractInMemoryDataFixture
    {
        private ISession _session;

        [SetUp]
        public void BaseSetup()
        {
            _session = InMemorySessionFactoryProvider.Instance.OpenSession();
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (_session != null)
                _session.Dispose();
        }

        protected ISession Session
        {
            get { return _session; }
        }
    }
}
