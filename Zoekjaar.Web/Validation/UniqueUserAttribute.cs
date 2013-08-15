using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities;
using Zoekjaar.Web.Models;

namespace Business.Entities.Validation
{

	public sealed class UniqueUserAttribute : ValidationAttribute
	{
		public UniqueUserAttribute()
		{

		}

		public override bool IsValid(object value)
		{
			return this.IsUnique((string)value);
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (validationContext.ObjectInstance.GetType() != typeof(GraduateModel) && validationContext.ObjectInstance != typeof(CompanyModel))
			{
				return new ValidationResult(string.Format("Type {0} is not supported.", validationContext.ObjectInstance.GetType().Name));
			}

			var graduate = validationContext.ObjectInstance as GraduateModel;

			if (graduate != null)
			{
				return this.IsValid(value) ? ValidationResult.Success : new ValidationResult("User already exists");
			}

			var company = validationContext.ObjectInstance as CompanyModel;

			if (company != null)
			{
				return this.IsValid(value) ? ValidationResult.Success : new ValidationResult("User already exists");
			}

			return ValidationResult.Success;
		}

		private bool IsUnique(string username)
		{
			var context = new ModelContainer();
			return !context.Users.Any(_ => _.Username == username);
		}

	}
}
