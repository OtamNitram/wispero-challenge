
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wispero.Web.Binders
{
    public class QnAModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BindQnAModel(controllerContext.HttpContext.Request.Form, bindingContext.ModelState);
        }

        public static object BindQnAModel(NameValueCollection values, ModelStateDictionary modelState)
        {
            //TODO: Implement model binder for QuestionAndAnswerModel
            throw new NotImplementedException();
        }
    }
}