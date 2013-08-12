using System;

namespace Zoekjaar.Web.Models
{
	[Serializable]
	public sealed class TokenModel
	{
		public string Url { get; set; }
		public string Text { get; set; }

	}
}