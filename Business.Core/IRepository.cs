using System;
using System.Collections.Generic;
namespace Business.Core
{
	public interface IRepository<TEntity>
	{
		void Add(TEntity entity);
		void Remove(TEntity entity);
		void Remove(Func<TEntity, bool> predicate);
		TEntity Create();
		TEntity Get(Func<TEntity, bool> predicate);
		IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate);
		TEntity Attach(TEntity entity);
		void SaveChanges();
	}
}
