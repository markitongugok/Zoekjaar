﻿using Autofac;
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
			component.RegisterType(typeof(JobRepository)).As<IRepository<Job>>();
			component.RegisterType(typeof(JobApplicationRepository)).As<IRepository<JobApplication>>();
			component.RegisterType(typeof(GraduateViewRepository)).As<ISearchRepository<GraduateView, SearchCriteria>>();
			component.RegisterType(typeof(JobViewRepository)).As<ISearchRepository<JobView, SearchCriteria>>();
			component.RegisterType(typeof(UserIdentityRepository)).As<ISearchRepository<UserIdentity, UserIdentityCriteria>>();
			component.RegisterType(typeof(CompanyViewRepository)).As<ISearchRepository<CompanyView, string>>();
			component.RegisterType(typeof(CandidateViewRepository)).As<ISearchRepository<CandidateView, int>>();
		}
	}
}
