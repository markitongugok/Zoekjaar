using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	[MetadataType(typeof(UserMetadata))]
	public partial class User
	{
	}

	public class UserMetadata
	{
		public int Id { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required]
		public string Username { get; set; }
		public byte[] Salt { get; set; }
		public byte[] Hash { get; set; }

		public short UserType { get; set; }

		public bool IsActive { get; set; }

		public virtual ICollection<Company> Companies { get; set; }
		public virtual ICollection<Graduate> Graduates { get; set; }
	}
}
