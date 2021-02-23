using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.DomainModel;
using System;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validations
{
    public class CheckApplicationValidation : AbstractValidator<ApplicantModel>
    {
        public CheckApplicationValidation()
        {
            //Checking Required
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.EmailAdress).NotEmpty().WithMessage("Email Adress is required");
            RuleFor(x => x.FamilyName).NotEmpty().WithMessage("Family Name is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.CountryOfOrigin).NotEmpty().WithMessage("Country Of Origin is required");
            RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
            // Checking Constraints 
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name – at least 5 Characters");
            RuleFor(x => x.FamilyName).MinimumLength(5).WithMessage("FamilyName – at least 5 Characters");
            RuleFor(x => x.Address).MinimumLength(10).WithMessage("Adress  – at least 10 Characters");
            RuleFor(x => x.Age).Must(Validate_Age).WithMessage("Age – must be between 20 and 60");
            RuleFor(x => x.CountryOfOrigin).Must(Validate_Country).WithMessage("CountryOfOrigin – must be a valid Country");
            RuleFor(x => x.EmailAdress).EmailAddress().WithMessage("EmailAdress – must be an valid email (only check for valid syntax *@*.[valid topleveldomain])");
        }

        private bool Validate_Country(string CountryOfOrigin)
        {
            try
            {
                using System.Net.WebClient client = new System.Net.WebClient();
                string IsValidCountry = client.DownloadString("https://restcountries.eu/rest/v2/name/" + CountryOfOrigin);
                if (IsValidCountry.Contains("404") || IsValidCountry.Contains("Not Found"))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Validate_Age(int age)
        {
            if (age >= 20 && age <= 60)
                return true;
            return false;
        }
    }
}
