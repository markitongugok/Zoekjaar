using System.Web.Mvc;
using Business;
using Business.Core;
using Business.Criteria;
using Zoekjaar.Web.Models;

namespace Zoekjaar.Web.Controllers
{
	public class HomeController : ControllerBase
	{
		public ActionResult Index()
		{
			var model = this.CreateHomeModel();

			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		private HomeModel CreateHomeModel()
		{
			var repository = (JobViewRepository)this.JobRepository;
			var companyRepository = (CompanyViewRepository)this.CompanyRepository;
			return new HomeModel
			{
				FeaturedJobs = repository.FetchFeaturedJobs(),
				LatestJobs = repository.FetchLatestJobs(),
				FeaturedCompanies = companyRepository.FetchFeaturedCompanies()
			};
		}

		public ISearchRepository<JobView, SearchCriteria> JobRepository { get; set; }

		public ISearchRepository<CompanyView, string> CompanyRepository { get; set; }
	}
}
