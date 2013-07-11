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
		private ModelContainer context = new ModelContainer();

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Apply(int id)
		{
			var jobApplication = this.JobApplicationRepository.Create();
			jobApplication.JobId = id;
			jobApplication.GraduateId = this.UserIdentity.EntityId;
			jobApplication.StatusId = null;

			this.JobApplicationRepository.Add(jobApplication);
			this.JobApplicationRepository.SaveChanges();

			return this.Json(true);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult EditProfile()
		{
			var name = this.User.Identity.Name;

			var model = this.CreateProfileModel();

			return this.View(model);
		}

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult EditProfile(GraduateProfileModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			this.GraduateRepository.Attach(model.Graduate);
			this.GraduateRepository.SaveChanges();
			return this.View(model);
		}
				
		public GraduateProfileModel CreateProfileModel()
		{
			return new GraduateProfileModel
			{
				CurrentStatus = this.GetLookups("Current Status"),
				Proficiencies = this.GetLookups("Proficiency"),
				VisaStatus = this.GetLookups("Visa Status"),
				Graduate = this.GraduateRepository.Get(_ => _.Id == this.UserIdentity.EntityId)
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateProfileModel)
					? this.CreateProfileModel()
					: Activator.CreateInstance(modelType);
		}


		public IRepository<Graduate> GraduateRepository { get; set; }

		public IRepository<JobApplication> JobApplicationRepository { get; set; }


	}
}
