using System;
using System.Web.Mvc;

namespace Zoekjaar.Web.Contracts
{
    public interface IModelFactory
    {
        object CreateModel(Type modelType, IValueProvider valueProvider);
    }
}
