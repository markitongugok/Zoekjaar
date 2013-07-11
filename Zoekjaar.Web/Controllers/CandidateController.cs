using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Business.Core;
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
			model.Candidates = this.CandidateRepository.Fetch(jobId);
			return this.View(model);
		}

		private ViewCandidatesModel CreateViewCandidatesModel()
		{
			return new ViewCandidatesModel
			{

			};
		}

		public override object CreateModel(Type modelType, System.Web.Mvc.IValueProvider valueProvider)
		{
			return modelType == typeof(ViewCandidatesModel)
				? this.CreateViewCandidatesModel()
				: base.CreateModel(modelType, valueProvider);
		}

		public ISearchRepository<CandidateView, int> CandidateRepository { get; set; }
	}
}