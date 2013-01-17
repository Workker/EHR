﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;
using EHR.Domain.Mapping;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHibernate.Cfg;

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

        #region Métodos Genericos para acesso ao BD

        public BaseRepository() { }

        public BaseRepository(ISession session)
        {
            Session = session;
        }

        public virtual void Save(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.SaveOrUpdate(root);
            transaction.Commit();
        }

        public virtual void Delete(IAggregateRoot<int> root)
        {
            var transaction = Session.BeginTransaction();
            Session.Delete(root);
            transaction.Commit();
        }

        public virtual IList<T> All<T>()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public virtual T Get<T>(int id)
        {
            return Session.Get<T>(id);
        }

        #endregion

        #region Métodos de Sessão e Transação

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
            return
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
                    .FromAppSetting("connection")
                    )).Mappings(m => m.FluentMappings.AddFromAssemblyOf<PatientMap>()).BuildSessionFactory();
        }

        #endregion
    }
}