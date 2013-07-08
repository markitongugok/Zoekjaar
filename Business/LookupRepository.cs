using Entities;
using System.Linq;

namespace Business
{
	public sealed class LookupRepository : SearchRepositoryBase<Lookup, string>
	{
		public override System.Collections.Generic.IEnumerable<Lookup> Fetch(string criteria)
		{
			return this.Context.LookupTypes.Where(_ => _.Name == criteria)
				.Join(this.Context.Lookups.Where(_ => _.IsActive),
				_ => _.Id,
				_ => _.LookupTypeId,
				(_, __) => __).AsEnumerable();
		}
	}
}
