using System.Collections.Generic;

namespace Business.Core
{
	public interface ISearchRepository<TEntity, TSearchCriteria> : IRepository<TEntity>
	{
		IEnumerable<TEntity> Fetch(TSearchCriteria criteria);
		TEntity Get(int id);
	}
}
