using NPE.Core.Common.Validation;
using NPE.Core.Modules.Cases.Models;

public static class CaseValidator
{
    // 🔹 Used in SaveCase flow
    #region Create

    public static void ValidateCreate(
        CaseDTO dto)
    {
        var validation =
            new ValidationResult();

        if (dto == null)
        {
            validation.Add(
                "Case data is required.");
        }
        else
        {
            if (dto.PatientId <= 0)
                validation.Add(
                    "Patient is required.");

            if (dto.RegistrationLocation <= 0)
                validation.Add(
                    "Registration location is required.");

            if (dto.RegistrationDate == default)
                validation.Add(
                    "Case date is required.");
        }

        ThrowIfInvalid(validation);
    }

    #endregion

    #region Update

    public static void ValidateUpdate(
        CaseDTO dto)
    {
        var validation =
            new ValidationResult();

        if (dto == null)
        {
            validation.Add(
                "Case data is required.");
        }
        else
        {
            if (dto.Id <= 0)
                validation.Add(
                    "Case id is required.");

            if (dto.PatientId <= 0)
                validation.Add(
                    "Patient is required.");

            if (dto.RegistrationLocation <= 0)
                validation.Add(
                    "Registration location is required.");
        }

        ThrowIfInvalid(validation);
    }

    #endregion

    #region Save

    public static void ValidateSave(
        CaseDTO caseInfo,
        ValidationResult validation)
    {
        if (caseInfo == null)
        {
            validation.Add(
                "Case information is required.");
            return;
        }

        if (caseInfo.PatientId <= 0)
            validation.Add(
                "Patient is required.");

        if (caseInfo.RegistrationLocation <= 0)
            validation.Add(
                "Registration location is required.");

        if (caseInfo.RegistrationDate == default)
            validation.Add(
                "Case registration date is required.");

        if (caseInfo.ConsultantId <= 0)
            validation.Add(
                "Consultant is required.");

        if (caseInfo.Discount < 0 ||
            caseInfo.Discount > 100)
        {
            validation.Add(
                "Discount must be between 0 and 100.");
        }

        if (caseInfo.PaidAmount < 0)
            validation.Add(
                "Paid amount cannot be negative.");

        if (caseInfo.Less < 0)
            validation.Add(
                "Less amount cannot be negative.");
    }

    #endregion

    #region Private

    private static void ThrowIfInvalid(
        ValidationResult validation)
    {
        if (!validation.IsValid)
            throw new ValidationException(
                validation.Errors);
    }

    #endregion
}