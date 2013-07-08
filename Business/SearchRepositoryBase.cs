using System;
using System.Collections.Generic;
using Business.Core;
using Entities;

namespace Business
{
	public abstract class SearchRepositoryBase<TEntity, TSearchCriteria> : ISearchRepository<TEntity, TSearchCriteria>
	{
		protected ModelContainer Context { get; private set; }
		protected SearchRepositoryBase()
		{
			this.Context = new ModelContainer();
		}

		public abstract IEnumerable<TEntity> Fetch(TSearchCriteria criteria);

		public virtual void Add(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public virtual void Remove(TEntity entity)
		{
			throw new NotImplementedException();
		}

		public virtual TEntity Create()
		{
			throw new NotImplementedException();
		}

		public virtual TEntity Get(Func<TEntity, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public virtual void SaveChanges()
		{
			throw new NotImplementedException();
		}


		public virtual TEntity Attach(TEntity entity)
		{
			throw new NotImplementedException();
		}


		public virtual TEntity Get(int id)
		{
			throw new NotImplementedException();
		}
	}
}
