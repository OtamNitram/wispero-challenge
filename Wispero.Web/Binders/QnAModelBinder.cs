
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
            string _answer = values.Get("txtAnswer");

            if (String.IsNullOrEmpty(_answer))
            {
                modelState.AddModelError("Answer", "Answer is null");
            }

            string _question = values.Get("txtQuestion");

            if (String.IsNullOrEmpty(_question))
            {
                modelState.AddModelError("Question", "Question is null");
            }

            string _tags = values.Get("txtTags");

            if (String.IsNullOrEmpty(_tags))
            {
                modelState.AddModelError("Tags", "Tags is null");
            }

            Models.QuestionAndAnswerModel model = new Models.QuestionAndAnswerModel
            {
                Answer = _answer,
                Question = _question,
                Tags = _tags
            };

            return model;
        }
    }
}