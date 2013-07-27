using System;
using System.Linq;
using System.Web.Mvc;
using Business.Core;
using Business.Criteria;
using Core;
using Core.Extensions;
using Entities;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using Zoekjaar.Web.Authentication.Contracts;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public class AccountController : ControllerBase
	{
		public ActionResult GraduateLogin()
		{
			var model = new UserAccountModel
			{
				LoginType = 1
			};
			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult GraduateLogin(UserAccountModel model)
		{
			return this.Login(model, "Search", "Job", 1);
		}

		public ActionResult EmployerLogin()
		{
			var model = new UserAccountModel
			{
				LoginType = 2
			};
			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EmployerLogin(UserAccountModel model)
		{
			return this.Login(model, "Index", "Job", 2);
		}

		private ActionResult Login(UserAccountModel model, string action, string controller, int loginType)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (this.IsAuthenticated(model.UserName, model.Password, loginType))
			{
				return this.RedirectToAction(action, controller);
			}
			return this.View(model);
		}

		[Authorize]
		public ActionResult Logout()
		{
			this.Authentication.Logout();
			return this.RedirectToAction("Index", "Home");
		}

		public ActionResult CreateGraduateProfile()
		{
			var model = this.CreateGraduateModel();
			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateGraduateProfile(GraduateModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			this.VerifyCaptcha();

			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}
			var generator = new PasswordGenerator(model.Password);
			model.Graduate.User.Username = model.Email;
			model.Graduate.User.Salt = generator.Salt.ToArray();
			model.Graduate.User.Hash = generator.Password.ToArray();

			this.GraduateRepository.Add(model.Graduate);
			this.GraduateRepository.SaveChanges();

			if (this.IsAuthenticated(model.Email, model.Password, 1))
			{
				return this.RedirectToAction("Edit", "Graduate");
			}

			return this.View(model);
		}

		public ActionResult CreateEmployerProfile()
		{
			var model = this.CreateCompanyModel();
			return this.View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult CreateEmployerProfile(CompanyModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			this.VerifyCaptcha();

			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			var generator = new PasswordGenerator(model.Password);
			model.Company.User.Username = model.Email;
			model.Company.User.Salt = generator.Salt.ToArray();
			model.Company.User.Hash = generator.Password.ToArray();

			this.CompanyRepository.Add(model.Company);
			this.CompanyRepository.SaveChanges();

			if (this.IsAuthenticated(model.Email, model.Password, 2))
			{
				return this.RedirectToAction("Edit", "Company");
			}

			return this.View(model);
		}

		private bool IsAuthenticated(string username, string password, int userTypeId)
		{
			return this.Authentication.Login(new UserIdentityCriteria
			{
				Id = username,
				Password = password,
				Authenticate = true,
				UserTypeId = userTypeId
			});
		}

		private void VerifyCaptcha()
		{
			var recaptchaHelper = this.GetRecaptchaVerificationHelper();

			if (String.IsNullOrEmpty(recaptchaHelper.Response))
			{
				ModelState.AddModelError("", "Captcha answer cannot be empty.");
			}

			RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

			if (recaptchaResult != RecaptchaVerificationResult.Success)
			{
				ModelState.AddModelError("", "Incorrect captcha answer.");
			}
		}

		private GraduateModel CreateGraduateModel()
		{
			var id = this.ValueProvider.GetValue("id") != null
				? (int?)int.Parse(this.ValueProvider.GetValue("id").AttemptedValue)
				: null;
			return new GraduateModel
			{
				Graduate = this.ValueProvider.GetValue("id") != null
					? this.GraduateRepository.Get(_ => _.Id == id)
					: this.GraduateRepository.Create(),
				CurrentStatus = Identifiers.CurrentStatus.NA.ToEnumerable(),
				VisaStatus = Identifiers.VisaStatus.NA.ToEnumerable(),
				Proficiencies = Identifiers.Proficiency.BasicUnderstanding.ToEnumerable()
			};
		}

		private CompanyModel CreateCompanyModel()
		{
			return new CompanyModel
			{
				Company = this.CompanyRepository.Create(),
				VisaStatus = Identifiers.VisaStatus.NA.ToEnumerable()
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateModel)
				? this.CreateGraduateModel()
				: modelType == typeof(CompanyModel)
					? this.CreateCompanyModel()
					: base.CreateModel(modelType, valueProvider);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }

		public IApplicationAuthentication Authentication { get; set; }

		public IRepository<Company> CompanyRepository { get; set; }
	}

}
