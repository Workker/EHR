using EHR.Domain.Repository;
using System;

namespace EHR.Controller
{
    public class FactoryRepository
    {
        public static BaseRepository GetRepository(RepositoryEnum repositoryEnum)
        {
            switch (repositoryEnum)
            {
                case RepositoryEnum.Accounts:
                    return new Accounts();
                case RepositoryEnum.Hospitals:
                    return new Hospitals();
                case RepositoryEnum.Sumaries:
                    return new Summaries();
                default:
                    throw new Exception("Controller not found.");
            }
        }
    }

    public enum RepositoryEnum : short
    {
        Accounts = 1,
        Hospitals = 2,
        Sumaries = 3
    }
}
