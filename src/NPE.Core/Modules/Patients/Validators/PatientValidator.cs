using NPE.Core.Common.Validation;
using NPE.Core.Modules.Patients.Models;
using System.Text.RegularExpressions;

namespace NPE.Core.Modules.Patients.Validators
{
    public static class PatientValidator
    {
        public static void Validate(PatientDTO patient)
        {
            if (patient == null)
                throw new ArgumentException("Patient data is required");

            var errors = new List<string>();

            // 🔹 Required Fields
            if (string.IsNullOrWhiteSpace(patient.FirstName))
                errors.Add("First Name is required");

            if (string.IsNullOrWhiteSpace(patient.LastName))
                errors.Add("Last Name is required");

            if (string.IsNullOrWhiteSpace(patient.Fhname))
                errors.Add("Father/Husband Name is required");

            // 🔹 Gender Validation
            if (patient.Sex != 0 && patient.Sex != 1 && patient.Sex != 2)
                errors.Add("Invalid gender value");

            // 🔹 Marital Status Validation
            if (patient.MaritalStatus < 0 || patient.MaritalStatus > 4)
                errors.Add("Invalid Marital Status value");

            //if (string.IsNullOrWhiteSpace(patient.BloodGroup))
            //    errors.Add("Blood Group is required");

            if (string.IsNullOrWhiteSpace(patient.City))
                errors.Add("City is required");

            if (string.IsNullOrWhiteSpace(patient.Country))
                errors.Add("Country is required");

            //if (string.IsNullOrWhiteSpace(patient.PatientNumber))
            //    errors.Add("Patient Number is required");

            if (patient.Location <= 0)
                errors.Add("Invalid location");

            // 🔹 Date Validation
            if (patient.DateOfBirth > DateTime.Now)
                errors.Add("Date of Birth cannot be in future");

            var age = DateTime.Now.Year - patient.DateOfBirth.Year;
            if (age < 0 || age > 120)
                errors.Add("Invalid Date of Birth");


            // 🔹 Mobile Validation (Pakistan)
            if (!string.IsNullOrWhiteSpace(patient.Mobile))
            {
                var mobileRegex = new Regex(@"^03\d{2}-?\d{7}$");
                if (!mobileRegex.IsMatch(patient.Mobile))
                    errors.Add("Invalid mobile number format");
            }

            //// 🔹 CNIC Validation
            //if (!string.IsNullOrWhiteSpace(patient.Nic))
            //{
            //    var cnicRegex = new Regex(@"^\d{5}-\d{7}-\d{1}$");
            //    if (!cnicRegex.IsMatch(patient.Nic))
            //        errors.Add("Invalid CNIC format");
            //}

            //// 🔹 Email Validation
            //if (!string.IsNullOrWhiteSpace(patient.Email))
            //{
            //    if (!patient.Email.Contains("@"))
            //        errors.Add("Invalid email format");
            //}

            // 🔹 Patient Settings Validation
            if (patient.PatientSetting != null)
            {
                if (patient.PatientSetting.AllowCredit &&
                    patient.PatientSetting.CreditLimit <= 0)
                {
                    throw new ArgumentException("Credit limit must be greater than zero");
                }

                if (patient.PatientSetting.Discount > patient.PatientSetting.MaxDiscount)
                {
                    throw new ArgumentException("Discount cannot exceed max discount");
                }
            }

            if (errors.Any())
                throw new ValidationException(errors);
        }
    }
}