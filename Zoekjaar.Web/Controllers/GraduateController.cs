using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business;
using Business.Core;
using Business.Criteria;
using Entities;
using Zoekjaar.Web.Contracts;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public sealed class GraduateController : ControllerBase
	{
		[Authorize(Roles = "Graduate")]
		public ActionResult Edit()
		{
			var name = this.User.Identity.Name;

			var model = this.CreateProfileModel();

			return this.View(model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Edit(GraduateProfileModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			this.GraduateRepository.Attach(model.Graduate);
			this.GraduateRepository.SaveChanges();
			return this.View(model);
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
		private GraduateDegreeModel CreateEducationModel()
		{
			var graduateId = this.UserIdentity.EntityId;

			return new GraduateDegreeModel
			{
				Template = this.GraduateDegreeRepository.Create(),
				Degrees = this.GraduateDegreeRepository.Fetch(_ => _.GraduateId == graduateId)
			};
		}

		public GraduateProfileModel CreateProfileModel()
		{
			return new GraduateProfileModel
			{
				CurrentStatus = this.GetLookups("Current Status"),
				Proficiencies = this.GetLookups("Proficiency"),
				VisaStatus = this.GetLookups("Visa Status"),
				Graduate = this.GraduateRepository.Get(_ => _.Id == this.UserIdentity.EntityId),
				Degree = new GraduateDegree()
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateProfileModel)
					? this.CreateProfileModel()
					: modelType == typeof(GraduateDegreeModel)
						? this.CreateEducationModel()
						: Activator.CreateInstance(modelType);
		}


		public IRepository<Graduate> GraduateRepository { get; set; }

		public IRepository<JobApplication> JobApplicationRepository { get; set; }

		public IRepository<GraduateDegree> GraduateDegreeRepository { get; set; }
	}
}
