using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public sealed class CompanyViewRepository : SearchRepositoryBase<CompanyView, int>
	{
		public override IEnumerable<CompanyView> Fetch(int criteria)
		{
			throw new NotImplementedException();
		}

		public override CompanyView Get(int id)
		{
			return this.Context.Companies.Where(_ => _.Id == id)
				.Select(_ => new CompanyView
				{
					Id = _.Id,
					Name = _.Name,
					Website = _.Website,
					LogoUrl = _.LogoUrl
				}).Single();
			//throw new NotImplementedException();
		}

		public IEnumerable<CompanyView> FetchFeaturedCompanies()
		{
			return this.Context.Companies.Where(_ => _.IsActive && _.IsFeatured)
				.Select(_ => new CompanyView
				{
					Id = _.Id,
					Name = _.Name,
					Website = _.Website,
					LogoUrl = _.LogoUrl
				});
		}
	}
}
