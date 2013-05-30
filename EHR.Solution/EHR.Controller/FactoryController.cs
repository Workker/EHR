using System;

namespace EHR.Controller
{
    public class FactoryController
    {
        public static EHRController GetController(ControllerEnum controllerEnum)
        {
            switch (controllerEnum)
            {
                case ControllerEnum.Patient:
                    return new PatientController();
                case ControllerEnum.Procedure:
                    return new ProcedureController();
                case ControllerEnum.Allergy:
                    return new AllergyController();
                case ControllerEnum.Diagnostic:
                    return new DiagnosticController();
                case ControllerEnum.Hemotransfusion:
                    return new HemotransfusionController();
                case ControllerEnum.Account:
                    return new AccountController();
                default:
                    throw new Exception("Controller not found.");
            }
        }
    }

    public enum ControllerEnum : short
    {
        Patient = 1,
        Procedure = 2,
        Allergy = 3,
        Diagnostic = 4,
        Hemotransfusion = 5,
        Account = 6
    }
}
