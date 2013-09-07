using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Business.Core;
using Business.Criteria;
using Core;
using Core.Extensions;
using Entities;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using Zoekjaar.Resources;
using Zoekjaar.Web.Authentication.Contracts;
using Zoekjaar.Web.Extensions;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public class AccountController : ControllerBase
	{
		public const string ChangePasswordKey = "ChangePassword";

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

#if(TEST)
			this.SendEmail(model.Graduate.User.Id, model.Graduate.FirstName, 1, "m_ortigas@hotmail.com", ConfigurationManager.AppSettings["accountActivationXsl"], ApplicationStrings.AccountActivation, "Activate");
#else
			this.SendEmail(model.Graduate.User.Id, model.Graduate.FirstName, 1, model.Graduate.User.Username, ConfigurationManager.AppSettings["accountActivationXsl"], ApplicationStrings.AccountActivation, "Activate");
#endif

			return this.RedirectToAction("Confirm");
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

#if(TEST)
			this.SendEmail(model.Company.User.Id, model.Company.User.Username, 2, "m_ortigas@hotmail.com", ConfigurationManager.AppSettings["accountActivationXsl"], ApplicationStrings.AccountActivation, "Activate");
#else
			this.SendEmail(model.Company.User.Id, model.Company.User.Username, 2, model.Company.User.Username, ConfigurationManager.AppSettings["accountActivationXsl"], ApplicationStrings.AccountActivation, "Activate");
#endif


			return this.RedirectToAction("Confirm");
		}

		[Authorize]
		public ActionResult ChangePassword()
		{
			var model = new ChangePasswordModel();
			return this.View(model);
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			var generator = new PasswordGenerator(model.CurrentPassword);

			var user = this.UserRepository.Fetch(_ => _.Id == this.UserIdentity.UserId).Single();

			if (!generator.Password.ToArray().IsEqualTo(user.Hash))
			{
				this.ModelState.AddModelError(AccountController.ChangePasswordKey, ApplicationStrings.IncorrectPassword);
				model.IsSuccessful = false;
				return this.View(model);
			}

			generator = new PasswordGenerator(model.NewPassword);
			user.Salt = generator.Salt.ToArray();
			user.Hash = generator.Password.ToArray();
			this.UserRepository.SaveChanges();

			model.IsSuccessful = true;

			return this.View(model);
		}

		public ActionResult Confirm()
		{
			return this.View();
		}

		public ActionResult Activate()
		{
			var token = this.ValueProvider.GetValue("token").AttemptedValue.Decrypt();
			var tokens = token.Split(':');

			var model = new AccountActivationModel();

			if (tokens.Length != 3)
			{
				model.IsSuccessful = false;
				model.Message = ApplicationStrings.InvalidActivationToken;
				return this.View(model);
			}

			if (DateTime.UtcNow.Ticks > long.Parse(tokens[2]))
			{
				model.IsSuccessful = false;
				model.Message = ApplicationStrings.ExpiredActivationToken;
				return this.View(model);
			}

			model.UserId = int.Parse(tokens[0]);
			model.UserTypeId = int.Parse(tokens[1]);

			switch (model.UserTypeId)
			{
				case 1:
					var graduate = this.GraduateRepository.Get(_ => _.User.Id == model.UserId);
					if (!graduate.IsActive)
					{
						graduate.IsActive = true;
						graduate.User.IsActive = true;
						this.GraduateRepository.Attach(graduate);
						this.GraduateRepository.SaveChanges();
					}
					break;
				case 2:
					var company = this.CompanyRepository.Get(_ => _.User.Id == model.UserId);
					if (!company.IsActive)
					{
						company.IsActive = true;
						company.User.IsActive = true;
						this.CompanyRepository.Attach(company);
						this.CompanyRepository.SaveChanges();
					}
					break;
				default:
					throw new NotSupportedException("User type is not supported.");
			}
			model.Message = ApplicationStrings.AccountActivated;
			model.IsSuccessful = true;
			return this.View(model);
		}

		public ActionResult ForgotPassword()
		{
			return this.View(new ForgotPasswordModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ForgotPassword(ForgotPasswordModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			try
			{
				var user = this.UserRepository.Get(_ => _.Username == model.Email);
				if (user != null)
				{
#if(TEST)
					this.SendEmail(user.Id, user.Username, user.UserType, "m_ortigas@hotmail.com", ConfigurationManager.AppSettings["resetPasswordXsl"], ApplicationStrings.ResetPassword, "ResetPassword");
#else
					this.SendEmail(user.Id, user.Username, user.UserType, model.Email, ConfigurationManager.AppSettings["resetPasswordXsl"], ApplicationStrings.ResetPassword, "ResetPassword");
#endif
					model.IsSuccessful = true;
					model.Message = ApplicationStrings.ResetPasswordEmailSent;
				}
				else
				{
					model.IsSuccessful = false;
					model.Message = ApplicationStrings.UserDoesNotExist;
				}
			}
			catch (Exception ex)
			{
				model.IsSuccessful = false;
				model.Message = ApplicationStrings.UnexpectedException;
			}

			return this.View(model);
		}

		public ActionResult ResetPassword()
		{
			var token = this.ValueProvider.GetValue("token").AttemptedValue.Decrypt();
			var tokens = token.Split(':');

			var model = new ResetPasswordModel();

			if (tokens.Length != 3)
			{
				model.IsTokenValid = false;
				model.Message = ApplicationStrings.InvalidActivationToken;
				return this.View(model);
			}

			if (DateTime.UtcNow.Ticks > long.Parse(tokens[2]))
			{
				model.IsTokenValid = false;
				model.Message = ApplicationStrings.ExpiredActivationToken;
				return this.View(model);
			}

			model.UserId = int.Parse(tokens[0]);
			model.UserTypeId = int.Parse(tokens[1]);
			model.IsTokenValid = true;
			return this.View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ResetPassword(ResetPasswordModel model)
		{
			var user = this.UserRepository.Fetch(_ => _.Id == model.UserId).Single();
			var generator = new PasswordGenerator(model.NewPassword);
			user.Salt = generator.Salt.ToArray();
			user.Hash = generator.Password.ToArray();
			this.UserRepository.SaveChanges();

			model.IsSuccessful = true;

			model.Message = ApplicationStrings.PasswordChanged;
			model.IsSuccessful = true;
			return this.View(model);
		}

		private void SendEmail(int userId, string name, int typeId, string recipient, string xsl, string title, string action)
		{
			var routeData = new RouteValueDictionary();
			routeData["token"] = (string.Format("{0}:{1}:{2}", userId, typeId, DateTime.UtcNow.AddHours(int.Parse(ConfigurationManager.AppSettings["tokenExpiration"])).Ticks)).Encrypt();

			var urlHelper = new UrlHelper(this.Request.RequestContext);

			var activationHtml = new TokenModel
			{
				Url = urlHelper.ContentFullPath(urlHelper.Action(action, routeData)),
				Text = "here",
				Name = name
			}
			.Transform(xsl);

			activationHtml.SendTo(new List<string> { recipient }, title);
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
				this.ModelState.AddModelError("", "Captcha answer cannot be empty.");
			}

			RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

			if (recaptchaResult != RecaptchaVerificationResult.Success)
			{
				this.ModelState.AddModelError("", "Incorrect captcha answer.");
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

		public IRepository<User> UserRepository { get; set; }
	}

}
