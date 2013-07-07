using System.Collections.Generic;
namespace Business.Core
{
	public interface IObjectFactory<TEntity>
	{
		TEntity Create();
		TEntity Get();
		TEntity Get(object criteria);
		IEnumerable<TEntity> Fetch(object criteria);
	}
}
