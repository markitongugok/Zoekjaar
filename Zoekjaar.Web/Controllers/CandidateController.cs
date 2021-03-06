﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business;
using Business.Core;
using Business.Criteria;
using Core.Extensions;
using Entities;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public class CandidateController : ControllerBase
	{
		[Authorize(Roles = "Company")]
		public ActionResult Index()
		{
			var jobId = int.Parse(this.ValueProvider.GetValue("id").AttemptedValue);
			var model = this.CreateViewCandidatesModel();
			model.Candidates = this.CandidateRepository.Fetch(new CandidateSearchCriteria
			{
				JobId = jobId,
				CompanyId = this.UserIdentity.EntityId
			});
			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		[HttpPost]
		public ActionResult Index(CandidateStatusModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			var application = this.JobApplicationRepository.Fetch(_ => _.GraduateId == model.GraduateId && _.JobId == model.JobId).Single();

			application.StatusId = model.StatusId;

			this.JobApplicationRepository.SaveChanges();

			return this.Json(true);
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult Applications()
		{
			var model = this.CreateJobApplicationModel();
			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		public ActionResult Search(int? pageNumber = null)
		{
			var model = this.CreateSearchModel();

			if (pageNumber.HasValue)
			{
				model.Criteria.PageNumber = pageNumber.GetValueOrDefault();
			}
			model.Graduates = pageNumber.HasValue
				? this.GraduateViewRepository.Fetch(model.Criteria)
				: new List<GraduateView>();

			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Search(GraduateSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			model.Graduates = this.GraduateViewRepository.Fetch(model.Criteria);

			return this.View(model);
		}

		private ViewCandidatesModel CreateViewCandidatesModel()
		{
			return new ViewCandidatesModel
			{

			};
		}

		private JobApplicationModel CreateJobApplicationModel()
		{
			var graduateId = this.UserIdentity.EntityId;
			return new JobApplicationModel
			{
				Jobs = this.JobsRepository.Fetch(graduateId)
			};
		}

		private GraduateSearchModel CreateSearchModel()
		{
			return new GraduateSearchModel
			{
				CurrentStatus = Identifiers.CurrentStatus.NA.ToEnumerable(),
				VisaStatus = Identifiers.VisaStatus.NA.ToEnumerable(),
				Criteria = new GraduateSearchCriteria
				{
					PageSize = CompanyController.PageSize,
					EntityId = this.UserIdentity.EntityId
				},
			};
		}

		public override object CreateModel(Type modelType, System.Web.Mvc.IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateSearchModel)
				? this.CreateSearchModel()
				: modelType == typeof(ViewCandidatesModel)
				? this.CreateViewCandidatesModel()
				: modelType == typeof(JobApplicationModel)
					? this.CreateJobApplicationModel()
					: base.CreateModel(modelType, valueProvider);
		}

		public ISearchRepository<CandidateView, CandidateSearchCriteria> CandidateRepository { get; set; }

		public ISearchRepository<JobApplicationView, int> JobsRepository { get; set; }

		public ISearchRepository<GraduateView, GraduateSearchCriteria> GraduateViewRepository { get; set; }

		public IRepository<JobApplication> JobApplicationRepository { get; set; }
	}
}