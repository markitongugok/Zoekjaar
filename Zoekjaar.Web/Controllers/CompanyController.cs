using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
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

		[Authorize(Roles = "Company")]
		[HttpPost]
		public ActionResult Upload()
		{
			// Validate we have a file being posted
			if (this.Request.Files.Count == 0)
			{
				return this.Json(new { statusCode = 500, status = "No image provided." }, "text/html");
			}

			// File we want to resize and save.
			var file = Request.Files[0];

			try
			{
				var filename = this.UploadFile(file);

				// Return JSON
				return Json(new
				{
					statusCode = 200,
					status = "Image uploaded.",
					file = filename,
				}, "text/html");
			}
			catch (Exception ex)
			{
				// Log using "NLog" NuGet package
				//Logger.ErrorException(ex.ToString(), ex);
				return Json(new
				{
					statusCode = 500,
					status = "Error uploading image.",
					file = string.Empty
				}, "text/html");
			}
		}

		private string UploadFile(HttpPostedFileBase file)
		{
			// Initialize variables we'll need for resizing and saving
			var width = 100;
			var height = 100;

			var relativePath = string.Format("/Content/Images/Companies/{0}/", this.UserIdentity.EntityId);
			var absPath = this.HttpContext.Server.MapPath(relativePath);
			var absFileAndPath = string.Format("{0}{1}", absPath, file.FileName);

			// Create directory as necessary and save image on server
			if (!Directory.Exists(absPath))
				Directory.CreateDirectory(absPath);
			file.SaveAs(absFileAndPath);

			// Resize image using "ImageResizer" NuGet package.
			var resizeSettings = new ImageResizer.ResizeSettings
			{
				Scale = ImageResizer.ScaleMode.DownscaleOnly,
				Width = width,
				Height = height,
				Mode = ImageResizer.FitMode.Crop
			};
			var b = ImageResizer.ImageBuilder.Current.Build(absFileAndPath, resizeSettings);

			// Save resized image
			b.Save(absFileAndPath);

			// Return relative file path
			return string.Format("..{0}{1}", relativePath, file.FileName);
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
			return modelType == typeof(CompanyProfileModel)
					? this.CreateProfileModel()
					: Activator.CreateInstance(modelType);
		}

		public IRepository<Company> CompanyRepository { get; set; }

	}
}
