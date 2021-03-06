﻿using System;
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
			model.IsSuccessful = null;
			if (this.ModelState.IsValid)
			{
				try
				{
					this.CompanyRepository.Attach(model.Company);
					this.CompanyRepository.SaveChanges();

					model.IsSuccessful = true;
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError(ex.Message, ex);
					model.IsSuccessful = false;
				}
			}
			else
			{
				model.IsSuccessful = false;
			}
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
				var company = this.CompanyRepository.Get(_ => _.Id == this.UserIdentity.EntityId);
				if (string.IsNullOrEmpty(company.LogoUrl))
				{
					company.LogoUrl = string.Format("{0}.png", Guid.NewGuid());
					this.CompanyRepository.SaveChanges();
				}

				var filename = this.UploadFile(file, company.LogoUrl);

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

		private string UploadFile(HttpPostedFileBase file, string filename)
		{
			// Initialize variables we'll need for resizing and saving
			var width = 200;
			var height = 200;

			var relativePath = "/Content/Images/Companies/";
			var absPath = this.HttpContext.Server.MapPath(relativePath);
			var absFileAndPath = string.Format("{0}{1}", absPath, filename);

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
			b.Save(absFileAndPath, System.Drawing.Imaging.ImageFormat.Png);

			// Return relative file path
			return string.Format("..{0}{1}", relativePath, filename);
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
