using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Core;
using Entities;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public sealed class ProfileController : ControllerBase
	{
		public ActionResult Company()
		{
			return this.View();
		}

		public ActionResult Graduate()
		{
			var model = this.CreateGraduateProfileModel();
			var graduateId = int.Parse(this.ValueProvider.GetValue(RouteConfig.Id).AttemptedValue);
			model.Graduate = this.GraduateRepository.Get(_ => _.Id == graduateId);
			return this.View(model);
		}

		private GraduateProfileModel CreateGraduateProfileModel()
		{
			return new GraduateProfileModel
			{

			};
		}
		public override object CreateModel(Type modelType, System.Web.Mvc.IValueProvider valueProvider)
		{
			return modelType == typeof(GraduateProfileModel)
				? this.CreateGraduateProfileModel()
				: base.CreateModel(modelType, valueProvider);
		}

		public IRepository<Graduate> GraduateRepository { get; set; }
	}
}