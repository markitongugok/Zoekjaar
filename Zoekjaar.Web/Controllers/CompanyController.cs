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
	public sealed class CompanyController : ControllerBase
	{
		public const int PageSize = 5;

		[Authorize(Roles = "Company")]
		public ActionResult Job()
		{
			var model = this.CreateJobModel();
			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Job(JobModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				model.NewJob.CompanyId = this.UserIdentity.EntityId;

				this.CompanyJobRepository.Add(model.NewJob);
				this.CompanyJobRepository.SaveChanges();

				model.PostedJobs = this.CompanyJobRepository.Fetch(_ => _.CompanyId == this.UserIdentity.EntityId);
				model.NewJob = this.CompanyJobRepository.Create();
				model.IsSuccessful = true;
				ModelState.Clear();
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		public ActionResult SearchGraduate(int? pageNumber = null)
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
		public ActionResult SearchGraduate(GraduateSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			model.Graduates = this.GraduateViewRepository.Fetch(model.Criteria);

			return this.View(model);
		}

		public ActionResult ViewJob()
		{
			var jobId = int.Parse(this.ValueProvider.GetValue("id").AttemptedValue);
			var model = this.CreateViewJobModel();
			model.Job = this.JobViewRepository.Get(jobId);
			model.Company = this.CompanyRepository.Get(c => c.Id == model.Job.CompanyId);
			return this.View(model);
		}

		private ViewJobModel CreateViewJobModel()
		{
			return new ViewJobModel();
		}

		private GraduateSearchModel CreateSearchModel()
		{
			return new GraduateSearchModel
			{
				CurrentStatus = this.GetLookups("Current Status"),
				VisaStatus = this.GetLookups("Visa Status"),
				Criteria = new SearchCriteria
				{
					PageSize = CompanyController.PageSize
				}
			};
		}

		private JobModel CreateJobModel()
		{
			return new JobModel
			{
				NewJob = this.CompanyJobRepository.Create(),
				VisaStatus = this.GetLookups("Visa Status"),
				PostedJobs = this.CompanyJobRepository.Fetch(_ => _.CompanyId == this.UserIdentity.EntityId)
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateSearchModel)
				? this.CreateSearchModel()
				: modelType == typeof(JobModel)
					? this.CreateJobModel()
					: Activator.CreateInstance(modelType);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }

		public ISearchRepository<GraduateView, SearchCriteria> GraduateViewRepository { get; set; }

		public IRepository<CompanyJob> CompanyJobRepository { get; set; }

		public ISearchRepository<JobView, SearchCriteria> JobViewRepository { get; set; }

		public IRepository<Company> CompanyRepository { get; set; }
	}
}
