using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Business.Core;
using Business.Criteria;
using Entities;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public class JobController : ControllerBase
	{
		[Authorize(Roles = "Company")]
		public ActionResult Index()
		{
			var model = this.CreateJobsModel();
			return this.View(model);
		}

		public ActionResult AddOrEdit()
		{
			var model = this.CreateJobModel();
			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddOrEdit(JobModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			if (this.ModelState.IsValid)
			{
				model.Job.CompanyId = this.UserIdentity.EntityId;

				if (model.Job.Id == 0)
				{
					this.CompanyJobRepository.Add(model.Job);
				}
				else
				{
					this.CompanyJobRepository.Attach(model.Job);
				}
				this.CompanyJobRepository.SaveChanges();

				model.IsSuccessful = true;
				ModelState.Clear();
			}
			else
			{
				model.IsSuccessful = false;
			}

			return this.View(model);
		}

		private JobModel CreateJobModel()
		{
			var value = this.ValueProvider.GetValue(RouteConfig.Id);
			var id = value != null ? (int?)int.Parse(value.AttemptedValue) : null;

			return new JobModel
			{
				Job = id.HasValue ? this.CompanyJobRepository.Get(_ => _.Id == id) : this.CompanyJobRepository.Create(),
				VisaStatus = this.GetLookups("Visa Status"),
				JobTypes = this.GetLookups("Job Type")
			};
		}
		private JobsModel CreateJobsModel()
		{
			return new JobsModel
			{
				PostedJobs = this.CompanyJobRepository.Fetch(_ => _.CompanyId == this.UserIdentity.EntityId),
			};
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

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(JobModel)
				? this.CreateJobModel()
				: modelType == typeof(JobsModel)
				? this.CreateJobsModel()
				: modelType == typeof(ViewJobModel)
					? this.CreateViewJobModel()
					: base.CreateModel(modelType, valueProvider);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }

		public IRepository<Job> CompanyJobRepository { get; set; }

		public ISearchRepository<JobView, SearchCriteria> JobViewRepository { get; set; }

		public IRepository<Company> CompanyRepository { get; set; }
	}
}
