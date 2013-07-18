using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Annotations{
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BadgeAttribute : Attribute
	{
		public string ClassName { get; private set; }
		
		public BadgeAttribute(string className)
		{
			this.ClassName = className;
		}
	}
}
