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
			return this.View();
		}

		public ActionResult Contact()
		{
			return this.View();
		}

		public ActionResult PrivacyPolicy()
		{
			return this.View();
		}

		public ActionResult TermsOfService()
		{
			return this.View();
		}

		private HomeModel CreateHomeModel()
		{
			var repository = (JobViewRepository)this.JobRepository;
			var companyRepository = (CompanyViewRepository)this.CompanyRepository;
			return new HomeModel
			{
				FeaturedJobs = repository.FetchFeaturedJobs(),
				LatestJobs = repository.FetchLatestJobs(),
				FeaturedInternships = repository.FetchFeaturedInternships(),
				FeaturedCompanies = companyRepository.FetchFeaturedCompanies()
			};
		}

		public ISearchRepository<JobView, SearchCriteria> JobRepository { get; set; }

		public ISearchRepository<CompanyView, int> CompanyRepository { get; set; }
	}
}
