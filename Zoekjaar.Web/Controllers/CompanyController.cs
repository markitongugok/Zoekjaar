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
		public const int PageSize = 2;

		[Authorize(Roles = "Graduate")]
		public ActionResult SearchJob(int? pageNumber = null)
		{
			var model = this.CreateSearchModel();

			if (pageNumber.HasValue)
			{
				model.Criteria.PageNumber = pageNumber.GetValueOrDefault();
			}
			model.Jobs = pageNumber.HasValue
				? this.JobRepository.Fetch(model.Criteria)
				: new List<JobView>();

			return this.View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Graduate")]
		[ValidateAntiForgeryToken]
		public ActionResult SearchJob(JobSearchModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			model.Jobs = this.JobRepository.Fetch(model.Criteria);

			return this.View(model);
		}

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
				var companyId = ((UserIdentity)this.User.Identity).EntityId;
				model.NewJob.CompanyId = companyId;

				this.CompanyJobRepository.Add(model.NewJob);
				this.CompanyJobRepository.SaveChanges();

				model.PostedJobs = this.CompanyJobRepository.Fetch(_ => _.CompanyId == companyId);
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

		private JobSearchModel CreateSearchModel()
		{
			return new JobSearchModel
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
			var companyId = ((UserIdentity)this.User.Identity).EntityId;
			return new JobModel
			{
				NewJob = this.CompanyJobRepository.Create(),
				VisaStatus = this.GetLookups("Visa Status"),
				PostedJobs = this.CompanyJobRepository.Fetch(_ => _.CompanyId == companyId)
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(JobSearchModel)
				? this.CreateSearchModel()
				: modelType == typeof(JobModel)
					? this.CreateJobModel()
					: Activator.CreateInstance(modelType);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }

		public ISearchRepository<JobView, SearchCriteria> JobRepository { get; set; }

		public IRepository<CompanyJob> CompanyJobRepository { get; set; }
	}
}
