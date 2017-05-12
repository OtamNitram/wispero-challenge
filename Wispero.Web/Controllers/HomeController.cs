using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wispero.Data;
using Wispero.Entities;
using Wispero.Web.Helpers;
using Wispero.Web.Models;

namespace Wispero.Web.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        Core.Services.IDataService<KnowledgeBaseItem> KnowledgeBaseData;
        Core.Services.IQueryService<KnowledgeBaseItem> KnowledgeBaseQuery;

        public HomeController(Core.Services.IDataService<KnowledgeBaseItem> dataService, Core.Services.IQueryService<KnowledgeBaseItem> queryService)
        {
            KnowledgeBaseData = dataService;
            KnowledgeBaseQuery = queryService;

            //TODO: Implement mapping from QuestionAndAnswerModel to Entities.KnowledgeBaseItem.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            throw new NotImplementedException();
            
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Entry()
        {
            //TODO: Return partial view "Entry";
            throw new NotImplementedException();
            
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult TagCloud()
        {
            //TODO: Return partial view "TagCloud" with an instance of TagCloudviewModel.
            //You need to call TagHelper.Process as shown below.
            int tagMaxCount;
            var tagCloud = TagHelper.Process(KnowledgeBaseQuery, out tagMaxCount);
            
            throw new NotImplementedException();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(QuestionAndAnswerModel model)
        {
            //TODO: Return partial view "Entry" with an instance of QuestionAndAnswerModel.
            //If model is valid then persists the new entry on DB. Make sure  data changes are committed.

            throw new NotImplementedException();
            
        }
    }
}