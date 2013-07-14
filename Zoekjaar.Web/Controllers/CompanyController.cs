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

		[Authorize(Roles = "Company")]
		public ActionResult Edit()
		{
			var model = this.CreateProfileModel();

			return this.View(model);
		}

		[Authorize(Roles = "Company")]
		[HttpPost]
		public ActionResult Edit(CompanyProfileModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}

			this.CompanyRepository.Attach(model.Company);
			this.CompanyRepository.SaveChanges();

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

		private CompanyProfileModel CreateProfileModel()
		{
			var companyId = this.UserIdentity.EntityId;

			return new CompanyProfileModel
			{
				Company = this.CompanyRepository.Get(_ => _.Id == companyId)
			};
		}

		public override object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateSearchModel)
				? this.CreateSearchModel()
				: modelType == typeof(CompanyProfileModel)
					? this.CreateProfileModel()
					: Activator.CreateInstance(modelType);
		}


		public ISearchRepository<GraduateView, SearchCriteria> GraduateViewRepository { get; set; }

		public IRepository<Company> CompanyRepository { get; set; }

	}
}
