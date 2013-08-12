using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Zoekjaar.Web.Extensions
{
	public static class EmailExtensions
	{
		public static void SendTo(this string body, IEnumerable<string> recipient, string subject)
		{
			using (var message = new MailMessage())
			{
				recipient.ToList().ForEach(_ => message.To.Add(_));
				message.Body = body;
				message.IsBodyHtml = true;
				message.Subject = subject;
				var smtp = new SmtpClient();
				smtp.Send(message);
			}
		}
	}
}