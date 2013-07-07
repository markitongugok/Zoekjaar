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
	public sealed class GraduateController : Controller, IModelFactory
	{
		private ModelContainer context = new ModelContainer();

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

		[Authorize(Roles = "Graduate")]
		[HttpPost]
		public ActionResult Apply(int jobId)
		{
			return this.View();
		}

		[Authorize(Roles = "Graduate")]
		public ActionResult EditProfile()
		{
			var name = this.User.Identity.Name;

			var model = this.CreateProfileModel();
			model.Graduate = this.GraduateRepository.Get(_ => _.User.Username == name);

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

		public GraduateProfileModel CreateProfileModel()
		{
			return new GraduateProfileModel
			{
				CurrentStatus = this.GetLookups("Current Status"),
				Proficiencies = this.GetLookups("Proficiency"),
				VisaStatus = this.GetLookups("Visa Status"),
			};
		}

		private IEnumerable<Lookup> GetLookups(string lookupTypeName)
		{
			var lookupType = this.context.LookupTypes.Single(_ => _.Name == lookupTypeName);
			return this.context.Lookups.Where(_ => _.LookupTypeId == lookupType.Id).AsEnumerable();
		}

		public object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateSearchModel)
				? this.CreateSearchModel()
				: modelType == typeof(GraduateProfileModel)
					? this.CreateProfileModel()
					: Activator.CreateInstance(modelType);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }

		public ISearchRepository<GraduateView, SearchCriteria> GraduateViewRepository { get; set; }
	}
}
