using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Business.Core;
using Entities;

namespace Business
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected ModelContainer Context { get; private set; }

		protected RepositoryBase()
		{
			this.Context = new ModelContainer();
		}

		public virtual void Add(TEntity entity)
		{
			this.Context.Set<TEntity>().Add(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			this.Context.Set<TEntity>().Attach(entity);
			this.Context.Set<TEntity>().Remove(entity);
		}

		public virtual TEntity Create()
		{
			return this.Context.Set<TEntity>().Create();
		}

		public TEntity Get(Func<TEntity, bool> predicate)
		{
			return this.Context.Set<TEntity>().Single(predicate);
		}

		public IEnumerable<TEntity> Fetch(Func<TEntity, bool> predicate)
		{
			return this.Context.Set<TEntity>().Where(predicate).AsEnumerable();
		}

		public void SaveChanges()
		{
			try
			{
				this.Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public virtual TEntity Attach(TEntity entity)
		{
			var e = this.Context.Set<TEntity>().Attach(entity);
			this.Context.Entry<TEntity>(e).State = System.Data.EntityState.Modified;
			return e;
		}

		protected virtual void AttachChildren<TChildEntity>(TEntity entity,
			IEnumerable<TChildEntity> children,
			Expression<Func<TChildEntity, int>> childIdSelector,
			Action<TEntity, TChildEntity> fkSetter) where TChildEntity : class
		{
			children.ToList()
				.ForEach(_ =>
				{
					if (childIdSelector.Compile()(_) == 0)
					{
						fkSetter(entity, _);
						this.Context.Set<TChildEntity>().Add(_);
					}
					else
					{
						this.Context.Set<TChildEntity>().Attach(_);
						this.Context.Entry<TChildEntity>(_).State = System.Data.EntityState.Modified;
					}
				});
		}
	}
}
