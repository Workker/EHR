using EHR.CoreShared.Interfaces;
using EHR.Domain.Mapping;
using EHR.Infrastructure.Util;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace EHR.Domain.Repository
{
    public abstract class BaseRepository
    {
        public const string NHibernateSessionKey = "nhibernate.session.key";
        public static ISessionFactory Factory = CreateSessionFactory();

        private static ISession _session;
        private static readonly object SyncObj = 1;

        public static ISession Session
        {
            get { return _session ?? (_session = GetCurrentSession()); }
            set { _session = value; }
        }

        #region Methods Generics to acess Database

        protected BaseRepository() { }

        protected BaseRepository(ISession session)
        {
            Session = session;
        }

        public virtual T Get<T>(int id)
        {
            return Session.Get<T>(id);
        }

        public virtual IList<T> All<T>()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public virtual void Save(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.SaveOrUpdate(root);
            transaction.Commit();
        }

        public virtual void Save(IAggregateRoot<string> root)
        {
            var transaction = Session.BeginTransaction();
            Session.SaveOrUpdate(root);
            transaction.Commit();
        }

        public virtual void SaveList(List<IAggregateRoot<int>> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public virtual void SaveList<T>(List<T> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public virtual void Delete(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.Delete(root);
            transaction.Commit();
        }

        #endregion

        #region Methods of Session and Transaction

        public static void CloseTransaction(ITransaction transaction)
        {
            transaction.Dispose();
        }

        public static ISession GetCurrentSession()
        {
            ISession currentSession;
            lock (SyncObj)
                currentSession = Factory.OpenSession();
            return currentSession;
        }

        public static ISessionFactory CreateSessionFactory()
        {
            if (ConfigurationManager.AppSettings["Environment"].Equals("Deploy"))
            {
                return Fluently.Configure().Database(OracleClientConfiguration.Oracle10.ConnectionString(c => c
                  .FromAppSetting("connection"))).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SummaryMap>()).BuildSessionFactory();
            }
            if (ConfigurationManager.AppSettings["Environment"].Equals("Development"))
            {
                return Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                    .FromAppSetting("connection"))).Mappings(m => m.FluentMappings.AddFromAssemblyOf<SummaryMap>()).ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor())).BuildSessionFactory();
            }
            return null;
        }

        #endregion
    }
}