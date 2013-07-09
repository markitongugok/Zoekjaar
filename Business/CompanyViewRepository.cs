using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public sealed class CompanyViewRepository : SearchRepositoryBase<CompanyView, string>
	{
		public override IEnumerable<CompanyView> Fetch(string criteria)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CompanyView> FetchFeaturedCompanies()
		{
			return this.Context.Companies.Where(_ => _.IsActive && _.IsFeatured)
				.Select(_ => new CompanyView
				{
					Id = _.Id,
					Name = _.Name,
					Website = _.Website
				});
		}
	}
}
