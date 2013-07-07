using System;
using System.Collections.Generic;
using Autofac;
using Business.Core;
using Business.Core.Extensions;

namespace Business
{
	public class ObjectFactory<TEntity> : IObjectFactory<TEntity>
	{
		public TEntity Create()
		{
			return IoC.Container.Resolve<IRepository<TEntity>>().Create();
		}

		public TEntity Get()
		{
			throw new NotImplementedException();
		}

		public TEntity Get(object criteria)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TEntity> Fetch(object criteria)
		{
			var repository = IoC.Container.Resolve<IRepository<TEntity>>();
			var method = repository.GetType().GetMethodInfo("Fetch", criteria.GetType());
			if (method != null)
			{
				return (IEnumerable<TEntity>)method.Invoke(repository, new object[] { criteria });
			}
			else
			{
				throw new NotSupportedException(string.Format("Get({0}) is not supported.", criteria.GetType()));
			}
		}
	}
}
