using System;
using System.Web.Mvc;
using Zoekjaar.Web.Contracts;

namespace Zoekjaar.Web.Binders
{
    public class ObjectBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }

            var controller = controllerContext.Controller as IModelFactory;

            return controller != null ? ((IModelFactory)controller).CreateModel(modelType, bindingContext.ValueProvider) : base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}