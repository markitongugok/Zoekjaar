using System;
using System.Linq;
using System.Web.Mvc;
using Business.Core;
using Core;
using Core.Extensions;
using Entities;
using Zoekjaar.Resources;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public sealed class GraduateController : ControllerBase
	{
		[Authorize(Roles = "Graduate")]
		public ActionResult Detail()
		{
			var model = this.CreateProfileModel();

			return this.View(model);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Edit()
		{
			var model = this.CreateProfileModel();

			return this.View(model);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult PersonalInformation()
		{
			var model = this.CreateProfileModel();

			return this.PartialView("_PersonalInformation", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult PersonalInformation(GraduateProfileModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					this.GraduateRepository.Attach(model.Graduate);
					this.GraduateRepository.SaveChanges();
					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.PartialView("_PersonalInformation", model);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Education()
		{
			var model = this.CreateEducationModel();
			return this.PartialView("_Education", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Education(GraduateDegreeModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					if (model.Template.Id == 0)
					{
						model.Template.GraduateId = this.UserIdentity.EntityId;
						this.GraduateDegreeRepository.Add(model.Template);
					}
					else
					{
						this.GraduateDegreeRepository.Attach(model.Template);
					}
					this.GraduateDegreeRepository.SaveChanges();
					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.PartialView("_Education", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult DeleteEducation(int id)
		{
			this.GraduateDegreeRepository.Remove(_ => _.Id == id);
			this.GraduateDegreeRepository.SaveChanges();
			return this.PartialView("_Education", this.CreateEducationModel());
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Language()
		{
			var model = this.CreateLanguageModel();

			return this.PartialView("_Language", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Language(GraduateLanguageModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					if (model.Template.Id == 0)
					{
						model.Template.GraduateId = this.UserIdentity.EntityId;
						this.GraduateLanguageRepository.Add(model.Template);
					}
					else
					{
						this.GraduateLanguageRepository.Attach(model.Template);
					}
					this.GraduateLanguageRepository.SaveChanges();
					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.PartialView("_Language", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult DeleteLanguage(int id)
		{
			this.GraduateLanguageRepository.Remove(_ => _.Id == id);
			this.GraduateLanguageRepository.SaveChanges();
			return this.PartialView("_Language", this.CreateLanguageModel());
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Experience()
		{
			var model = this.CreateExperienceModel();
			return this.PartialView("_Experience", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Experience(GraduateExperienceModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					if (model.Template.Id == 0)
					{
						model.Template.GraduateId = this.UserIdentity.EntityId;
						this.GraduateExperienceRepository.Add(model.Template);
					}
					else
					{
						this.GraduateExperienceRepository.Attach(model.Template);
					}
					this.GraduateExperienceRepository.SaveChanges();
					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.PartialView("_Experience", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult DeleteExperience(int id)
		{
			this.GraduateExperienceRepository.Remove(_ => _.Id == id);
			this.GraduateExperienceRepository.SaveChanges();
			return this.PartialView("_Experience", this.CreateExperienceModel());
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Link()
		{
			var model = this.CreateGraduateLinkModel();
			return this.PartialView("_Link", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Link(GraduateLinkModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					var graduate = this.GraduateRepository.Get(_ => _.Id == this.UserIdentity.EntityId);

					graduate.LinkedIn = model.LinkedIn;
					graduate.GooglePlus = model.GooglePlus;
					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			this.GraduateRepository.SaveChanges();
			return this.PartialView("_Link", model);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult ChangePassword()
		{
			var model = this.CreateChangePasswordModel();
			return this.PartialView("_ChangePassword", model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				try
				{
					var user = this.UserRepository.Fetch(_ => _.Id == this.UserIdentity.UserId).Single();
					var generator = new PasswordGenerator(model.CurrentPassword);
					if (!generator.Password.ToArray().IsEqualTo(user.Hash))
					{
						this.ModelState.AddModelError(AccountController.ChangePasswordKey, ApplicationStrings.IncorrectPassword);
						model.IsSuccessful = false;
						return this.PartialView("_ChangePassword", model);
					}

					generator = new PasswordGenerator(model.NewPassword);
					user.Salt = generator.Salt.ToArray();
					user.Hash = generator.Password.ToArray();
					this.UserRepository.SaveChanges();
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Data", ex.Message);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}

			this.GraduateRepository.SaveChanges();
			return this.PartialView("_ChangePassword", model);
		}

		private GraduateDegreeModel CreateEducationModel()
		{
			var graduateId = this.UserIdentity.EntityId;

			return new GraduateDegreeModel
			{
				Template = this.GraduateDegreeRepository.Create(),
				Items = this.GraduateDegreeRepository.Fetch(_ => _.GraduateId == graduateId)
			};
		}

		private GraduateLanguageModel CreateLanguageModel()
		{
			return new GraduateLanguageModel
			{
				Template = this.GraduateLanguageRepository.Create(),
				Items = this.GraduateLanguageRepository.Fetch(_ => _.GraduateId == this.UserIdentity.EntityId),
				Proficiencies = Identifiers.Proficiency.BasicUnderstanding.ToEnumerable()
			};
		}

		private GraduateExperienceModel CreateExperienceModel()
		{
			return new GraduateExperienceModel
			{
				Template = this.GraduateExperienceRepository.Create(),
				Items = this.GraduateExperienceRepository.Fetch(_ => _.GraduateId == this.UserIdentity.EntityId)
			};
		}

		public GraduateProfileModel CreateProfileModel()
		{
			return new GraduateProfileModel
			{
				CurrentStatus = Identifiers.CurrentStatus.NA.ToEnumerable(),
				Proficiencies = Identifiers.Proficiency.BasicUnderstanding.ToEnumerable(),
				VisaStatus = Identifiers.VisaStatus.NA.ToEnumerable(),
				Graduate = this.GraduateRepository.Get(_ => _.Id == this.UserIdentity.EntityId),
				Degree = new GraduateDegree()
			};
		}

		private GraduateLinkModel CreateGraduateLinkModel()
		{
			var graduate = this.GraduateRepository.Get(_ => _.Id == this.UserIdentity.EntityId);
			return new GraduateLinkModel
			{
				LinkedIn = graduate.LinkedIn,
				GooglePlus = graduate.GooglePlus
			};
		}

		private ChangePasswordModel CreateChangePasswordModel()
		{
			return new ChangePasswordModel();
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateProfileModel)
					? this.CreateProfileModel()
					: modelType == typeof(GraduateDegreeModel)
						? this.CreateEducationModel()
						: modelType == typeof(GraduateLanguageModel)
							? this.CreateLanguageModel()
							: modelType == typeof(GraduateExperienceModel)
								? this.CreateExperienceModel()
								: modelType == typeof(GraduateLinkModel)
									? this.CreateGraduateLinkModel()
									: modelType == typeof(ChangePasswordModel)
										? this.CreateChangePasswordModel()
										: Activator.CreateInstance(modelType);
		}


		public IRepository<Graduate> GraduateRepository { get; set; }

		public IRepository<JobApplication> JobApplicationRepository { get; set; }

		public IRepository<GraduateDegree> GraduateDegreeRepository { get; set; }

		public IRepository<GraduateLanguage> GraduateLanguageRepository { get; set; }

		public IRepository<GraduateExperience> GraduateExperienceRepository { get; set; }

		public IRepository<User> UserRepository { get; set; }
	}
}
