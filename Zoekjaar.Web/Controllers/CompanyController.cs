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

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateSearchModel)
				? this.CreateSearchModel()
				: Activator.CreateInstance(modelType);
		}


		public ISearchRepository<GraduateView, SearchCriteria> GraduateViewRepository { get; set; }

	}
}
