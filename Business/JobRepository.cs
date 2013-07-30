using System;
using Entities;

namespace Business
{
	public sealed class JobRepository : RepositoryBase<Job>
	{
		public override void Add(Job entity)
		{
			entity.DatePosted = DateTime.Now;
			base.Add(entity);
		}
	}
}
