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
                case RepositoryEnum.Def:
                    return new DEFRepository();
                case RepositoryEnum.Patient:
                    return new Patients();
                case RepositoryEnum.Prescriptions:
                    return new PrescriptionForServiceRepository();
                default:
                    throw new Exception("Repositório não encontrado.");
            }
        }
    }

    public enum RepositoryEnum : short
    {
        Accounts = 1,
        Hospitals = 2,
        Sumaries = 3,
        Def = 4,
        Patient = 5,
        Prescriptions = 6
    }
}
