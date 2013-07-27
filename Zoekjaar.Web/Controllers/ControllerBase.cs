using System;
using System.Web.Mvc;
using Business;
using Zoekjaar.Web.Contracts;

namespace Zoekjaar.Web.Controllers
{
	public abstract class ControllerBase : Controller, IModelFactory
	{
		public virtual object CreateModel(Type modelType, IValueProvider valueProvider)
		{
			return Activator.CreateInstance(modelType);
		}

		public UserIdentity UserIdentity
		{
			get
			{
				return this.User.Identity as UserIdentity;
			}
		}
	}
}