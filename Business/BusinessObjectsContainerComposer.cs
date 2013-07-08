using Autofac;
using Business.Core;
using Business.Criteria;
using Entities;

namespace Business
{
	public sealed class BusinessObjectsContainerComposer : IComposer<ContainerBuilder>
	{
		public void Compose(ContainerBuilder component)
		{
			component.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
			component.RegisterType(typeof(GraduateRepository)).As<IRepository<Graduate>>();
			component.RegisterType(typeof(LookupRepository)).As<ISearchRepository<Lookup, string>>();
			component.RegisterType(typeof(CompanyRepository)).As<IRepository<Company>>();
			component.RegisterType(typeof(CompanyJobRepository)).As<IRepository<CompanyJob>>();
			component.RegisterType(typeof(GraduateViewRepository)).As<ISearchRepository<GraduateView, SearchCriteria>>();
			component.RegisterType(typeof(JobViewRepository)).As<ISearchRepository<JobView, SearchCriteria>>();
			component.RegisterType(typeof(UserIdentityRepository)).As<ISearchRepository<UserIdentity, UserIdentityCriteria>>();
		}
	}
}
