using System;

namespace EHR.Domain.Repository
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
                case RepositoryEnum.Def:
                    return new DEFRepository();
                case RepositoryEnum.Patient:
                    return new Patients();
                default:
                    throw new Exception("Controller not found.");
            }
        }
    }

    public enum RepositoryEnum : short
    {
        Accounts = 1,
        Hospitals = 2,
        Sumaries = 3,
        Def = 4,
        Patient = 5
    }
}